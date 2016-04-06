using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QM1571
{
    class Program
    {
        static void Main(string[] args)
        {
            Core app = new Core();

            app.StartLogging();

            Console.WriteLine("Press Enter to exit.");

            Console.ReadLine();

            LogIt("Exiting ...");
            app.StopLogging();

        }

        public static void LogIt(string Msg)
        {
            Console.WriteLine(Msg);
        }

    }




}
