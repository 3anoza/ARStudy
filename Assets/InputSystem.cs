using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{

    public static InputSystem Instance;
    public event UnityAction KeyPressed;

    private void Awake()
    {
        Instance = this;
    }
    
    void Update()
    {
        StartCoroutine(InputHandler());
    }

    IEnumerator InputHandler()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
        KeyPressed?.Invoke();
    }

    /// <summary>
    /// Check mouse left button state. Debug method
    /// </summary>
    /// <returns>True or False (<see cref="bool"/>)</returns>
    public bool IsMouseLB_Pressed()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public Vector3 GetMousePosition()
    {
       return Input.mousePosition;
    }
}
