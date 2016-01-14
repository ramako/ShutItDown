using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ShutItDown
{
    class Program
    {

        static int Main(string[] args)

        {
            if (args.Length == 0)
            {
                Console.WriteLine("You need to pass the process to monitor as a parameter.");
                return -1;
            }
            var earg = new ProcessExistEventArgs();

            Timer t = new Timer();
            t.Interval = 2000; //ms
            t.AutoReset = true; //Stops it from repeating
            t.Elapsed += 
                (sender,  e) => checkForProcess( sender, e, earg, args[0]);
            t.Elapsed += (sender, e) => decidePowerOff(sender, e, earg) ;
            t.Enabled = true;


            Console.ReadKey();
            return 0;
        }

        private static void decidePowerOff(object sender, ElapsedEventArgs e, ProcessExistEventArgs earg)
        {
            if (!earg.exists)
            {
                powerOff();
            }
                
        }


        //hackish, but works.
        private static void checkForProcess(object sender, ElapsedEventArgs e, ProcessExistEventArgs earg, string process)
        {
             Process[] localByName = Process.GetProcessesByName(process);
            if(localByName.Length<1)
                earg.exists = false;

        }
        private static void powerOff()
        {
            Process.Start("shutdown", "/s /t 10");
        }
    }

  
}
