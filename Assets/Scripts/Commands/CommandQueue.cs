using UnityEngine;
using System.Collections.Generic;

namespace Command
{
    public class CommandQueue
    {
        private Dictionary<int, ICommand> commands = new Dictionary<int, ICommand>();

        public bool Insert(int key, ICommand command)
        {
            if (!this.commands.ContainsKey(key)) {
                this.commands.Add(key, command);
                return true;
            }

            for (int idx = this.commands.Count - 1; idx >= key; idx--) {
                ICommand tmp = this.commands[idx];
                this.commands.Remove(idx);
                commands[idx + 1] = tmp;
            }
            this.commands.Add(key, command);

            return true;
        }

        public bool Move(int lastKey, int newKey)
        {
            ICommand commandToMove = this.commands[lastKey];

            if (newKey > lastKey) {
                for (int idx = lastKey; idx < newKey && this.commands.ContainsKey(idx + 1); idx++) {
                    this.commands[idx] = this.commands[idx + 1];
                }
                this.commands[newKey] = commandToMove;
            }
            if (newKey < lastKey) {
                for (int idx = lastKey; idx > newKey; idx--) {
                    this.commands[idx] = this.commands[idx - 1];
                }
                this.commands[newKey] = commandToMove;
            }

            return true;
        }

        public void Undo()
        {
            // Undo
        }

        public bool Run()
        {
            for (int i = 0; i < this.commands.Count; i++) {
                if (!this.commands[i].Run()) {
                    return false;
                }
            }

            return true;
        }

        // Log commands in queue
        public void Log()
        {
            Debug.Log("----- DEBUG CommandQueue -----");
            for (int i = 0; i < this.commands.Count; i++) {
                Debug.Log("[" + i + "] => " + this.commands[i]);
            }
            Debug.Log("------------------------------");
        }
    }
}