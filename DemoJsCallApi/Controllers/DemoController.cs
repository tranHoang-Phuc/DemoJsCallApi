using DemoJsCallApi.Dtos;
using DemoJsCallApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Xml.Serialization;

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


    [HttpGet("product/xml")]
    public IActionResult GetXml()
    {
        try
        {
            // Map EF entities to DTOs
            var products = _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductResponse
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Image = x.Image,
                    UnitPrice = x.UnitPrice!.Value,
                    UnitsInStock = x.UnitsInStock!.Value,
                    Category = new CategoryResponse
                    {
                        CategoryId = x.Category.CategoryId,
                        CategoryName = x.Category.CategoryName
                    }
                })
                .ToList();

            var xmlSerializer = new XmlSerializer(typeof(List<ProductResponse>));

            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, products);
                return Content(stringWriter.ToString(), "application/xml", Encoding.UTF8);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
        [HttpGet("product/xml/{id}")]
        public IActionResult GetXml(int id)
        {
            try
            {
                var product = _context.Products
                    .Include(x => x.Category)
                    .Select(x => new ProductResponse
                    {
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        Image = x.Image,
                        UnitPrice = x.UnitPrice!.Value,
                        UnitsInStock = x.UnitsInStock!.Value,
                        Category = new CategoryResponse
                        {
                            CategoryId = x.Category.CategoryId,
                            CategoryName = x.Category.CategoryName
                        }
                    })
                    .FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                    return NotFound();
                var xmlSerializer = new XmlSerializer(typeof(ProductResponse));
                using (var stringWriter = new StringWriter())
                {
                    xmlSerializer.Serialize(stringWriter, product);
                    return Content(stringWriter.ToString(), "application/xml", Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // POST - XML -AJAX
        [HttpPost("product/xml")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public IActionResult CreateXml([FromBody] XmlCreationProduct XmlCreationProduct)
        {
            try
            {
                if (XmlCreationProduct == null)
                {
                    return BadRequest("Invalid XML format.");
                }

                var product = new Product
                {
                    ProductName = XmlCreationProduct.ProductName,
                    UnitPrice = XmlCreationProduct.UnitPrice ?? 0,
                    UnitsInStock = XmlCreationProduct.UnitsInStock ?? 0,
                    Image = XmlCreationProduct.Image,
                    CategoryId = XmlCreationProduct.CategoryId ?? 0
                };

                _context.Products.Add(product);
                _context.SaveChanges();

                return Created("",null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        // PUT - XML - AJAX
        [HttpPut("product/xml/{id}")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public IActionResult UpdateXml(int id, [FromBody] XmlCreationProduct XmlCreationProduct)
        {
            try
            {
                if (XmlCreationProduct == null)
                {
                    return BadRequest("Invalid XML format.");
                }
                var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }
                product.ProductName = XmlCreationProduct.ProductName;
                product.UnitPrice = XmlCreationProduct.UnitPrice ?? 0;
                product.UnitsInStock = XmlCreationProduct.UnitsInStock ?? 0;
                product.Image = XmlCreationProduct.Image;
                product.CategoryId = XmlCreationProduct.CategoryId ?? 0;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
        // DELETE - XML - AJAX
        [HttpDelete("product/xml/{id}")]
        public IActionResult DeleteXml(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

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
