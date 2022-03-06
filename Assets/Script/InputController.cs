using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] weaponFire gun;
    [SerializeField] weaponAim aim;
    playerMove move;
    playerLook look;
    PlayerInput input;
    private void Awake()
    {
        TryGetComponent<PlayerInput>(out input);
        TryGetComponent<playerMove>(out move);
        TryGetComponent<playerLook>(out look);
    }

    void OnEnable()
    {
        input.actions["Fire"].started += OnFire;
        input.actions["Reload"].started += OnReload;
        input.actions["Pause"].started += OnPause;
        input.actions["Sprint"].started += OnSprint;
        input.actions["Sprint"].canceled += OnSprint;
        input.actions["Look"].performed += OnLook;
        // input.actions["Jump"].started += OnJump;
    }



    void OnDisable()
    {
        input.actions["Fire"].started -= OnFire;
        input.actions["Reload"].started -= OnReload;
        input.actions["Pause"].started += OnPause;
        input.actions["Sprint"].started -= OnSprint;
        input.actions["Sprint"].canceled -= OnSprint;
        // input.actions["Jump"].started -= OnJump;
        input.actions["Look"].performed -= OnLook;
    }

    void OnJump(InputAction.CallbackContext obj)
    {
        move.Jump();
    }

    void Update()
    {
        var direction = input.actions["Move"].ReadValue<Vector2>();
        var setDirection = new Vector3(direction.x,0,direction.y);
        move.Move(setDirection);
        if (input.actions["Jump"].phase == InputActionPhase.Performed) move.Jump();
    }

    void OnPause(InputAction.CallbackContext obj)
    {

    }

    void OnReload(InputAction.CallbackContext obj)
    {
        gun.ReloadStart();
    }

    void OnFire(InputAction.CallbackContext obj)
    {
        gun.Fire();
    }

    void OnLook(InputAction.CallbackContext obj)
    {
        var direction = obj.ReadValue<Vector2>();
        look.Look(direction);
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
