using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    //[Header("Inputs")]
    public Vector2 moveInputs { get; private set; }
    public Vector2 lookInputs { get; private set; }

    public bool MoveIsPressed = false;
    public bool InvertMouseY { get; private set; }

    PlayerControls input;

    private void OnEnable()
    {
        input = new PlayerControls();
        input.Gameplay.Enable();

        input.Gameplay.Move.performed += SetMove;
        input.Gameplay.Move.canceled += SetMove;

        input.Gameplay.Look.performed += SetLook;
        input.Gameplay.Look.canceled += SetLook;
    }

    private void OnDisable()
    {
        input.Gameplay.Move.performed -= SetMove;
        input.Gameplay.Move.canceled -= SetMove;

        input.Gameplay.Look.performed -= SetLook;
        input.Gameplay.Look.canceled -= SetLook;

        input.Gameplay.Disable();
    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        moveInputs = ctx.ReadValue<Vector2>();
        MoveIsPressed = !(moveInputs == Vector2.zero);
    }

    private void SetLook(InputAction.CallbackContext ctx)
    {
        lookInputs = ctx.ReadValue<Vector2>();
    }
}
