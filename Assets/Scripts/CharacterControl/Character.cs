using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public float run_speed = 500f;
    public float jump_force = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
    }

    public void Move(InputAction.CallbackContext context)
    {
        var v = context.ReadValue<Vector2>();
        rb.AddForce(Vector3.right * v.x * run_speed, ForceMode.Force);
    }
}
