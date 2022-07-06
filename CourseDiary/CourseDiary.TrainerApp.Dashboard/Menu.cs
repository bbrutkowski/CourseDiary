using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseDiary.TrainerApp.Dashboard
{
    public class Menu
    {
        private Dictionary<int, MenuItem> _commands = new Dictionary<int, MenuItem>();

        public void Register(int commandId, MenuItem menuItem)
        {
            if (_commands.ContainsKey(commandId))
            {
                throw new ArgumentException($"Command is already registered");
            }

            _commands[commandId] = menuItem;
        }

        public void ExecuteCommand(int commandId)
        {
            if (!_commands.ContainsKey(commandId))
            {
                throw new ArgumentException($"Command with Id {commandId} doesn't exist!");
            }

            _commands[commandId].Action();
        }

        public void PrintAllCommands()
        {
            Console.WriteLine("Select action:");
            foreach (KeyValuePair<int, MenuItem> command in _commands)
            {
                Console.WriteLine($"#{command.Key}: {command.Value.Name} - {command.Value.Description}");
            }
        }
    }
}
