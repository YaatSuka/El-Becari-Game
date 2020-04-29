using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text_send_path : MonoBehaviour
{
    string path = "";

    private void Awake() {
        if (PlayerPrefs.HasKey("Path"))
            path = PlayerPrefs.GetString("Path");
        Debug.Log("the path is : " + path);
    }
}
