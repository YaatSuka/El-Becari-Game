using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Level;
using Command;

public class GameController : MonoBehaviour
{
    private LevelReader levelReader;

    // Start is called before the first frame update
    void Start()
    {
        levelReader = new LevelReader();

        if (!ReadLevel(Application.dataPath + "/Scripts/Level/Levels/level1.json")) { return; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool ReadLevel(string path)
    {
        return levelReader.Init(path);
    }

    private bool SetInputQueue(int[] input)
    {
        return true;
    }

    private bool SetCommandList(ICommand[] commands)
    {
        return true;
    }
}
