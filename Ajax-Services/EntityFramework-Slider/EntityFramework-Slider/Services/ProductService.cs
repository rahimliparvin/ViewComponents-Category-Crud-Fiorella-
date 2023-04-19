using EntityFramework_Slider.Data;
using EntityFramework_Slider.Models;
using EntityFramework_Slider.Services.Interfaces;
using EntityFramework_Slider.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EntityFramework_Slider.Services
{
    public class ProductService : IProductService
    {

        private readonly AppDbContext _context;
  

        public ProductService(AppDbContext context)
        {
            _context = context;
           
        }

       

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(m => m.Images).ToListAsync();
        }

        public async Task<Product> GetFullDataById(int id) => await _context.Products.Include(m => m.Images).Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);
      

        public async Task<Product> GetProductById(int id) => await _context.Products.FindAsync(id);
        
    }
}
