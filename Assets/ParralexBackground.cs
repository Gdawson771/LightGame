using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralexBackground : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    public float parralexEffectMultiplier;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position -= deltaMovement * parralexEffectMultiplier;
        lastCameraPosition = cameraTransform.position;
        Debug.Log(cameraTransform);
    }
}
