using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTeleporter : MonoBehaviour
{
    [SerializeField] GameObject teleportPoint;

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player")) collider.transform.position = teleportPoint.transform.position;
    }
}
