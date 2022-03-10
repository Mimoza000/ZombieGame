using UnityEngine;
using DG.Tweening;

public class itemSystem : MonoBehaviour
{
    float duration = 1;
    Vector3 player;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = collider.transform.position;
            transform.DOMove(player,duration)
            .SetEase(Ease.InOutSine)
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
        Debug.Log(player);
    }
}
