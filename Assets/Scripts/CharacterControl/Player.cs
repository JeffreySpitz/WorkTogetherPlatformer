using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputMaster controls;
    public float run_speed = 500f;
    public float jump_force = 10f;

    private Rigidbody rb;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Jump.performed += _ => Jump();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        var move_x = controls.Player.Movement.ReadValue<float>();
        rb.AddForce(Vector3.right * move_x * run_speed, ForceMode.Force);
    }
}
