﻿// MyHalp © 2016-2018 Damian 'Erdroy' Korczowski

#define USE_MYLOGGER

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MyHalp
{
    /// <summary>
    /// Generic class used by MyConsole and MyRCon.
    /// Allows to register commands.
    /// </summary>
    public sealed class MyCommands
    {
        /// <summary>
        /// Structure for command data.
        /// </summary>
        public struct Command
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Group { get; set; }
            public ParameterInfo[] Parameters { get; set; }
            public object MethodTarget { get; set; }
            public MethodInfo Method { get; set; }
        }
        
        // private
        private readonly List<Command> _commands = new List<Command>();

        [Obsolete("Please use MyCommands.Register")]
        public void RegisterCommand(string commandGroup, string commandName, Action onExecute, string description = "No description.")
        {
            RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }

        [Obsolete("Please use MyCommands.Register")]
        public void RegisterCommand<T1>(string commandGroup, string commandName, Action<T1> onExecute, string description = "No description.")
        {
            RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }

        [Obsolete("Please use MyCommands.Register")]
        public void RegisterCommand<T1, T2>(string commandGroup, string commandName, Action<T1, T2> onExecute, string description = "No description.")
        {
            RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }

        [Obsolete("Please use MyCommands.Register")]
        public void RegisterCommand<T1, T2, T3>(string commandGroup, string commandName, Action<T1, T2, T3> onExecute, string description = "No description.")
        {
            RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }

        [Obsolete("Please use MyCommands.Register")]
        public void RegisterCommand<T1, T2, T3, T4>(string commandGroup, string commandName, Action<T1, T2, T3, T4> onExecute, string description = "No description.")
        {
            RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }
        
        /// <summary>
        /// Registers command with specified name and execution method in given command group.
        /// </summary>
        /// <param name="commandGroup">The command group, easily unregister batch of commands by one call.
        /// Use this of eg.: level dependent commands, only 'when playing' command etc. </param>
        /// <param name="commandName">The command name.</param>
        /// <param name="onExecute">Called when command is being executed.</param>
        /// <param name="description">(optional)The command description.</param>
        public static void Register(string commandGroup, string commandName, Action onExecute, string description = "No description.")
        {
            Instance.RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }

        /// <summary>
        /// Registers command with specified name and execution method in given command group.
        /// </summary>
        /// <param name="commandGroup">The command group, easily unregister batch of commands by one call.
        /// Use this of eg.: level dependent commands, only 'when playing' command etc. </param>
        /// <param name="commandName">The command name.</param>
        /// <param name="onExecute">Called when command is being executed.</param>
        /// <param name="description">(optional)The command description.</param>
        public static void Register<T1>(string commandGroup, string commandName, Action<T1> onExecute, string description = "No description.")
        {
            Instance.RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }

        /// <summary>
        /// Registers command with specified name and execution method in given command group.
        /// </summary>
        /// <param name="commandGroup">The command group, easily unregister batch of commands by one call.
        /// Use this of eg.: level dependent commands, only 'when playing' command etc. </param>
        /// <param name="commandName">The command name.</param>
        /// <param name="onExecute">Called when command is being executed.</param>
        /// <param name="description">(optional)The command description.</param>
        public static void Register<T1, T2>(string commandGroup, string commandName, Action<T1, T2> onExecute, string description = "No description.")
        {
            Instance.RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }

        /// <summary>
        /// Registers command with specified name and execution method in given command group.
        /// </summary>
        /// <param name="commandGroup">The command group, easily unregister batch of commands by one call.
        /// Use this of eg.: level dependent commands, only 'when playing' command etc. </param>
        /// <param name="commandName">The command name.</param>
        /// <param name="onExecute">Called when command is being executed.</param>
        /// <param name="description">(optional)The command description.</param>
        public static void Register<T1, T2, T3>(string commandGroup, string commandName, Action<T1, T2, T3> onExecute, string description = "No description.")
        {
            Instance.RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }

        /// <summary>
        /// Registers command with specified name and execution method in given command group.
        /// </summary>
        /// <param name="commandGroup">The command group, easily unregister batch of commands by one call.
        /// Use this of eg.: level dependent commands, only 'when playing' command etc. </param>
        /// <param name="commandName">The command name.</param>
        /// <param name="onExecute">Called when command is being executed.</param>
        /// <param name="description">(optional)The command description.</param>
        public static void Register<T1, T2, T3, T4>(string commandGroup, string commandName, Action<T1, T2, T3, T4> onExecute, string description = "No description.")
        {
            Instance.RegisterCommandInternal(commandGroup, commandName, onExecute.Target, onExecute.Method, description);
        }
        
        // internal
        internal void RegisterCommandInternal(string commandGroup, string commandName, object instance, MethodInfo method, string description)
        {
            var parameters = method.GetParameters();

            // check commands, do not allow duplicates!
            var commands = _commands.Where(x => x.Name == commandName && x.Parameters.Length == parameters.Length).ToArray();
            if (commands.Length != 0)
            {
#if USE_MYLOGGER
                MyLogger.Add("Command with this name(" + commandName + ") and the same parameters count already exists.", MyLoggerLevel.Error);
#else
                UnityLog.Log("Command with this name(" + commandName + ") and the same parameters count already exists.", MyLoggerLevel.Error);
#endif
                return;
            }

            // register command
            _commands.Add(new Command
            {
                Name = commandName,
                Description = description,
                Group = commandGroup,
                Parameters = parameters,
                MethodTarget = instance,
                Method = method
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
            // check
            var commands = _commands.Where(x => x.Name == commandName).ToArray();

            if (commands.Length == 0)
            {
#if USE_MYLOGGER
                MyLogger.Add("'" + commandName + "' command not found.", MyLoggerLevel.Error);
#else
                UnityLog.Log("'" + commandName +"' command not found.", MyLoggerLevel.Error);
#endif
                return;
            }

            var found = false;
            var command = new Command();
            foreach (var cmd in commands)
            {
                if (cmd.Parameters.Length != parameters.Length)
                    continue;

                found = true;
                command = cmd;
                break;
            }

            if (!found)
            {
#if USE_MYLOGGER
                MyLogger.Add("'" + commandName + "' command exists, but invalid parameters were given." + parameters.Length, MyLoggerLevel.Error);
#else
                UnityLog.Log("'" + commandName + "' command exists, but invalid parameters were given." + parameters.Length, MyLoggerLevel.Error);
#endif
                return;
            }

            // parse
            var cmdParams = command.Parameters;
            var paramIndex = 0;
            var parseParams = new object[parameters.Length];

            foreach (var parameter in parameters)
            {
                var cmdParameter = cmdParams[paramIndex];

                switch (cmdParameter.ParameterType.Name.ToLower())
                {
                    case "string":
                        // string does not need any type check
                        parseParams[paramIndex] = parameter;
                        break;
                    case "int32":
                        int resultInt;
                        if (int.TryParse(parameter, out resultInt))
                        {
                            parseParams[paramIndex] = resultInt;
                        }
                        else
                        {
#if USE_MYLOGGER
                            MyLogger.Add("invalid parameter type were given for '" + parameter + "'", MyLoggerLevel.Error);
#else
                            UnityLog.Log("invalid parameter type were given for '" + parameter + "'", MyLoggerLevel.Error);
#endif
                            return;
                        }
                        break;
                    case "single":
                        float resultSingle;
                        if (float.TryParse(parameter, out resultSingle))
                        {
                            parseParams[paramIndex] = resultSingle;
                        }
                        else
                        {
#if USE_MYLOGGER
                            MyLogger.Add("invalid parameter type were given for '" + parameter + "'", MyLoggerLevel.Error);
#else
                            UnityLog.Log("invalid parameter type were given for '" + parameter + "'", MyLoggerLevel.Error);
#endif
                            return;
                        }
                        break;
                    case "double":
                        double resultDouble;
                        if (double.TryParse(parameter, out resultDouble))
                        {
                            parseParams[paramIndex] = resultDouble;
                        }
                        else
                        {
#if USE_MYLOGGER
                            MyLogger.Add("invalid parameter type were given for '" + parameter + "'", MyLoggerLevel.Error);
#else
                            UnityLog.Log("invalid parameter type were given for '" + parameter + "'", MyLoggerLevel.Error);
#endif
                            return;
                        }
                        break;
                    case "boolean":
                        bool resultBoolean;
                        if (bool.TryParse(parameter, out resultBoolean))
                        {
                            parseParams[paramIndex] = resultBoolean;
                        }
                        else
                        {
#if USE_MYLOGGER
                            MyLogger.Add("invalid parameter type were given for '" + parameter + "'", MyLoggerLevel.Error);
#else
                            UnityLog.Log("invalid parameter type were given for '" + parameter + "'", MyLoggerLevel.Error);
#endif
                            return;
                        }
                        break;
                    default:
#if USE_MYLOGGER
                        MyLogger.Add("command target method has invalid type in parameters!", MyLoggerLevel.Error);
#else
                        UnityLog.Log("command target method has invalid type in parameters!", MyLoggerLevel.Error);
#endif
                        return;
                }
                
                paramIndex++;
            }
            
            // execute!
            command.Method.Invoke(command.MethodTarget, parseParams);
        }

        /// <summary>
        /// Executes raw command, parse arguments etc.
        /// </summary>
        /// <param name="command">The command string, eg.: 'volume master 0.2' or 'volume "master" 0.2'</param>
        public void ExecuteRaw(string command)
        {
            // parse
            string name;
            var parameters = ParseCommand(command, out name);
            
            // execute
            Execute(name, parameters.ToArray());
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

            // create instance
            Instance = new MyCommands();
        }

        /// <summary>
        /// Parse command to get name and parameters.
        /// Strings are supported! eg.: 'test "Hello, World!"'
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<string> ParseCommand(string command, out string name)
        {
            // trim
            command = command.Trim();

            name = "";

            var parameters = new List<string>();
            var parameter = "";
            var stringRead = false;

            foreach (var ch in command)
            {
                if (ch == ' ' && !stringRead)
                {
                    // next param

                    if (!string.IsNullOrEmpty(parameter))
                        parameters.Add(parameter);

                    parameter = string.Empty;
                }
                else if (ch == '"')
                {
                    // start or stop string param   
                    if (stringRead)
                    {
                        stringRead = false;
                        continue;
                    }

                    stringRead = true;
                }
                else
                {
                    // add to current.
                    parameter += ch;
                }
            }

            // add last parameter
            if (!string.IsNullOrEmpty(parameter))
            {
                parameters.Add(parameter);
            }

            // set name
            if (string.IsNullOrEmpty(name) && parameters.Count > 0)
            {
                name = parameters[0];
                parameters.RemoveAt(0);
            }
            
            return parameters;
        }

        /// <summary>
        /// The MyCommand current instance.
        /// </summary>
        public static MyCommands Instance { get; private set; }
    }
}
