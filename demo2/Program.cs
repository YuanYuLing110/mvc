using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace demo2
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "a c v o e";

             System.Text.RegularExpressions.Regex reg=new System.Text.RegularExpressions.Regex("");
             reg.Replace(" ", new System.Text.RegularExpressions.MatchEvaluator((System.Text.RegularExpressions.Match match) =>
             {


             }));
        }
    }
}
