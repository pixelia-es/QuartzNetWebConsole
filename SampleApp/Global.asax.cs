using System;
using System.Web;
using System.Web.Routing;
using Quartz;
using Quartz.Impl;
using QuartzNetWebConsole;

namespace SampleApp {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication {
        void Application_Start() {
            // First, initialize Quartz.NET as usual. In this sample app I'll configure Quartz.NET by code.

            //var schedulerFactory = new StdSchedulerFactory();
            //var scheduler = schedulerFactory.GetScheduler();
            IScheduler scheduler = new StdSchedulerFactory(Utils.QuartzProperties).GetScheduler();
            if (!scheduler.IsStarted) {
                scheduler.Start();
            }

            // This tells the QuartzNetWebConsole what scheduler to use
            Setup.Scheduler = () => scheduler;

            //// I'll add some global listeners
            //scheduler.ListenerManager.AddJobListener(new GlobalJobListener());
            //scheduler.ListenerManager.AddTriggerListener(new GlobalTriggerListener());

            //// A sample trigger and job
            ////var trigger = TriggerBuilder.Create()
            ////    .WithIdentity("myTrigger")
            ////    .WithSchedule(DailyTimeIntervalScheduleBuilder.Create()
            ////        .WithIntervalInSeconds(6))
            ////    .StartNow()
            ////    .Build();
            ////var job = new JobDetailImpl("myJob", null, typeof(HelloJob));

            //var trigger = TriggerBuilder.Create()
            //    .WithIdentity("msi_trigger1", "msi_group1")
            //    .WithCronSchedule("0 0/1 * 1/1 * ? *").StartNow()
            //    .WithPriority(1)
            //    .Build();

            //var job = new JobDetailImpl("MSI_Job", "MSI_JobGroup", typeof(MSI_Job));

            //// Validate that the job doesn't already exists
            //if (scheduler.CheckExists(new JobKey("MSI_Job", "MSI_JobGroup"))) {
            //    scheduler.DeleteJob(new JobKey("MSI_Job", "MSI_JobGroup"));
            //}

            //scheduler.ScheduleJob(job, trigger);

            //// A dummy calendar
            ////scheduler.AddCalendar("myCalendar",[NonSerializedAttribute] new DummyCalendar { Description = "dummy calendar" }, false, false);
            RouteTable.Routes.Add(new R());
        }

        private class R : RouteBase {
            private static readonly Lazy<object> initLogger = new Lazy<object>(() => {
                // This adds an logger to the QuartzNetWebConsole. It's optional.
                var partialQuartzConsoleUrl = string.Format("http://{0}:{1}/quartz/", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port);
                Setup.Logger = new MemoryLogger(1000, partialQuartzConsoleUrl);
                return null;
            });

            public override RouteData GetRouteData(HttpContextBase httpContext) {
                var _ = initLogger.Value;
                if (httpContext.Request.Url.PathAndQuery == "/")
                    httpContext.Response.Redirect("/quartz/index.ashx", endResponse: true);
                return null;
            }

            public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
                throw new NotImplementedException();
            }
        }
    }
}