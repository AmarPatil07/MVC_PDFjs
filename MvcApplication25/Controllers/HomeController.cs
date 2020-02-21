using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace MvcApplication25.Controllers
{
    public class HomeController : Controller
    {
        private WebClient client;

        //
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Viewer(string filePath)
        {
            //build the file path here
           // filePath = "/MyPDFs/" + filePath;
           //pass the file path to the View using a viewbag variable
          
            //We could also just return a view along with a query string with a file param pointing to the
            //location of the file on our server, for example: "Viewer?file=/MyPDFs/Pdf1.pdf"
            //but here I've just chosen to change the default URL of the viewer object, which is essentially
            //the same thing\
            if (!filePath.StartsWith("/MyPDFs/"))
            {
                string[] files = Directory.GetFiles(System.Web.HttpRuntime.AppDomainAppPath + "MyPDFs\\");
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.LastAccessTime < DateTime.Now.AddHours(-6))
                        fi.Delete();
                }
                var getFileName = filePath.Split('/');
                string fileNamePath = System.Web.HttpRuntime.AppDomainAppPath + "MyPDFs\\" + getFileName[getFileName.Length - 1];               
                client = new WebClient();
                client.DownloadFile(filePath, fileNamePath);
                ViewBag.filePath = "/MyPDFs/"+ getFileName[getFileName.Length - 1];

               
            }
            else
            {
                ViewBag.filePath = filePath;
            }  

            return View();
        }


    }
}
