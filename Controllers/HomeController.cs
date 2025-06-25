using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyCollections.Data;
using MyCollections.Models;  // Replace with the correct namespace for your models
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollections.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            var cars = _context.Cars.Include(c => c.Brand).ToList();
            return View(cars);
        }

        // POST: Home/AddCar
        [HttpPost]
        public async Task<IActionResult> AddCar(Car car, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                string imagePath = Path.Combine(_env.WebRootPath, "images", imageFile.FileName);
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                car.ImagePath = "/images/" + imageFile.FileName;  // Store the relative path
            }

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Home/AddBrand
        [HttpPost]
        public async Task<IActionResult> AddBrand(string brandName)
        {
            var brand = new Brand { Name = brandName };
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Edit Page - Show Cars and Brands Management Options
        public IActionResult Edit()
        {
            return View();
        }

        // Manage Cars - List all cars
        public IActionResult ManageCars()
        {
            var cars = _context.Cars.Include(c => c.Brand).ToList();
            return View(cars);
        }

        // Manage Brands - List all brands
        public IActionResult ManageBrands()
        {
            var brands = _context.Brands.ToList();
            return View(brands);
        }


    }

}
