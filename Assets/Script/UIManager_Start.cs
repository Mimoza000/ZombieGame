using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class UIManager_Start : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject option;
    [SerializeField] GameObject empty;
    [SerializeField] TextMeshProUGUI loading;
    CanvasGroup startPanel;
    CanvasGroup optionPanel;
    CanvasGroup emptyPanel;
    float duration = 0.25f;

    void Awake()
    {
        startPanel = start.GetComponent<CanvasGroup>();
        optionPanel = option.GetComponent<CanvasGroup>();
        emptyPanel = empty.GetComponent<CanvasGroup>();
    }

    void Start()
    {
        start.SetActive(true);
        option.SetActive(false);
        empty.SetActive(false);

        startPanel.alpha = 1;
        optionPanel.alpha = 0;
        emptyPanel.alpha = 0;
    }

    public void LoadGame()
    {
        option.SetActive(false);
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

    public void OnStart()
    {
        optionPanel.DOFade(0,duration)
        .OnComplete(() => option.SetActive(false));
        start.SetActive(true);
        startPanel.DOFade(1,duration);
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
