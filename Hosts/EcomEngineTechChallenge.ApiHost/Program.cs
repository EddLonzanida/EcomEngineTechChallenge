using System;
using Eml.Mef;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EcomEngineTechChallenge.ApiHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Application Started...");

                BuildWebHost(args).Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of unhandled exception.");
                throw;
            }
            finally
            {
                ClassFactory.Dispose(Startup.ClassFactory);
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

