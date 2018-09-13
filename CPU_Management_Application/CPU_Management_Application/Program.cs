using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;
using System.IO;

namespace CPU_Management_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CPU Management Application";
            Console.ForegroundColor = ConsoleColor.Green;

            //This will Greet the user when the application starts
            SpeechSynthesizer synth = new SpeechSynthesizer();
            //synth.Speak("Hello, Welcome to CPU Management v2.0");

            #region Performance Counters
            // This displays the current CPU Usage as a percentage
            PerformanceCounter perfCPUcount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            perfCPUcount.NextValue();

            // This Displays the available memory in MB
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
            perfMemCount.NextValue();

            // This Displays the System Uptime (In Seconds)
            PerformanceCounter perfSystemUptime = new PerformanceCounter("System", "System Up Time");
            perfSystemUptime.NextValue();
            #endregion

            TimeSpan totalUptime = TimeSpan.FromSeconds(perfSystemUptime.NextValue());
            string uptimemessage = string.Format("The Current uptime is {0} Days {1} Hours {2} minutes {3} Seconds",
                (int)totalUptime.TotalDays,
                (int)totalUptime.Hours,
                (int)totalUptime.Minutes,
                (int)totalUptime.Seconds
                );
            //synth.Speak(uptimemessage);
            Console.WriteLine(uptimemessage);
            // This is an infinite while loop
            while (true)
            {
                int CPUCount = (int)perfCPUcount.NextValue();
                int AvailableMemory = (int)perfMemCount.NextValue();


                if (CPUCount >= 50 || AvailableMemory < 2048)
                {
                    Console.WriteLine("CPU Load        : {0}%", CPUCount);
                    Console.WriteLine("Available Memory: {0}MB", AvailableMemory);

                    Console.WriteLine("");
                }

                if (CPUCount > 80)
                {

                    string CPUloadVocalMessage = string.Format("Your Current CPU Load Is: {0} percent", CPUCount);
                    //synth.Speak(CPUloadVocalMessage);

                }
                if (AvailableMemory < 1024)
                {
                    string AvailableMemoryVocalMessage = string.Format("You Currently have {0} Megabytes of Memory Available", AvailableMemory);
                    //synth.Speak(AvailableMemoryVocalMessage);
                }
                if (totalUptime.TotalDays >= 7)
                {
                    //synth.Speak("Please Restart Your Computer It Needs A Break!!");
                    Thread.Sleep(100000);
                }

                Thread.Sleep(1000);
            }
        }
    }
}
