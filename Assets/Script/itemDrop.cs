using UnityEngine;
using DG.Tweening;

public class itemDrop : MonoBehaviour
{
    [SerializeField] float duration;
    Transform player;
    void Start()
    {
        transform.DOMoveY(0.6f,duration)
        .SetLoops(-1,LoopType.Yoyo)
        .SetEase(Ease.OutQuad);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = collider.transform;
            transform.DOMove(player.position,duration)
            .SetEase(Ease.InOutExpo)
            .OnComplete(() => 
            {
                GameManager.Instance.dropItemSize++;
                Destroy(gameObject);
            });
        }
    }
}
