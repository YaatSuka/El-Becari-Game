using UnityEngine;

namespace Interactable
{
    public class Slot: Interactable
    {
        public string name {get; set;}
        public Vector2 position {get; set;}

        private Value slot;

        public Slot(Vector2 position, Value value)
        {
            this.name = "Slot";
            this.slot = value;
            this.position = position;
        }

        public bool Put(Value value)
        {
            this.slot = value;

            return true;
        }

        public Value Take()
        {
            return this.slot;
        }
    }
}