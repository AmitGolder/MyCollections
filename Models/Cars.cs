using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollections.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public string ProductNo { get; set; }
        public int Year { get; set; }

        // Add ImagePath property
        public string ImagePath { get; set; }
    }

}
