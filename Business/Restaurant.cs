using AspNetCoreHero.ToastNotification.Abstractions;
using HandsOnCore.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HandsOnCore.Business
{
    public class Restaurant
    {
        private readonly IWebHostEnvironment environment;
        public Restaurant(IWebHostEnvironment env) 
        {
            environment = env;
        }

        public List<Models.Restaurant> CitySearch(string city)
        {
            DatabaseContext dbContext = new DatabaseContext();
            if (!String.IsNullOrEmpty(city))
            {
                var result = dbContext.Restaurant.Where(x => x.City.ToLower().Contains(city.ToLower())).ToList();
                return result;
            }
            else
            {
                return dbContext.Restaurant.ToList();
            }
        }

        public List<Models.Restaurant> RestaurantSearch(string restaurant)
        {
            DatabaseContext dbContext = new DatabaseContext();
            if (!String.IsNullOrEmpty(restaurant))
            {
                var result = dbContext.Restaurant.Where(x => x.Name.ToLower().Contains(restaurant.ToLower())).ToList();
                return result;
            }
            else
            {
                return dbContext.Restaurant.ToList();
            }
        }

        public Models.Restaurant Details(int id)
        {
            DatabaseContext dbContext = new DatabaseContext();
            var result = dbContext.Restaurant.Find(id);
            //result.Menus = dbContext.Menu.Where(m => m.RestaurantId == result.Id).ToList();
            return result;
        }

        public List<Models.Menu> FoodItems(int restaurantId)
        {
            DatabaseContext dbContext = new DatabaseContext();
            var result = dbContext.Menu.Where(m => m.RestaurantId == restaurantId).ToList();
            return result;
        }

        public Result Create(Models.Restaurant obj)
        {
            DatabaseContext dbContext = new DatabaseContext();
            Result result = new Result();
            Models.Restaurant restaurant = new Models.Restaurant();

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    if(obj.Image != null)
                    {
                        string folder = "images/restaurant/";
                        folder += obj.Image.FileName;
                        obj.PhotoUrl = "/" + folder;
                        string serverFolder = Path.Combine(environment.WebRootPath, folder);
                        obj.Image.CopyTo(new FileStream(serverFolder, FileMode.Create));
                    }

                    var check = dbContext.Restaurant.FirstOrDefault(r => r.Name == obj.Name && r.Area == obj.Area);
                    if (check != null)
                    {
                        result.success = false;
                        result.message = "Combination of Restaurant Name and Area already exists";
                        return result;
                    }

                    restaurant.Name = obj.Name;
                    restaurant.Area = obj.Area;
                    restaurant.City = obj.City;
                    restaurant.PinCode = obj.PinCode;
                    restaurant.ContactPersonName = obj.ContactPersonName;
                    restaurant.PhoneNumber = obj.PhoneNumber;
                    restaurant.CostForTwo = obj.CostForTwo;
                    restaurant.MinDeliveryTime = obj.MinDeliveryTime;
                    restaurant.PhotoUrl = obj.PhotoUrl;

                    var data = dbContext.Restaurant.Add(restaurant);
                    dbContext.SaveChanges();

                    if (obj.Menus.Count > 0)
                    {
                        obj.Menus.ForEach(item =>
                        {
                            if(!String.IsNullOrEmpty(item.FoodName) && !String.IsNullOrEmpty(item.Description) && item.Image != null)
                            {
                                if (item.Image != null)
                                {
                                    string folder = "images/menu/";
                                    folder += item.Image.FileName;
                                    item.ImageUrl = "/" + folder;
                                    string serverFolder = Path.Combine(environment.WebRootPath, folder);
                                    item.Image.CopyTo(new FileStream(serverFolder, FileMode.Create));
                                }

                                var restId = dbContext.Restaurant.FirstOrDefault(y => y.Id == data.Entity.Id).Id;

                                Menu menu = new Menu();
                                menu.FoodName = item.FoodName;
                                menu.Description = item.Description;
                                menu.ImageUrl = item.ImageUrl;
                                menu.Price = item.Price;
                                menu.IsVeg = item.IsVeg;
                                menu.RestaurantId = restId;

                                dbContext.Menu.Add(menu);
                                dbContext.SaveChanges();
                            }
                        });
                    }
                    transaction.Commit();
                    result.success = true;
                    result.message = "Data added successfully";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.success = false;
                    result.message = "Something went wrong";
                    Console.WriteLine("Exception: ", ex);
                }

            }
            return result;
        }
    }
}
