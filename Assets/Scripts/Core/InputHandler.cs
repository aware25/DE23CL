using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;


        if (rayHit.collider.gameObject.name == "continue_button")
        {
            InputDecoder.CommandLine++;
        }
        else if (rayHit.collider.gameObject.name == "button_accuse")
        {
            InputDecoder.ClearScreen();
        }
        
        //Debug.Log(rayHit.collider.gameObject.name);
    }
}
