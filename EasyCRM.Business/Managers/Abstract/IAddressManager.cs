
using EasyCRM.Business.ModelHelpers;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Abstract
{
    public interface IAddressManager
    {
        Task<bool> CreateAddress(Address address);
        Task<Address> GetAddress(int id);
        Task<Address> GetAddress(int id, int accountId);
        Task<Address> GetAddress(int id, int contactId, int? accountId=null);
        Task<bool> EditAddress(int id, Address address);
        Task<bool> DeleteAddress(int id);
        Task<PagedList<Address>> SearchAddresses(AddressParams addressParams);

    }
}
