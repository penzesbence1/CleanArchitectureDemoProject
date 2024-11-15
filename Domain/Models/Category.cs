﻿using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }

        
        public ICollection<Product>? Products { get; set; }
    }

}
