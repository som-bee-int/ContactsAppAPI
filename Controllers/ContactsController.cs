using ContactsAppAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ContactsAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private const string DbPath = "contacts.json";
        private static int _lastId = 0;

        private List<Contact> ReadContacts()
        {
            if (!System.IO.File.Exists(DbPath))
                return new List<Contact>();

            var json = System.IO.File.ReadAllText(DbPath);
            var contacts = JsonSerializer.Deserialize<List<Contact>>(json);
            _lastId = contacts.Count > 0 ? contacts.Max(c => c.Id) : 0;
            return contacts;
        }

        private void WriteContacts(List<Contact> contacts)
        {
            var json = JsonSerializer.Serialize(contacts);
            System.IO.File.WriteAllText(DbPath, json);
        }


        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContacts([FromQuery] string search = null)
        {
            var contacts = ReadContacts();
            var activeContacts = contacts.Where(c => c.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                activeContacts = activeContacts.Where(c =>
                    c.FirstName.ToLower().Contains(search) ||
                    c.LastName.ToLower().Contains(search) ||
                    c.Email.ToLower().Contains(search)
                );
            }

            return Ok(activeContacts);
        }


        [HttpGet("{id}")]
        public ActionResult<Contact> GetContact(int id)
        {
            var contacts = ReadContacts();
            var contact = contacts.FirstOrDefault(c => c.Id == id && c.IsActive);
            if (contact == null)
                return NotFound(new { message = $"Contact with ID {id} not found." });

            return Ok(contact);
        }


        [HttpPost]
        public ActionResult<Contact> CreateContact(Contact contact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contacts = ReadContacts();
            if (contacts.Any(c => c.Email == contact.Email && c.IsActive))
                return BadRequest(new { message = "A contact with this email already exists." });

            contact.Id = ++_lastId;
            contact.IsActive = true;
            contacts.Add(contact);
            WriteContacts(contacts);

            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, Contact updatedContact)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var contacts = ReadContacts();
            var contact = contacts.FirstOrDefault(c => c.Id == id && c.IsActive);
            if (contact == null)
                return NotFound(new { message = $"Contact with ID {id} not found." });

            if (contacts.Any(c => c.Email == updatedContact.Email && c.Id != id && c.IsActive))
                return BadRequest(new { message = "A contact with this email already exists." });

            contact.FirstName = updatedContact.FirstName;
            contact.LastName = updatedContact.LastName;
            contact.Email = updatedContact.Email;
            WriteContacts(contacts);

            return Ok(new { message = "Contact updated successfully." });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contacts = ReadContacts();
            var contact = contacts.FirstOrDefault(c => c.Id == id && c.IsActive);
            if (contact == null)
                return NotFound(new { message = $"Contact with ID {id} not found." });

            contact.IsActive = false;
            WriteContacts(contacts);

            return Ok(new { message = "Contact deleted successfully." });
        }
    }
}
