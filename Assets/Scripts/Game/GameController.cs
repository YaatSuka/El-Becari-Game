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

    private GameObject inputQueueObj;
    private GameObject ouputQueueObj;
    private GameObject commandListObj;
    private GameObject commandQueueObj;
    private GameObject instructionSheet;

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
        this.inputQueueObj = GameObject.Find("InputQueue");
        this.inputQueue = this.inputQueueObj.GetComponent<InputQueue>();
        this.inputQueue.Fill(this.levelReader.input);

        // Set InstructionSheetObject
        this.instructionSheet = GameObject.Find("InstructionSheet");
        this.instructionSheet.GetComponent<InstructionController>().SetTitle(this.levelReader.title);
        this.instructionSheet.GetComponent<InstructionController>().SetInstructions(this.levelReader.instructions);

        // Set OutputQueueObject
        this.ouputQueueObj = GameObject.Find("OutputQueue");
        this.outputQueue = this.ouputQueueObj.GetComponent<OutputQueue>();
        this.outputQueue.Init(this.levelReader.input);

        // Set CommandListObject
        this.commandListObj = GameObject.Find("CommandList");
        this.commandList = this.commandListObj.GetComponent<CommandList>();
        this.commandList.Fill(this.levelReader.commands);

        // Set CommandQueueObject
        this.commandQueueObj = GameObject.Find("CommandQueue");
        this.commandQueue = this.commandQueueObj.GetComponent<CommandQueue>();
        this.commandQueue.Init();

        this.player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunCommandQueue()
    {
        this.commandQueue = this.commandQueueObj.GetComponent<CommandQueue>();

        Debug.Log("CommandQueue running...");
        if (!this.commandQueue.Run()) {
            Debug.LogError("Something went wrong while executing the CommandQueue");
        }
        
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
