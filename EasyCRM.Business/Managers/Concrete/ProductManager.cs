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
    public class ProductManager : IProductManager
    {
        private readonly DataContext dataContext;

        public ProductManager(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }
        public async Task<bool> CreateProduct(Product product)
        {
            try
            {
                if (product.CurrencyId > 0)
                {
                    var currency = await dataContext.Currency.Where(c => c.Id == product.CurrencyId).FirstOrDefaultAsync();
                    if (currency == null)
                    {
                        throw new Exception("Invalid currency Id");
                    }
                }

                if (product.CategoryId > 0)
                {
                    var category = await dataContext.Category.Where(c => c.Id == product.CategoryId).FirstOrDefaultAsync();
                    if (category == null)
                    {
                        throw new Exception("Invalid category Id");
                    }
                }

                if (product.BrandId > 0)
                {
                    var brand = await dataContext.Brand.Where(b => b.Id == product.BrandId).FirstOrDefaultAsync();
                    if (brand == null)
                    {
                        throw new Exception("Invalid brand Id");
                    }
                }

                if (product.UnitCodeId > 0)
                {
                    var unitCode = await dataContext.UnitCode.Where(u => u.Id == product.UnitCodeId).FirstOrDefaultAsync();
                    if (unitCode == null)
                    {
                        throw new Exception("Invalid unit code Id");
                    }
                }

                await dataContext.Product.AddAsync(product);
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

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                var product = await dataContext.Product.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    throw new Exception("No product found with given id");
                }
                else
                {
                    dataContext.Product.Remove(product);
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

        public async Task<bool> EditProduct(int id, Product product)
        {
            try
            {
                var p = await dataContext.Product.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                

                if (p != null)
                {
                    p = product;
                    p.Id = id;

                    dataContext.Product.Update(p);
                    var result = await dataContext.SaveChangesAsync();

                    if (result > 0)
                    {
                        return true;
                    }

                    return false;
                }

                throw new Exception("No product found with given id");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Product> GetProduct(int id)
        {
            try
            {
                var product = await dataContext.Product.Include(p => p.Category)
                                                        .Include(p => p.Currency)
                                                        .Include(p => p.Brand)
                                                        .Include(p => p.UnitCode)
                                                        .Include(p => p.Vendor)
                                                        .FirstOrDefaultAsync(p => p.Id == id);

                if (product != null)
                {
                    return product;
                }

                throw new Exception("No product found!");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PagedList<Product>> SearchProduct(ProductParams productParams)
        {
            try
            {
                var products = dataContext.Product.AsQueryable();

                if (productParams.Id > 0)
                {
                    products = products.Where(p => p.Id == productParams.Id);
                    return await PagedList<Product>.CreateAsync(products, productParams.PageNumber, productParams.PageSize);
                }

                if (!String.IsNullOrEmpty(productParams.Name))
                {
                    products = products.Where(p => p.Name.Contains(productParams.Name));
                }

                if (!String.IsNullOrEmpty(productParams.Description))
                {
                    products = products.Where(p => p.Description.Contains(productParams.Description));
                }

                if (productParams.CategoryId > 0)
                {
                    products = products.Where(p => p.CategoryId == productParams.CategoryId);
                }

                if (productParams.BrandId > 0)
                {
                    products = products.Where(p => p.BrandId == productParams.BrandId);
                }

                if (!String.IsNullOrEmpty(productParams.UPC))
                {
                    products = products.Where(p => p.UPC.Contains(productParams.UPC));
                }

                if (!String.IsNullOrEmpty(productParams.SKU))
                {
                    products = products.Where(p => p.SKU.Contains(productParams.SKU));
                }

                if (!String.IsNullOrEmpty(productParams.EAN))
                {
                    products = products.Where(p => p.EAN.Contains(productParams.EAN));
                }

                if (!String.IsNullOrEmpty(productParams.Volume))
                {
                    products = products.Where(p => p.Volume.Contains(productParams.Volume));
                }

                if (!String.IsNullOrEmpty(productParams.Size))
                {
                    products = products.Where(p => p.Size.Contains(productParams.Size));
                }

                if (productParams.VendorId > 0)
                {
                    products = products.Where(p => p.VendorId == productParams.VendorId);
                }

                products = products.OrderByDescending(p => p.Id);

                return await PagedList<Product>.CreateAsync(products, productParams.PageNumber, productParams.PageSize);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
