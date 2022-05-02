using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HandsOnCore.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Restaurant Name")]
        [RegularExpression("^[a-zA-Z0-9\\s.]*$", ErrorMessage = "No special characters allowed")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9\\s.]*$", ErrorMessage = "No special characters allowed")]
        public string Area { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9\\s.]*$", ErrorMessage = "No special characters allowed")]
        public string City { get; set; }

        [Required]
        [DisplayName("Pin Code")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Invalid Pin-Code")]
        public string PinCode { get; set; }

        [Required]
        [DisplayName("Phone number")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Contact person name")]
        [RegularExpression("^[a-zA-Z0-9\\s.]*$", ErrorMessage = "No special characters allowed")]
        public string ContactPersonName { get; set; } = "";

        [DisplayName("Minimum delivery time (hh:mm)")]
        [RegularExpression("^[0-9]{2}:[0-9]{2}$", ErrorMessage = "Time format must be HH:mm")]
        public string MinDeliveryTime { get; set; } = "";

        [DisplayName("Cost for two")]
        public string CostForTwo { get; set; } = "";

        public string PhotoUrl { get; set; } = "";

        [NotMapped]
        public IFormFile Image { get; set; }

        public List<Menu> Menus { get; set; }
    }
}
