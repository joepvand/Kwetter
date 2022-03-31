﻿using MassTransit;

namespace NotificationService.Api
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

            services.AddMassTransit(config =>
            {
                config.AddConsumer<TweetTweetedConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Environment.GetEnvironmentVariable("RabbitMQConnectionString"));

                    cfg.ReceiveEndpoint("TweetTweeted-queue", c =>
                    {
                        c.ConfigureConsumer<TweetTweetedConsumer>(ctx);

                    });

                    cfg.ConfigureEndpoints(ctx);

                });
            });

            services.Configure<MassTransitHostOptions>(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(30);
                options.StopTimeout = TimeSpan.FromMinutes(1);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


        }
    }
}
