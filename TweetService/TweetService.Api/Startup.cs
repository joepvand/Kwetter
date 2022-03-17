using Microsoft.EntityFrameworkCore;
using TweetService.Application;
using TweetService.Data;
using TweetService.Data.Context;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TweetService.Api
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
            services.AddDbContext<TweetContext>(options =>
            {
                var connString = Configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connString);
            });
            services.AddTransient<Data.ITweetRepository, TweetRepository>();

            services.AddTransient<TweetApplication>();

            services.AddTransient<IEventSender, EventSender>();
            
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((cfx, cnf) =>
                {
                    cnf.Host(Environment.GetEnvironmentVariable("RabbitMQConnectionString"));
                });
            });

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TweetContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            context.Database.Migrate();
        }
    }
}
