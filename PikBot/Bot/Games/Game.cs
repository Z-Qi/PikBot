using Discord.WebSocket;
using System;
using System.Linq;

namespace PikBot.Bot.Games
{
    public class Game
    {
        private ulong[] Players { get; }
        private SocketChannel Channel { get; }

        public Game(SocketChannel channel)
        {
            Channel = channel;
        }

        public bool AddPlayer(ulong playerId)
        {
            if (Players.Contains(playerId)) return false;

            return true;
        }

        public void Debug()
        {
            Console.WriteLine(Channel.Id);
            if (Players.Length > 0) foreach (ulong playerId in Players) Console.WriteLine(playerId);
        }
    }
}
