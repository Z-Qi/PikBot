using Discord.WebSocket;
using PikBot.Bot;
using System;
using System.Reflection;

namespace PikBot.Commands
{
    class CommandFactory
    {
        private static CommandFactory factory = null;

        private CommandFactory() { }

        public static CommandFactory GetCommandFactory()
        {
            if (factory == null) factory = new CommandFactory();
            return factory;
        }

        public Command GetCommand(ISocketMessageChannel channel, User user, string[] args, string commandName)
        {
            commandName = "PikBot.Commands." + commandName + "Command";
            Type commandType = Type.GetType(commandName, true);
            Type[] paramTypes = new Type[] { channel.GetType(), user.GetType(), args.GetType() };

            ConstructorInfo constructor = commandType.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null, paramTypes, null);

            Object obj = constructor.Invoke(new object[] { channel, user, args });

            if (obj is Command)
                return (Command)obj;
            else
                return null;
        }
    }
}
