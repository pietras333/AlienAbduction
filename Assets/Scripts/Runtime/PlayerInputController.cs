using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private PlayerWeaponController playerWeaponController;

    public void OnMove(InputValue value)
    {
        playerController.Move(value.Get<Vector2>());
    }

    public void OnFire(InputValue value)
    {
        Debug.Log("Fire" + value.isPressed);
        playerWeaponController.Shoot(value.isPressed);
    }

    public void OnBlock(InputValue value)
    {
        Debug.Log("Block" + value.isPressed);
        playerWeaponController.Block(value.isPressed);
    }
}
