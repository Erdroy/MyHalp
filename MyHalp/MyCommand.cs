// MyHalp © 2016-2017 Damian 'Erdroy' Korczowski

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// Generic class used by MyConsole and MyRCon.
    /// Allows to register commands.
    /// </summary>
    public class MyCommands
    {
        /// <summary>
        /// Structure for command data.
        /// </summary>
        public struct Command
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Group { get; set; }
            public Type[] Parameters { get; set; }
        }

        private readonly List<Command> _commands = new List<Command>();

        /// <summary>
        /// Registers command with specified name and execution method in given command group.
        /// </summary>
        /// <param name="commandGroup">The command group, easly unregister batch of commands by one call.
        /// Use this of eg.: level dependent commands, only 'when playing' command etc. </param>
        /// <param name="commandName">The command name.</param>
        /// <param name="onExecute">Called when command is being executed.</param>
        /// <param name="description">(optional)The command description.</param>
        public void RegisterCommand(string commandGroup, string commandName, Action<object[]> onExecute, string description = "No description.")
        {
            var parameters = onExecute.Method.GetGenericArguments();

            // check commands, do not allow duplicates!
            var commands = _commands.Where(x => x.Name == commandName && x.Parameters.Length == parameters.Length).ToArray();
            if (commands.Length != 0)
            {
                MyLogger.Add("Command with this name(" + commandName + ") and the same parameters count already exists.", MyLoggerLevel.Error);
                return;
            }

            // register command
            _commands.Add(new Command
            {
                Name = commandName,
                Description = description,
                Group = commandGroup,
                Parameters = parameters
            });
        }

        /// <summary>
        /// Unregisters batch of commands by group name.
        /// </summary>
        /// <param name="commandGroup">The group name.</param>
        public void UnregisterGroup(string commandGroup)
        {
            // select commands with given group name which will be deleted
            var toRemove = _commands.Where(x=> x.Group == commandGroup);

            // delete all selected commands
            foreach (var remove in toRemove)
                _commands.Remove(remove);
        }

        /// <summary>
        /// Executes command with given parameters.
        /// </summary>
        /// <param name="commandName">The command name.</param>
        /// <param name="parameters">The command parameters, typeless, checking and parsing will be done.</param>
        public void Execute(string commandName, params string[] parameters)
        {
            var commands = _commands.Where(x => x.Name == commandName && x.Parameters.Length == parameters.Length).ToArray();
            if (commands.Length == 0)
            {
                MyLogger.Add("Command with this name(" + commandName + ") and the given parameters count does not exist!", MyLoggerLevel.Error);
                return;
            }

        }

        /// <summary>
        /// Executes raw command, parse arguments etc.
        /// </summary>
        /// <param name="command">The command string, eg.: 'volume master 0.2' or 'volume "master" 0.2'</param>
        public void ExecuteRaw(string command)
        {
            // parse

        }

        /// <summary>
        /// Lists all available commands.
        /// </summary>
        /// <returns>The commands array containing all commands.</returns>
        public Command[] GetAllCommands()
        {
            return _commands.ToArray();
        }

        /// <summary>
        /// Initializes the MyCommand instance and internal commands.
        /// </summary>
        public static void Init()
        {
            if (Instance != null)
            {
                Debug.LogError("MyCommand can be initialized only once!");
                return;
            }

            Instance = new MyCommands();
            
        }

        /// <summary>
        /// The MyCommand current instance.
        /// </summary>
        public static MyCommands Instance { get; private set; }
    }
}
