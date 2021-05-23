using AutoMapper;
using EasyCRM.API.DTOs;
using EasyCRM.Business.Managers.Abstract;
using EasyCRM.Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductManager productManager;
        private IMapper mapper;

        public ProductController(IProductManager _productManager,
                                 IMapper _mapper)
        {
            productManager = _productManager;
            mapper = _mapper;
        }

        [HttpGet("{id}", Name ="GetProduct")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await productManager.GetProduct(id);
                if (product != null)
                {
                    var productToReturn = mapper.Map<ProductToReturnDTO>(product);
                    return Ok(productToReturn);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreationDTO productCreationDTO)
        {
            try
            {
                var product = mapper.Map<Product>(productCreationDTO);
                var result = await productManager.CreateProduct(product);

                if(result == true)
                {
                    var productToReturn = mapper.Map<ProductToReturnDTO>(product);
                    return CreatedAtRoute("GetProduct", new { Id = productToReturn.Id}, productToReturn);
                }

                return BadRequest("Given Product could not be added.");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct(int id, [FromBody] ProductEditDTO productEditDTO)
        {
            try
            {
                
                var product = mapper.Map<Product>(productEditDTO);
                var isEdited = await productManager.EditProduct(id, product);

                if (isEdited == true)
                {
                    return NoContent();
                }

                return BadRequest();
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await productManager.GetProduct(id);
                if (product == null)
                {
                    return NotFound();
                }
                
                var result = await productManager.DeleteProduct(id);

                if(result == true)
                {
                    return NoContent();
                }

                return BadRequest();
                
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}
