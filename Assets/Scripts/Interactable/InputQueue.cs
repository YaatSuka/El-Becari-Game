using UnityEngine;

namespace Interactables
{
    public class InputQueue: MonoBehaviour, Interactable
    {
        public new string name {get; set;}
        public Vector2 position {get; set;}
        public int length = 0;
        public Transform[] prefabs;
        public Vector2 startPosition;
        public float gap;

        private Transform[] boxes;
        private int[] values;

        void Start()
        {
            this.name = "InputQueue";
            this.position = gameObject.transform.position;
        }

        public bool Put(Transform box)
        {
            Debug.LogError("Impossible to push in InputQueue");

            return false;
        }

        public Transform Take()
        {
            Transform box = null;
            
            for (int i = 0; i < this.length; i++) {
                if (box == null && this.boxes[i] != null) {
                    box = this.boxes[i];
                    this.boxes[i] = null;
                }
                if (box != null && this.boxes[i] != null) {
                    this.boxes[i].position += new Vector3(0, this.gap, 0);
                }

            }
            
            if (box == null) {
                Debug.LogError("InputQueue is empty");
            }
            return box;
        }

        public void Fill(int[] values)
        {
            int idx = 0;
            Vector2 position = this.startPosition;

            this.length = values.Length;
            this.boxes = new Transform[this.length];
            this.values = values;

            foreach (int value in values) {
                Transform obj = Instantiate(this.prefabs[0], position, this.transform.rotation);

                ValueComponent component = obj.gameObject.AddComponent<ValueComponent>();
                component.value = new Value(idx, value);
                obj.transform.GetChild(0).GetComponent<TextMesh>().text = component.value.value.ToString();
                this.boxes[idx] = obj;

                idx++;
                position.y -= this.gap;
            }
        }

        public void Reset()
        {
            for (int i = 0; i < this.length; i++) {
                if (this.boxes[i] != null) {
                    Destroy(this.boxes[i].gameObject);
                    this.boxes[i] = null;
                }
            }
            this.boxes = null;
            
            this.Fill(this.values);
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

        // Display values content
        public void Log()
        {
            Debug.Log("----- DEBUG InputQueue -----");
            foreach(Transform box in this.boxes) {
                if (box != null) {
                    ValueComponent component = box.GetComponent<ValueComponent>();
                    Debug.Log(component.value.uid + " => " + component.value.value);
                } else {
                    Debug.Log("null");
                }
            }
            Debug.Log("----------------------------");
        }
    }
}