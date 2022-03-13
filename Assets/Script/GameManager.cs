using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;
    [SerializeField] UIManager_Game UI;
    [HideInInspector] public int playerHP = 0;
    [HideInInspector] public int maxHP = 100;
    [HideInInspector] public bool startTimer = false;
    public int[] itemList = new int[3];
    public int bandageValue;
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
        itemList[0] = 145;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnHeal()
    {
        if (itemList[1] <= 0) return;
        else if (playerHP >= maxHP) 
        {
            Debug.Log("Can not heal playerHP");
            return;
        }
        var healValue = bandageValue / itemList[1];

        itemList[1] -= 1;
        bandageValue -= healValue;
        if (itemList[1] == 1) playerHP += healValue;
        playerHP += healValue;
    }
}
 