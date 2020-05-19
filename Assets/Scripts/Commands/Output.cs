using UnityEngine;

using Player;
using Interactables;

namespace Command
{
    public class Output: ICommand
    {
        public string name {get; set;}
        public int[] parameters {get; set;}
        public PlayerController player {get; set;}

        private OutputQueue outputQueue;

        public Output()
        {
            this.name = "Output";
            this.player = GameObject.Find("Player").GetComponent<PlayerController>();
            this.outputQueue = GameObject.Find("OutputQueue").GetComponent<OutputQueue>();
        }

        bool ICommand.Run()
        {
            Debug.Log("Moving to target");
            player.MoveTo("OutLocation");

            Debug.Log("Running Output function...");
           // this.player.interactable = this.outputQueue;
           // this.player.Put();

            return true;
        }
    }
}