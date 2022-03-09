using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class result : MonoBehaviour
{
    [SerializeField] UI_Manager_Game UI;

    void OnTriggerEnter(Collider collider)
    {
        UI.OnResult();
    }
}
