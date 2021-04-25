using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallZone : MonoBehaviour
{
    private PlayerController playerController;
    private PolygonCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        collider = gameObject.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.name == "Player") {
            Debug.Log(playerController.dashing);
            if(!playerController.dashing)
                playerController.Death();
        }
            


    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.transform.name == "Player")
            Debug.Log("Exie");
    }
}
