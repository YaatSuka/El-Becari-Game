using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Button returnButton;
    public int scene = 0;


	void Start() {
		Button play = this.GetComponent<Button>();
		play.onClick.AddListener(LoadGame);
	}

    public void LoadGame()
    {
        SceneManager.LoadScene(scene);
    }
}
