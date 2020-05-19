using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Command;

public class AppBarController : MonoBehaviour
{
    public Button runButton;
    public Button undoButton;
    public Button reButton;

    private GameController gameController;
    private CommandQueue commandQueue;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        commandQueue = GameObject.Find("CommandQueue").GetComponent<CommandQueue>();
        Button run = runButton.GetComponent<Button>();
        Button undo = undoButton.GetComponent<Button>();
        Button redo = reButton.GetComponent<Button>();

        run.onClick.AddListener(Run);
        undo.onClick.AddListener(Undo);
        redo.onClick.AddListener(Redo);
    }

    void Run()
    {
        gameController.RunCommandQueue();
    }

    void Undo()
    {
        commandQueue.Undo();
    }

    void Redo()
    {
        commandQueue.Redo();
    }
}
