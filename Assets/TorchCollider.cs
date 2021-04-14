using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using Pathfinding;
public class TorchCollider : MonoBehaviour
{
    public PolygonCollider2D collider;
    public UnityEngine.Experimental.Rendering.Universal.Light2D torch;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        torch = gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        GenerateCollider();

    }

    // Update is called once per frame
    void Update()
    {


        // Debug.DrawLine(negative, normal, Color.red, 2.5f);
        // Debug.DrawLine(normal, positive, Color.yellow, 2.5f);
        // Debug.DrawLine(positive, transform.parent.position, Color.magenta, 2.5f);

    }

    public void GenerateCollider() {

        if(gameObject.GetComponent<PolygonCollider2D>() != null) {
            Debug.Log("Destroy");
            Destroy(gameObject.GetComponent<PolygonCollider2D>());
        }

        collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
        float radius = torch.pointLightOuterRadius;
        float torchAngle = torch.pointLightOuterAngle;
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float mouseAngle = Helpers.AngleBetweenVector2(transform.parent.position, mouseScreenPosition);
        float normalX = radius * Mathf.Cos(mouseAngle * Mathf.Deg2Rad);
        float normalY = radius * Mathf.Sin(mouseAngle * Mathf.Deg2Rad);
        Vector2 normal = new Vector2(normalX + transform.parent.position.x, normalY + transform.parent.position.y);

        float halfAngle = torchAngle / 2;

        float negativeX = radius * Mathf.Cos(Helpers.AngleModulus(mouseAngle, -halfAngle) * Mathf.Deg2Rad);
        float negativeY = radius * Mathf.Sin(Helpers.AngleModulus(mouseAngle, -halfAngle) * Mathf.Deg2Rad);
        Vector2 negative = new Vector2(transform.parent.position.x + negativeX , transform.parent.position.y + negativeY);

        float positiveX = radius * Mathf.Cos(Helpers.AngleModulus(mouseAngle, halfAngle) * Mathf.Deg2Rad);
        float positiveY = radius * Mathf.Sin(Helpers.AngleModulus(mouseAngle, halfAngle) * Mathf.Deg2Rad);
        Vector2 positive = new Vector2(transform.parent.position.x + positiveX , transform.parent.position.y + positiveY);

        Vector2[] precisionPoints = new Vector2[11];
        float negativeAngle = Helpers.AngleBetweenVector2(transform.parent.position, negative);

        precisionPoints[0] = transform.InverseTransformPoint(transform.parent.position);
        precisionPoints[1] = transform.InverseTransformPoint(negative);

        for (int i = 0; i <= 8; i++) {
            float degreeIncrement = torchAngle / precisionPoints.Length;
            float x = radius * Mathf.Cos(Helpers.AngleModulus(negativeAngle, degreeIncrement * i + 1) * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(Helpers.AngleModulus(negativeAngle, degreeIncrement * i + 1) * Mathf.Deg2Rad);

            precisionPoints[i + 2] = transform.InverseTransformPoint(new Vector2(transform.parent.position.x + x , transform.parent.position.y + y));
        }
        precisionPoints[precisionPoints.Length - 1] = transform.InverseTransformPoint(positive);
        collider.SetPath(0, precisionPoints);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<AIPath>().canMove = false;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponent<AIPath>().canMove = true;
    }
}
