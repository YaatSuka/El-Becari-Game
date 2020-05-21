using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Level;
using Command;
using Interactables;
using Player;

public class GameController : MonoBehaviour
{
    private LevelReader levelReader;
    private InputQueue inputQueue;
    private OutputQueue outputQueue;
    private CommandList commandList;
    private CommandQueue commandQueue;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        this.levelReader = new LevelReader();

        // Read JSON + Store level data
        if (PlayerPrefs.GetString("Path") == null) {
            Debug.LogError("No level file found");
            return;
        }
        if (!ReadLevel(PlayerPrefs.GetString("Path"))) { return; }

        // Set InputQueueObject
        this.inputQueue = GameObject.Find("InputQueue").GetComponent<InputQueue>();
        this.inputQueue.Fill(this.levelReader.input);

        // Set InstructionSheetObject
        GameObject instructionSheet = GameObject.Find("InstructionSheet");
        instructionSheet.GetComponent<InstructionController>().SetTitle(this.levelReader.title);
        instructionSheet.GetComponent<InstructionController>().SetInstructions(this.levelReader.instructions);

        // Set OutputQueueObject
        this.outputQueue = GameObject.Find("OutputQueue").GetComponent<OutputQueue>();
        this.outputQueue.Init(this.levelReader.input);

        // Set CommandListObject
        this.commandList = GameObject.Find("CommandList").GetComponent<CommandList>();
        this.commandList.Fill(this.levelReader.commands);

        // Set CommandQueueObject
        this.commandQueue = GameObject.Find("CommandQueue").GetComponent<CommandQueue>();
        this.commandQueue.Init();

        this.player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void RunCommandQueue()
    {
        this.commandQueue = GameObject.Find("CommandQueue").GetComponent<CommandQueue>();

        Debug.Log("CommandQueue running...");
        if (!this.commandQueue.Run(CheckOutput)) {
            Debug.LogError("Something went wrong while executing the CommandQueue");
        }
    }

    public void CheckOutput()
    {
        int[] output = this.outputQueue.ExtractOutput();

        if (output.SequenceEqual(this.levelReader.output)) {
            Debug.Log("CONGRATULATION! YOU SUCCEED!");
        } else {
            this.inputQueue.Reset();
            this.outputQueue.Reset();
            this.player.Reset();
            this.commandQueue.Reset();
            Debug.Log("OH! WRONG OUTPUT...");
        }
    }

    private bool ReadLevel(string path)
    {
        return levelReader.Init(path);
    }
}
