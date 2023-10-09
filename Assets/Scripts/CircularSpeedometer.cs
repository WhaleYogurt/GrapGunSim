using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CircularSpeedometer : MonoBehaviour
{
    public Rigidbody targetRigidbody;
    public GameObject needleObject;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI multiplierText;
    public float maxSpeedMPH = 100f;
    public Color minSpeedColor = Color.green;
    public Color maxSpeedColor = Color.red;
    public float scoreMultiplier = 1.0f;
    public Image backplateImage;

    private void Update()
    {
        if (targetRigidbody != null)
        {
            // Calculate the speed in meters per second (m/s)
            float speedMS = targetRigidbody.velocity.magnitude;

            // Convert the speed to miles per hour (MPH)
            int speedMPH = Mathf.RoundToInt(speedMS * 2.23694f); // 1 m/s = 2.23694 mph

            float[] speedPoints = { 0f, 25f, 35f, 75f };
            float[] multiplierPoints = { 0.5f, 1.0f, 2.0f, 3.0f };

            // Interpolate the score multiplier based on speed points
            for (int i = 0; i < speedPoints.Length - 1; i++)
            {
                if (speedMPH >= speedPoints[i] && speedMPH < speedPoints[i + 1])
                {
                    float t = Mathf.InverseLerp(speedPoints[i], speedPoints[i + 1], speedMPH);
                    scoreMultiplier = Mathf.Lerp(multiplierPoints[i], multiplierPoints[i + 1], t);
                    break; // Exit loop once we've found the appropriate range
                }
            }

            // Display the speed in MPH using TextMeshPro
            speedText.text = speedMPH.ToString("0");
            multiplierText.text = scoreMultiplier.ToString("F1") + "X";
            // Calculate the normalized speed between 0 and 1
            float normalizedSpeed = Mathf.Clamp01(speedMPH / maxSpeedMPH);

            // Interpolate color between minSpeedColor (green) and maxSpeedColor (red)
            Color currentColor = Color.Lerp(minSpeedColor, maxSpeedColor, normalizedSpeed);

            // Update the backplate color
            backplateImage.color = currentColor;

            // Calculate the rotation angle based on the speed, limited to a 180-degree scale
            float rotationAngle = Mathf.Clamp(speedMPH / maxSpeedMPH, -0.5f, 0.5f) * 360f;

            // Rotate the needle object to the calculated angle
            needleObject.transform.rotation = Quaternion.Euler(0f, 0f, -rotationAngle);
        }
    }
}
