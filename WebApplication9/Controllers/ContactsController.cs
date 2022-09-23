using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication9.Data;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await dbContext.Contacts.ToListAsync();
            return Ok(contacts);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Email = addContactRequest.Email,
                Phone = addContactRequest.Phone,
                Address = addContactRequest.Address
            };
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var dbcontact = await dbContext.Contacts.FindAsync(id);
            if (dbcontact == null)
            {
                return BadRequest("not found");
            }
            dbcontact.Name = updateContactRequest.Name;
            dbcontact.Email = updateContactRequest.Email;
            dbcontact.Address = updateContactRequest.Address;
            dbcontact.Phone = updateContactRequest.Phone;

            await dbContext.SaveChangesAsync();
            return Ok(dbContext);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var dbContact = await dbContext.Contacts.FindAsync(id);
            if(dbContact == null)
            {
                return BadRequest("not found");
            }
             dbContext.Contacts.Remove(dbContact);
             dbContext.SaveChanges();
            return Ok(dbContext);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContactById([FromRoute]Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact == null)
            {
                return BadRequest("not found");
            }
            return Ok(contact);
        }
    }
}
