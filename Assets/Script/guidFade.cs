using DG.Tweening;
using UnityEngine;

public class guidFade : MonoBehaviour
{
    [SerializeField] CanvasGroup guidText;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) guidText.DOFade(1,0.25f);
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player")) guidText.DOFade(0,0.5f);
    }
}
