using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Player;
using Command;

public class AppBarController : MonoBehaviour
{
    public Button runButton;
    public Button undoButton;
    public Button reButton;
    public Slider SpeedAnimation;

    private GameController gameController;
    private CommandQueue commandQueue;
    private PlayerController PlayerControlle;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        commandQueue = GameObject.Find("CommandQueue").GetComponent<CommandQueue>();
        PlayerControlle = GameObject.Find("Player").GetComponent<PlayerController>();
        Button run = runButton.GetComponent<Button>();
        Button undo = undoButton.GetComponent<Button>();
        Button redo = reButton.GetComponent<Button>();

        run.onClick.AddListener(Run);
        undo.onClick.AddListener(Undo);
        redo.onClick.AddListener(Redo);
    }

    private void Update() {
        PlayerControlle.ChangeSpeed(SpeedAnimation.value);
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
