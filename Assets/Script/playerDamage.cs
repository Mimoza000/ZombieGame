using System;
using System.Collections.Generic;
using UnityEngine;

public class playerDamage : MonoBehaviour
{
    [SerializeField] float damageStartTime;
    [SerializeField] float damageRate;
    enemySystem enemy;
    void Update()
    {
        if (enemy != null) 
        {
            if (enemy.animator.GetBool("dead")) CancelInvoke("PlayerHP");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackField"))
        {
            enemy = other.GetComponentInParent<enemySystem>();
            InvokeRepeating("PlayerHP",damageStartTime,damageRate);
        }
    }

    void OnTriggerExit(Collider other)
    {
        CancelInvoke("PlayerHP");
    }

    void PlayerHP()
    {
        GameManager.Instance.playerHP -= enemy.status.attack;
    }
}
