using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCollections.Data;
using MyCollections.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollections.Controllers
{
    public class AdminController : Controller
    {
        private const string AdminUsername = "admin";
        private const string AdminPassword = "password123"; // Change this or use a secure method

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin model)
        {
            if (model.Username == AdminUsername && model.Password == AdminPassword)
            {
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // Dashboard
        public IActionResult Dashboard()
        {
            return View();
        }

        // Cars List
        public async Task<IActionResult> CarsList()
        {
            var cars = await _context.Cars.Include(c => c.Brand).ToListAsync();
            return View(cars);
        }

        // Brands List
        public async Task<IActionResult> BrandsList()
        {
            var brands = await _context.Brands.ToListAsync();
            return View(brands);
        }

        // Add Car
        public async Task<IActionResult> AddCar()
        {
            ViewBag.Brands = (await _context.Brands.ToListAsync())
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                });

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddCar(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("CarsList");
            }

            ViewBag.Brands = await _context.Brands.ToListAsync();
            return View(car);
        }

        // Edit Car
        public async Task<IActionResult> EditCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            ViewBag.Brands = (await _context.Brands.ToListAsync())
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                });

            return View(car);
        }


        [HttpPost]
        public async Task<IActionResult> EditCar(int id, Car updatedCar)
        {
            if (id != updatedCar.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(updatedCar);
                await _context.SaveChangesAsync();
                return RedirectToAction("CarsList");
            }

            ViewBag.Brands = await _context.Brands.ToListAsync();
            return View(updatedCar);
        }

        // Add Brand
        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction("BrandsList");
            }

            return View(brand);
        }

        // Edit Brand
        public async Task<IActionResult> EditBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();

            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> EditBrand(int id, Brand updatedBrand)
        {
            if (id != updatedBrand.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(updatedBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction("BrandsList");
            }

            return View(updatedBrand);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction("CarsList");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction("BrandsList");
        }

    }
}
