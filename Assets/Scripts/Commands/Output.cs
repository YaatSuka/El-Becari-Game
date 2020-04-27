using UnityEngine;

using Player;

namespace Command
{
    public class Output: ICommand
    {
        public string name {get; set;}
        public int[] parameters {get; set;}
        public PlayerController player {get; set;}

        public Output()
        {
            name = "Output";
        }

        bool ICommand.Run()
        {
            Debug.Log("Running Output function...");

            return true;
        }
    }
}