using UnityEngine;
using DG.Tweening;

public class itemDrop : MonoBehaviour
{
    float duration = 1;
    Vector3 player;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            transform.DOMove(player,duration)
            .SetLink(this.gameObject)
            .OnComplete(() => 
            {
                // Add to Inventory
                Destroy(this.gameObject);
            });
        }
    }

    void OnTriggerStay(Collider collider)
    {
        player = collider.transform.position;
    }
}
