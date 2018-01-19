using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Show(string txt_ip, string btn)
        {
            //txt_ip = "127.0.0.1";
            if (btn == "确定")
              
                 return RedirectToAction("Result", "Home", new { txt=txt_ip});
            else if(btn=="取消")
            {
                ViewBag.tit = "Home Page";              
               
            }
               

            return View();
        }

        public ActionResult Result()
        {

            //ViewBag.ip = Request.QueryString["txt"];

            string url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + Request.QueryString["txt"];

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream stream = res.GetResponseStream();

            StreamReader sr = new StreamReader(stream, Encoding.Default);
            string s = sr.ReadToEnd();
            //textBox1.Text = s;
            //webBrowser1.DocumentText = s;
            //ViewBag.city = s;
            ViewBag.city = s;
            sr.Close();

            return View();
        }
    }
}
