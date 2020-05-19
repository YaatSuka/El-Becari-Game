using UnityEngine;

using Player;
using Interactables;

namespace Command
{
    public class Input: ICommand
    {
        public string name {get; set;}
        public int[] parameters {get; set;}
        public PlayerController player {get; set;}
        
        private InputQueue inputQueue;

        public Input()
        {
            this.name = "Input";
            this.player = GameObject.Find("Player").GetComponent<PlayerController>();
            this.inputQueue = GameObject.Find("InputQueue").GetComponent<InputQueue>();
        }

        bool ICommand.Run()
        {
            Debug.Log("Moving to target");
            player.MoveTo("BoxLocation");

            Debug.Log("Running Input function...");
           // this.player.interactable = this.inputQueue;
           // this.player.Take();

            return true;
        }
    }
}