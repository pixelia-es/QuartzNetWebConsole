using Common.Logging;
using System;
using System.Threading;
using Quartz;

namespace SampleApp {

    /// <summary>
    /// A sample dummy job
    /// </summary>
    [DisallowConcurrentExecution]
    public class MSI_Job : IJob {

        private static readonly ILog logging = LogManager.GetLogger(typeof(MSI_Job));

        public void Execute(IJobExecutionContext context) {
            try {
                logging.DebugFormat("{0}****{0}Job {1} fired @ {2} next scheduled for {3}{0}***{0}",
                                                        Environment.NewLine,
                                                        context.JobDetail.Key,
                                                        context.FireTimeUtc.Value.ToString("r"),
                                                        context.NextFireTimeUtc.Value.ToString("r"));

                logging.DebugFormat("{0}***{0}Hello World!{0}***{0}", Environment.NewLine);
            } catch(Exception ex) {
                logging.DebugFormat("{0}***{0}Failed: {1}{0}***{0}", Environment.NewLine, ex.Message);
            }
            //Thread.Sleep(5000);
        }
    }
}