using UnityEngine;
using DG.Tweening;

public class chestboxController : MonoBehaviour
{
    [SerializeField] float duration = 0.4f;
    [SerializeField] CanvasGroup canvas;
    Animator animator;
    bool canOpen;
    void Start()
    {
        canvas.alpha = 0;
        animator = GetComponentInParent<Animator>();
        canOpen = false;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            canvas.DOFade(1,duration);
            canOpen = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            canvas.DOFade(0,duration);
            canOpen = false;
        }
    }

    void Open()
    {
        if (canOpen) 
        {
            animator.SetTrigger("open");
            
        }
    }
}
