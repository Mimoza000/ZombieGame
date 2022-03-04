using UnityEngine;
using DG.Tweening;

public class chestUI : MonoBehaviour
{
    [SerializeField] float duration = 0.4f;
    [SerializeField] CanvasGroup canvas;
    void Start()
    {
        canvas.alpha = 0;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            canvas.DOFade(1,duration);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            canvas.DOFade(0,duration);
        }
    }
}
