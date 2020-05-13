using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float transitionSpeed = 2f; 
    public string actualPosition = "";

    private Dictionary<string, Vector3> newLocation;
     private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        newLocation = new Dictionary<string, Vector3>();
        anim = GetComponent<Animator>();
        fillLocation();
    }

    private void FixedUpdate() {
        if (actualPosition != "" && newLocation.ContainsKey(actualPosition))
        {
            // if (transform.position.y >= 0 && newLocation[actualPosition].y >= 0 ||    transform.position == newLocation["OutLocationAngle"])    && newLocation[actualPosition].x >= -2.43
            if (transform.position.x <= newLocation["OutLocationAngle"].x - 0.2 && newLocation[actualPosition].x >= -0.6 ||
            transform.position.y <= newLocation["OutLocationAngle"].y - 0.2 && newLocation[actualPosition].y >= 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, newLocation["OutLocationAngle"], transitionSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, newLocation[actualPosition], transitionSpeed * Time.deltaTime);
            }
            
            if (transform.position != newLocation[actualPosition])
                anim.SetInteger("State", 1);
            else
                anim.SetInteger("State", 0);
        } 
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
