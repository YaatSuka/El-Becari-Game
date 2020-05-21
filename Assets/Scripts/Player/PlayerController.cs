using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Interactables;
using Command;

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
        private List<string> path;

        public delegate void ICommand();
        protected List<ICommand> callbacks;
        
        private InputQueue inputQueue;
        private OutputQueue outputQueue;


        void Start()
        {
            
            this.inputQueue = GameObject.Find("InputQueue").GetComponent<InputQueue>();
            this.outputQueue = GameObject.Find("OutputQueue").GetComponent<OutputQueue>();            
            this.startPosition = transform.position;
            this.newLocation = new Dictionary<string, Vector3>();
            this.anim = GetComponent<Animator>();
            this.path = new List<string>();
            this.callbacks = new List<ICommand>();

            FillLocation();
        }

        private void FixedUpdate()
        {
            if (path.Count == 0 || path.Count == path_idx) { return; }

            if (path[path_idx] != "" && newLocation.ContainsKey(path[path_idx])) {
                if (transform.position.x <= newLocation["OutLocationAngle"].x - 0.2 && newLocation[path[path_idx]].x >= -0.6 ||
                    transform.position.y <= newLocation["OutLocationAngle"].y - 0.2 && newLocation[path[path_idx]].y >= 0) {
                    transform.position = Vector3.MoveTowards(transform.position, newLocation["OutLocationAngle"], transitionSpeed * Time.deltaTime);
                } else {
                    transform.position = Vector3.MoveTowards(transform.position, newLocation[path[path_idx]], transitionSpeed * Time.deltaTime);
                }

                if (this.box) {
                    this.box.position = new Vector2(transform.position.x, transform.position.y - 0.4f);
                }
                
                if (transform.position != newLocation[path[path_idx]]) {
                    anim.SetInteger("State", 1);
                } else {
                    anim.SetInteger("State", 0);
                    callbacks[path_idx]();
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
            this.box = this.interactable.Take();
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
            this.interactable.Put(this.box);
            this.box = null;
        }

        public void Reset()
        {
            transform.position = this.startPosition;
            this.interactable = null;
            this.path.Clear();
            this.callbacks.Clear();
            if (this.box != null) {
                Destroy(this.box.gameObject);
                this.box = null;
            }
        }

        public void MoveTo(string location, ICommand callback)
        {
            FillLocation();
            path.Add(location);
            callbacks.Add(callback);
        }

        private void FillLocation()
        {
            if (newLocation.Count != 0) {
                newLocation.Clear();
                path_idx = 0;
            }

            foreach(GameObject location in GameObject.FindGameObjectsWithTag("location")) {
                newLocation.Add(location.name, location.transform.position);
            }
        }
    }
}