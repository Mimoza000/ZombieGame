
using DG.Tweening;
using UnityEngine;

public class materialFade : MonoBehaviour
{
    float duration = 3;
    Material material;
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }
     void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            material.DOFade(0,duration)
            .SetLink(gameObject);
        }
    }
}
