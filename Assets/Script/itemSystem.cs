using UnityEngine;
using DG.Tweening;

public class itemSystem : MonoBehaviour
{
    [SerializeField] itemStatus status;
    float duration = 10;
    Vector3 player;
    bool xClear = false;
    bool yClear = false;
    bool zClear = false;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            player = collider.transform.position;
            transform.DOMoveX(player.x,duration)
            .SetEase(Ease.InOutSine)
            .SetLink(this.gameObject)
            .OnComplete(() => xClear = true);

            transform.DOMoveZ(player.z,duration)
            .SetEase(Ease.InOutSine)
            .SetLink(this.gameObject)
            .OnComplete(() => zClear = true);

            transform.DOMoveY(0.3f,duration)
            .SetEase(Ease.InOutSine)
            .SetLink(this.gameObject)
            .OnComplete(() => yClear = true);
        }
    }

    void Update()
    {
        if (xClear && yClear && zClear)
        {
            Debug.Log("All checks are CLEAR");
            // Add to Inventory
            // Destroy(this.gameObject);
        }
    }
}
