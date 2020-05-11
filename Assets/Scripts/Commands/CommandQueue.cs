using UnityEngine;
using System.Collections.Generic;

namespace Command
{
    public class CommandQueue: MonoBehaviour
    {
        public int maxLength = 4;
        public Transform prefab;
        public float gap = 55f;

        private Dictionary<int, ICommand> commands = new Dictionary<int, ICommand>();
        // private History history = new History();
        private Transform[] boxes;

        public void Init()
        {
            this.boxes = new Transform[this.maxLength];
            this.AddTokenBoxes();
        }

        public void UpdateQueue(GameObject token, GameObject target)
        {
            if (token.transform.parent.name == "CommandList") {
                if (isFull()) {
                    Debug.LogError("Impossible to add a command: CommandQueue is full");
                }

                InsertToken(token, target);
            } else if (token.transform.parent.parent.name == "CommandQueue") {
                SwitchToken(token.transform.parent.gameObject, target);
            }
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
                    Debug.Log(this.boxes[i].GetComponent<DropHandler>().id);
                    if (boxToken != null) {
                        Debug.Log("Drop");
                        this.boxes[i].GetComponent<DropHandler>().Drop(boxToken);
                    }
                }
                target.GetComponent<DropHandler>().DropNew(token);
            }
        }

        public bool Insert(int key, ICommand command)
        {
            if (!this.commands.ContainsKey(key)) {
                this.commands.Add(key, command);
                //this.history.Save(this.commands);
                return true;
            }

            for (int idx = this.commands.Count - 1; idx >= key; idx--) {
                ICommand tmp = this.commands[idx];
                this.commands.Remove(idx);
                commands[idx + 1] = tmp;
            }
            this.commands.Add(key, command);

            //this.history.Save(this.commands);

            return true;
        }

        public bool Move(int lastKey, int newKey)
        {
            ICommand commandToMove = this.commands[lastKey];

            if (newKey > lastKey) {
                for (int idx = lastKey; idx < newKey && this.commands.ContainsKey(idx + 1); idx++) {
                    this.commands[idx] = this.commands[idx + 1];
                }
                this.commands[newKey] = commandToMove;
            }
            if (newKey < lastKey) {
                for (int idx = lastKey; idx > newKey; idx--) {
                    this.commands[idx] = this.commands[idx - 1];
                }
                this.commands[newKey] = commandToMove;
            }

            //this.history.Save(this.commands);

            return true;
        }

        public bool Undo()
        {
            /* Dictionary<int, ICommand> state = this.history.Undo();

            if (state == null) {
                Debug.LogError("Cannot undo");
                return false;
            }
            this.commands = new Dictionary<int, ICommand>(state); */

            return true;
        }

        public bool Run()
        {
            /* for (int i = 0; i < this.commands.Count; i++) {
                if (!this.commands[i].Run()) {
                    return false;
                }
            } */

            foreach (Transform box in this.boxes) {
                if (box.GetComponent<DropHandler>().token != null) {
                    ICommand command = box.GetComponent<DropHandler>().token.GetComponent<CommandComponent>().command;

                    if (!command.Run()) {
                        return false;
                    }
                }
            }

            return true;
        }

        private void AddTokenBoxes()
        {
            Vector2 position = new Vector2(0, 0);

            for (int i = 0; i < maxLength; i++) {
                Transform obj = Instantiate(this.prefab, position, this.transform.rotation);

                obj.SetParent(gameObject.transform);
                obj.localPosition = position;
                obj.GetComponent<DropHandler>().id = i;

                this.boxes[i] = obj;

                position.y -= this.gap;
            }
        }

        public bool isFull()
        {
            int filled = 0;

            foreach (Transform box in this.boxes) {
                if (box.GetComponent<DropHandler>().token != null) {
                    filled++;
                }
            }

            return filled == this.maxLength;
        }

        // Log commands in queue
        public void Log()
        {
            Debug.Log("----- DEBUG CommandQueue -----");
            for (int i = 0; i < this.commands.Count; i++) {
                Debug.Log("[" + i + "] => " + this.commands[i]);
            }
            Debug.Log("------------------------------");
        }
    }
}