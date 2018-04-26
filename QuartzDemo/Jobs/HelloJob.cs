using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using log4net;

namespace QuartzDemo.Jobs
{
    [DisallowConcurrentExecution]
    [PersistJobDataAfterExecution]
    public class HelloJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobKey key = context.JobDetail.Key;
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                string jobName = dataMap.GetString("jobname");
                dataMap["jobname"] = jobName + "1";

                var now = DateTime.Now;

                await Console.Out.WriteLineAsync("Hello!"
                + "\tkey:" + key + "\tjobName:" + jobName
                + "\t" + now);

                Thread.Sleep(4000);

                await Console.Out.WriteLineAsync("Hello!"
                + "\tkey:" + key + "\tjobName:" + jobName
                + "\t" + now);
            }
            catch (Exception ex)
            {
                logger.Error("HelloJob异常", ex);
                throw new JobExecutionException();
            }
        }
    }
}