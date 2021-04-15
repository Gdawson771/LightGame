using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using Pathfinding;

public class PlayerController : MonoBehaviour
{
    public LayerMask objects;
    public Rope rope;
    public AIPath aIPath;
    public Vector2 startPos;
    private DistanceJoint2D joint;
    private GameManager gm;
    private UnityEngine.Experimental.Rendering.Universal.Light2D torch;
    private Rigidbody2D rb;
    private Vector2 startDrag;

    private TorchCollider torchCollider;
    private float currentSpeed;
    private Transform powerUpSpriteMask;
    public float playerSpeed;
    public float maxSpeed;
    public bool isMoving = false;

    public float maxStrength = 100000000000f;
    public float forceStrength = 10000f;
    private float torchInnerRadius;
    private float torchOuterRadius;
    private float torchInnerAngle;
    private float torchOuterAngle;
    private float torchSpeed = 18f;

    private bool torchToggled = false;
    // Start is called before the first frame update
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        torch = gameObject.transform.Find("Torch").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        torchCollider = gameObject.transform.Find("Torch").GetComponent<TorchCollider>();
        rb.freezeRotation = true;
        powerUpSpriteMask = gameObject.transform.Find("PowerUpSpriteMask");
        startPos = transform.position;
        joint.enabled = false;


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

        if(Input.GetMouseButtonDown(0)) {
            torchToggled = !torchToggled;
        }
        if (torchToggled)
        {
            startDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            torch.pointLightInnerRadius = Mathf.Lerp(torch.pointLightInnerRadius, 20f, torchSpeed * Time.deltaTime);
            torch.pointLightOuterRadius = Mathf.Lerp(torch.pointLightOuterRadius, 40f, torchSpeed * Time.deltaTime);
            torch.pointLightInnerAngle = Mathf.Lerp(torch.pointLightInnerAngle, 9f, torchSpeed * Time.deltaTime);
            torch.pointLightOuterAngle = Mathf.Lerp(torch.pointLightOuterAngle, 9f, torchSpeed * Time.deltaTime);
        } else {
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



        if(Input.GetMouseButtonUp(0)) {
            torchCollider.GenerateCollider();
        }
        rb.velocity = rb.velocity * 0.99f;

        isMoving = rb.velocity.x != 0 || rb.velocity.y != 0;
    }

    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            rb.AddForce(Vector2.up * playerSpeed);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector2.left * playerSpeed);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector2.right * playerSpeed);
        }

        if (Input.GetKey("s"))
        {
            rb.AddForce(Vector2.down * playerSpeed);

        }

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide!");
    }
    public void reduceDistance()
    {
        joint.distance -= joint.distance * Time.deltaTime;
    }

    public void increaseDistance()
    {
        joint.distance += joint.distance * Time.deltaTime;

    }
    public DistanceJoint2D createAnchor(Vector3 position)
    {

        return joint;
    }


    public void Death()
    {
        transform.position = startPos;
        rb.velocity = Vector2.zero;

        gm.incrementScore();
    }

}
