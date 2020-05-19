using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Interactables;

namespace Player
{
    public class PlayerController: MonoBehaviour
    {
        public Interactable interactable = null;

        public Transform box = null;

        private Vector3 startPosition;
        
        
        public float transitionSpeed = 2f; 
        private int path_idx = 0;

        private Dictionary<string, Vector3> newLocation;
        private Animator anim;
        private List<string> Path;


        void Start()
        {
            this.startPosition = transform.position;
            newLocation = new Dictionary<string, Vector3>();
            anim = GetComponent<Animator>();
            Path = new List<string>();
            fillLocation();
        }

        private void FixedUpdate() {
             if (Path.Count == 0 || Path.Count == path_idx)
                return;
            if (Path[path_idx] != "" && newLocation.ContainsKey(Path[path_idx]))
            {
                if (transform.position.x <= newLocation["OutLocationAngle"].x - 0.2 && newLocation[Path[path_idx]].x >= -0.6 ||
                transform.position.y <= newLocation["OutLocationAngle"].y - 0.2 && newLocation[Path[path_idx]].y >= 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, newLocation["OutLocationAngle"], transitionSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, newLocation[Path[path_idx]], transitionSpeed * Time.deltaTime);
                }
                
                if (transform.position != newLocation[Path[path_idx]])
                    anim.SetInteger("State", 1);
                else{
                    anim.SetInteger("State", 0);
                    path_idx++;
                }
            } 
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

            MoveTo("BoxLocation");
            this.box = this.interactable.Take();
            this.box.position = new Vector2(transform.position.x, transform.position.y - 0.4f);
        }

        public void Put()
        {
            if (box == null) {
                Debug.LogError("The character doesn't carry a box");
                return;
            }
            if (this.interactable == null) {
                Debug.LogError("You didn't set an Interactable object");
                return;
            }

            MoveTo("OutLocation");
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

        private void MoveTo(string Location)
        {
            Path.Add(Location);
        }

        private void fillLocation()
        {
            foreach(GameObject Location in GameObject.FindGameObjectsWithTag("location"))
                newLocation.Add(Location.name, Location.transform.position);
        }
    }
}