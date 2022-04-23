using System;
using System.IO;
using System.Web;
using System.Web.SessionState;
using Serilog;

namespace CustomModule
{
    public class CustomModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication app)
        {
            initLogger(app.Context);

            app.BeginRequest += (sender, args) =>
            {
                var httpApplication = (HttpApplication)sender;

                // every request require session, otherwise some request(html) will not obtain session.
                httpApplication.Context.SetSessionStateBehavior(SessionStateBehavior.Required);
            };

            app.PostAcquireRequestState += PostAcquireRequestState;
        }

        private void initLogger(HttpContext context)
        {
            var appFilePath = context.Server.MapPath("~");

            var absoluteLogPath = Path.Combine(appFilePath, "log\\custom-module.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                // save logs in relevant folder
                .WriteTo.File(absoluteLogPath, rollingInterval: RollingInterval.Day, shared: true)
                .CreateLogger();
        }

        private void PostAcquireRequestState(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;
            var httpSessionState = httpApplication.Context.Session;
            var url = httpApplication.Request.Url.ToString();

            //symotion-prefix)s throw new NotImplementedException();
            Log.Information("session: " + (httpSessionState != null) + "; [url]: " + url);

            if (httpSessionState == null) return;

            Log.Information("session: " + httpSessionState["a"]);
            var random = new Random();
            httpSessionState["a"] = random.Next();
        }
    }
}