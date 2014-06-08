using System;
using System.Net;
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


        //Note: Product is name of controller,  
        // GET myApi/Products/5
        public MyProduct GetProduct(int id)
        {

            if (id <= 0)
            {
                //Responses are generally just raw objects whichdefault as a success case, edge conditions such as failures are dealt with by throwing  HttpResponseException
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var myProd = new MyProduct(String.Format("AMB_{0}", id));
            return myProd;
        }
    }
}
