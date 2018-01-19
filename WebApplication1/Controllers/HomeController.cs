using System;
using System.Collections.Generic;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        efEntities tc = new efEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Show(string txt_ip, string btn)
        {
            //txt_ip = "127.0.0.1";
            if (btn == "确定")
            {
                var sysUsers = new List<DAL.table_ip>
                {
                new DAL.table_ip {ip="127.0.0.1",city="aaaa"}

                };
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"\d");
                reg.Replace("ss3ss6a21ad342s", new System.Text.RegularExpressions.MatchEvaluator(
                    (System.Text.RegularExpressions.Match match) => {

                    var number = int.Parse(match.Value);
                    if (number % 2 == 0)
                        return "]";
                    else
                        return "[";
                }));
                
                sysUsers.ForEach(s => tc.table_ip.Add(s));
                tc.SaveChanges();
                var td = from table_ip_ in tc.table_ip where table_ip_.ip == "" select new { table_ip_.id };
                td.FirstOrDefault();
                //var ip_city = tc.city.Where("");
                List<user> users = new List<user>(10000);

                return RedirectToAction("Result", "Home", new { txt = txt_ip });
            }
                 
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
           

            ViewBag.city = s;
            sr.Close();

            return View();
        }
    }
}
