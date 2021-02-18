﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float run_speed = 500f;
    public float jump_force = 10f;
    public LayerMask ground_layer;
    public Transform ground_check_transform;
    public float ground_check_radius = 0.1f;
    public float jump_delay = 0.5f;

    private Rigidbody rb;
    private bool is_grounded = true;
    private bool can_jump = true;
    private bool is_jumping = false;
    private float max_horizonal_velocity = 10f;
    private float time_till_next_jump = 0.0f;

    private PlayerSFX playerSFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerSFX = FindObjectOfType<PlayerSFX>();
    }

    public void Jump()
    {
        if (can_jump)
        {
            Debug.Log("jumped");
            can_jump = false;
            is_jumping = true;
            rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
            playerSFX.JumpSoundsPlayerSFX();
        }
    }

    public void Move(float move_x)
    {
        if (rb.velocity.x > max_horizonal_velocity && move_x > 0)
            return;
        if (rb.velocity.x < -max_horizonal_velocity && move_x < 0)
            return;

        if (is_grounded)
            rb.AddForce(Vector3.right * move_x * run_speed, ForceMode.Force);
        else
            rb.AddForce(Vector3.right * move_x * run_speed * 0.75f, ForceMode.Force);
    }

    private void CheckGround()
    {
        var ground_colliders = Physics.OverlapSphere(ground_check_transform.position, ground_check_radius, ground_layer);

        if (ground_colliders.Length != 0)
            is_grounded = true;
        else
            is_grounded = false;

        if (rb.velocity.y <= 0.0f && Time.time > time_till_next_jump)
        {
            Debug.Log("jump recharged");
            is_jumping = false;
            time_till_next_jump = Time.time + jump_delay;
            
        }

        if (is_grounded && !is_jumping && !can_jump)
        {
            Debug.Log("We can jump again");
            can_jump = true;
        }

    }

    private void FixedUpdate()
    {
        CheckGround();
    }


}
