using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBGCore
{
    /// <summary>
    /// This is a factory to generate and register commands for handling through SBGCore
    /// </summary>
    /// <example>Example implementation:
    /// var cmd = new CommandFactory();
    /// cmd.Register("CommandName", someFunction);
    /// 
    /// Please note that the function name is inserted without the brackets afterwards!
    /// </example>
    public class CommandFactory
    {
        /// <summary>
        /// This holds a list of commands and their respective void Function
        /// </summary>
        private readonly IDictionary<string, Action> _commands = new Dictionary<string, Action>();

        /// <summary>
        /// Registers a command and its void Function into the <seealso cref="_commands"/> Dictionary
        /// </summary>
        /// <param name="commandName">String containing the name of the action</param>
        /// <param name="action">This holds an actual void Function to call</param>
        public void RegisterCommand(string commandName, Action action)
        {
            _commands.Add(commandName, action);
        }

        /// <summary>
        /// This retrieves the command by its name
        /// </summary>
        /// <param name="commandName">The name of the function</param>
        /// <returns>Action member: eg.: void SomethingSomethingDarkside();</returns>
        private Action GetCommand(string commandName)
        {
            return _commands[commandName];
        }
        /// <summary>
        /// This retrieves a command and thus executes it.
        /// </summary>
        /// <param name="commandName">The string describing the void Function</param>
        public void ExecuteCommand(string commandName)
        {
            GetCommand(commandName)();
        }
    }
}
