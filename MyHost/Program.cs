﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using ProductsService;
using RouteParameter = System.Web.Http.RouteParameter;



namespace MyHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            const string baseAddress = "http://localhost:8080";

            var config = new HttpSelfHostConfiguration(baseAddress);


            ReferenceAllDllsWithControllers();


            // Set our own assembly resolver where we add the assemblies we need
            //var assemblyResolver = new CustomAssembliesResolver();
            //config.Services.Replace(typeof(IAssembliesResolver), assemblyResolver);


            config.Routes.MapHttpRoute(
                "API Default", 
                "myApi/{controller}/{id}",
                new { id = RouteParameter.Optional });



            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Server is listening at: {0}", baseAddress);
                Console.WriteLine();

                OutputApiDiscoveryDetail(config, baseAddress);

                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }

        }


        private static void OutputApiDiscoveryDetail(HttpSelfHostConfiguration config, string baseAddress)
        {
            IApiExplorer apiExplorer = config.Services.GetApiExplorer();
            foreach (ApiDescription api in apiExplorer.ApiDescriptions)
            {
                Console.WriteLine("URI: {0}/{1}", baseAddress, api.RelativePath);
                Console.WriteLine("HTTP method: {0}", api.HttpMethod);
                foreach (ApiParameterDescription parameter in api.ParameterDescriptions)
                {
                    Console.WriteLine("Parameter: {0} - {1}", parameter.Name, parameter.Source);
                }
                Console.WriteLine();
            }
        }

        private static void ReferenceAllDllsWithControllers()
        {
            //Note: Need to reference all other dlls that contain controllers so that the route registration includes them
#pragma warning disable 168
            Type products2Type = typeof(ProductController);
#pragma warning restore 168
        }


        //public class CustomAssembliesResolver : DefaultAssembliesResolver
        //{
        //    public override ICollection<Assembly> GetAssemblies()
        //    {
        //        Type products2Type = typeof(Products2Controller);
        //        var externalAssembly = products2Type.Assembly.GetType();


        //        ICollection<Assembly> baseAssemblies = base.GetAssemblies();

        //        var assemblies = new List<Assembly>(baseAssemblies);

        //        var controllersAssembly = Assembly.GetAssembly(externalAssembly);

        //        baseAssemblies.Add(controllersAssembly);

        //        return assemblies;
        //    }
        //}

    }
}
