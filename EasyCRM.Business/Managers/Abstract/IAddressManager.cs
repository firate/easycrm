
using EasyCRM.Business.ModelHelpers;
using EasyCRM.Entity.Models;
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
        Task<bool> EditAddress(int id, Address address);
        Task<bool> DeleteAddress(int id);

    }
}
