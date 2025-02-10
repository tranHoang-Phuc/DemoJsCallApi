using DemoJsCallApi.Models;
using Microsoft.AspNetCore.Mvc;

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

        // POST - JSON -AJAX

        // PUT - JSON - AJAX

        // DELETE - JSON - AJAX


        // GET -  XML - AJAX

        // POST - XML -AJAX

        // PUT - XML - AJAX

        // DELETE - XML - AJAX


        // File Upload - AJAX - PhucTH
        // File Upload - FETCH - PhucTH

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
