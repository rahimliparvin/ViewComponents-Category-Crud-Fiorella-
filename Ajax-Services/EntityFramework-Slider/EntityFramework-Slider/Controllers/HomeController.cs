using EntityFramework_Slider.Data;
using EntityFramework_Slider.Models;
using EntityFramework_Slider.Services.Interfaces;
using EntityFramework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.ContentModel;
using System.Diagnostics;

namespace EntityFramework_Slider.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(AppDbContext context, IBasketService basketService, IProductService productService, ICategoryService categoryService)
        {
            _context = context;
            _basketService = basketService;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {

            SliderInfo sliderInfo = await _context.SliderInfos.FirstOrDefaultAsync();

            IEnumerable<Product> products = await _productService.GetAll();

            IEnumerable<Category> categories = await _categoryService.GetAll();


           HomeVM model = new()
            {
                SliderInfo = sliderInfo,
                Products = products,
                Categories = categories
            };

            return View(model);



        }






        [HttpPost]

        //[ValidateAntiForgeryToken]
        public async Task <IActionResult> AddBasket(int? id)
        {
            if (id is null) return BadRequest();

            Product dbProduct = await _productService.GetProductById((int)id);

            if (dbProduct == null) return NotFound();


            List<BasketVM> basket = _basketService.GetBasketDatas();


            BasketVM? existProduct = basket?.FirstOrDefault(m => m.Id == dbProduct.Id);



            _basketService.AddProductToBasket(existProduct, dbProduct, basket);


            int basketCount = basket.Sum(m => m.Count);  //count'u gonderirik methodumuzun icine ki ajaxs ile qebul edib layhotda istifade edek


            return Ok(basketCount);


        }



        

      
    

     



    }



}