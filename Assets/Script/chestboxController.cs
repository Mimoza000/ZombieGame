using UnityEngine;
using DG.Tweening;

public class chestboxController : MonoBehaviour
{
    [SerializeField] float duration = 0.4f;
    [SerializeField] CanvasGroup canvas;
    [SerializeField] itemStatus ammo;
    [SerializeField] itemStatus bandage;
    [SerializeField] itemStatus enegyCore;
    Animator animator;
    bool canOpen;
    AnimatorStateInfo animationInfo;
    
    void Start()
    {
        canvas.alpha = 0;
        animator = GetComponent<Animator>();
        animationInfo = animator.GetCurrentAnimatorStateInfo(0);
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

    public void Open()
    {
        if (canOpen) animator.SetTrigger("open");
    }

    public void ChestOpened()
    {
        Instantiate(RandomPicker,)
    }

    GameObject RandomPicker()
    {
        var randomValue = (int)Random.Range(0,3);
        switch (randomValue)
        {
            case 0:
                return ammo.gameObject;
            case 1:
                return bandage.gameObject;
            case 2:
                return enegyCore.gameObject;
            default:
                Debug.LogWarning("Can NOT pick ID");
                return null;
        }
    }
}
