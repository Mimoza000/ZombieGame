using UnityEngine;
using DG.Tweening;

public class itemSystem : MonoBehaviour
{
    public itemStatus status;
    [HideInInspector] public Sprite sprite;
    float duration = 0.7f;
    Vector3 player;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = collider.transform.position;

            transform.DOMove(new Vector3(player.x,0.3f,player.z),duration)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject)
            .OnComplete(() => 
            {
                GameManager.Instance.itemList[status.id] += status.size;
                if (status.id == 1) GameManager.Instance.bandageValue += status.value;
                Destroy(gameObject);
            });
        }
    }
}
