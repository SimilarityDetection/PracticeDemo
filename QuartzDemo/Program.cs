using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;
using QuartzDemo.Jobs;

namespace QuartzDemo
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            XmlConfigurator.Configure(
                LogManager.GetRepository(Assembly.GetAssembly(typeof(LogManager))),
                new FileInfo("log4net.config"));

            logger.Info("Main");

            // trigger async evaluation
            RunProgram().GetAwaiter().GetResult();
        }

        private static async Task RunProgram()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                NameValueCollection props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(props);
                IScheduler scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                SetupHelloJob(scheduler);
                SetupCronJob(scheduler);

                // some sleep to show what's happening
                await Task.Delay(TimeSpan.FromDays(10));

                // and last shut down the scheduler when you are ready to close your program
                await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }

        private static async void SetupHelloJob(IScheduler scheduler)
        {
            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .UsingJobData("jobname", "job1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(2)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>  
        ///  
        /// </summary>  
        private static async void SetupCronJob(IScheduler scheduler)
        {
            // define the job and tie it to our CronJob class
            IJobDetail job = JobBuilder.Create<CronJob>()
                .WithIdentity("job2", "group2")
                .UsingJobData("jobname", "job2")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group2")
                .StartNow()
                //.WithCronSchedule("0 0 0 1 1/1 ?")
                .WithCronSchedule("0 0/1 * * * ?")
                .Build();

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}