using System.ComponentModel.DataAnnotations;

namespace WebAPI2
{
    public class Product
    {    
        [Key]
        public int ProductID { get; set; }
       

        public int ProductCategoryID { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
