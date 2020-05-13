using UnityEngine;

namespace Interactables
{
    public class OutputQueue: MonoBehaviour, Interactable
    {
        public new string name {get; set;}
        public Vector2 position {get; set;}
        public int length = 0;
        public Vector2 startPosition;
        public float gap;

        private Transform[] boxes;

        void Start()
        {
            this.name = "OutputQueue";
            this.position = new Vector2(0, 0);
        }

        public void Init(int[] input)
        {
            this.length = input.Length;
            this.boxes = new Transform[this.length];
        }

        public bool Put(Transform box)
        {
            if (this.GetNbBoxes() >= this.length) {
                Debug.LogError("OutputQueue is full");
                return false;
            }
            for (int i = this.length - 1; i > 0; i--) {
                if (this.boxes[i - 1] != null) {
                    this.boxes[i] = this.boxes[i - 1];
                    this.boxes[i - 1] = null;
                    this.boxes[i].position -= new Vector3(0, this.gap, 0);
                }
            }

            this.boxes[0] = box;
            box.position = this.startPosition;

            return true;

        }

        public Transform Take()
        {
            Debug.LogError("Impossible to pop from OutputQueue");

            return null;
        }

        public void Reset()
        {
            for (int i = 0; i < this.length; i++) {
                if (this.boxes[i] != null) {
                    Destroy(this.boxes[i].gameObject);
                    this.boxes[i] = null;
                }
            }
        }

        public int GetNbBoxes()
        {
            int i = 0;

            foreach (Transform box in this.boxes) {
                if (box != null) {
                    i++;
                }
            }
            
            return i;
        }

        public int[] ExtractOutput()
        {
            int[] output = new int[this.GetNbBoxes()];
            int idx = 0;

            foreach (Transform box in this.boxes) {
                if (box != null) {
                    output[idx] = box.GetComponent<ValueComponent>().value.value;
                    idx++;
                }
            }

            return output;
        }

        // Display values content
        public void Log()
        {
            int idx = 0;

            Debug.Log("----- DEBUG OutputQueue -----");
            foreach(Transform box in this.boxes) {
                if (box != null) {
                    ValueComponent component = box.GetComponent<ValueComponent>();
                    Debug.Log(idx + " => " + component.value.value);
                } else {
                    Debug.Log("null");
                }
                idx++;
            }
            Debug.Log("----------------------------");
        }
    }
}