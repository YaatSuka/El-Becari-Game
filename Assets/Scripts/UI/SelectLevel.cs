using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public string Path_Tag = "1";
    public int NB_level = 0;
    public string Desc_Level = "";
    public bool is_Bonus = false;

    GameObject NumText; 
    GameObject LevelText;

    public Sprite ImageBonus;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Path_Tag);
        GameObject NumText = gameObject.transform.GetChild(0).gameObject;
        GameObject LevelText = gameObject.transform.GetChild(1).gameObject;

        NumText.GetComponent<Text>().text = NB_level.ToString();
        LevelText.GetComponent<Text>().text = Desc_Level;

        if (is_Bonus)
        {
            NumText.GetComponent<Text>().text = "";
            gameObject.GetComponent<Image>().sprite = ImageBonus;
        }

        this.GetComponent<Button>().onClick.AddListener(LoadGame);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetString("Path", "/Scripts/Level/Levels/level" + Path_Tag +".json");
        SceneManager.LoadScene(2);
    }
}
