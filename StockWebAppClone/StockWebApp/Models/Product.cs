using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        [Required, StringLength(100)
            /*, RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Characters are not allowed.")*/]
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "N/A")]
        public DateTime? ExpirationDate { get; set; }

        public Brand Brand { get; set; }
        public Category Category { get; set; }
        //public ICollection<Category> Categorys { get; set; }
        //One to many not many to many
    }
}