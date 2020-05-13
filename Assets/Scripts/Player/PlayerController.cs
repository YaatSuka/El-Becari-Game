using UnityEngine;

using Interactables;

namespace Player
{
    public class PlayerController: MonoBehaviour
    {
        public Interactable interactable = null;

        public Transform box = null;

        private Vector3 startPosition;

        void Start()
        {
            this.startPosition = transform.position;
        }

        public void Take()
        {
            if (this.box != null) {
                Debug.LogError("The character carry already something");
                return;
            }
            if (this.interactable == null) {
                Debug.LogError("You didn't set an Interactable object");
                return;
            }

            this.box = this.interactable.Take();
            this.box.position = new Vector2(transform.position.x, transform.position.y - 0.4f);
        }

        public void Put()
        {
            if (box == null) {
                Debug.LogError("The chanracter doesn't carry a box");
                return;
            }
            if (this.interactable == null) {
                Debug.LogError("You didn't set an Interactable object");
                return;
            }

            this.interactable.Put(this.box);
            this.box = null;
        }

        public void Reset()
        {
            transform.position = this.startPosition;
            this.interactable = null;
            if (this.box != null) {
                Destroy(this.box.gameObject);
                this.box = null;
            }
        }
    }
}