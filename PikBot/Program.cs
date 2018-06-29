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
        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private class Credentials
        {
            public string token { get; set; }
        }

        public async Task MainAsync()
        {
            var discordClient = new DiscordSocketClient();

            discordClient.Log += Log;

            Credentials credentials = JsonConvert.DeserializeObject<Credentials>(File.ReadAllText(@"./cred.json"));
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
    }
}