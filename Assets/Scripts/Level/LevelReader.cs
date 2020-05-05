using UnityEngine;
using System;
using System.IO;

using Command;

namespace Level
{
    [System.Serializable]
    public class Slot
    {
        public int index;
        public int value;
    }

    [System.Serializable]
    public class LevelData
    {
        public string level;
        public string title;
        public int[] input;
        public int[] output;
        public string[] commands;
        public Slot[] slots;
        public string instructions;
    }
    public class LevelReader
    {
        public string level;
        public string title;
        public int[] input;
        public int[] output;
        public ICommand[] commands;
        public Value[] slots;
        public string instructions;

        public bool Init(string path)
        {
            try {
                int idx = 0;
                string json = File.ReadAllText(path);
                LevelData levelData = JsonUtility.FromJson<LevelData>(json);
                CommandFactory factory = new CommandFactory();

                this.level = levelData.level;
                this.title = levelData.title;
                this.input = levelData.input;
                this.output = levelData.output;

                this.commands = new ICommand[levelData.commands.Length];
                foreach (string name in levelData.commands) {
                    ICommand command = factory.Build(name);
                    this.commands[idx] = command;
                    idx++;
                }
                idx = 0;

                this.slots = new Value[levelData.slots.Length];
                foreach(Slot slot in levelData.slots) {
                    this.slots[idx] = new Value(slot.index, slot.value);
                    idx++;
                }

                this.instructions = levelData.instructions;

                // LOGS DEBUG
                /* Debug.Log("Level: " + this.level);
                Debug.Log("Title: " + this.title);

                string inputs = "";
                foreach (int value in this.input) {
                    inputs += value.ToString();
                    inputs += ", ";
                }
                Debug.Log("Input: " + inputs);

                string outputs = "";
                foreach (int value in this.output) {
                    outputs += value.ToString();
                    outputs += ", ";
                }
                Debug.Log("Ouput: " + outputs);

                Debug.Log("Commands: ");
                foreach (ICommand command in this.commands) {
                    Debug.Log(command.name);
                    command.Run();
                }

                Debug.Log("Slots:");
                foreach(Value slot in this.slots) {
                    Debug.Log("slot[" + slot.uid + "] = " + slot.value);
                }
                Debug.Log("Instructions: " + this.instructions); */
                // LOGS DEBUG - END

                return true;
            } catch (Exception e) {
                Debug.LogError("An error occured while parsing JSON level file");
                Debug.LogException(e);

                return false;
            }
        }
    }
}

