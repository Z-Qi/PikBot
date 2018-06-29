using Discord;
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

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {

            if (!message.Author.IsBot && message.Content.StartsWith(credentials.prefix))
            {
                await message.Channel.SendMessageAsync("I'm listening");
            }
            else
            {
                return;
            }
        }
    }
}