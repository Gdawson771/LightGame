using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using Pathfinding;

public class lightFlicker : MonoBehaviour
{
    private UnityEngine.Experimental.Rendering.Universal.Light2D light;
    private CircleCollider2D collider;
    private float flickerStartTime;

    private float flickerDuration;
    //Get the point light and store that locally here
    //GET 2d box COLLIDER AND STORE THAT HERE
    //set variable for how many flickers per second
    //in update get random number, disable everything, enable after the flicker time
    void Start()
    {
        light = gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        collider = gameObject.GetComponent<CircleCollider2D>();
        flickerDuration = 3f;
        flickerStartTime=Time.time;

    }

    //get a random number store this locallyoutside update
    //time.deltatime figure out how to count time in update
    //after time has passed renable
    // Update is called once per frame
    void Update()
    {
        if(Time.time>(flickerStartTime+flickerDuration))
        {
            flickerDuration = Random.Range(0.2f, 2f);
            flickerStartTime=Time.time;
            light.enabled = !light.enabled;
            collider.enabled = !collider.enabled;
        }
        Debug.Log(Time.time);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Enter");
        col.gameObject.GetComponent<AIPath>().canMove = false;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Exit");
        col.gameObject.GetComponent<AIPath>().canMove = true;
    }
}
