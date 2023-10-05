using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaker : MonoBehaviour
{
    [System.Serializable]
    public class PercentageGameObject
    {
        public GameObject gameObject;
        public float percentage;  // This should be set between 0 and 100
    }

    // List of game objects with their spawn percentages
    public List<PercentageGameObject> percentageGameObjects;

    void Start()
    {
        SpawnRandomObject();
    }

    void SpawnRandomObject()
    {
        if (percentageGameObjects.Count == 0)
        {
            Debug.LogError("No game objects provided in the list!");
            return;
        }

        float totalPercentage = 0f;
        foreach (PercentageGameObject pgo in percentageGameObjects)
        {
            totalPercentage += pgo.percentage;
        }

        if (Mathf.Abs(totalPercentage - 100f) > 0.01f)
        {
            Debug.LogError("Total percentages do not add up to 100!");
            return;
        }

        float randomPercentage = Random.Range(0f, 100f);
        GameObject selectedObject = null;

        foreach (PercentageGameObject pgo in percentageGameObjects)
        {
            if (randomPercentage < pgo.percentage)
            {
                selectedObject = pgo.gameObject;
                break;
            }

            randomPercentage -= pgo.percentage;
        }

        // Instantiate the selected game object at the script's location
        if (selectedObject != null)
        {
            Instantiate(selectedObject, transform.position, Quaternion.identity);
        }
    }
}
