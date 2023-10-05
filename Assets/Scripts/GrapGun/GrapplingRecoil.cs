using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Transform))]
public class GrapplingRecoil : MonoBehaviour
{
    public Transform gunTransform;      // Reference to the gun's transform.
    public Transform attractionPoint;  // Reference to the fixed point of attraction.
    public float recoilForce = 5f;     // Adjust this value to control the recoil force.
    public float recoilDuration = 0.1f; // Duration of the recoil effect in seconds.
    public float returnSpeed = 5f;      // Speed at which the gun returns to its original position.
    public Rigidbody gunRigidbody;      // Reference to the gun's rigidbody.

    private Quaternion originalRotation;
    private float recoilTime = 0f;

    private void Start()
    {
        originalRotation = gunTransform.localRotation;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Change "Fire1" to your input button.
        {
            ApplyRecoil();
        }

        // Apply recoil over time.
        if (recoilTime > 0)
        {
            float t = 1 - (recoilTime / recoilDuration);
            gunTransform.localRotation = Quaternion.Lerp(originalRotation, originalRotation * Quaternion.Euler(Vector3.right * recoilForce), t);
            recoilTime -= Time.deltaTime;
        }
        else
        {
            gunTransform.localRotation = Quaternion.Lerp(gunTransform.localRotation, originalRotation, returnSpeed * Time.deltaTime);
        }
    }

    private void ApplyRecoil()
    {
        recoilTime = recoilDuration;

        // Apply recoil force to the rigidbody.
        if (gunRigidbody != null)
        {
            gunRigidbody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
        }
    }

    private void LateUpdate()
    {
        // Ensure that the gun's position follows the attraction point.
        if (attractionPoint != null)
        {
            gunTransform.position = attractionPoint.position;
        }
    }
}
