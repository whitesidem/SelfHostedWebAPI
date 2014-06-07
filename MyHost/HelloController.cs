using System.Web.Http;

namespace MyHost
{
    public class HelloController : ApiController
    {

        // GET myApi/Hello/5
        public string GetHelloWorld(int id)
        {
            //Note: Hello is name of controller,  
            return "Hello World";
        }

    }
}
