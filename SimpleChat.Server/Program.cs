using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SimpleChat.Server
{
    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Function when application starts.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns><see cref="IHostBuilder"/>Host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
