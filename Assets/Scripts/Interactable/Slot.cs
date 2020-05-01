using UnityEngine;

namespace Interactable
{
    public class Slot: Interactable
    {
        public string name {get; set;}
        public Vector2 position {get; set;}
        public int uid;

        private Value slot;

        public Slot(Vector2 position, int uid, Value value)
        {
            this.name = "Slot";
            this.uid = uid;
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
            Value value = this.slot;

            this.slot = null;

            return value;
        }
    }
}