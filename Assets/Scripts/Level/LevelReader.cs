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

                return true;
            } catch (Exception e) {
                Debug.LogError("An error occured while parsing JSON level file");
                Debug.LogException(e);

                return false;
            }
        }
    }
}

