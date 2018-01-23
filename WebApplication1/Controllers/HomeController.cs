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
using System.Drawing;
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
                
                //Session.id
                //sysUsers.ForEach(s => tc.table_ip.Add(s));


                  
                tc.table_ip.Max(s=>s.id);
                tc.table_ip.OrderBy(s=>s.id).OrderByDescending(s=>s.ip);

               // Response.Write();
                //var ss = (from s in tc.table_ip select s).Take(5);
                //var ss = (from s in tc.table_ip select s).First();
                var ss = (from s in tc.table_ip
                          group s by s.city into t
                          select new
                          {
                           t.Key,
                           ipCount=t.Count()
                          });

                //sysUsers.Max(s=>s.id);
                tc.SaveChanges();
                var td = from table_ip_ in tc.table_ip where table_ip_.ip == "" select new { table_ip_.id };

                //var city=from s in tc.table_ip.SqlQuery("exec ")
                td.FirstOrDefault();
                //var ip_city = tc.city.Where("");
                //List<user> users = new List<user>(10000);

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
        public void Image()
        {

            Response.AddHeader("Content-Type", "image/png");
            var bitmap = new Bitmap(200,100);
            Graphics g = Graphics.FromImage(bitmap);
            var pen=new Pen(Color.Red,20);

            g.DrawString("Hello world", new Font(FontFamily.GenericSerif,20,GraphicsUnit.Pixel), pen.Brush, new PointF(10, 10));
            g.Dispose();
            var ms = new MemoryStream(); bitmap.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
            bitmap.Dispose();
            ms.Seek(0, SeekOrigin.Begin);
            Response.BinaryWrite(ms.ToArray());
            ms.Close();
            ms.Dispose();
            Response.End();
        }
    }
}
