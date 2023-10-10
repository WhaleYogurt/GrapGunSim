using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 RespawnPosition;
    public ScoreCounter scoreCounter;
    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGameObject = collision.gameObject;
        // Debug.Log(otherGameObject.name);
        if (collision.gameObject.name == "Charlie")
        {
            scoreCounter.EarnPoint(250);
        }
        else if (collision.gameObject.name == "Chris")
        {
            scoreCounter.EarnPoint(100);
        }
        else if (collision.gameObject.name == "Player")
        {
            scoreCounter.TakePoints((scoreCounter.Score / 3) + 200);
        }
        otherGameObject.transform.position = RespawnPosition;
    }
}
