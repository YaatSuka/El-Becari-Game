using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionController : MonoBehaviour
{
    private Transform title;
    private Transform instructions;

    // Start is called before the first frame update
    void Start()
    {
        this.title = transform.GetChild(0);
        this.instructions = transform.GetChild(1);

        this.title.GetComponent<MeshRenderer>().sortingOrder = 3;
        this.instructions.GetComponent<MeshRenderer>().sortingOrder = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTitle(string title)
    {
        title = ResolveTextSize(title, 18);
        this.title.GetComponent<TextMesh>().text = title;
    }

    public void SetInstructions(string instructions)
    {
        instructions = ResolveTextSize(instructions, 30);
        this.instructions.GetComponent<TextMesh>().text = instructions;
    }

    private string ResolveTextSize(string input, int lineLength)
    {
        string[] words = input.Split(" "[0]);
        string result = "";
        string line = "";

        foreach (string s in words) {
            string temp = line + " " + s;

            if (temp.Length > lineLength) {
                result += line + "\n";
                line = s;
            } else {
                line = temp;
            }
        }     
        result += line;

        return result.Substring(1,result.Length-1);
    }
}
