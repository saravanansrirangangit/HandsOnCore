using AspNetCoreHero.ToastNotification.Abstractions;
using HandsOnCore.Models;
using HandsOnCore.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace HandsOnCore.Controllers
{
    public class RestaurantController : Controller
    {
        private static DatabaseContext dbContext;
        public INotyfService notyf;
        private static IWebHostEnvironment environment;
        public Business.Restaurant restClass = new Business.Restaurant(environment);

        public RestaurantController(DatabaseContext _dbContext, INotyfService _notyf)
        {
            dbContext = _dbContext;
            notyf = _notyf;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CitySearch(string city)
        {
            var result = restClass.CitySearch(city);
            return View(result);
        }

        [HttpGet]
        public IActionResult RestaurantSearch(string restaurant)
        {
            var result = restClass.RestaurantSearch(restaurant);
            return View("CitySearch", result);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var result = restClass.Details(id);
            return View(result);
        }

        [HttpGet]
        public IActionResult FoodItems(int restaurantId)
        {
            var result = restClass.FoodItems(restaurantId);
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Models.Restaurant obj)
        {
            if (ModelState.IsValid)
            {

                var result = restClass.Create(obj);
                if (!result.success)
                {
                    notyf.Error(result.message);
                    return View();
                }
                else
                {
                    notyf.Success(result.message);
                }
            }
            else
            {
                notyf.Error("Enter all the fields");
            }
            return View();
        }
    }
}
