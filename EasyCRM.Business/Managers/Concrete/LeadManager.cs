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
    public class LeadManager : ILeadManager
    {
        private readonly DataContext dataContext;

        public LeadManager(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public async Task<bool> CreateLead(Lead lead)
        {
            try
            {
                await dataContext.Lead.AddAsync(lead);
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

        public async Task<bool> DeleteLead(int id)
        {
            try
            {
                var lead =await dataContext.Lead.FindAsync(id);
                if (lead!=null)
                {
                    dataContext.Lead.Remove(lead);
                    var result = await dataContext.SaveChangesAsync();

                    if(result > 0)
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

        public async Task<bool> EditLead(int id, Lead lead)
        {
            try
            {
                var leadToEdit = await dataContext.Lead.FirstOrDefaultAsync(l=>l.Id==id);
               
                if(leadToEdit != null)
                {
                    lead.Id = id;
                    dataContext.Lead.Update(lead);
                    var result = await dataContext.SaveChangesAsync();

                    if(result > 0)
                    {
                        return true;
                    }

                }

                return false;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Lead> GetLead(int id)
        {
            try
            {
                var lead = await dataContext.Lead.FirstOrDefaultAsync(l=>l.Id==id);
                if(lead!=null)
                {
                    return lead;
                }

                throw new Exception("No lead found!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PagedList<Lead>> SearchLeads(LeadParams leadParams)
        {
            try
            {
                var leads = dataContext.Lead.Include(l => l.DefaultAddress).Include(l => l.CommunicationInfo).AsQueryable();

                if(leadParams.Id > 0)
                {
                    leads = leads.Where(l=>l.Id==leadParams.Id);
                    return await PagedList<Lead>.CreateAsync(leads,leadParams.PageNumber,leadParams.PageSize);
                }

                if (!String.IsNullOrEmpty(leadParams.Name))
                {
                    leads = leads.Where(l=>l.Name.Contains(leadParams.Name));
                }

                if (!String.IsNullOrEmpty(leadParams.FirstName))
                {
                    leads = leads.Where(l => l.FirstName.Contains(leadParams.FirstName));
                }

                if (!String.IsNullOrEmpty(leadParams.MiddleName)) 
                {
                    leads = leads.Where(l => l.MiddleName.Contains(leadParams.MiddleName));
                }
                
                if (!String.IsNullOrEmpty(leadParams.LastName))
                {
                    leads = leads.Where(l => l.LastName.Contains(leadParams.LastName));
                }

                if (leadParams.IndustryId >0)
                {
                    leads = leads.Where(l => l.Industry.Id == leadParams.IndustryId);
                }

                if (leadParams.AccountId >0)
                {
                    leads = leads.Where(l => l.Industry.Id == leadParams.AccountId);
                }

                leads = leads.OrderByDescending(l=>l.Id);

                return await PagedList<Lead>.CreateAsync(leads,leadParams.PageNumber,leadParams.PageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
