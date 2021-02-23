using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputMaster controls;
    public Player player = null;

    public void SwitchPlayer(Player new_player)
    {
        if(player != null)
        {
            player.OnNoLongerUnderControl();
        }
        player = new_player;
        player.OnUnderControl();
        player.point_light.intensity = 1;
    }

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Jump.performed += _ => Jump();
        controls.Player.Interact.performed += _ => Interact();
    }

    void Jump()
    {
        //if (player != null)
        //{
        //    player.Jump();
        //}
    }

    void Interact()
    {
        if (player != null)
        {
            player.Interact();
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var move_2d = controls.Player.Move.ReadValue<Vector2>();
        if (player != null)
        {
            player.Move(move_2d);
        }
    }
}
