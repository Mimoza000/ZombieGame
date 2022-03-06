using UnityEngine;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;
public class CountDownManager : MonoBehaviour
{
    [Header("CountDown Screen")]
    [SerializeField] CanvasGroup gameStart;
    [SerializeField] TextMeshProUGUI countDown;
    [SerializeField] GameObject[] disabledGameObjectList;
    [SerializeField] MonoBehaviour playerInput;
    float count = 4;
    void Start()
    {
        gameStart.alpha = 1;
    }

    void Update()
    {
        if (count == -1) 
        {
            foreach (GameObject gameObject in disabledGameObjectList)
            {
                gameObject.SetActive(true);
            }
            foreach (MonoBehaviour script in disabledScriptList)
            {
                script.enabled = true;
            }
        }
        if (count <= -1)  return;
        switch ((int)count)
        {
            case 3:
                countDown.text = "3";
                break;
            case 2:
                countDown.text = "2";
                break;
            case 1:
                countDown.text = "1";
                break;
            case 0:
                countDown.text = "Game Start!";
                gameStart.DOFade(0,1);
                GameManager.Instance.startTimer = true;
                break;
            default :
                break;
        }
        count -= Time.deltaTime;
    }
}
