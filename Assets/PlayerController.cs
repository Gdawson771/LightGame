using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using Pathfinding;

public class PlayerController : MonoBehaviour
{
    public LayerMask objects;
    public AIPath aIPath;
    public Vector2 startPos;
    public GameManager gm;
    public UnityEngine.Experimental.Rendering.Universal.Light2D torch;
    public UnityEngine.Experimental.Rendering.Universal.Light2D gunfire;
    public Rigidbody2D rb;
    private Vector2 startDrag;

    private TorchCollider torchCollider;
    private AudioSource audioSource;
    private float currentSpeed;
    private Transform powerUpSpriteMask;
    public float playerSpeed;
    public float maxSpeed;
    public bool isMoving = false;
    public AudioClip shotAudio;

    public float maxStrength = 100000000000f;
    public float forceStrength = 10000f;
    private float torchInnerRadius;
    private float torchOuterRadius;
    private float torchInnerAngle;
    private float torchOuterAngle;
    private float torchSpeed = 18f;

    public GameObject sparksPrefab;
    private bool torchToggled = false;

    private float gunfireDuration = 0.1f;
    private float lastShotFires;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        gunfire = gameObject.transform.Find("Gunfire").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        torchCollider = gameObject.transform.Find("Torch").GetComponent<TorchCollider>();
        rb.freezeRotation = true;
        startPos = transform.position;


        torchInnerRadius = torch.pointLightInnerRadius;
        torchOuterRadius = torch.pointLightOuterRadius;
        torchInnerAngle = torch.pointLightInnerAngle;
        torchOuterAngle = torch.pointLightOuterAngle;
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // get direction you want to point at
        Vector2 direction = (mouseScreenPosition - (Vector2)transform.position).normalized;

        // set vector of transform directly
        transform.up = Vector2.Lerp(transform.up, direction, 10f * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            torchToggled = !torchToggled;
        }
        if (torchToggled)
        {

            startDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            torch.pointLightInnerRadius = Mathf.Lerp(torch.pointLightInnerRadius, 20f, torchSpeed * Time.deltaTime);
            torch.pointLightOuterRadius = Mathf.Lerp(torch.pointLightOuterRadius, 40f, torchSpeed * Time.deltaTime);
            torch.pointLightInnerAngle = Mathf.Lerp(torch.pointLightInnerAngle, 9f, torchSpeed * Time.deltaTime);
            torch.pointLightOuterAngle = Mathf.Lerp(torch.pointLightOuterAngle, torchOuterAngle, torchSpeed * Time.deltaTime);
        }
        else
        {
            torch.pointLightInnerRadius = Mathf.Lerp(torch.pointLightInnerRadius, torchInnerRadius, torchSpeed * Time.deltaTime);
            torch.pointLightOuterRadius = Mathf.Lerp(torch.pointLightOuterRadius, torchOuterRadius, torchSpeed * Time.deltaTime);
            torch.pointLightInnerAngle = Mathf.Lerp(torch.pointLightInnerAngle, torchInnerAngle, torchSpeed * Time.deltaTime);
            torch.pointLightOuterAngle = Mathf.Lerp(torch.pointLightOuterAngle, torchOuterAngle, torchSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector2 releasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentSpeed = Vector2.Distance(startDrag, releasePos) * forceStrength > maxStrength
                ? maxStrength
                : Vector2.Distance(startDrag, releasePos) * forceStrength;
            Vector2 scaleChange = new Vector2(currentSpeed / 10, currentSpeed / 10);
            powerUpSpriteMask.localScale = scaleChange;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Vector2 releasePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Helpers.AngleBetweenVector2(transform.position, releasePos);
            rb.AddForce(Helpers.AngleToForce(angle, forceStrength));
            gm.incrementScore();
            powerUpSpriteMask.localScale = new Vector2(0, 0);
        }


        if (Input.GetMouseButtonDown(1))
        {
            gunfire.enabled = true;
            lastShotFires = Time.time;
            Shoot(transform.position, direction);
        }
        if (Input.GetMouseButtonUp(0))
        {
            torchCollider.GenerateCollider();
        }
        rb.velocity = rb.velocity * 0.99f;

        isMoving = rb.velocity.x != 0 || rb.velocity.y != 0;

        if (lastShotFires + gunfireDuration < Time.time)
            gunfire.enabled = false;
    }

    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(Vector2.up * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector2.left * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector2.right * playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            rb.AddForce(Vector2.down * playerSpeed * Time.deltaTime);

        }
        if (Input.GetKey("e"))
        {
            rb.AddForce(Vector2.down * playerSpeed * Time.deltaTime);

        }

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide!");
    }

    void Shoot(Vector2 origin, Vector2 direction)
    {
        int layer_mask = LayerMask.GetMask("Enemy", "EnemyObstacles");
        audioSource.PlayOneShot(shotAudio, 0.3f);
        RaycastHit2D hitInfo = Physics2D.Raycast(origin, direction, 2000.0f, layer_mask);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float mouseAngle = Helpers.AngleBetweenVector2(transform.position, mousePos);
        Vector2 force = Helpers.AngleToForce(mouseAngle, 2f);

        Camera.main.transform.position = Camera.main.transform.position - new Vector3(force.x, force.y, 0);
        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.tag);
            if (hitInfo.transform.tag == "LurkerEnemy")
                hitInfo.transform.GetChild(0).gameObject.GetComponent<Enemy1>().Die();
            if (hitInfo.transform.tag == "WalkerEnemy")
                hitInfo.transform.gameObject.GetComponent<Enemy1>().Die();
            if (hitInfo.transform.tag == "Wall")
            {
                Debug.Log("Wall");
                float angle = Helpers.AngleBetweenVector2(hitInfo.point, transform.position);
                Quaternion qRotation = Quaternion.Euler(0, 0, 0);
                Debug.Log(qRotation);
                Instantiate(sparksPrefab, hitInfo.point, qRotation);
            }


        }


    }
    public void Death()
    {
        transform.position = startPos;
        rb.velocity = Vector2.zero;

        gm.incrementScore();
    }

}
