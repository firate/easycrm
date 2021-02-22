using EasyCRM.Business.ModelHelpers;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Abstract
{
    public interface ISalesOrderManager
    {
        Task<bool> CreateSalesOrder(SalesOrder salesOrder);
        Task<SalesOrder> GetSalesOrder(int id);
        Task<bool> EditSalesOrder(int id, SalesOrder salesOrder);
        Task<bool> DeleteSalesOrder(int id);
        Task<PagedList<SalesOrder>> SearchSalesOrder(SalesOrderParams salesOrderParams);
    }
}
