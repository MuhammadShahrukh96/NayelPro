using System.ComponentModel.DataAnnotations.Schema;

namespace NayelPro.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        [NotMapped]
        public string CategoryName { get; set; }
    }
}
