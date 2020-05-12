using UnityEngine;
using UnityEngine.UI;

namespace Command
{
    public class CommandComponent: MonoBehaviour
    {
        public ICommand command;

        public void SetText()
        {
            transform.GetChild(0).GetComponent<Text>().text = this.command.name;
        }
    }
}