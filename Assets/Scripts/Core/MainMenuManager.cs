using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public List<Button> levels;

    void Start()
    {
        int unlocked_levels = 0;
        PlayerData d = SaveSystem.LoadPlayer();
        if(d != null)
        {
            unlocked_levels = d.max_level;
        }
        for (int i = 0; i < levels.Count; i++)
        {
            if(i <= unlocked_levels)
            {
                levels[i].interactable = true;
            }
            else
            {
                levels[i].interactable = false;
            }
        }
    }

    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void LoadLevelNum(int level_num)
    {
        PlayerData d = SaveSystem.LoadPlayer();
        if (d != null)
        {
            SaveSystem.SavePlayer(level_num, d.max_level);
        }
        else
        {
            SaveSystem.SavePlayer(level_num, level_num);
        }
        LoadScene("MasterScene");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
