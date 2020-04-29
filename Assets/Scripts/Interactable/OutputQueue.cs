using UnityEngine;

namespace Interactable
{
    public class OutputQueue: Interactable
    {
        public string name {get; set;}
        public Vector2 position {get; set;}
        public int length = 0;

        private Value[] values;

        public OutputQueue()
        {
            this.name = "OutputQueue";
            this.position = new Vector2(0, 0); // TO CHANGE
        }

        public OutputQueue(int length)
        {
            int idx = 0;
            this.length = length;
            this.values = new Value[length];
            
            while (idx < length) {
                this.values[idx] = null;
                idx++;
            }
        }

        public bool Put(Value value)
        {
            return Push(value);
        }

        public Value Take()
        {
            Debug.LogError("Impossible to pop from OutputQueue");

            return null;
        }

        public Value[] GetValues()
        {
            return this.values;
        }

        public bool Push(Value value)
        {
            int idx = 0;

            while (idx < length && this.values[idx] != null) { idx++; }
            if (idx >= length) {
                Debug.LogError("OutputQueue is full");

                return false;
            }

            this.values[idx] = value;

            return true;
        }

        // Display values content
        public void Log()
        {
            Debug.Log("----- DEBUG OutputQueue -----");
            foreach(Value value in this.values) {
                if (value != null) {
                    Debug.Log(value.uid + " => " + value.value);
                } else {
                    Debug.Log("null");
                }
            }
            Debug.Log("----------------------------");
        }
    }
}