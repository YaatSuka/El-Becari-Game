using UnityEngine;

namespace Interactable
{
    interface Interactable
    {
        string name {get; set;}
        Vector2 position {get; set;}

        Value Take();
        bool Put(Value box);
    }
}