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
    [SerializeField] GameObject empty;
    [SerializeField] CinemachineVirtualCamera cam2;
    [SerializeField] TextMeshProUGUI loading;
    CanvasGroup startPanel;
    CanvasGroup gunPanel;
    CanvasGroup emptyPanel;
    float duration = 0.25f;

    void Awake()
    {
        startPanel = start.GetComponent<CanvasGroup>();
        emptyPanel = empty.GetComponent<CanvasGroup>();
        gunPanel = gun.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        start.SetActive(true);
        gun.SetActive(false);
        empty.SetActive(false);

        startPanel.alpha = 1;
        gunPanel.alpha = 0;
        emptyPanel.alpha = 0;
    }

    public void LoadGame()
    {
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

    public void OnBack()
    {
        if (cam2.m_Priority != 9) cam2.m_Priority = 9;
        if (gun.activeSelf)
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
        cam2.m_Priority = 11;
        gunPanel.DOFade(1,1);
    }

    IEnumerator LoadScene()
    {
        var load = SceneManager.LoadSceneAsync("Game");
        while (!load.isDone)
        {
            loading.text = $"Now Loading... {(int)(load.progress* 100)}%";
            yield return null;
        }
    }

}
