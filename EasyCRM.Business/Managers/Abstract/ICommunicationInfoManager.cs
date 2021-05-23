using EasyCRM.Business.ModelHelpers;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Abstract
{
    public interface ICommunicationInfoManager
    {
        Task<bool> CreateCommunicationInfo(CommunicationInfo communicationInfo);
        Task<CommunicationInfo> GetCommunicationInfo(int id);
        Task<bool> EditCommunicationInfo(int id, CommunicationInfo communicationInfo);
        Task<bool> DeleteCommunicationInfo(int id);
        Task<PagedList<CommunicationInfo>> SearchCommunicationInfos(CommInfoParams commInfoParams);

    }
}
