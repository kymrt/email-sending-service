using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using email_sending_service.Data;
using Hangfire;
using email_sending_service.Utilities;
using SendGrid;

namespace email_sending_service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Add Hangfire services.
            services.AddHangfire((container, configuration) => configuration
                    .UseSqlServerStorage(container.GetRequiredService<IConfiguration>().GetConnectionString("EmailDataContext")));
            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddDbContext<EmailDataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EmailDataContext")));

            services.AddTransient<CronJobService>();
            services.AddScoped<IMailService, SendGridMailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                EmailDataContext context = serviceScope.ServiceProvider.GetRequiredService<EmailDataContext>();
                context.Database.EnsureCreated();
                string apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
                SendGridClient client = new SendGridClient(apiKey);
                IMailService mailService = new SendGridMailService(client);
                CronJobService cronJobService = new CronJobService(context, mailService);
                app.UseHangfireDashboard();
                string cronExpression = Environment.GetEnvironmentVariable("CRON_EXPRESSION");
                RecurringJob.AddOrUpdate("emailsenderjob", () => cronJobService.SendMailFromDatabase(), cronExpression);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
