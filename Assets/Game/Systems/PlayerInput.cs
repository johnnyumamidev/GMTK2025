using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool Interaction()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
    public bool Sprint()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
    public Vector2 Movement()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        return input;
    }
}
