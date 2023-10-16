using Cesxhin.AnimeManga.Application.Consumers;
using Cesxhin.AnimeManga.Modules.Generic;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System;

namespace Cesxhin.AnimeManga.NotifyService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var selectService = (Environment.GetEnvironmentVariable("SELECT_SERVICE") ?? "any").ToLower();

                    //check envs
                    if (selectService == "discord" || selectService == "any")
                    {
                        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBHOOK_DISCORD_BOOK")))
                            throw new Exception("Missing env WEBHOOK_DISCORD_BOOK");

                        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBHOOK_DISCORD_BOOK_REQUEST")))
                            throw new Exception("Missing env WEBHOOK_DISCORD_BOOK_REQUEST");

                        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBHOOK_DISCORD_VIDEO")))
                            throw new Exception("Missing env WEBHOOK_DISCORD_VIDEO");

                        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBHOOK_DISCORD_VIDEO_REQUEST")))
                            throw new Exception("Missing env WEBHOOK_DISCORD_VIDEO_REQUEST");
                    }

                    if (selectService == "telegram" || selectService == "any")
                    {
                        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TOKEN_BOT")))
                            throw new Exception("Missing env TOKEN_BOT");

                        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CHAT_ID")))
                            throw new Exception("Missing env CHAT_ID");

                        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CHAT_ID_REQUEST")))
                            throw new Exception("Missing env CHAT_ID_REQUEST");
                    }

                    //rabbit
                    services.AddMassTransit(
                    x =>
                    {
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(
                                Environment.GetEnvironmentVariable("ADDRESS_RABBIT") ?? "localhost",
                                "/",
                                credentials =>
                                {
                                    credentials.Username(Environment.GetEnvironmentVariable("USERNAME_RABBIT") ?? "guest");
                                    credentials.Password(Environment.GetEnvironmentVariable("PASSWORD_RABBIT") ?? "guest");
                                });

                            if (selectService == "discord" || selectService == "any")
                            {
                                cfg.ReceiveEndpoint("notify-video-discord", e => {
                                    e.Consumer<NotifyVideoConsumer>();
                                });
                                cfg.ReceiveEndpoint("notify-book-discord", e => {
                                    e.Consumer<NotifyBookConsumer>();
                                });
                                cfg.ReceiveEndpoint("notify-request-video-discord", e => {
                                    e.Consumer<NotifyRequestVideoConsumer>();
                                });
                                cfg.ReceiveEndpoint("notify-request-book-discord", e => {
                                    e.Consumer<NotifyRequestBookConsumer>();
                                });
                            }

                            if(selectService == "telegram" || selectService == "any")
                            {
                                cfg.ReceiveEndpoint("notify-video-telegram", e => {
                                    e.Consumer<NotifyVideoTelegramConsumer>();
                                });
                                cfg.ReceiveEndpoint("notify-book-telegram", e => {
                                    e.Consumer<NotifyBookTelegramConsumer>();
                                });
                                cfg.ReceiveEndpoint("notify-request-video-telegram", e => {
                                    e.Consumer<NotifyRequestVideoTelegramConsumer>();
                                });
                                cfg.ReceiveEndpoint("notify-request-book-telegram", e => {
                                    e.Consumer<NotifyRequestBookTelegramConsumer>();
                                });
                            }

                        });
                    });

                    //setup nlog
                    var level = (Environment.GetEnvironmentVariable("LOG_LEVEL") ?? "info").ToLower();
                    LogLevel logLevel = NLogManager.GetLevel(level);
                    NLogManager.Configure(logLevel);

                    services.AddHostedService<Worker>();
                });
    }
}
