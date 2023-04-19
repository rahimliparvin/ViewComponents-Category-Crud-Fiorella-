﻿using EntityFramework_Slider.Data;
using EntityFramework_Slider.Models;
using EntityFramework_Slider.Services.Interfaces;
using EntityFramework_Slider.ViewModels;
using Newtonsoft.Json;

namespace EntityFramework_Slider.Services
{
    public class BasketService : IBasketService
    {
        private readonly AppDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }



        public void AddProductToBasket(BasketVM existProduct, Product product, List<BasketVM> basket)
        {
            if (existProduct == null)
            {
                basket?.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1

                });

            }
            else
            {
                existProduct.Count++;

            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
        }

        public void DeleteProductFormBasket(int id)
        {
            List<BasketVM> basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);

            BasketVM deletedProdcut = basketProducts.FirstOrDefault(m => m.Id == id);

            basketProducts.Remove(deletedProdcut);


            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts));
        }

        public List<BasketVM> GetBasketDatas()
        {
            List<BasketVM> basket;

            if (_httpContextAccessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);

            }
            else
            {
                basket = new List<BasketVM>();
            }


            return basket;
        }
    }
}
