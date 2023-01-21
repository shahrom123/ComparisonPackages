using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public decimal Price { get; set;}

        public Product()
        {

        }
        public Product(int id,string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    } 
} 
