using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot
{
    public class BotManager
    {
        public static DiscordSocketClient BotClient;
        public static CommandService Commands;
        public static IServiceProvider ServiceProvider;
        public const string PREFIX = "-";


        public async Task RunBot()
        {
            Commands = new CommandService();
            BotClient = new DiscordSocketClient();
            ServiceProvider = ConfigureServices();
            await BotClient.LoginAsync(Discord.TokenType.Bot, Secret.GetToken());
            await BotClient.StartAsync();
            BotClient.Log += BothatwasGelogged;
            BotClient.Ready += BotistBereit;


            await Task.Delay(-1);
        }

        public Task BothatwasGelogged(LogMessage message)
        {
            Console.WriteLine("Botstatus: " + message);
            return Task.CompletedTask;
        }

        public IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<CommandsModule>()
                .BuildServiceProvider();
        }



        public async Task BotistBereit()
        {
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), ServiceProvider);
            await BotClient.SetGameAsync("Multi spionieren");
            await BotClient.SetStatusAsync(UserStatus.Online);
            BotClient.MessageReceived += Nachricht;
        }

        public async Task Nachricht(SocketMessage arg)
        {
            SocketUserMessage message = arg as SocketUserMessage;
            int commandPosition = 0;
            if (message.HasStringPrefix(PREFIX, ref commandPosition))
            {
                SocketCommandContext context = new SocketCommandContext(BotClient, message);
                IResult result = await Commands.ExecuteAsync(context, commandPosition, ServiceProvider);
                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
