using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public LineRenderer rope;
    public Vector2 destination;
    public Vector2 origin;
    public float ropeWidth = 0.1f;
    public float ropeMaxLength = 5f;
    // Start is called before the first frame update

    void Start()
    {
        rope.startWidth = ropeWidth;
        rope.enabled = false;
    }

    private void FixedUpdate()
    {
        rope.SetPosition(0, origin);
        rope.SetPosition(1, destination);
    }
    public void enableRope()
    {
        rope.enabled = true;
    }
    
    public void disableRope()
    {
        rope.enabled = false;
   
    }
}

