using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public int pointsToAdd = 10; // Adjust this to set the number of points to add.
    public GameObject self;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider has a ScoreCounter component.
        ScoreCounter scoreCounter = other.GetComponent<ScoreCounter>();

        // If the other collider has a ScoreCounter component, add points.
        if (scoreCounter != null)
        {
            scoreCounter.EarnPoint(pointsToAdd);
        }

        // Destroy both the GameObject this script is attached to and the script itself.
        Destroy(self);
    }
}
