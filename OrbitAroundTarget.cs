using UnityEngine;

public class OrbitAroundTarget : MonoBehaviour
{
    public Transform target;
    public float distance = 3.0f;
    public float zoomSpeed = 2.0f;
    public float rotationSpeed = 150.0f;

    private float _yaw;
    private float _pitch;

    void Start()
    {
        if (target != null)
        {
            Vector3 offset = transform.position - target.position;
            distance = offset.magnitude;
            _yaw = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
            _pitch = Mathf.Asin(offset.y / distance) * Mathf.Rad2Deg;
        }
    }

    void LateUpdate()
    {
        if (!target) return;

        if (Input.GetMouseButton(0))
        {
            _yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            _pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            _pitch = Mathf.Clamp(_pitch, -89f, 89f);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, 1.0f, 10.0f);

        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
        Vector3 offset = rotation * Vector3.forward * distance;

        transform.position = target.position + offset;
        transform.LookAt(target);
    }
}