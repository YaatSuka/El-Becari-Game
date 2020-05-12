using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppBarController : MonoBehaviour
{
    public Button runButton;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        Button run = runButton.GetComponent<Button>();

        run.onClick.AddListener(Run);
    }

    void Run()
    {
        gameController.RunCommandQueue();
    }
}
