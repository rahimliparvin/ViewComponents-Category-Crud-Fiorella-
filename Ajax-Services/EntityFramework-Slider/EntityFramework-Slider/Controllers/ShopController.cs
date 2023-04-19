﻿using EntityFramework_Slider.Data;
using EntityFramework_Slider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework_Slider.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            int count = await _context.Products.Where(m => !m.SoftDelete).CountAsync();

            ViewBag.Count = count;

            IEnumerable<Product> products = await _context.Products
                                                 .Include(m => m.Images)
                                                 .Where(m => !m.SoftDelete)
                                                 .Take(4)
                                                 .ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> LoadMore(int skip)
        {
            IEnumerable<Product> products = await _context.Products
                                             .Include(m => m.Images)
                                             .Where(m => !m.SoftDelete)
                                             .Skip(skip)
                                             .Take(4)
                                             .ToListAsync();

            return PartialView("_ProductsPartial", products);
        }



        public IActionResult Serach(string item)
        {
            IEnumerable<Product> products = _context.Products.Include(m => m.Images).Include(m => m.Category)
                .OrderByDescending(m => m.Id)
                .Where(m =>!m.SoftDelete && m.Name.ToLower()
                .Contains(item.ToLower()))
                .Take(5).ToList();

            return PartialView("_SerachPartial", products);


        }

    }
}
