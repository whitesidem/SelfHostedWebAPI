using System.Web.Http;

namespace MyHost
{
    public class HelloController : ApiController
    {

        // GET api/Hello/5
        public string Get(int id)
        {
            //Note: Hello is name of controller,  
            return "Hello World";
        }

    }
}
