using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Vintasoft.Imaging;

namespace Kotak.WebAPI
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ImagingGlobalSettings.RegisterImaging("Rajesh Chonde (Servers)", "rchonde@crownww.com", "ExdWcereZCWqg2qowguEK8aW0pFVjkpL8nM8a2/bgub92FXrSibHT7a0HWPwdWHiyp728PlFDIvRAyYrBY1aXwH7hpMGndsEOhAIEGrvU5c0PUW6hRqny/u25HXKYrwEkfF8OWlCBW3zV7+MsU7Z+DdtgoiKgfzJdKx+vhuhPJHg");
            //ImagingGlobalSettings.RegisterAnnotation("XOcUyQAjjq/6rPpsT0LNck6Ln6agmg9Ia3FQf8meNSu7cCIi2D43gaGl9Wya5kf9USbuo8R+nP/AvgxfcrDEsekqf91SIC+jqvZiTH2tYKVNENp0D/+51ohf+EC7N3BuycZ/XzF3yFnySKsDcw7m1Nr/8KuOwjNJpVdzconRrpDw");
            //ImagingGlobalSettings.RegisterPdfReader("aaEOzKrcE47n5B7j/okzRNZHtfSMAtLWpneyyVe1OClDPQjSskdNTkfMpyGNqwsFrHKdCuMu4MfB0i431AOgL6hOEXrN37tzc1gsyH9SgVNvW68Jff37imNyrUwJgT8rG+RABwSqGK77tqSbH4XdTnLye1nQN3P6YvAwSCsyO15Q");
            //ImagingGlobalSettings.RegisterPdfWriter("aJ8YyfWpCji+CotFQf0q7odCf+23a3eQYypbubrzB9DW0sb2qm8BUD3/3WW3UhE0E4dfNuCWGRgGSWyQDKbT9rKD3cdGFnfpFa4qEQuCr+fpxiPidC4lQ6N4lRTrX7zKl/0QJukSmLlsjjNDaDK21Dd+IZbgtLBwsKJLCuyUVYoA");
            //ImagingGlobalSettings.RegisterOcr("fsMpx5pr/gC8cEkzX14/FTDuoUd7cPKASk+xHWu6qg6rv51JYAf1kbgubk/T1SiPnkolpEO0M2XJ4M3eH/32NfzBO0vn/kkvInum4zIKCx2HT+v36wIlxjn8g48C+8wbzh8af6XLze4fASnGwNpHTZ4fAXwE96CcCg7MoJdJ0DFU");
            //ImagingGlobalSettings.RegisterOffice("OPzbb7dkINjlKUpWtFosIUwlbUF8EtYufr9pLnyGEAZmk8ZAQOA3QHh/rEgLoeBktGpV3wSYdLYwU6GoX6vrY3Ggw2rEhr1W9lCp5w3Wq4itBw90Vf/sZ5inZ7VblbZPTls0LfibWBSK92EAM/JE+XbEVEUS25C7nSZAyfEiAJZg");


            //      if (ImagingGlobalSettings.ServerName != null)
            //{
            //    ImagingGlobalSettings.RegisterImaging("Rajesh Chonde (Servers)", "rchonde@crownww.com", "ExdWcereZCWqg2qowguEK8aW0pFVjkpL8nM8a2/bgub92FXrSibHT7a0HWPwdWHiyp728PlFDIvRAyYrBY1aXwH7hpMGndsEOhAIEGrvU5c0PUW6hRqny/u25HXKYrwEkfF8OWlCBW3zV7+MsU7Z+DdtgoiKgfzJdKx+vhuhPJHg");

            //    //  ImagingGlobalSettings.RegisterImaging("Reg.name for server license", "Reg.email for server license", "Reg.code for server license");

            //}
            //else
            //{
            //    ImagingGlobalSettings.RegisterImaging("Rajesh Chonde (Servers)", "rchonde@crownww.com", "ExdWcereZCWqg2qowguEK8aW0pFVjkpL8nM8a2/bgub92FXrSibHT7a0HWPwdWHiyp728PlFDIvRAyYrBY1aXwH7hpMGndsEOhAIEGrvU5c0PUW6hRqny/u25HXKYrwEkfF8OWlCBW3zV7+MsU7Z+DdtgoiKgfzJdKx+vhuhPJHg");

            //    //  ImagingGlobalSettings.RegisterImaging("Reg.name for desktop license", "Reg.email for desktop license", "Reg.code for desktop license");
            //}


        }
        protected void Application_BeginRequest()
        {

          //  HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            {
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                Response.Headers.Add("Access-Control-Allow-Headers",
                  "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With,Accept,Pragma,Cache-Control");
                Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                Response.Headers.Add("Access-Control-Allow-Credentials", "true");
               // Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Pragma, Cache-Control, Authorization ");
                Response.Flush();

                // Preflight request comes with HttpMethod OPTIONS
                // The following line solves the error message

            }
            //if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            //{

            //    HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Pragma, Cache-Control, Authorization ");
            //    HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            //    HttpContext.Current.Response.End();

            //    // If any http headers are shown in preflight error in browser console add them below

            //}
        }
    }
}
