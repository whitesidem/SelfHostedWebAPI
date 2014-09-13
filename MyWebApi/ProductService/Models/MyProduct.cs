using System;

namespace ProductService.Models
{
    public class MyProduct
    {
        public int Id { get; set; }
        public String Sku { get; set; }
        public String Description { get { return String.Format("My Sku is {0}", Sku); } }

        public MyProduct(string sku, int id)
        {
            Sku = sku;
            Id = id;
        }
    }
}