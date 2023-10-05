using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Vector3 RespawnPosition;
    void OnCollisionEnter(Collision collision)
    {
        GameObject otherGameObject = collision.gameObject;

        otherGameObject.transform.position = RespawnPosition;
    }
}
