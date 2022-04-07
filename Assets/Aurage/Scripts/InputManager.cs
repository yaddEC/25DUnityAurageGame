using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Vector2 InspectorInputAxis;

    [SerializeField] private bool InspectorPerformChangePlan;
    [SerializeField] private bool InspectorPerformDash;

    [SerializeField] private bool InspectorXPressed;
    [SerializeField] private bool InspectorBPressed;

    public static bool performDash;
    public static Vector2 inputAxis;
    public static bool performX;
    public static bool performB;
    public static bool performChangePlan;
    public void onPlanInput(InputAction.CallbackContext context)
    {
        performChangePlan = context.performed;
    }
    public void onJoystickInput(InputAction.CallbackContext context)
    {
        inputAxis = context.ReadValue<Vector2>();
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        performDash = context.performed;
    }
    public void onXInput(InputAction.CallbackContext context)
    {
        performX = context.performed;
    }
    public void onBInput(InputAction.CallbackContext context)
    {
        performB = context.performed;
    }

    private void Update()
    {
        InspectorPerformDash = performDash;
        InspectorInputAxis = inputAxis;
        InspectorXPressed = performX;
        InspectorBPressed = performB;
        InspectorPerformChangePlan = performChangePlan;
    }
}
