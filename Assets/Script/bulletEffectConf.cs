using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEffectConf : MonoBehaviour
{
    [Header("Debug")]
    public GameObject bulletObject;
    public int standbyNumber;

    Dictionary<int,GameObject> objectDictionary;
    void Start()
    {
        objectDictionary = new Dictionary<int, GameObject>();
        for (int i = 0;i <= standbyNumber;i++)
        {
            bulletObject.SetActive(false);
            Instantiate(bulletObject,Vector3.zero,Quaternion.identity);
            
            objectDictionary.Add(i, bulletObject);
            
            /*Debug.Log("Instatiate Number " + i);*/
        }
        
    }

    /// <summary>
    /// Set GameObject to True and set position and rotation.
    /// if return value -1 => can't find value
    /// else return value int => key of this value
    /// </summary>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public void SetActive(int key,Vector3 position,Quaternion rotation)
    {
        GameObject currentObject;
        if (key <= standbyNumber)
        {
            objectDictionary.TryGetValue(key, out currentObject);
            currentObject.transform.position = position;
            currentObject.transform.rotation = rotation;
            currentObject.SetActive(true);
            Debug.Log(currentObject.activeSelf);
        }
        // Error code
    }

    /// <summary>
    /// Get Object of the right key.
    /// if return value null => can't find value
    /// else return this object
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public GameObject GetObject(int key)
    {
        GameObject currentObject;
        if (key > objectDictionary.Count) return null;
        else objectDictionary.TryGetValue(key, out currentObject);
        return currentObject;
    }

    /// <summary>
    /// Set GameObject to False and set position and rotation.
    /// if return value -1 => can't find value
    /// else return value 0 => clear finish 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int SetInActive(int key)
    {
        GameObject currentObject;
        if (key < objectDictionary.Count) return -1; //Error code
        else objectDictionary.TryGetValue(key, out currentObject);
        currentObject.SetActive(false);
        return 0;
    }
}
