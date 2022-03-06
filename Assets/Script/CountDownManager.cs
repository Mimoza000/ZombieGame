using UnityEngine;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class CountDownManager : MonoBehaviour
{
    [Header("CountDown Screen")]
    [SerializeField] CanvasGroup gameStart;
    [SerializeField] TextMeshProUGUI countDown;
    [SerializeField] playerLook look;
    [SerializeField] playerMove move;
    [SerializeField] weaponAim aim;
    [SerializeField] weaponFire fire;
    GameObject[] enemyList;
    async void Awake()
    {
        look.enabled = false;
        move.enabled = false;
        aim.enabled = false;
        fire.enabled = false;
        gameStart.alpha = 1;
        await UniTask.Delay(1000);
        countDown.text = "3";
        await UniTask.Delay(2000);
        countDown.text = "2";
        await UniTask.Delay(3000);
        countDown.text = "1";
        await UniTask.Delay(4000);
        countDown.text = "Game Start!";
        gameStart.DOFade(0,1);
        look.enabled = true;
        move.enabled = true;
        aim.enabled = true;
        fire.enabled = true;
        GameManager.Instance.startTimer = true;
    }
}
