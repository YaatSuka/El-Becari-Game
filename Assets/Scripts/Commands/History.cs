using UnityEngine;
using System.Collections.Generic;

namespace Command
{
    public class History
    {
        private SortedList<int, Dictionary<int, ICommand>> states = new SortedList<int, Dictionary<int, ICommand>>();
        private int currentState = 0;

        public History()
        {
            this.states.Add(this.currentState, new Dictionary<int, ICommand>());
        }

        public Dictionary<int, ICommand> Undo()
        {
            if (currentState == 0) {
                return null;
            }

            this.states.Remove(this.currentState);
            this.currentState--;

            Dictionary<int, ICommand> tmp = this.states[this.currentState - 1];

            return this.states.Values[this.currentState];
        }

        public void Save(Dictionary<int, ICommand> state)
        {
            this.currentState++;
            this.states.Add(this.currentState, new Dictionary<int, ICommand>(state));
        }

        public void Log()
        {
            Debug.Log("----- DEBUG History -----");
            for (int i = 0; i < this.states.Count; i++) {
                Debug.Log("[" + i + "]");
                Dictionary<int, ICommand> tmp = this.states[i];
                for (int j = 0; j < tmp.Count; j++) {
                    Debug.Log("\t[" + j + "] => " + tmp[j]);
                }
            }
            Debug.Log("-------------------------");
        }
    }
}