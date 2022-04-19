using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Vector2 InspectorInputAxis;

    [SerializeField] private bool InspectorXPressed;
    [SerializeField] private bool InspectorBPressed;
    [SerializeField] private bool InspectorAPressed;
    [SerializeField] private bool InspectorYPressed;

    [SerializeField] private bool InspectorTrigger;
    [SerializeField] private bool InspectorMid;

    public static Vector2 inputAxis;

    public static bool performX;
    public static bool performB;
    public static bool performA;
    public static bool performY;

    public static bool performTrigger;

    public static bool performMid;

    public void onJoystickInput(InputAction.CallbackContext context)
    {
        inputAxis = context.ReadValue<Vector2>();
    }

    public void onTriggerInput(InputAction.CallbackContext context)
    {
        performTrigger = context.performed;
    }

    private void Update()
    {
        InspectorInputAxis = inputAxis;

        InspectorXPressed = performX;
        InspectorBPressed = performB;
        InspectorAPressed = performA;
        InspectorYPressed = performY;

        InspectorTrigger = performTrigger;
        InspectorMid = performMid;

        if (performMid) Debug.Log("ew");

    }

    public void onXInput(InputAction.CallbackContext context)
    {
        performX = context.performed;
    }
    public void onBInput(InputAction.CallbackContext context)
    {
        performB = context.performed;
    }

    public void onYInput(InputAction.CallbackContext context)
    {
        performY = context.performed;
    }

    public void onAInput(InputAction.CallbackContext context)
    {
        performA = context.performed;
    }

    public void onMidInput(InputAction.CallbackContext context)
    {
        performMid = context.performed;
    }
}
