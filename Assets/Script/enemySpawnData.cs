using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnData",fileName = "NewSpawnData")]
public class enemySpawnData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int spawnSize;
}
