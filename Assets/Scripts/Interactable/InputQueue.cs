using UnityEngine;

namespace Interactable
{
    public class InputQueue: Interactable
    {
        public string name {get; set;}
        public Vector2 position {get; set;}
        public int length = 0;

        private Value[] values;

        public InputQueue()
        {
            this.name = "InputQueue";
            this.position = new Vector2(0, 0); // TO CHANGE
        }

        public bool Put(Value value)
        {
            Debug.LogError("Impossible to push in InputQueue");

            return false;
        }

        public Value Take()
        {
            return Pop();
        }

        public void Fill(int[] values)
        {
            int idx = 0;

            this.length = values.Length;
            this.values = new Value[length];

            foreach (int value in values) {
                this.values[idx] = new Value(idx, value);
                idx++;
            }
        }

        public Value Pop()
        {
            Value value = null;
            int idx = 0;

            while (idx < this.length && this.values[idx] == null) { idx++; }
            if (idx >= length) {
                Debug.LogError("InputQueue is empty");
                return null;
            }
            
            value = this.values[idx];
            this.values[idx] = null;

            return value;
        }

        // Display values content
        public void Log()
        {
            Debug.Log("----- DEBUG InputQueue -----");
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