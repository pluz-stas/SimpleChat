using System.Linq;
using SimpleChat.Server.Hub;
using SimpleChat.Shared.Hub;
using SimpleChat.Bll.Interfaces;
using SimpleChat.Bll.Services;
using System.Reflection;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using SimpleChat.Dal;
using Microsoft.EntityFrameworkCore;
using SimpleChat.Dal.Interfaces;
using SimpleChat.Dal.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using SimpleChat.Server.Filters;
using AutoMapper;
using SimpleChat.Bll.Mapper;

namespace SimpleChat.Server
{
    /// <summary>
    /// Configures application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Application configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures application services.
        /// </summary>
        /// <param name="services">Application services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<SimpleChatDbContext>(o => o.UseSqlServer(connectionString));
            services.AddAutoMapper(config => config.AddProfile(new MappingProfile()), typeof(Startup));
            
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpExceptionFilter));
                options.SuppressAsyncSuffixInActionNames = false;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                    throw new ArgumentException(context.ModelState.Values
                        .First(x => x.Errors?.Any() == true).Errors
                        .FirstOrDefault(x => !string.IsNullOrEmpty(x.ErrorMessage)).ErrorMessage);
            })
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSignalR();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configures http pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleChat API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>(HubConstants.ChatUri);
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
