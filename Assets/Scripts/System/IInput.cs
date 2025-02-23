using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class IInput
{
    public static float HorizontalAxis()
    {
        return Input.GetAxis("Horizontal");
    }
    public static float HorizontalAxisRaw()
    {
        return Input.GetAxisRaw("Horizontal");
    }
    public static float VerticalAxis()
    {
        return Input.GetAxis("Vertical");
    }
    public static float VerticalAxisRaw()
    {
        return Input.GetAxisRaw("Vertical");
    }
    public static bool FireKeyDown()
    {
        return Input.GetMouseButtonDown(0);
    }
    public static bool FireKey()
    {
        return Input.GetMouseButton(0);
    }
    public static bool DashKeyDown()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }
    public static Vector2 AimingPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public static int WeaponChoose()
    {
        int val = -1;
        for(int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown((KeyCode)(49 + i))) val = i;
        }
        return val;
    }
    public static bool ReloadKey()
    {
        return Input.GetKey(KeyCode.R);
    }
}
