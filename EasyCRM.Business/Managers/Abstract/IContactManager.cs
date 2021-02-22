using EasyCRM.Business.ModelHelpers;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Abstract
{
    public interface IContactManager
    {
        Task<PagedList<Contact>> SearchAccounts(ContactParams contactParams);

        Task<bool> CreateContact(Contact contact);
        Task<Contact> GetContact(int id);
        Task<bool> EditContact(int id, Contact contact);
        Task<bool> DeleteContact(int id);

        Task<bool> ConnectAddressToContact(int contactId, int addressId);
        Task<IEnumerable<Address>> GetContactAddresses(int contactId);
    }
}
