using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager_Game : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image crosshair;
    [SerializeField] Image hitCrosshair;
    [SerializeField] Slider playerHP_Bar;
    [SerializeField] weaponFire fire;
    [SerializeField] TextMeshProUGUI playerHP;
    [SerializeField] TextMeshProUGUI Ammo;
    [SerializeField] TextMeshProUGUI Reload;
    [SerializeField] TextMeshProUGUI enegyCoreCount;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject optionsPanel;
    float time = 0;
    int sec = 0;
    int min = 0;
    int hour = 0;

    private void Start()
    {
        playerHP_Bar.maxValue = GameManager.Instance.maxHP;
        time = 0;
    }

    private void Update()
    {
        playerHP_Bar.value = GameManager.Instance.playerHP;
        playerHP.text = $"{GameManager.Instance.playerHP.ToString()} / {GameManager.Instance.maxHP.ToString()}";
        Ammo.text = $"Ammo: {fire.ammo.ToString()}/{fire.maxAmmo.ToString()}";
        Reload.text = $"Reload: {fire.reloadingTime}";
        enegyCoreCount.text = $"EnegyCore: {GameManager.Instance.dropItemSize.ToString()}";

        // Open Menu Panel
        if (GameManager.Instance.menu == 1) 
        {
            playerInput.input.Player.Disable();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pausePanel.SetActive(true);
        }

        // Timer
        if (GameManager.Instance.startTimer) 
        {
            time += Time.deltaTime;
            sec = (int)time % 60;
            min = (int)time / 60 % 60;
            hour = (int)time / 60 / 60;
        }
        timer.text = $"{hour.ToString()} : {min.ToString()} : {sec.ToString("F2")}";
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

        public void LoadStart()
    {
        SceneManager.LoadScene(0);
    }

    public void BackToGame()
    {
        GameManager.Instance.menu = 0;
        playerInput.input.Player.Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void GoToOptions()
    {
        GameManager.Instance.menu = 0;
        playerInput.input.Player.Disable();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
}
