using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;
using Interactables;

namespace Command
{
    public class Input: ICommand
    {
        public string name {get; set;}
        public List<int> parameters {get; set;}
        public PlayerController player {get; set;}

        protected CommandQueue callback;
        
        private InputQueue inputQueue;

        public Input()
        {
            this.name = "Input";
            this.player = GameObject.Find("Player").GetComponent<PlayerController>();
            this.inputQueue = GameObject.Find("InputQueue").GetComponent<InputQueue>();
        }

        bool ICommand.Run(CommandQueue callback)
        {
            this.callback = callback;

            player.MoveTo("BoxLocation", Interact);

            return true;
        }

        public void Interact()
        {
            this.player.interactable = this.inputQueue;
            this.player.Take();

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