#define NEW_INPUT_SYSTEM

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Has more one than Input System:" + Instance + ", ", transform);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

#if OLD_INPUT_SYSTEM
    public float MoveX()
    {
        return Input.GetAxisRaw("Horizontal");
    }
    public float MoveY()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public float Jump()
    {
        return Input.GetAxisRaw("Jump");
    }

    public float Crouch()
    {
        return Input.GetAxisRaw("Crouch");
    }

#else
    public float MoveX()
    {
        return playerInputActions.Player.Movement.ReadValue<float>();
    }
    public float MoveY()
    {
        return playerInputActions.Player.Climb.ReadValue<float>();
    }

    public float Jump()
    {
        return playerInputActions.Player.Jump.ReadValue<float>();
    }

    public float Crouch()
    {
        return playerInputActions.Player.Crouch.ReadValue<float>();
    }
#endif

}
