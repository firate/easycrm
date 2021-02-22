using EasyCRM.Business.Managers.Abstract;
using EasyCRM.Business.ModelHelpers;
using EasyCRM.Data.EF;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Concrete
{
    public class ContactManager : IContactManager
    {
        private readonly DataContext dataContext;

        public ContactManager(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public async Task<bool> CreateContact(Contact contact)
        {
            try
            {
                await dataContext.Contacts.AddAsync(contact);
                var result = await dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    return true;
                }

                return false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteContact(int id)
        {
            try
            {
                var contact = await dataContext.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);

                if (contact != null)
                {
                    dataContext.Remove(contact);
                    var result = await dataContext.SaveChangesAsync();

                    if (result > 0)
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> EditContact(int id, Contact contact)
        {
            try
            {
                var c = await dataContext.Contacts.AsNoTracking().FirstOrDefaultAsync(c => c.ContactId == id);

                if (c != null)
                {
                    contact.ContactId = id;
                    dataContext.Contacts.Update(contact);
                    var result = await dataContext.SaveChangesAsync();

                    if (result > 0)
                    {
                        return true;
                    }

                    return false;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Contact> GetContact(int id)
        {
            try
            {
                var contact = await dataContext.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);

                if (contact != null)
                {
                    return contact;
                }

                throw new Exception("Contact not found!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> ConnectAddressToContact(int contactId, int addressId)
        {
            try
            {
                var contact = await dataContext.Contacts.FirstOrDefaultAsync(c => c.ContactId == contactId);
                var address = await dataContext.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);

                if (contact != null && address != null)
                {
                    ContactAddress contactAddress = new ContactAddress
                    {
                        Address = address,
                        AddressId = address.Id,
                        Contact = contact,
                        ContactId = contact.ContactId
                    };

                    await dataContext.Set<ContactAddress>().AddAsync(contactAddress);
                    await dataContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Address>> GetContactAddresses(int contactId)
        {
            try
            {
                var addresses = await dataContext.Addresses
                    .Include(a => a.ContactAddresses)
                    .Where(a=>a.ContactAddresses.Any(x=>x.ContactId==contactId))
                    .ToListAsync();

                if(addresses != null)
                {
                    return addresses;
                }
                return new List<Address>();
                    
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PagedList<Contact>> SearchAccounts(ContactParams contactParams)
        {
            var contacts = dataContext.Contacts.AsQueryable();

            if(contactParams.ContactId > 0)
            {
                contacts = contacts.Where(c=>c.ContactId==contactParams.ContactId);
                return await PagedList<Contact>.CreateAsync(contacts,contactParams.PageNumber,contactParams.PageSize);
            }

            if (!String.IsNullOrEmpty(contactParams.FirstName))
            {
                contacts = contacts.Where(c=>c.FirstName.Contains(contactParams.FirstName));
            }

            if (!String.IsNullOrEmpty(contactParams.MiddleName))
            {
                contacts = contacts.Where(c=>c.MiddleName.Contains(contactParams.MiddleName));
            }

            if (!String.IsNullOrEmpty(contactParams.LastName))
            {
                contacts = contacts.Where(c => c.LastName.Contains(contactParams.LastName));
            }

            if (!String.IsNullOrEmpty(contactParams.NameTitle))
            {
                contacts = contacts.Where(c => c.NameTitle.Contains(contactParams.NameTitle));
            }

            if (contactParams.AccountId > 0)
            {
                contacts = contacts.Where(c => c.AccountId == contactParams.AccountId);
            }

            if(contactParams.CreatedAt != null)
            {
                contacts = contacts.Where(c => c.CreatedAt >= contactParams.CreatedAt);
            }

            return await PagedList<Contact>.CreateAsync(contacts,contactParams.PageNumber,contactParams.PageSize);
        }
    }
}
