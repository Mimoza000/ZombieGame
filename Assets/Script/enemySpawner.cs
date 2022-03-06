using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] enemySpawnData spawnData;
    [SerializeField] Transform spawnField1;
    [SerializeField] Transform spawnField2;

    void Start()
    {
        for (int i = 0;i < spawnData.spawnSize;i++)
        {
            GameObject gameObject;
            float X = Random.Range(spawnField1.position.x,spawnField2.position.x);
            float Z = Random.Range(spawnField1.position.z,spawnField2.position.z);
            Vector3 position = new Vector3(X,0,Z);
            gameObject = Instantiate(spawnData.enemyPrefab,position,Quaternion.identity);
        }
    }
}
