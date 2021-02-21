using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int next_level;
    public int max_level;

    public PlayerData(int player_next_level, int player_max_level)
    {
        if (player_next_level != null)
            next_level = player_next_level;
        if (player_max_level != null)
            max_level = player_max_level;
    }
}
