using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Sprite sprite;
    private Vector2 pivot;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        pivot = Helpers.GetSpritePivot(sprite);
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // get direction you want to point at
        Vector2 direction = (mouseScreenPosition - (Vector2) transform.position).normalized;
        
        // set vector of transform directly
        transform.up = direction;
    }
}
