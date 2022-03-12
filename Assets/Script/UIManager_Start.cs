using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
using Cinemachine;

public class UIManager_Start : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject option;
    [SerializeField] GameObject empty;
    [SerializeField] CinemachineVirtualCamera cam2;
    [SerializeField] TextMeshProUGUI loading;
    CanvasGroup startPanel;
    CanvasGroup gunPanel;
    CanvasGroup optionPanel;
    CanvasGroup emptyPanel;
    float duration = 0.25f;

    void Awake()
    {
        startPanel = start.GetComponent<CanvasGroup>();
        optionPanel = option.GetComponent<CanvasGroup>();
        emptyPanel = empty.GetComponent<CanvasGroup>();
        gunPanel = gun.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        start.SetActive(true);
        option.SetActive(false);
        gun.SetActive(false);
        empty.SetActive(false);

        startPanel.alpha = 1;
        optionPanel.alpha = 0;
        gunPanel.alpha = 0;
        emptyPanel.alpha = 0;
    }

    public void LoadGame()
    {
        option.SetActive(false);
        gun.SetActive(false);
        empty.SetActive(true);

        startPanel.DOFade(0,duration)
        .OnComplete(() => start.SetActive(false));
        emptyPanel.DOFade(1,duration)
        .OnComplete(() => StartCoroutine("LoadScene"));
    }

    public void ExitGame()
    {
        empty.SetActive(true);
        loading.text = "";
        emptyPanel.DOFade(1,duration)
        .OnComplete(() =>
        {
            Debug.Log("Quit the GAME");
            Application.Quit();
        });
        
    }

    public void OnOption()
    {
        startPanel.DOFade(0,duration)
        .OnComplete(() => start.SetActive(false));
        
        option.SetActive(true);
        optionPanel.DOFade(1,duration);
    }

    public void OnBack()
    {
        if (cam2.m_Priority != 9) cam2.m_Priority = 9;
        if (option.activeSelf)
        {
            optionPanel.DOFade(0,duration)
            .OnComplete(() => option.SetActive(false));
        }
        else if (gun.activeSelf)
        {
            gunPanel.DOFade(0,duration)
            .OnComplete(() => gun.SetActive(false));
        }
        
        start.SetActive(true);
        startPanel.DOFade(1,duration);
    }

    public void OnGunChoose()
    {
        startPanel.DOFade(0,duration)
        .OnComplete(() => start.SetActive(false));

        gun.SetActive(true);
        gunPanel.DOFade(1,duration)
        .OnComplete(() => cam2.m_Priority = 11);
    }

    IEnumerator LoadScene()
    {
        var load = SceneManager.LoadSceneAsync("Game");

        while (!load.isDone)
        {
            loading.text = $"Now Loading ({load.progress * 100} / 100)";
            yield return null;
        }
    }

}
