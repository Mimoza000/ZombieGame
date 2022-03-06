using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Input - GameMode")]
    public float vertical = 0;
    public float horizotal = 0;
    public float sprint = 0;
    public float jump = 0;
    public float mouseX = 0;
    public float mouseY = 0;
    public float aim = 0;
    public float fire = 0;
    public float reload = 0;
    public float excute = 0;
    public float inventory = 0;
    [Header("Input - MenuMode")]
    public float menu = 0;

    [Header("PlayerDefaultData")]
    public int playerHP = 0;
    public int maxHP = 100;
    [Header("Value")]
    public bool startTimer = false;
    public int dropItemSize = 0;
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        playerHP = maxHP;
    }
}
 