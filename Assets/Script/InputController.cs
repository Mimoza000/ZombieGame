using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [HideInInspector] public static InputController Instance;
    [SerializeField] playerMove move;
    [SerializeField] playerLook look;
    [SerializeField] weaponFire gun;
    [SerializeField] weaponAim aim;
    public PlayerInput input;
    [SerializeField] UIManager_Game UI;

    void Awake()
    {
        if (Instance != null) 
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        move.Move(input.actions["Move"].ReadValue<Vector2>());
        if (input.actions["Jump"].triggered) move.Gravity(true);
        else move.Gravity(false);
        look.Look(input.actions["Look"].ReadValue<Vector2>());
    }

    void OnEnable()
    {
        input.actions["Reload"].started += OnReload;
        input.actions["Pause"].started += OnUI;
        input.actions["Sprint"].started += OnSprint;
        input.actions["Sprint"].canceled += OnSprint;
        input.actions["Aim"].started += OnAim;
        input.actions["Aim"].canceled += OnAimCanceled;
        input.actions["FireMode"].started += OnFireMode;
        input.actions["Fire"].started += OnFire;
        input.actions["Fire"].canceled += OnFireCanceled;
        input.actions["Excute"].started += OnExcute;
        input.actions["Inventory"].started += OnInventory;
    }

    void OnDisable()
    {
        input.actions["Reload"].started -= OnReload;
        input.actions["Pause"].started += OnUI;
        input.actions["Sprint"].started -= OnSprint;
        input.actions["Sprint"].canceled -= OnSprint;
        input.actions["Aim"].started += OnAim;
        input.actions["Aim"].canceled += OnAimCanceled;
        input.actions["FireMode"].started -= OnFireMode;
        input.actions["Fire"].started -= OnFire;
        input.actions["Fire"].canceled -= OnFireCanceled;
        input.actions["Excute"].started -= OnExcute;
        input.actions["Inventory"].started -= OnInventory;
    }

    void OnInventory(InputAction.CallbackContext obj)
    {
        UI.OnInventory();
    }

    void OnExcute(InputAction.CallbackContext obj)
    {
        if (gun.hitInfo_0.collider.CompareTag("Chestbox"))
        {
            var chestbox = gun.hitInfo_0.collider.GetComponent<chestboxController>();
            if (chestbox != null) chestbox.Open();
        }
    }

    void OnUI(InputAction.CallbackContext obj)
    {
        UI.ToPause();
    }

    void OnFireMode(InputAction.CallbackContext obj)
    {
        gun.enableSemiAuto = !gun.enableSemiAuto;
    }

    void OnAim(InputAction.CallbackContext obj)
    {
        aim.Aim(true);
    }

    void OnAimCanceled(InputAction.CallbackContext obj)
    {
        aim.Aim(false);
    }

    void OnReload(InputAction.CallbackContext obj)
    {
        gun.ReloadStart();
    }

    void OnFire(InputAction.CallbackContext obj)
    {
        gun.Fire(true);
    }

    void OnFireCanceled(InputAction.CallbackContext obj)
    {
        gun.Fire(false);
    }

    void OnSprint(InputAction.CallbackContext obj)
    {
        switch ( obj.phase )
        {
            case InputActionPhase.Started:
                move.Sprint(true);
                break;
            case InputActionPhase.Canceled:
                move.Sprint(false);
                break;
        }
    }
}
