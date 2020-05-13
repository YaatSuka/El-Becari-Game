using UnityEngine;

namespace Interactables
{
    public class Slot: Interactable
    {
        public string name {get; set;}
        public Vector2 position {get; set;}
        public int uid;

        private Transform slot;

        public Slot(Vector2 position, int uid, Transform box)
        {
            this.name = "Slot";
            this.uid = uid;
            this.slot = box;
            this.position = position;
        }

        public bool Put(Transform box)
        {
            this.slot = box;

            return true;
        }

        public Transform Take()
        {
            return this.slot;
        }
    }
}