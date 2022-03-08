using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class UI_Manager_Game : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image crosshair;
    [SerializeField] Image hitCrosshair;
    [SerializeField] Slider playerHP_Bar;
    [SerializeField] TextMeshProUGUI playerHP;
    [SerializeField] TextMeshProUGUI Ammo;
    [SerializeField] TextMeshProUGUI Reload;
    [SerializeField] TextMeshProUGUI enegyCoreCount;
    [SerializeField] TextMeshProUGUI timer;
    [Header("Panels")]
    [SerializeField] float duration = 1;
    [SerializeField] CanvasGroup pausePanel;
    [SerializeField] CanvasGroup optionPanel;
    [SerializeField] CanvasGroup resultPanel;
    [SerializeField] CanvasGroup gameoverPanel;
    [Header("Count Down Scene")]
    [SerializeField] CanvasGroup gameStart;
    [SerializeField] TextMeshProUGUI countDown;

    [Header("Disable when Player Input")]
    [SerializeField] playerLook look;
    [SerializeField] playerMove move;
    [SerializeField] weaponAim aim;
    [SerializeField] weaponFire fire;
    float time = 0;
    float sec = 0;
    int min = 0;
    int hour = 0;
    
    async void Start()
    {
        // look.enabled = false;
        // move.enabled = false;
        // aim.enabled = false;
        // fire.enabled = false;
        gameStart.alpha = 1;
        await UniTask.Delay(1000);
        countDown.text = "3";
        await UniTask.Delay(1000);
        countDown.text = "2";
        await UniTask.Delay(1000);
        countDown.text = "1";
        await UniTask.Delay(1000);
        countDown.text = "Game Start!";
        gameStart.DOFade(0,1);
        // look.enabled = true;
        // move.enabled = true;
        // aim.enabled = true;
        // fire.enabled = true;
        playerHP_Bar.maxValue = GameManager.Instance.maxHP;
        pausePanel.alpha = 0;
        optionPanel.alpha = 0;
        resultPanel.alpha = 0;
        gameoverPanel.alpha = 0;
        GameManager.Instance.startTimer = true;
    }

    private void Update()
    {
        playerHP_Bar.value = GameManager.Instance.playerHP;
        playerHP.text = $"{GameManager.Instance.playerHP.ToString()} / {GameManager.Instance.maxHP.ToString()}";
        Ammo.text = $"Ammo: {fire.ammo.ToString()}/{fire.maxAmmo.ToString()}";
        Reload.text = $"Reload: {fire.nowReloadTime}";
        enegyCoreCount.text = $"EnegyCore: {GameManager.Instance.dropItemSize.ToString()}";

        // Open Menu Panel
        if (GameManager.Instance.menu == 1) 
        {
            look.enabled = false;
            move.enabled = false;
            aim.enabled = false;
            fire.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pausePanel.DOFade(1,duration);
        }

        // Timer
        if (GameManager.Instance.startTimer) 
        {
            time += Time.deltaTime;
            sec = time % 60;
            min = (int)time / 60 % 60;
            hour = (int)time / 60 / 60;
        }
        timer.text = $"{hour.ToString("D2")} : {min.ToString("D2")} : {sec.ToString("F2")}";
    }

    /// <summary>
    /// If is aiming, bool needs to be true.
    /// </summary>
    /// <param name="enabled"></param>
    /// <param name="time"></param>
    public void AimingCrosshair(bool enabled,float time)
    {
        if (enabled) crosshair.DOFade(0, time);
        else crosshair.DOFade(1, time);
    }

    /// <summary>
    /// If is Damaged, bool needs to be true.
    /// </summary>
    /// <param name="enabled"></param>
    /// <param name="time"></param>
    public void DamagedCrosshair(bool enabled,float time,bool FadeSoon = false)
    {
        if (FadeSoon) hitCrosshair.DOFade(0, 0);
        else
        {
            if (enabled) hitCrosshair.DOFade(1, time);
            else hitCrosshair.DOFade(0, time);
        }
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {

    }

    public void BackToGame()
    {
        GameManager.Instance.menu = 0;
        // playerInput.input.Player.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausePanel.DOFade(0,duration);
    }

    public void GoToOptions()
    {
        GameManager.Instance.menu = 0;
        // playerInput.input.Player.Disable();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pausePanel.DOFade(0,duration);
        optionPanel.DOFade(1,duration);
    }
}
