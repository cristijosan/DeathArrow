using System;
using UnityEngine;
using System.Collections;

public class ButtonControlMove : MonoBehaviour
{
    private FloatingJoystick floatingJoystick;

    private void Start()
    {
        floatingJoystick = gameObject.GetComponent<FloatingJoystick>();
    }

    private void FixedUpdate()
    {
        Movement.ChangeVector?.Invoke(floatingJoystick.Vertical, floatingJoystick.Horizontal);
    }

    private void LateUpdate()
    {
        if (!Input.GetMouseButton(0))
            Movement.OnRelease?.Invoke();
    }
}