using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostOwinApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup1>("http://localhost:5000"))
            {
                Console.WriteLine("Press [enter] to quit...");
                Console.ReadLine();
            }
        }
    }
}
