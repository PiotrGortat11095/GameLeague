using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float distance = 5;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;
    [SerializeField] Vector2 framingOffset;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;
    [SerializeField] LayerMask obstacleMask;

    float distance2 = 4;
    float rotationY;
    float rotationX;

    float invertXVal;
    float invertYVal;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        invertXVal = (invertX) ? -1 : 1;
        invertYVal = (invertY) ? -1 : 1;

        rotationX -= Input.GetAxis("Mouse Y") * invertYVal * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY -= Input.GetAxis("Mouse X") * invertXVal * rotationSpeed;

        var targetRotation = Quaternion.Euler(rotationX, rotationY, 0);

        var focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y);

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            distance -= scrollInput * 5f;
            distance = Mathf.Clamp(distance, 4f, 8f);
            distance2 = distance;

        }

        Vector3 direction = transform.position - focusPosition;
        if (Physics.Raycast(focusPosition, direction, out RaycastHit hit, distance, obstacleMask))
        {
            distance = Mathf.Lerp(distance, hit.distance, Time.deltaTime * rotationSpeed);
        }
        else
        {
            distance = distance2;
        }


        transform.position = focusPosition - targetRotation * new Vector3(0, 0, distance);
        transform.rotation = targetRotation;
    }
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationY, 0);
    public Quaternion GetPlanarRotation()
    {
        return Quaternion.Euler(0, rotationY, 0);
    }
}
