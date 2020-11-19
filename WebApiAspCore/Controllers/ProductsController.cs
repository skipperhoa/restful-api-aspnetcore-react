using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAspCore.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiAspCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly EFDataContext _db;
        public ProductsController(EFDataContext db)
        {
            this._db = db;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var products = _db.Products.Select(s => new Product
            {
                idProduct = s.idProduct,
                Title = s.Title,
                Body = s.Body,
                Slug = s.Slug,
                idCategory = s.idCategory,
                Category = _db.Categories.Where(a => a.idCategory == s.idCategory).FirstOrDefault()
            }).ToList();
            return products;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var products = _db.Products.Select(s => new Product
            {
                idProduct = s.idProduct,
                Title = s.Title,
                Body = s.Body,
                Slug = s.Slug,
                idCategory = s.idCategory,
                Category = _db.Categories.Where(a => a.idCategory == s.idCategory).FirstOrDefault()
            }).Where(a => a.idProduct == id).FirstOrDefault();
            return products;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormProductView _product)
        {

            var product = new Product()
            {
                Title = _product.Title,
                Body = _product.Body,
                Slug = _product.Slug,
                Category = _db.Categories.Find(_product.idCategory)
            };
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            if (product.idProduct > 0)
            {
                return Ok(1);
            }
            return Ok(0);
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FormProductView _user)
        {
            var product = _db.Products.Find(id);
            product.Title = _user.Title;
            product.Body = _user.Body;
            product.Slug = _user.Slug;
            product.Category = _db.Categories.Find(_user.idCategory);
            await _db.SaveChangesAsync();
            return Ok(1);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = _db.Products.Find(id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(1);
        }
    }
}
