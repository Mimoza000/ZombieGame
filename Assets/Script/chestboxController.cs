using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class chestboxController : MonoBehaviour
{
    [SerializeField] float duration = 0.4f;
    [SerializeField] UIManager_Game UIManager;
    [SerializeField] CanvasGroup excute;
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] GameObject bandagePrefab;
    [SerializeField] GameObject enegyCorePrefab;
    [SerializeField] Transform chestboxTop;
    Animator animator;
    float openDuration = 2;
    bool canInit = true;
    CanvasGroup panel;
    
    void Start()
    {
        excute.alpha = 0;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            excute.DOFade(1,duration);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            excute.DOFade(0,duration);
            if (!canInit) 
            {
                chestboxTop.DOLocalRotate(new Vector3(0,180,0),openDuration)
                .OnComplete(() => canInit = true);
                UIManager.OnItemPop(false,null);
            }
        }
    }

    public void Open()
    {
        excute.DOFade(0,duration);
        chestboxTop.DOLocalRotate(new Vector3(0,180,-80),openDuration)
        .OnComplete(() => 
        {
            
            if (canInit) 
            {
                GameObject gameobject = RandomPicker();
                itemSystem script = gameobject.GetComponent<itemSystem>();

                Instantiate(gameobject,Vector3.up + transform.position,Quaternion.identity);
                UIManager.OnItemPop(true,script.status);

                canInit = false;
            }
        });
    }

    GameObject RandomPicker()
    {
        var randomValue = (int)Random.Range(0,3);
        switch (randomValue)
        {
            case 0:
                return ammoPrefab;
            case 1:
                return bandagePrefab;
            case 2:
                return enegyCorePrefab;
            default:
                return ammoPrefab;
        }
    }
}
