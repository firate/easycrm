using EasyCRM.Business.ModelHelpers;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Abstract
{
    public interface ILeadManager
    {
        Task<bool> CreateLead(Lead lead);
        Task<Lead> GetLead(int id);
        Task<bool> EditLead(int id, Lead lead);
        Task<bool> DeleteLead(int id);
        Task<PagedList<Lead>> SearchLeads(LeadParams leadParams);
    }
}
