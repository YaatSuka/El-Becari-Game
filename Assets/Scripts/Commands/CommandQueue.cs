using UnityEngine;
using System.Collections.Generic;

using Player;

namespace Command
{
    public class CommandQueue: MonoBehaviour
    {
        public int maxLength = 6;
        public Transform prefab;
        public float gap = 46f;

        public delegate void GameController();
        protected GameController callback;

        private History history;
        private CommandList commandList;
        private Transform[] boxes;

        public void Init()
        {
            this.boxes = new Transform[this.maxLength];
            this.AddTokenBoxes();
            this.history = new History(this.boxes);
            this.commandList = GameObject.Find("CommandList").GetComponent<CommandList>();
        }

        public void UpdateQueue(GameObject token, GameObject target)
        {
            if (token.transform.parent.name == "CommandList") {
                InsertToken(token, target);
            } else if (token.transform.parent.parent.name == "CommandQueue") {
                SwitchToken(token.transform.parent.gameObject, target);
            }

            this.history.Save(this.boxes);
        }

        public void SwitchToken(GameObject source, GameObject target)
        {
            GameObject token = target.GetComponent<DropHandler>().token;
            DropHandler targetDropHandler = target.GetComponent<DropHandler>();
            DropHandler sourceDropHandler = source.GetComponent<DropHandler>();

            targetDropHandler.Drop(sourceDropHandler.token);
            sourceDropHandler.Drop(token);
        }

        public void InsertToken(GameObject token, GameObject target)
        {
            DropHandler dropHandler = target.GetComponent<DropHandler>();

            if (dropHandler.token == null) {
                foreach(Transform box in this.boxes) {
                    if (box.GetComponent<DropHandler>().token == null) {
                        box.GetComponent<DropHandler>().DropNew(token);
                        break;
                    }
                }
            } else {
                for (int i = this.maxLength - 1; i > dropHandler.id; i--) {
                    GameObject boxToken = this.boxes[i - 1].GetComponent<DropHandler>().token;
                    if (boxToken != null) {
                        this.boxes[i].GetComponent<DropHandler>().Drop(boxToken);
                    }
                }
                target.GetComponent<DropHandler>().DropNew(token);
            }
        }

        public bool Undo()
        {
            ICommand[] lastState = this.history.Undo();
            int i = 0;

            this.Reset();
            foreach (ICommand command in lastState) {
                if (command != null) {
                    GameObject token = this.commandList.Get(command.name);
                    GameObject target = this.GetBoxById(i);
                    this.InsertToken(token, target);
                }
                i++;
            }

            return true;
        }

        public bool Redo()
        {
            ICommand[] nextState = this.history.Redo();
            int i = 0;

            this.Reset();
            foreach (ICommand command in nextState) {
                if (command != null) {
                    GameObject token = this.commandList.Get(command.name);
                    GameObject target = this.GetBoxById(i);
                    this.InsertToken(token, target);
                }
                i++;
            }

            return true;
        }

        public void Reset()
        {
            foreach (Transform box in this.boxes) {
                if (box.childCount > 0) {
                    Destroy(box.GetChild(0).gameObject);
                }
            }
        }

        private GameObject GetBoxById(int id)
        {
            foreach (Transform box in this.boxes) {
                if (box.GetComponent<DropHandler>().id == id) {
                    return box.gameObject;
                }
            }

            return null;
        }

        public bool Run(GameController callback)
        {
            int i = 0;
            this.callback = callback;

            foreach (Transform box in this.boxes) {
                if (box.GetComponent<DropHandler>().token != null) {
                    ICommand command = box.GetComponent<DropHandler>().token.GetComponent<CommandComponent>().command;

                    //Debug.Log(i + " / " + GetLastIndex());
                    if (i == GetLastIndex()) {
                        command.Run(this);
                    } else {
                        command.Run(null);
                    }
                    i++;
                }
            }

            return true;
        }

        private int GetLastIndex()
        {
            int i = 0;
            int index = -1;

            foreach (Transform box in this.boxes) {
                if (box.GetComponent<DropHandler>().token != null) {
                    index = i;
                }
                i++;
            }

            return index;
        }

        public void CheckOutput()
        {
            this.callback();
        }

        private void AddTokenBoxes()
        {
            Vector2 position = new Vector2(0, 0);

            for (int i = 0; i < maxLength; i++) {
                Transform obj = Instantiate(this.prefab, position, this.transform.rotation);

                obj.SetParent(gameObject.transform);
                obj.localPosition = position;
                obj.localScale = new Vector2(0.8f, 0.8f);
                obj.GetComponent<DropHandler>().id = i;

                this.boxes[i] = obj;

                position.y -= this.gap;
            }
        }

        // Log commands in queue
        public void Log()
        {
            Debug.Log("----- DEBUG CommandQueue -----");
            for (int i = 0; i < this.maxLength - 1; i++) {
                if (this.boxes[i] != null && this.boxes[i].childCount > 0) {
                    Transform token = this.boxes[i].GetChild(0);

                    if (token != null) {
                        Debug.Log("[" + i + "] => " + token.GetComponent<CommandComponent>().command.name);
                    } else {
                        Debug.Log("NULL");
                    }
                }
            }
            Debug.Log("------------------------------");
        }
    }
}