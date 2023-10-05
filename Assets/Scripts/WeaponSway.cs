using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.02f; // Amount of sway.
    public float maxSwayAmount = 0.06f; // Maximum sway amount.
    public float smoothSpeed = 6.0f; // Smoothing speed of the sway.

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        // Get input for weapon sway (e.g., from mouse or controller).
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate new weapon position based on input.
        float newXPos = Mathf.Clamp(mouseX * swayAmount, -maxSwayAmount, maxSwayAmount);
        float newYPos = Mathf.Clamp(mouseY * swayAmount, -maxSwayAmount, maxSwayAmount);

        Vector3 targetPosition = new Vector3(newXPos, newYPos, 0f) + initialPosition;

        // Smoothly interpolate between the current position and the target position.
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothSpeed);
    }
}
