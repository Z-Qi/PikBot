using Discord.WebSocket;
using PikBot.Bot.Games;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PikBot.Bot.ServiceManager
{
    enum Games
    {
        Countries,
        OddOnesOut
    }

    public class GameManager
    {
        private static GameManager _gameManager = null;
        private DiscordSocketClient _client;
        private Dictionary<ulong, Game> _activeGames;

        private readonly string gameNameSpace = "PikBot.Bot.Games.";

        private GameManager(DiscordSocketClient client)
        {
            _client = client;
            _activeGames = new Dictionary<ulong, Game>();
        }

        public static GameManager GetManager(DiscordSocketClient client)
        {
            return _gameManager ?? (_gameManager = new GameManager(client));
        }

        public bool HasActiveGame(ulong channelId)
        {
            return _activeGames.ContainsKey(channelId);
        }

        public Game GetNewGame(ulong channelId, string gameName)
        {
            Type GameType = Type.GetType(gameNameSpace + gameName, true);
            Type[] paramTypes = new Type[] { typeof(SocketChannel) };

            ConstructorInfo constructor = GameType.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null, paramTypes, null);

            Object obj = constructor.Invoke(new object[] { _client.GetChannel(channelId) });

            if (obj is Game)
            {
                _activeGames.Add(channelId, obj as Game);
                return obj as Game;
            }
            else
                return null;
        }

        public Game GetActiveGame(ulong channelId)
        {
            if (_activeGames.TryGetValue(channelId, out Game game)) return game;

            return null;
        }

        public bool Abort(ulong channelId)
        {
            if (HasActiveGame(channelId))
                return _activeGames.Remove(channelId);

            return false;
        }

        public void Debug()
        {
            Console.WriteLine(_gameManager);
            Console.WriteLine(_client);
            Console.WriteLine(_activeGames.Count);

            foreach (KeyValuePair<ulong, Game> pair in _activeGames)
            {
                Console.WriteLine(pair.Key + "\t:\t" + pair.Value.GetType().ToString());
                pair.Value.Debug();
            }
        }
    }
}
