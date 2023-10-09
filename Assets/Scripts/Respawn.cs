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
        if (collision.gameObject.name == "Charlie" || collision.gameObject.name == "Chris")
        {
            scoreCounter.EarnPoint(500);
        }
        otherGameObject.transform.position = RespawnPosition;
    }
}
