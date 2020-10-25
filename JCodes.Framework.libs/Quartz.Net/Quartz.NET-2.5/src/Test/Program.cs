using log4net;
using log4net.Config;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Topshelf;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo6();
        }

        // 有问题 FileNotFound
        private static void Demo1()
        {
            // http://www.cnblogs.com/jys509/p/4628926.html
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.xml");
            XmlConfigurator.ConfigureAndWatch(logCfg);

            try
            {
                HostFactory.Run(x =>
                {
                    x.UseLog4Net();

                    x.Service<ServiceRunner>();

                    x.SetDescription("QuartzDemo服务描述");
                    x.SetDisplayName("QuartzDemo服务显示名称");
                    x.SetServiceName("QuartzDemo服务名称");

                    x.EnablePauseAndContinue();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void Demo2()
        {
            // http://www.cnblogs.com/jys509/p/4628926.html
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.xml");
            XmlConfigurator.ConfigureAndWatch(logCfg);

            HostFactory.Run(x =>                                 //1
            {
                x.Service<TownCrier>();
                x.RunAsLocalSystem();
                x.SetDescription("Sample Topshelf Host");        //7
                x.SetDisplayName("Stuff");                       //8
                x.SetServiceName("Stuff");                       //9
            });           
        }

        private static void Demo3()
        {
            // http://docs.topshelf-project.com/en/latest/configuration/config_api.html
            // http://www.cnblogs.com/jys509/p/4614975.html
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.xml");
            XmlConfigurator.ConfigureAndWatch(logCfg);

            HostFactory.Run(x =>
            {
                x.Service<TownCrier>();
                x.RunAsLocalSystem();

                x.SetDescription("Sample Topshelf Host服务的描述");
                x.SetDisplayName("Stuff显示名称");
                x.SetServiceName("Stuff服务名称");
            });
        }

        private static void Demo4()
        {
            try
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                scheduler.Start();

                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(60));

                scheduler.Shutdown();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void Demo5() {
            try
            {
                Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build();

                // Tell quartz to schedule the job using our trigger
                scheduler.ScheduleJob(job, trigger);

                // some sleep to show what's happening
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }

        private static void Demo6() {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.xml");
            XmlConfigurator.ConfigureAndWatch(logCfg);

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
        }
    }

    public class TownCrier : ServiceControl
    {
        private Timer _timer = null;
        readonly ILog _log = LogManager.GetLogger(typeof(TownCrier));
        public TownCrier()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += (sender, eventArgs) => {
                _log.Info(DateTime.Now);
                Console.WriteLine("It is {0} and all is well", DateTime.Now);
            };

        }
        public bool Start(HostControl hostControl)
        {
            _log.Info("TopshelfDemo is Started");
            _timer.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _log.Info("TopshelfDemo is Stoped");
            _timer.Stop();
            return true;
        }
    }
}
