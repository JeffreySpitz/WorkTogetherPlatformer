using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputMaster controls;
    public PlayerController player_controller;
    public Player player1;
    public Player player2;

    private bool is_controlling_player_1 = true;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Switch.performed += _ => Switch();
    }

    void Start()
    {
        player_controller.player = player1;
        is_controlling_player_1 = true;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Switch()
    {
        if (is_controlling_player_1)
        {
            is_controlling_player_1 = false;
            player_controller.player = player2;
        }
        else
        {
            is_controlling_player_1 = true;
            player_controller.player = player1;
        }
    }

}
