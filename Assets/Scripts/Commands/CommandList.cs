using UnityEngine;

namespace Command
{
    public class CommandList
    {
        public ICommand[] commands;

        public CommandList(ICommand[] commands)
        {
            this.commands = commands;
        }

        public ICommand Get(int index)
        {
            if (index >= commands.Length || index < 0) {
                Debug.LogError("Invalid index or null command");
                return null;
            }

            return commands[index];
        }

        // Display commands names
        public void Log()
        {
            Debug.Log("----- DEBUG CommandList -----");
            foreach(ICommand command in commands) {
                Debug.Log(command.name);
            }
            Debug.Log("-----------------------------");
        }
    }
}