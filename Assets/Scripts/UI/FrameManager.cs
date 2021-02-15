using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public List<GameObject> frames;
    public float swap_time = 0.2f;

    private int current_frame_idx = 0;
    private float screen_shift = 2000.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set frame positions
        for(int i = 0; i < frames.Count; i++)
        {
            if(i!=0)
            {
                RectTransform target_frame = frames[i].GetComponent<RectTransform>();
                LeanTween.moveX(target_frame, screen_shift, 0);
            }
        }
    }

    private void ResetFramePositions()
    {
        // Set frame positions
        for (int i = 0; i < frames.Count; i++)
        {
            if (i != current_frame_idx)
            {
                RectTransform target_frame = frames[i].GetComponent<RectTransform>();
                LeanTween.moveX(target_frame, screen_shift, 0);
            }
        }
    }

    public void SwapFrame(int frame_idx)
    {
        // Exit current frame to the left
        if (frame_idx != current_frame_idx)
        {
            RectTransform current_frame = frames[current_frame_idx].GetComponent<RectTransform>();
            RectTransform new_frame = frames[frame_idx].GetComponent<RectTransform>();
            LeanTween.moveX(current_frame, -screen_shift, swap_time).setOnComplete(ResetFramePositions);
            LeanTween.moveX(new_frame, 0, swap_time);
            current_frame_idx = frame_idx;
        }
    }

    
}
