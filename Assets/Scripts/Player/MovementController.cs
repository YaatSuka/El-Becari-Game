using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float transitionSpeed = 2f; 
    public string actualPosition = "";

    
    private Dictionary<string, Vector3> newLocation;


    // Start is called before the first frame update
    void Start()
    {
        newLocation = new Dictionary<string, Vector3>();
        fillLocation();
    }

    private void FixedUpdate() {
        if (actualPosition != "" && actualPosition != "OutLocation" && newLocation.ContainsKey(actualPosition))
            transform.position = Vector3.Lerp(transform.position, newLocation[actualPosition], transitionSpeed * Time.deltaTime);
        else if (actualPosition == "OutLocation")
           transform.position = Vector3.Lerp(transform.position, newLocation[actualPosition], transitionSpeed * Time.deltaTime);
    }

    public void MoveTo(string Location)
    {
        actualPosition = Location;
    }

    private void fillLocation()
    {
        foreach(GameObject Location in GameObject.FindGameObjectsWithTag("location")) {
            newLocation.Add(Location.name, Location.transform.position);
         }
    }
}
