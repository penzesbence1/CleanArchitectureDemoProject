using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

      
        public int Price { get; set; }

        public int Rating { get; set; }

        public int CategoryId { get; set; }

      
        public Category Category { get; set; }
    }

}
