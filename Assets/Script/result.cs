using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class result : MonoBehaviour
{
    [SerializeField] UIManager_Game UI;

    void OnTriggerEnter(Collider collider)
    {
        UI.OnResult();
    }
}
