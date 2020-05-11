using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Level;
using Command;
using Interactable;

public class GameController : MonoBehaviour
{
    private LevelReader levelReader;
    private InputQueue inputQueue;
    private OutputQueue outputQueue;
    private CommandList commandList;
    private CommandQueue commandQueue;

    private GameObject inputQueueObj;
    private GameObject commandListObj;
    private GameObject commandQueueObj;
    private GameObject instructionSheet;

    // Start is called before the first frame update
    void Start()
    {
        this.levelReader = new LevelReader();

        // Read JSON + Store level data
        if (!ReadLevel(Application.dataPath + "/Scripts/Level/Levels/level1.json")) { return; }

        // Set InputQueueObject
        this.inputQueueObj = GameObject.Find("InputQueue");
        this.inputQueue = this.inputQueueObj.GetComponent<InputQueue>();
        this.inputQueue.Fill(this.levelReader.input);

        // Set InstructionSheetObject
        this.instructionSheet = GameObject.Find("InstructionSheet");
        this.instructionSheet.GetComponent<InstructionController>().SetTitle(this.levelReader.title);
        this.instructionSheet.GetComponent<InstructionController>().SetInstructions(this.levelReader.instructions);

        // TODO: Set OutputQueueObject
        this.outputQueue = new OutputQueue(this.inputQueue.length);

        // Set CommandListObject
        this.commandListObj = GameObject.Find("CommandList");
        this.commandList = this.commandListObj.GetComponent<CommandList>();
        this.commandList.Fill(this.levelReader.commands);

        // Set CommandQueueObject
        this.commandQueueObj = GameObject.Find("CommandQueue");
        this.commandQueue = this.commandQueueObj.GetComponent<CommandQueue>();
        this.commandQueue.Init();

        this.commandQueue = new CommandQueue();
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
    }

    private bool ReadLevel(string path)
    {
        return levelReader.Init(path);
    }
}
