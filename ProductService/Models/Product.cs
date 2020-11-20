using System.Collections.Generic;
using System;

namespace ProductService.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Image> Image { get; set; }
        public IEnumerable<Price> Price { get; set; }
    }
}
