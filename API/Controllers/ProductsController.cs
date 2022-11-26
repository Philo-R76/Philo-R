using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SQLitePCL;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : ControllerBase
    {
        

    private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
           var products = await _context.Products.ToListAsync();
           
           return Ok(products);
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<Product>>GetCategory(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}
