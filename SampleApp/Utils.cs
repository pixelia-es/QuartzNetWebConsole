using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace SampleApp
{
    class Utils
    {
        public static string QuartzDbConnectionString
        {
            get { 
                //return "Data Source=192.168.2.196,5033;Initial Catalog=QuartzNETStore;User ID=sa;Password=0a0l0f0F";
                return "Data Source=192.168.2.49;Initial Catalog=QuartzNETStore;User ID=sa;Password=LCDAyL"; 
            }
        }
            
        public static NameValueCollection QuartzProperties
        {
            get 
            {
                var _quartzProperties = new NameValueCollection();

                // Configure Scheduler
                _quartzProperties.Add("quartz.scheduler.instanceName", "ServerScheduler");

                // Set Remoting Exporter
                //_quartzProperties.Add("quartz.scheduler.proxy", "true");
                //_quartzProperties.Add("quartz.scheduler.proxy.address", string.Format("tcp://{0}:{1}/{2}", "localhost", "555", "QuartzScheduler"));

                // Configure Thread Pool
                _quartzProperties.Add("quartz.threadPool.type", "Quartz.Simpl.SimpleThreadPool, Quartz");
                _quartzProperties.Add("quartz.threadPool.threadCount", "10");
                _quartzProperties.Add("quartz.threadPool.threadPriority", "Normal");

                // Configure Job Store
                _quartzProperties.Add("quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz");
                _quartzProperties.Add("quartz.jobStore.misfireThreshold", "60000");
                _quartzProperties.Add("quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz");
                _quartzProperties.Add("quartz.jobStore.useProperties", "true");
                _quartzProperties.Add("quartz.jobStore.dataSource", "default");
                _quartzProperties.Add("quartz.jobStore.tablePrefix", "QRTZ_");
                _quartzProperties.Add("quartz.jobStore.lockHandler.type", "Quartz.Impl.AdoJobStore.UpdateLockRowSemaphore, Quartz");

                // Configure Data Source
                _quartzProperties.Add("quartz.dataSource.default.connectionString", Utils.QuartzDbConnectionString);
                _quartzProperties.Add("quartz.dataSource.default.provider", "SqlServer-20");

                return _quartzProperties;
            }
        }


    }
}