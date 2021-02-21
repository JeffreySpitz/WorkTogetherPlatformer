using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public InputMaster controls;
    public PlayerController player_controller;
    public Player player1;
    public Player player2;
    public List<Transform> player1_spawns;
    public List<Transform> player2_spawns;

    private bool is_controlling_player_1 = true;
    private int level_num = 0;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Switch.performed += _ => Switch();
        controls.Player.Pause.performed += _ => PauseSwitch();
    }

    void SetSpawns()
    {
        PlayerData d = SaveSystem.LoadPlayer();
        if (d == null)
        {
            SaveSystem.SavePlayer(0, 0);
            level_num = 0;
        }
        else
        {
            level_num = d.next_level;
        }
    }

    void Start()
    {
        player_controller.SwitchPlayer(player1);
        is_controlling_player_1 = true;
    }

    private void OnEnable()
    {
        controls.Enable();
        PauseController.onPause += Pause;
        PauseController.onResume += Resume;
    }

    private void OnDisable()
    {
        controls.Disable();
        PauseController.onPause -= Pause;
        PauseController.onResume -= Resume;
    }

    private void Switch()
    {
        if (is_controlling_player_1)
        {
            is_controlling_player_1 = false;
            player_controller.SwitchPlayer(player2);
        }
        else
        {
            is_controlling_player_1 = true;
            player_controller.SwitchPlayer(player1);
        }
    }

    private void Resume()
    {
        Time.timeScale = 1;
        // So we can read button presses when time is resumed
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
    }

    private void Pause()
    {
        Time.timeScale = 0;
        // So we can read button presses when time is stopped
        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
    }

    public void PauseSwitch()
    {
        if(PauseController.is_paused)
        {
            PauseController.Resume();
        }
        else
        {
            PauseController.Pause();
        }
    }
}
