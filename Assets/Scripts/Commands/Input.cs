using UnityEngine;

using Player;

namespace Command
{
    public class Input: ICommand
    {
        public string name {get; set;}
        public int[] parameters {get; set;}
        public PlayerController player {get; set;}

        public Input()
        {
            name = "Input";
        }

        bool ICommand.Run()
        {
            Debug.Log("Running Input function...");

            return true;
        }
    }
}