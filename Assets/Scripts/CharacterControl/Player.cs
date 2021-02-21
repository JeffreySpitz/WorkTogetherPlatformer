using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float run_speed = 500f;
    public float max_horizonal_velocity = 10f;
    public float climb_speed = 2f;
    public float climbing_smooth_speed = 0.125f;
    public float jump_force = 10f;
    public LayerMask ground_layer;
    public LayerMask ladder_layer;
    public Transform ground_check_transform;
    public float ground_check_radius = 0.1f;
    public float jump_delay = 0.5f;
    public PhysicMaterial no_friction;
    public PhysicMaterial full_friction;
    public CapsuleCollider collider;
    public ConstantForce extra_gravity;
    public Light point_light;

    private Rigidbody rb;
    private Animator player_animator;
    private bool is_grounded = true;
    private bool can_jump = true;
    private bool is_jumping = false;
    private bool is_climbing = false;
    private bool can_climb = false;
    private bool is_interacting = false;
    private float max_climbing_speed = 2f;
    private float time_till_next_jump = 0.0f;
    private float facing_direction = 1;

    private PlayerSFX playerSFX;
    private GameSFX gameSFX;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_animator = GetComponent<Animator>();
        playerSFX = FindObjectOfType<PlayerSFX>();
        gameSFX = FindObjectOfType<GameSFX>();
    }

    public void Interact()
    {
        if (!is_climbing && Mathf.Abs(rb.velocity.x) < 0.01 && Mathf.Abs(rb.velocity.y) < 0.01)
        {
            is_interacting = true;
            player_animator.SetTrigger("pull_lever");
            StartCoroutine(InteractWait());
        }
    }

    IEnumerator InteractWait()
    {
        yield return new WaitForSeconds(1);
        is_interacting = false;
    }

    public void Jump()
    {
        if (can_jump && !is_interacting)
        {
            can_jump = false;
            is_jumping = true;
            rb.AddForce(Vector3.up * jump_force, ForceMode.Impulse);
            playerSFX.JumpSoundsPlayerSFX();
        }
    }

    public void Move(Vector2 move_2d)
    {
        if (!is_interacting)
        {
            float move_x = move_2d.x;
            float move_y = move_2d.y;
            if (rb.velocity.x > 0 && facing_direction == -1 && move_x > 0)
            {
                Flip();
            }
            else if (rb.velocity.x < 0 && facing_direction == 1 && move_x < 0)
            {
                Flip();
            }


            if (is_climbing)
            {
                rb.useGravity = false;
                extra_gravity.enabled = false;

                float current_speed = rb.velocity.y;
                float target_speed = climb_speed * move_y;
                float smoothed_velocity = Mathf.Lerp(current_speed, target_speed, climbing_smooth_speed);
                rb.velocity = new Vector3(rb.velocity.x, smoothed_velocity, rb.velocity.z);
            }
            else
            {
                rb.useGravity = true;
                extra_gravity.enabled = true;
            }

            if (is_grounded)
            {
                rb.AddForce(Vector3.right * move_x * run_speed, ForceMode.Force);
            }
            else
            {
                rb.AddForce(Vector3.right * move_x * run_speed * 0.75f, ForceMode.Force);
            }

            if (rb.velocity.x > max_horizonal_velocity && move_x > 0)
            {
                rb.AddForce(Vector3.right * -run_speed * 1.25f, ForceMode.Force);
            }
            if (rb.velocity.x < -max_horizonal_velocity && move_x < 0)
            {
                rb.AddForce(Vector3.right * run_speed * 1.25f, ForceMode.Force);
            }

            if (is_grounded && move_x == 0.0f)
            {
                collider.material = full_friction;
            }
            else
            {
                collider.material = no_friction;
            }
        }
    }

    private void Flip()
    {
        facing_direction *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
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
            is_jumping = false;

            time_till_next_jump = Time.time + jump_delay;
            
        }

        if (is_grounded && !is_jumping && !is_climbing)
        {
            can_jump = true;
        }

    }

    private void CheckHorizontal()
    {
        Debug.DrawRay(ground_check_transform.position, transform.TransformDirection(Vector3.forward) * 0.6f, Color.green, 1.0f);
        if (Physics.Raycast(ground_check_transform.position, transform.TransformDirection(Vector3.forward), 0.6f, ladder_layer))
        {
            can_climb = true;
        }
        else
        {
            can_climb = false;
        }

        if (can_climb && !is_grounded)
        {
            is_climbing = true;
        } else
        {
            is_climbing = false;
        }
    }

    private void UpdateAnimator()
    {
        player_animator.SetFloat("x_speed", Mathf.Abs(rb.velocity.x));
        player_animator.SetFloat("y_speed", Mathf.Abs(rb.velocity.y));
        player_animator.SetBool("is_climbing", is_climbing);
    }

    private void FixedUpdate()
    {
        CheckGround();
        CheckHorizontal();
        UpdateAnimator();
    }

    //AUDIO
    public void FSTrigger()
    {
        playerSFX.FootstepPlayerSFX();
    }

    public void ClimbTrigger()
    {
        playerSFX.ClimbingSoundsPlayerSFX();
        
    }

    public void CharacterButtonPress()
    {
        gameSFX.PlayGameSFX("ButtonSFX");
    }




}
