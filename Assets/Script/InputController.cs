using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] playerMove move;
    [SerializeField] playerLook look;
    [SerializeField] weaponFire gun;
    [SerializeField] weaponAim aim;
    [SerializeField] PlayerInput input;

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
        //input.actions["Pause"].started += OnPause;
        input.actions["Sprint"].started += OnSprint;
        input.actions["Sprint"].canceled += OnSprint;
        input.actions["Aim"].started += OnAim;
        input.actions["Aim"].canceled += OnAimCanceled;
        input.actions["FireMode"].started += OnFireMode;
        input.actions["Fire"].started += OnFire;
        input.actions["Fire"].canceled += OnFireCanceled;
    }



    void OnDisable()
    {
        input.actions["Reload"].started -= OnReload;
        //input.actions["Pause"].started += OnPause;
        input.actions["Sprint"].started -= OnSprint;
        input.actions["Sprint"].canceled -= OnSprint;
        input.actions["Aim"].started += OnAim;
        input.actions["Aim"].canceled += OnAimCanceled;
        input.actions["FireMode"].started -= OnFireMode;
        input.actions["Fire"].started -= OnFire;
        input.actions["Fire"].canceled -= OnFireCanceled;
    }

    void OnFireMode(InputAction.CallbackContext obj)
    {

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
        Debug.Log("NOW SPrint");
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
