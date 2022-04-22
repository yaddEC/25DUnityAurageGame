using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerType : MonoBehaviour
{
    public string GamepadType;
    private void Start()
    {
        var gamepad = Gamepad.current;
        //Debug.Log(gamepad);
        string[] types = { "PS", "XBOX"};
        string[] triggers = { "shock", };

        foreach (var trigger in triggers)
        {
            bool contains = gamepad.ToString().Contains(trigger, StringComparison.OrdinalIgnoreCase);
            if (contains && trigger == triggers[0]) GamepadType = types[0];
        }
    }
}
public static class StringExtensions
{
    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source?.IndexOf(toCheck, comp) >= 0;
    }
}