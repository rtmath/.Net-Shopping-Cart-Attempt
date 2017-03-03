using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WingtipToys.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WingtipToys.Controllers
{
    public class CategoriesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cars()
        {
            var cars = db.Products.Include(p => p.Category).Where(p => p.Category.CategoryId == 1).ToList();
            return View(cars);
        }

        public IActionResult Details(int id)
        {
            var car = db.Products.FirstOrDefault(p => p.ProductId == id);
            return View(car);
        }
    }
}
