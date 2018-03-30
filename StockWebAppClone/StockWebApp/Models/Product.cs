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
        //No way a product should have a name longer than 100 characters
        [Required, StringLength(100)]
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        //This will prevent the app from containing a bunch of empty space in place of missing Expiration Date values
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "N/A")]
        public DateTime? ExpirationDate { get; set; }

        public Brand Brand { get; set; }
        public Category Category { get; set; }
        //public ICollection<Category> Categorys { get; set; }
        //One to many not many to many
    }
}