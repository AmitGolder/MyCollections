using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollections.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>(); // One-to-Many Relationship

    }

}
