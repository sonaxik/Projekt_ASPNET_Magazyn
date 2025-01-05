using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class Asortyment
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Brand { get; set; }
        public decimal Price { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }


    }
}
