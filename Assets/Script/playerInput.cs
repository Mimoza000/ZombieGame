using UnityEngine;
using UnityEngine.InputSystem;

public class playerInput : MonoBehaviour
{
    public static Inputs input;

    private void Awake()
    {
        input = new Inputs();

        input.Enable();

        input.Player.Look.performed += ctx => LookInput(ctx.ReadValue<Vector2>());
        input.Player.Move.performed += ctx => MoveInput(ctx.ReadValue<Vector2>());
        input.Player.Jump.performed += ctx => JumpInput(ctx.ReadValue<float>());

        input.Player.Move.canceled += ctx => MoveInput(Vector2.zero);
        input.Player.Jump.canceled += ctx => JumpInput(0);
        // extension
        input.Player.Sprint.performed += ctx => SprintInput(ctx.ReadValue<float>());
        input.Player.Sprint.canceled += ctx => SprintInput(0);
        input.Player.Aim.performed += ctx => AimInput(ctx.ReadValue<float>());
        input.Player.Aim.canceled += ctx => AimInput(0);
        input.Player.Fire.performed += ctx => FireInput(ctx.ReadValue<float>());
        input.Player.Fire.canceled += ctx => FireInput(0);
        input.Player.Reload.performed += ctx => ReloadInput(ctx.ReadValue<float>());
        input.Player.Reload.canceled += ctx => ReloadInput(0);
        input.Player.Execute.performed += ctx => ExcuteInput(ctx.ReadValue<float>());
        input.Player.Execute.canceled += ctx => ExcuteInput(0);
        input.Player.Inventory.performed += ctx => InventoryInput(ctx.ReadValue<float>());
        input.Player.Inventory.canceled += ctx => InventoryInput(0);

        // Menu
        input.Menu.Pause.performed += ctx => MenuInput(ctx.ReadValue<float>());
        // input.Menu.Click.performed += ctx 

        // Debug
        //input.Player.LookDebug.performed += ctx => LookInput(ctx.ReadValue<Vector2>()/);
        //input.Player.LookDebug.canceled += ctx => LookInput(Vector2.zero);
    }

    void LookInput(Vector2 value)
    {
        GameManager.Instance.mouseX = value.x;
        GameManager.Instance.mouseY = value.y;
    }

    void MoveInput(Vector2 value)
    {
        GameManager.Instance.vertical = value.y;
        GameManager.Instance.horizotal = value.x;
    }

    void JumpInput(float value)
    {
        GameManager.Instance.jump = value;
    }
    // extension
    void SprintInput(float value)
    {
        GameManager.Instance.sprint = value;
    }
    void AimInput(float value)
    {
        GameManager.Instance.aim = value;
    }
    void FireInput(float value)
    {
        GameManager.Instance.fire = value;
    }
    void ReloadInput(float value)
    {
        GameManager.Instance.reload = value;
    }
    void ExcuteInput(float value)
    {
        GameManager.Instance.excute = value;
    }
    void InventoryInput(float value)
    {
        GameManager.Instance.inventory = value;
    }
    void MenuInput(float value)
    {
        GameManager.Instance.menu = value;
    }
}
