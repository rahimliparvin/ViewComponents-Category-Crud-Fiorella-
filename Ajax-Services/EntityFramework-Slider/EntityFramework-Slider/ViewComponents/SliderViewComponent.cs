using EntityFramework_Slider.Data;
using EntityFramework_Slider.Models;
using EntityFramework_Slider.Services.Interfaces;
using EntityFramework_Slider.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EntityFramework_Slider.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly  ISliderService _sliderService;
        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var slider = await _sliderService.GetAll();
            var sliderInfo = await _sliderService.GetSliderInfo();

            var model = new SliderVM
            {
                Sliders = slider,
                SliderInfo = sliderInfo
            };

            return await Task.FromResult(View(model));
        }
    }
}
