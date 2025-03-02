using DemoJsCallApi.Dtos;
using DemoJsCallApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoJsCallApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly MySaleDBContext _context;

        public DemoController(MySaleDBContext context)
        {
            _context = context;
        }

        // GET -  JSON - AJAX
        [HttpGet("product")]
        public IActionResult Get()
        {
            try
            {
                var products = _context.Products
                    .Include(x => x.Category)
                    .Select(x => new
                    {
                        x.ProductId,
                        x.ProductName,
                        x.Image,
                        x.UnitPrice,
                        x.UnitsInStock,
                        Category = new
                        {
                            x.Category.CategoryId,
                            x.Category.CategoryName
                        }
                    })
                    .ToList();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("product/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var product = _context.Products
                    .Include(x => x.Category)
                    .Select(x => new
                    {
                        x.ProductId,
                        x.ProductName,
                        x.Image,
                        x.UnitPrice,
                        x.UnitsInStock,
                        Category = new
                        {
                            x.Category.CategoryId,
                            x.Category.CategoryName
                        }
                    })
                    .FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                    return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST - JSON -AJAX
        [HttpPost("product")]
        public IActionResult Post(ProductDto p)
        {
            try
            {
                Product newProduct = new Product()
                {
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock,
                    Image = p.Image,
                    CategoryId = p.CategoryId
                };
                _context.Products.Add(newProduct);
                if (_context.SaveChanges() > 0)
                {
                    return Created("Created sucessfully", newProduct);
                }
                return BadRequest("Can't create product");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // PUT - JSON - AJAX
        [HttpPut("product/{id}")]
        public IActionResult Put(int id, ProductDto p)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return BadRequest(id + " does not exist");
                }
                product.ProductName = p.ProductName;
                product.UnitPrice = p.UnitPrice;
                product.UnitsInStock = p.UnitsInStock;
                product.Image = p.Image;
                product.CategoryId = p.CategoryId;
                _context.SaveChanges();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE - JSON - AJAX
        [HttpDelete("product/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return BadRequest(id + " does not exist");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET -  XML - AJAX

        // POST - XML -AJAX

        // PUT - XML - AJAX

        // DELETE - XML - AJAX


        // GET -  JSON - FETCH

        // POST - JSON - FETCH

        // PUT - JSON - FETCH

        // DELETE - JSON - FETCH


        // GET -  XML - FETCH

        // POST - XML - FETCH

        // PUT - XML - FETCH

        // DELETE - XML - FETCH

    }
}
