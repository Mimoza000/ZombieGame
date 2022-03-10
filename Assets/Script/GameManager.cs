using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [HideInInspector] public int playerHP = 0;
    public int maxHP = 100;
    [Header("Value")]
    [HideInInspector] public bool startTimer = false;
    [HideInInspector] public int dropItemSize = 0;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        playerHP = maxHP;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
 