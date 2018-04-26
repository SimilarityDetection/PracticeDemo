using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using log4net;

namespace QuartzDemo.Jobs
{
    public class CronJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobKey key = context.JobDetail.Key;
                JobDataMap dataMap = context.JobDetail.JobDataMap;
                string jobName = dataMap.GetString("jobname");

                var now = DateTime.Now;

                await Console.Out.WriteLineAsync("Cron!"
                + "\tkey:" + key + "\tjobName:" + jobName
                + "\t" + now);
            }
            catch (Exception ex)
            {
                logger.Error("CronJob异常", ex);
                throw new JobExecutionException();
            }
        }
    }
}