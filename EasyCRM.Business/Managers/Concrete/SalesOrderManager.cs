using AutoMapper;
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
    public class SalesOrderManager : ISalesOrderManager
    {
        private readonly DataContext dataContext;
        

        public SalesOrderManager(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }

        public async Task<bool> CreateSalesOrder(SalesOrder salesOrder)
        {
            try
            {
                await dataContext.SalesOrder.AddAsync(salesOrder);
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

        public async Task<bool> DeleteSalesOrder(int id)
        {
            try
            {
                var salesOrder = await dataContext.SalesOrder.Where(so => so.SalesOrderId == id).FirstOrDefaultAsync();
                if (salesOrder != null)
                {
                    var solCount = await dataContext.SalesOrderLine.Where(sol => sol.SalesOrderId == id).CountAsync();
                    if(solCount <= 0)
                    {
                        dataContext.Remove(salesOrder);
                        var result = await dataContext.SaveChangesAsync();

                        if (result > 0)
                        {
                            return true;
                        }
                        return false;
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

        public async Task<bool> EditSalesOrder(int id, SalesOrder salesOrder)
        {
            var so = await dataContext.SalesOrder.Where(so=>so.SalesOrderId==id).FirstOrDefaultAsync();

            if (so != null)
            {
                salesOrder.SalesOrderId = id;
                dataContext.SalesOrder.Update(salesOrder);

                var result = await dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    return true;
                }
                return false;
            }

            return false;
        }

        public async Task<SalesOrder> GetSalesOrder(int id)
        {
            var so = await dataContext.SalesOrder.Where(so => so.SalesOrderId == id).FirstOrDefaultAsync();
            if (so != null)
            {
                return so;
            }

            throw new Exception("No Sales Order Found!"); 
        }

        public async Task<PagedList<SalesOrder>> SearchSalesOrder(SalesOrderParams salesOrderParams)
        {
            var salesOrders = dataContext.SalesOrder.AsQueryable();

            if(salesOrderParams.SalesOrderId > 0)
            {
                salesOrders = salesOrders.Where(so => so.SalesOrderId == salesOrderParams.SalesOrderId);
                return await PagedList<SalesOrder>.CreateAsync(salesOrders,salesOrderParams.PageNumber,salesOrderParams.PageSize);
            }

            if(salesOrderParams.AccountId > 0)
            {
                salesOrders = salesOrders.Where(so=>so.AccountId ==salesOrderParams.AccountId);
            }

            if(!String.IsNullOrEmpty(salesOrderParams.Status))
            {
                salesOrders = salesOrders.Where(so=>so.Status.ToString() ==salesOrderParams.Status);
            }

            if (salesOrderParams.BeginDate != null)
            {
                salesOrders = salesOrders.Where(so=>so.OrderDate >= salesOrderParams.BeginDate);
                if(salesOrderParams.EndDate != null)
                {
                    salesOrders = salesOrders.Where(so=>so.OrderDate <= salesOrderParams.EndDate);
                }
            }

            return await PagedList<SalesOrder>.CreateAsync(salesOrders, salesOrderParams.PageNumber, salesOrderParams.PageSize);
        }
    }
}
