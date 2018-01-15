using UnityEngine;
using System.Collections;
public class UICamScript : MonoBehaviour
{
    private float velocity = 0.0f;
    private float smoothTime = 0.3f;
    private bool moveCamera = false;
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    public float lerpSpeed;
    public float initialZ;
    public float targetZ;
    public Camera cam;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialPosition = transform.position;
            targetPosition = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5), transform.position.z);
            initialZ = transform.eulerAngles.z;
            targetZ = initialZ + Random.Range(-50, 50);
            moveCamera = true;
            lerpSpeed = 0;
        }
        if (moveCamera)
        {
            CameraMovementMethod();
        }
    }
    private void CameraMovementMethod()
    {
        lerpSpeed = Mathf.SmoothDamp(lerpSpeed, 1.0f, ref velocity, smoothTime);
        cam.transform.position = Vector3.Lerp(initialPosition, targetPosition, lerpSpeed);
        cam.transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(initialZ, targetZ, lerpSpeed));
    }
}