using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public delegate void PauseAction();
    public static event PauseAction onPause;

    public delegate void ResumeAction();
    public static event ResumeAction onResume;

    public static bool is_paused = false;

    public static void Pause()
    {
        if(!is_paused)
        {
            is_paused = true;
            if(onPause != null)
            {
                onPause();
            }
        }
    }

    public static void Resume()
    {
        if(is_paused)
        {
            is_paused = false;
            if (onResume != null)
            {
                onResume();
            }
        }
    }
}
