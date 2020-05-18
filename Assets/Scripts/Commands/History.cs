using UnityEngine;
using System.Collections.Generic;

namespace Command
{
    public class History
    {
        private SortedList<int, ICommand[]> states = new SortedList<int, ICommand[]>();
        private int currentState = 0;
        private CommandFactory factory = new CommandFactory();

        public History(Transform[] state)
        {
            this.AddState(state);
        }

        public ICommand[] Undo()
        {
            if (currentState == 0) {
                return null;
            }

            this.states.Remove(this.currentState);
            this.currentState--;

            return this.states.Values[this.currentState];
        }

        public void Save(Transform[] state)
        {
            this.currentState++;
            this.AddState(state);
        }

        private void AddState(Transform[] state)
        {
            ICommand[] tmp = new ICommand[state.Length];
            int idx = 0;

            foreach(Transform box in state) {
                if (box.childCount > 0) {
                    tmp[idx] = this.factory.Build(box.GetChild(0).GetComponent<CommandComponent>().command.name);
                } else {
                    tmp[idx] = null;
                }
                idx++;
            }

            this.states.Add(this.currentState, tmp);
        }

        public void Log()
        {
            Debug.Log("----- DEBUG History -----");
            for (int i = 0; i < this.states.Count; i++) {
                Debug.Log("[" + i + "]");
                ICommand[] tmp = this.states[i];
                for (int j = 0; j < tmp.Length; j++) {
                    if (tmp[j] != null) {
                        Debug.Log("\t[" + j + "] => " + tmp[j].name);
                    } else {
                        Debug.Log("\t[" + j + "] => null");
                    }
                }
            }
            Debug.Log("-------------------------");
        }
    }
}