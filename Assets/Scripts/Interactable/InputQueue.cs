using UnityEngine;

namespace Interactable
{
    public class InputQueue: MonoBehaviour, Interactable
    {
        public new string name {get; set;}
        public Vector2 position {get; set;}
        public int length = 0;
        public Transform[] prefabs;
        public Vector2 startPosition;
        public float gap;

        private Value[] values;
        private Transform[] boxes;

        void Start()
        {
            this.name = "InputQueue";
            this.position = gameObject.transform.position;
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
            this.boxes = new Transform[this.length];
            this.values = new Value[length];

            foreach (int value in values) {
                this.values[idx] = new Value(idx, value);
                idx++;
            }

            this.AddBoxes();
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

        private void AddBoxes()
        {
            Vector2 position = this.startPosition;
            int idx = 0;

            foreach (Value value in this.values) {
                Transform obj = Instantiate(this.prefabs[0], position, this.transform.rotation);

                obj.transform.GetChild(0).GetComponent<TextMesh>().text = value.value.ToString();
                ValueComponent component = obj.gameObject.AddComponent<ValueComponent>();
                component.value = value;
                this.boxes[idx] = obj;

                idx++;
                position.y -= this.gap;
            }
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