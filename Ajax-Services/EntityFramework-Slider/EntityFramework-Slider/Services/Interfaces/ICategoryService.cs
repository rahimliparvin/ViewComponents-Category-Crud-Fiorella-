using EntityFramework_Slider.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFramework_Slider.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
    }
}
