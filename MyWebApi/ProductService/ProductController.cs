using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using ProductService.Models;

namespace ProductService
{
    public class ProductController : ApiController
    {
        //Note: Product is name of controller,  
        // GET myApi/Product/5
        public MyProduct GetProduct(int id)
        {
            var prods = AllProductsRepository();
            var prod = prods.FirstOrDefault(p => p.Id == id);
            if (prod==null)
            {
                //Responses are generally just raw objects whichdefault as a success case, edge conditions such as failures are dealt with by throwing  HttpResponseException
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return prod;
        }


        //Note: This does not play well with browser, use wfetch or ajax etc to see result 
        // GET myApi/Product
        public MyProductSet GetAllProducts()
        {
            var prods = AllProductsRepository();
            var mySet = new MyProductSet
                {
                    Products = prods.ToList()
                };
            return mySet;
        }




        // GET myApi/Product?query=all
        public MyProductSet GetProduct(string query)
        {
            var prods = AllProductsRepository();
            query = query.ToLower();
            switch (query)
            {
                case "all":
                    {
                        break;
                    }
                default:
                    {
                        prods = prods.Where(p => p.Sku.ToLower().Contains(query));
                        break;
                    }
            }
            var mySet = new MyProductSet
            {
                Products = prods.ToList()
            };
            return mySet;
        }

        private static IEnumerable<MyProduct> AllProductsRepository()
        {
            var prods = new List<MyProduct>();
            prods.Add(new MyProduct("ABC_1",1));
            prods.Add(new MyProduct("ABC_2",2));
            prods.Add(new MyProduct("ABC_3",3));
            prods.Add(new MyProduct("DEF_1",4));
            prods.Add(new MyProduct("DEF_2",5));
            return prods;
        }
    }
}
