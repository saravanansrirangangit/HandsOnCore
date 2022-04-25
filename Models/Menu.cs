using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HandsOnCore.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId { get; set; }

        [DisplayName("Food Name")]
        public string FoodName { get; set; } = "";

        public string Description { get; set; } = "";

        public string ImageUrl { get; set; } = "";

        public IFormFile Image { get; set; }

        public decimal Price { get; set; } = 0;

        [DisplayName("Is Veg?")]
        public bool IsVeg { get; set; } = true;
    }
}
