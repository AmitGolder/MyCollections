using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCollections.Data;
using MyCollections.Models;
using System.Linq;

public class CarsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CarsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Details(int id)
    {
        var car = _context.Cars.Include(c => c.Brand).FirstOrDefault(c => c.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    public IActionResult AllCollections()
    {
        var cars = _context.Cars.Include(c => c.Brand).ToList(); // Include Brand
        return View(cars);
    }


    public IActionResult Search(string query)
    {
        var results = _context.Cars
            .Include(c => c.Brand)
            .Where(c => c.Name.Contains(query) || c.Brand.Name.Contains(query))
            .ToList();
        return PartialView("_SearchResults", results);
    }


}
