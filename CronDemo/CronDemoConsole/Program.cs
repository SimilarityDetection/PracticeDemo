using Common.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CronDemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            // 调度器
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler();

            // JobDetail
            JobDetailImpl jobDetail = new JobDetailImpl("job1", typeof(HelloJob));

            // CronTrigger
            CronTriggerImpl cronTrigger = new CronTriggerImpl("triggerName", "triggerGroup", "0/1 * * * * ? ");

            // 调度增加job
            sched.ScheduleJob(jobDetail, cronTrigger);

            // 开始调度
            sched.Start();

            // 主线程睡觉
            Thread.Sleep(90 * 1000);

            // 等待jobs执行完，就关闭调度器
            sched.Shutdown(true);
        }
    }
}
