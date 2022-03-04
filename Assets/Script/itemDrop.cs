using UnityEngine;
using DG.Tweening;

public class itemDrop : MonoBehaviour
{
    [SerializeField] float duration;
    GameObject player;
    Vector3 position;
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
            player = collider.GetComponent<GameObject>();
            transform.DOMove(player.transform.position,duration)
            .SetEase(Ease.InOutExpo)
            .OnComplete(() => 
            {
                GameManager.Instance.dropItemSize++;
                Destroy(gameObject);
            });
        }
    }
}
