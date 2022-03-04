using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NewEnemyStatus",fileName = "EnemyStatus")]
public class enemyStatus : ScriptableObject
{
    public string enemyName;
    public int attack;
    public int maxHP;
    public float walkSpeed;
    public float runSpeed;
    public float startWalkDistance;
    public float stopDistance;
}