using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;

namespace Command
{
    public interface ICommand
    {
        string name {get; set;}
        List<int> parameters {get; set;}
        PlayerController player {get; set;}

        bool Run(CommandQueue callback);
        void AddParameter(int parameter);
    }
}