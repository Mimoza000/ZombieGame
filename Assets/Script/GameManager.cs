using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [HideInInspector] public int playerHP = 0;
    public int maxHP = 100;
    [HideInInspector] public bool startTimer = false;
    public int[] itemList = new int[3];
    
    void Awake()
    {
        if (Instance != null) 
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        playerHP = maxHP;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
 