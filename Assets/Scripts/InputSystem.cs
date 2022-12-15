using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Has more one than Input System:" + Instance + ", ", transform);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

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
}
