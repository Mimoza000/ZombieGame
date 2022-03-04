using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDead : MonoBehaviour
{
    [SerializeField] enemySystem enemy;
    void DeadTrigger()
    {
        enemy.Dead();
    }
}
