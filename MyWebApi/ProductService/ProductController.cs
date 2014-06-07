using System;
using System.Web.Http;

namespace ProductsService
{
    public class ProductController : ApiController
    {
        public class MyProduct
        {
            public String Sku { get; set; }
            public String Description { get { return String.Format("My Sku is {0}", Sku); } }

            public MyProduct(string sku)
            {
                Sku = sku;
            }
        }


        // GET myApi/Products/5
        public MyProduct GetProduct(int id)
        {
            var myProd = new MyProduct("DABC123");
            //Note: Product is name of controller,  
            return myProd;
        }
    }
}
