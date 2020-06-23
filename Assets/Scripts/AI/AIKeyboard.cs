using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIKeyboard
{
    private Dictionary<KeyCode, bool> keyboard;
    public AIKeyboard()
    {
        keyboard = new Dictionary<KeyCode, bool>();
        keyboard[KeyCode.Q] = false;
    }

    public bool GetKey(KeyCode key)
    {
        return keyboard[key];
    }

    public void ClickKey(KeyCode key)
    {
        keyboard[key] = true;
    }

    public void LiftKey(KeyCode key)
    {
        keyboard[key] = false;
    }
}
