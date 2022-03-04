using UnityEngine;
using DG.Tweening;

public class itemDrop : MonoBehaviour
{
    [SerializeField] float duration;
    Transform player;
    Tweener tween;
    bool canDestroy = false;
    void Start()
    {
        transform.DOMoveY(0.6f,duration)
        .SetLoops(-1,LoopType.Yoyo)
        .SetEase(Ease.OutQuad);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !canDestroy)
        {
            player = collider.GetComponent<Transform>();
            transform.DOMove(player.position,duration)
            .SetEase(Ease.InOutExpo)
            .OnComplete(() => 
            {
                GameManager.Instance.dropItemSize++;
                Destroy(gameObject);
            });
        }
    }

    void Update()
    {

    }
}
