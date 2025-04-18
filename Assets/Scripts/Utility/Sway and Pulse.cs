using UnityEngine;

public class SwayandPulse : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float swayAmount = 5f;
    public float swaySpeed = 2f;

    [Header("Pulse Settings")]
    public float pulseScale = 0.05f;
    public float pulseSpeed = 1.5f;

    private Vector3 initialScale;
    private float initialRotationZ;

    private void Start()
    {
        initialScale = transform.localScale;
        initialRotationZ = transform.eulerAngles.z;


    }

    private void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseScale;

        // apply thje sway

        transform.rotation = Quaternion.Euler(0, 0, initialRotationZ + sway);

        // apply pulse

        transform.localScale = initialScale * (1 + pulse);
    }
}
