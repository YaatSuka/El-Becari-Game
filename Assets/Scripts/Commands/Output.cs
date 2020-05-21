using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;
using Interactables;

namespace Command
{
    public class Output: ICommand
    {
        public string name {get; set;}
        public List<int> parameters {get; set;}
        public PlayerController player {get; set;}

        protected CommandQueue callback;

        private OutputQueue outputQueue;

        public Output()
        {
            this.name = "Output";
            this.player = GameObject.Find("Player").GetComponent<PlayerController>();
            this.outputQueue = GameObject.Find("OutputQueue").GetComponent<OutputQueue>();
        }

        bool ICommand.Run(CommandQueue callback)
        {
            this.callback = callback;

            player.MoveTo("OutLocation", Interact);

            return true;
        }

        void Interact()
        {
            this.player.interactable = this.outputQueue;
            this.player.Put();

            if (this.callback != null) {
                this.callback.CheckOutput();
            }
        }

        void ICommand.AddParameter(int parameter)
        {
            this.parameters.Add(parameter);
        }
    }
}