using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : MonoBehaviour
{
    [Header("References")]
    private BoxCollider2D boxCollider2D;
    private PlayerController player;
    [System.NonSerialized] public EnemyBase enemyBase;
    [System.NonSerialized] private Rigidbody2D rb;
    [SerializeField] private Vector2 startPosition;
    [SerializeField] private Vector2 endPosition;
    [SerializeField] private float speed;
    private bool direction;
    public float deltaX = 15f;
    public float deltaY = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = startPosition;
        if (deltaX != 0f)
            v.x += deltaX * Mathf.Sin(Time.time * speed);
        if (deltaY != 0f)
            v.y += deltaY * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Here");
        // TODO: do safety checks
        collision.collider.GetComponent<PlayerController>().Death();
    }
}
