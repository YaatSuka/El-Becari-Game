using UnityEngine;

namespace Command
{
    public class CommandList: MonoBehaviour
    {
        public int length = 0;
        public ICommand[] commands;
        public Transform[] prefabs;
        public float gap = 46f;

        private Transform[] tokens;

        public void Fill(ICommand[] commands)
        {
            this.length = commands.Length;
            this.commands = commands;
            this.tokens = new Transform[this.length];
            this.AddTokens();
        }

        public ICommand Get(int index)
        {
            if (index >= commands.Length || index < 0) {
                Debug.LogError("Invalid index or null command");
                return null;
            }

            return commands[index];
        }

        public GameObject Get(string commandName)
        {
            foreach (Transform token in this.tokens) {
                if (token.GetComponent<CommandComponent>().command.name == commandName) {
                    return token.gameObject;
                }
            }

            return null;
        }

        private void AddTokens()
        {
            Vector2 position = new Vector2(0, 0);
            int idx = 0;

            foreach (ICommand command in this.commands) {
                Transform obj = Instantiate(this.prefabs[0], position, this.transform.rotation);

                obj.SetParent(gameObject.transform);
                obj.localPosition = position;
                obj.localScale = new Vector2(0.8f, 0.8f);
                obj.gameObject.AddComponent<CommandComponent>();
                obj.GetComponent<CommandComponent>().command = command;
                obj.GetComponent<CommandComponent>().SetText();

                this.tokens[idx] = obj;

                idx++;
                position.y -= this.gap;
            }
        }

        // Display commands names
        public void Log()
        {
            Debug.Log("----- DEBUG CommandList -----");
            foreach(ICommand command in commands) {
                Debug.Log(command.name);
            }
            Debug.Log("-----------------------------");
        }
    }
}