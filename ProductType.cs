using System.ComponentModel.DataAnnotations;

namespace WebAPI2
{
    public class ProductType
    {
        [Key]
        public int ProductCategoryID { get; set; }

        public string ProductName { get; set; }

        
    }
}
