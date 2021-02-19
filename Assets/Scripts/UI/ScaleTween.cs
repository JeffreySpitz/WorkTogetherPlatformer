using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    private float scale_x;
    private float scale_y;

    public float scale_multiplier = 1.2f;
    public float scale_time = 0.5f;

    private void Start()
    {
        scale_x = transform.localScale.x;
        scale_y = transform.localScale.y;
    }

    public void ScaleUp()
    {
        LeanTween.scale(gameObject, new Vector2(scale_x * scale_multiplier, scale_y * scale_multiplier), scale_time).setIgnoreTimeScale(true);
    }

    public void ScaleDown()
    {
        LeanTween.scale(gameObject, new Vector2(scale_x, scale_y), scale_time).setIgnoreTimeScale(true);
    }

}
