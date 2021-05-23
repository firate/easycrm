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
    public class CommunicationInfoManager : ICommunicationInfoManager
    {
        private readonly DataContext dataContext;

        public CommunicationInfoManager(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public async Task<bool> CreateCommunicationInfo(CommunicationInfo communicationInfo)
        {
            try
            {
                if(communicationInfo.CommunicationTypeId > 0)
                {
                    var commType = await dataContext.CommunicationType.FirstOrDefaultAsync(c=>c.Id==communicationInfo.CommunicationTypeId);

                    if (commType == null)
                    {
                        throw new Exception("Invalid Communication Type Id");
                    }
                }

                if(communicationInfo.AccountId !=null && communicationInfo.AccountId > 0)
                {
                    var account = await dataContext.Account.FirstOrDefaultAsync(a=>a.AccountId==communicationInfo.AccountId);

                    if(account == null)
                    {
                        throw new Exception("Invalid Account Id");
                    }
                }

                if(communicationInfo.ContactId !=null && communicationInfo.ContactId > 0)
                {
                    var contact = await dataContext.Contact.FirstOrDefaultAsync(c=>c.ContactId==communicationInfo.ContactId);
                    if(contact == null)
                    {
                        throw new Exception("Invalid Contact Id");
                    }
                }

                if(communicationInfo.LeadId !=null && communicationInfo.LeadId > 0)
                {
                    var lead = await dataContext.Lead.FirstOrDefaultAsync(l=>l.Id==communicationInfo.LeadId);
                    if(lead == null)
                    {
                        throw new Exception("Invalid Lead Id");
                    }
                }

                await dataContext.CommunicationInfo.AddAsync(communicationInfo);
                var result = await dataContext.SaveChangesAsync();

                if(result > 0)
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

        public async Task<bool> DeleteCommunicationInfo(int id)
        {
            try
            {
                var commInfo = await dataContext.CommunicationInfo.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (commInfo == null)
                {
                    throw new Exception("No Communication Info found with given Id");
                }

                else
                {
                    dataContext.CommunicationInfo.Remove(commInfo);
                    var result = await dataContext.SaveChangesAsync();

                    if (result > 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> EditCommunicationInfo(int id, CommunicationInfo communicationInfo)
        {
            try
            {
                var commInfoToEdit = await dataContext.CommunicationInfo.AsNoTracking().FirstOrDefaultAsync(c=>c.Id==id);

                if(commInfoToEdit != null)
                {
                    communicationInfo.Id = id;
                    dataContext.CommunicationInfo.Update(communicationInfo);
                    var result = await dataContext.SaveChangesAsync();

                    if(result >0)
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

        public async Task<CommunicationInfo> GetCommunicationInfo(int id)
        {
            try
            {
                var commInfo = await dataContext.CommunicationInfo.FirstOrDefaultAsync(c=>c.Id==id);
                if (commInfo != null)
                {
                    return commInfo;
                }
                
                throw new Exception("Invalid Communication Info Id");
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PagedList<CommunicationInfo>> SearchCommunicationInfos(CommInfoParams commInfoParams)
        {
            try
            {
                var commInfos =  dataContext.CommunicationInfo.AsQueryable();
                if (commInfoParams.Id > 0)
                {
                    commInfos = commInfos.Where(c => c.Id == commInfoParams.Id);
                    return await PagedList<CommunicationInfo>.CreateAsync(commInfos,commInfoParams.PageNumber,commInfoParams.PageSize);
                }

                if(commInfoParams.AccountId !=null && commInfoParams.AccountId > 0)
                {
                    commInfos = commInfos.Where(c=> c.AccountId==commInfoParams.AccountId);
                }

                if (commInfoParams.ContactId != null && commInfoParams.ContactId > 0)
                {
                    commInfos = commInfos.Where(c => c.ContactId == commInfoParams.ContactId);
                }

                if (commInfoParams.LeadId != null && commInfoParams.LeadId > 0)
                {
                    commInfos = commInfos.Where(c => c.LeadId == commInfoParams.LeadId);
                }

                if (commInfoParams.LeadId != null && commInfoParams.LeadId > 0)
                {
                    commInfos = commInfos.Where(c => c.LeadId == commInfoParams.LeadId);
                }

                if (commInfoParams.CommunicationTypeId > 0)
                {
                    commInfos = commInfos.Where(c => c.CommunicationTypeId == commInfoParams.CommunicationTypeId);
                }

                if (!String.IsNullOrEmpty(commInfoParams.Name))
                {
                    commInfos = commInfos.Where(c => c.Name.Contains(commInfoParams.Name));
                }

                if (!String.IsNullOrEmpty(commInfoParams.Value))
                {
                    commInfos = commInfos.Where(c => c.Value.Contains(commInfoParams.Value));
                }

                return await PagedList<CommunicationInfo>.CreateAsync(commInfos,commInfoParams.PageNumber,commInfoParams.PageSize);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
