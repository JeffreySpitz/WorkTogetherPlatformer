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
    public bool force_transform = false;
    public float target_rotation = 90.0f;
    public float target_z_coord = 0.0f;
    public float adjust_lerp = 0.125f;

    private Rigidbody rb;
    private Animator player_animator;
    private bool is_grounded = true;
    private bool can_jump = true;
    private bool is_jumping = false;
    private bool is_climbing = false;
    private bool can_climb = false;
    private bool is_interacting = false;
    private float interact_time = 0;
    private float max_climbing_speed = 2f;
    private float time_till_next_jump = 0.0f;
    private float facing_direction = 1;

    private PlayerSFX playerSFX;
    private GameSFX gameSFX;

    private float interact_duration = 1.5f;
    private float rotate_time = 0.3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player_animator = GetComponent<Animator>();
        playerSFX = FindObjectOfType<PlayerSFX>();
        gameSFX = FindObjectOfType<GameSFX>();
    }

    public void Interact()
    {
        if (!is_interacting)
        {
            // climb if we can
            if (!is_climbing && can_climb)
            {
                is_climbing = true;
            }
            if (!is_climbing && Mathf.Abs(rb.velocity.x) < 0.1 && Mathf.Abs(rb.velocity.y) < 0.1)
            {
                interact_time = 0;
                is_interacting = true;
                player_animator.SetTrigger("pull_lever");
                StartCoroutine(InteractWait());
            }
        }
    }

    IEnumerator InteractWait()
    {
        yield return new WaitForSeconds(interact_duration);
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
            if (!is_climbing || is_grounded)
            {
                if (rb.velocity.x > 0 && facing_direction == -1 && move_x > 0)
                {
                    Flip();
                }
                else if (rb.velocity.x < 0 && facing_direction == 1 && move_x < 0)
                {
                    Flip();
                }
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
            else if (is_climbing)
            {
                // climbing on a ladder we want to get off the ladder at the earliest convenience
                rb.AddForce(transform.forward * Mathf.Abs(move_x) * run_speed, ForceMode.Force);
            }

            if (rb.velocity.x > max_horizonal_velocity && move_x > 0)
            {
                rb.AddForce(Vector3.right * -run_speed * 1.25f, ForceMode.Force);
            }
            if (rb.velocity.x < -max_horizonal_velocity && move_x < 0)
            {
                rb.AddForce(Vector3.right * run_speed * 1.25f, ForceMode.Force);
            }

            if (is_grounded && move_x == 0.0f && !is_climbing)
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
        if (is_climbing)
            is_climbing = false;
        //transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void CheckGround()
    {
        var ground_colliders = Physics.OverlapSphere(ground_check_transform.position, ground_check_radius, ground_layer);

        if (ground_colliders.Length != 0)
        {
            if (!is_grounded)
            {
                playerSFX.LandingSoundsPlayerSFX();
            }
            is_grounded = true;
        }
        else
        {
            is_grounded = false;
        }

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

        if (!can_climb)
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

    // We use this function to return the player back to the proper coordinates after interaction
    private void RestorePositionAndRotation()
    {
        if (!is_interacting && !is_climbing)
        {
            // correct rotation
            Vector3 current_rotation = transform.eulerAngles;
            float correct_rotation = 90.0f;
            if (facing_direction == -1)
                correct_rotation = 270.0f;
            if (force_transform)
                correct_rotation = target_rotation;
            Debug.Log("correct rotation " + correct_rotation + " current rotation " + current_rotation.y);
            current_rotation.y = Mathf.Lerp(current_rotation.y, correct_rotation, adjust_lerp);
            transform.eulerAngles = current_rotation;

            // correct position
            Vector3 current_position = transform.position;
            current_position.z = Mathf.Lerp(current_position.z, target_z_coord, adjust_lerp);
            transform.position = current_position;
        }

    }

    private void FixedUpdate()
    {
        CheckGround();
        CheckHorizontal();
        UpdateAnimator();
        InteractionPerformance();
        RestorePositionAndRotation();
    }

    public void InteractionPerformance()
    {
        if(is_interacting)
        {
            //interact_time += Time.deltaTime;
            //Vector3 current_rotation = transform.eulerAngles;
            //if (interact_time < rotate_time)
            //{
            //    float rotate_out = interact_time / rotate_time;
            //    float original_rotation = 90f;
            //    if (facing_direction < 0)
            //        original_rotation = 270.0f;
            //    current_rotation.y = Mathf.Lerp(original_rotation, 0, rotate_out);
            //    transform.eulerAngles = current_rotation;
            //}
            //else if (interact_time > (interact_duration - (rotate_time + 0.1f)))
            //{
            //    float rotate_in = (interact_time - (interact_duration - (rotate_time + 0.1f))) / rotate_time;
            //    Debug.Log("rotating in" + rotate_in);
            //    current_rotation.y = Mathf.Lerp(0, 90*facing_direction, rotate_in);
            //    transform.eulerAngles = current_rotation;
            //}
            //Debug.Log("interact time " + interact_time);
        }
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
