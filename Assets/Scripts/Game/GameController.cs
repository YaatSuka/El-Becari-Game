﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        this.levelReader = new LevelReader();
        this.inputQueue = new InputQueue();

        // Read JSON + Store level data
        if (!ReadLevel(Application.dataPath + "/Scripts/Level/Levels/level1.json")) { return; }

        SetInputQueue(this.levelReader.input);
        this.outputQueue = new OutputQueue(this.inputQueue.length);

        this.commandList = new CommandList(this.levelReader.commands);

        this.commandQueue = new CommandQueue();
        this.commandQueue.Insert(0, this.commandList.Get(0));
        this.commandQueue.Insert(1, this.commandList.Get(1));
        this.commandQueue.Insert(2, this.commandList.Get(1));
        this.commandQueue.Insert(3, this.commandList.Get(0));
        this.commandQueue.Move(3, 2);
        this.commandQueue.Run();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool ReadLevel(string path)
    {
        return levelReader.Init(path);
    }

    private bool SetInputQueue(int[] values)
    {
        inputQueue.Fill(values);
        return true;
    }

    private bool SetCommandList(ICommand[] commands)
    {
        return true;
    }
}