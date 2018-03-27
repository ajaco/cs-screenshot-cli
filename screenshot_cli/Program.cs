using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace screenshot_cli
{
    enum ExitCode : int
    {
        Success = 0,
        MissingPid = 1,
        ProcessNotFound = 2,
        MissingOutputPath = 8,
        BadPidInput = 9,
        UnknownError = 10
    }


    class Program
    {
        static int Main(string[] args)
        {
            var arguments = new Dictionary<string, string>();

            foreach (string argument in args)
            {
                string[] splitted = argument.Split('=');

                if (splitted.Length == 2)
                {
                    arguments[splitted[0]] = splitted[1];
                }
            }

            if (!arguments.ContainsKey("pid")) return (int)ExitCode.MissingPid;
            if (!arguments.ContainsKey("output")) return (int)ExitCode.MissingOutputPath;

            int pid = 0;
            if (!Int32.TryParse(arguments["pid"], out pid)) return (int)ExitCode.BadPidInput;


            try
            {
                var process = Process.GetProcessById(pid);
                var screenshotter = new Screenshotter();
                Bitmap screen = (Bitmap)screenshotter.CaptureWindow(process.MainWindowHandle);
                screen.Save(arguments["output"]);
            }
            catch (ArgumentException)
            {
                return (int)ExitCode.ProcessNotFound;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (int)ExitCode.UnknownError;
            }

            return (int)ExitCode.Success;
        }
    }
}
