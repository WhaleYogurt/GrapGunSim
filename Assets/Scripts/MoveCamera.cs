using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;
    public bool takeRot = false;

    void Update() {
        transform.position = player.transform.position;
        if (takeRot)
        {
            transform.rotation = player.transform.rotation;
        }
    }
}
