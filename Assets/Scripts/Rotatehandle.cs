using UnityEngine;

public class Rotatehandle : MonoBehaviour
{

    [Header("Base Rotation Settings")]

    [SerializeField] private float rotationSpeedZ = 5f;
    [SerializeField] private Vector3 rotationSpeed;

    [Header("Jitter settings")]
    public float baseAngle = 5f;
    [SerializeField] private float jitterAmount = 2f;
    [SerializeField] private float jitterSpeed = 5f;

    private float currentAngle;
    float targetAngle;


    private void Start()
    {
        rotationSpeed = new Vector3(0f, 0f, rotationSpeedZ);

        currentAngle = baseAngle;
    }

    void Update()
    {

        // transform.Rotate(rotationSpeed * Time.deltaTime); // this is continous rotation, to be removed

        // puts jitter together

        if (Mathf.Abs(currentAngle - targetAngle) < 0.1f)
        {
            targetAngle = baseAngle + Random.Range(-jitterAmount, jitterAmount);
        }
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, jitterSpeed);

        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
