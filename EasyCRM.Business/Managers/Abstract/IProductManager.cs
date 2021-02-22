using EasyCRM.Business.ModelHelpers;
using EasyCRM.Entity.Models;
using EasyCRM.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.Business.Managers.Abstract
{
    public interface IProductManager
    {
        Task<bool> CreateProduct(Product product);
        Task<Product> GetProduct(int id);
        Task<bool> EditProduct(int id, Product product);
        Task<bool> DeleteProduct(int id);
        Task<PagedList<Product>> SearchProduct(ProductParams productParams);
    }
}
