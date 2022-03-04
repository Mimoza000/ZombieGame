using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTeleporter : MonoBehaviour
{
    [SerializeField] GameObject teleportPoint;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.name == "Player") collider.transform.position = teleportPoint.transform.position;
    }
}
