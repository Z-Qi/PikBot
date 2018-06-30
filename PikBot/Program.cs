using Discord;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace PikBot
{
    public class Program
    {
        private class Credentials
        {
            public string prefix { get; set; }
            public string token { get; set; }
        }

        Credentials credentials = JsonConvert.DeserializeObject<Credentials>(File.ReadAllText(@"./cred.json"));

        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            var discordClient = new DiscordSocketClient();

            discordClient.Log += Log;
            discordClient.MessageReceived += MessageReceived;

            string token = credentials.token;

            await discordClient.LoginAsync(TokenType.Bot, token);
            await discordClient.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Author.IsBot) return;
            if (!message.Content.StartsWith(credentials.prefix)) return;

            ISocketMessageChannel channel = message.Channel;
            string[] args = message.Content.Substring(credentials.prefix.Length).Split(' ');
            string command = args[0];

            if (command == "echo")
            {
                string[] echo = new string[args.Length - 1];
                Array.Copy(args, 1, echo, 0, args.Length - 1);

                await channel.SendMessageAsync(string.Join(" ", echo));
            }

            if (command == "ping")
            {
                RestUserMessage pingMessage = await channel.SendMessageAsync("pinging");
                TimeSpan ping = pingMessage.CreatedAt.Subtract(message.CreatedAt);

                await pingMessage.ModifyAsync(msg => msg.Content = ping.TotalMilliseconds.ToString() + "ms");
            }
        }
    }
}