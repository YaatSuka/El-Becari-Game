using UnityEngine;

namespace Interactables
{
    public interface Interactable
    {
        string name {get; set;}
        Vector2 position {get; set;}

        Transform Take();
        bool Put(Transform box);
    }
}