using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;

namespace ProductClient
{


    public class ProductServiceACL
    {
        private const string _httpLocalhost = "http://localhost:8080";

        public static ProductViewModel GetProduct(int id)
        {
            var client = new RestClient(_httpLocalhost);
            var request = new RestRequest("myApi/Product/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString(CultureInfo.InvariantCulture)); 
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var jsonAsString = response.Content; 
            dynamic expandoObj = JsonConvert.DeserializeObject<ExpandoObject>(jsonAsString, new ExpandoObjectConverter());
            return MapFromJsonDynamicProductToViewModel(expandoObj);
        }

        public static List<ProductViewModel> GetAllProducts()
        {
            var client = new RestClient(_httpLocalhost);
            var request = new RestRequest("myApi/Product", Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var jsonAsString = response.Content;
            dynamic expandoObj = JsonConvert.DeserializeObject<ExpandoObject>(jsonAsString, new ExpandoObjectConverter());
            return CreateProductSetFromDynmicProductList(expandoObj);
        }

        public static List<ProductViewModel> GetProductsByQuery(string queryString)
        {
            var client = new RestClient(_httpLocalhost);
            var request = new RestRequest("myApi/Product", Method.GET);
            request.AddParameter("query", queryString); 
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var jsonAsString = response.Content;
            dynamic expandoObj = JsonConvert.DeserializeObject<ExpandoObject>(jsonAsString, new ExpandoObjectConverter());
            return CreateProductSetFromDynmicProductList(expandoObj);
        }

        private static object CreateProductSetFromDynmicProductList(dynamic expandoObj)
        {
            var productList = new List<ProductViewModel>();

            foreach (dynamic product in expandoObj.Products)
            {
                productList.Add(MapFromJsonDynamicProductToViewModel(product));
            }
            return productList;
        }

        private static ProductViewModel MapFromJsonDynamicProductToViewModel(dynamic product)
        {
            return new ProductViewModel
                {
                    ProductCode = product.Sku,
                    ProductDescription = product.Description
                };
        }




/*
        public static ProductViewModel GetProductWithHttpWebRequest(int id)
        {
            const int POINT_OF_SATISFIED_CURIOSITY = 7;
            try
            {
                string uri = "http://localhost:8080/myApi/Product/1";  // <-- this returns formatted json
                var webRequest = (HttpWebRequest)WebRequest.Create(uri);
                webRequest.Method = "GET";  // <-- GET is the default method/verb, but it's here for clarity
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                if ((webResponse.StatusCode == HttpStatusCode.OK) && (webResponse.ContentLength > 0))
                {
                    var reader = new StreamReader(webResponse.GetResponseStream());
                    string s = reader.ReadToEnd();

                    //var arr = JsonConvert.DeserializeObject<JArray>(s);
                    //int i = 1;
                    //string cat;
                    //string film;
                    //string instavid;
                    //string bluray;
                    //string dvd;
                    //string imghtml;
                    //foreach (JObject obj in arr)
                    //{
                    //    cat = (string)obj["category"];
                    //    film = (string)obj["film"];
                    //    instavid = (string)obj["instavid"];
                    //    bluray = (string)obj["bluray"];
                    //    dvd = (string)obj["dvd"];
                    //    imghtml = (string)obj["imghtml"];
                    //    MessageBox.Show(string.Format("Object {0} in JSON array: cat == {1}, " +
                    //      "film == {2}, instavid == {3}, bluray == {4}, dvd == {5}, imghtml == {6}",
                    //         i, cat, film, instavid, bluray, dvd, imghtml));
                    //    i++;
                    //    if (i > POINT_OF_SATISFIED_CURIOSITY) break;
                    //}
                }
                else
                {
                    MessageBox.Show(string.Format("Status code == {0}, Content length == {1}",
                      webResponse.StatusCode, webResponse.ContentLength));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new ProductViewModel();
        }  



        public static ProductViewModel GetProductWithrestSharp(int id)
        {
            var client = new RestClient("http://localhost:8080");

//            var client = new RestClient("http://api.openweathermap.org");

            // client.Authenticator = new HttpBasicAuthenticator(username, password);

  //          var request = new RestRequest("/data/2.5/weather?id=2172797", Method.GET);

            var request = new RestRequest("myApi/Product/{id}", Method.GET);
            // request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("id", id.ToString()); // replaces matching token in request.Resource

            // add parameters for all properties on an object
            //request.AddObject(object);

            // or just whitelisted properties
            //request.AddObject(object, "PersonId", "Name", ...);

            // easily add HTTP Headers
            //request.AddHeader("header", "value");


            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response.Content);
            //});


            return new ProductViewModel();
        }
*/

    }
}
