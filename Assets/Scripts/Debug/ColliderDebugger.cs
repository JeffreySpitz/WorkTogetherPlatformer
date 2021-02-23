using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderDebugger : MonoBehaviour
{
    public LayerMask red_layers;
    public LayerMask green_layers;
    public LayerMask blue_layers;

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        GameObject[] game_obj_arr = FindObjectsOfType<GameObject>();
        List<GameObject> game_object_targets = new List<GameObject>();
        foreach (GameObject obj in game_obj_arr)
        {
            if (red_layers == (red_layers | (1 << obj.layer)) ||
                green_layers == (green_layers | (1 << obj.layer)) ||
                blue_layers == (blue_layers | (1 << obj.layer)) ||
                obj.GetComponent<PlayerTransformTrigger>() != null)
            {
                // object is in target layers
                MeshRenderer mr = obj.GetComponent<MeshRenderer>();
                if (mr != null && !mr.isVisible)
                {
                    // object has a mesh renderer and it is invisible
                    game_object_targets.Add(obj);
                }
                else if (mr == null)
                {
                    game_object_targets.Add(obj);
                }
            }
        }
        foreach (GameObject obj in game_object_targets)
        {
            if (red_layers == (red_layers | (1 << obj.layer)))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(obj.transform.position, obj.transform.lossyScale);
            }
            else if (green_layers == (green_layers | (1 << obj.layer)))
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(obj.transform.position, obj.transform.lossyScale);
            }
            else if (blue_layers == (blue_layers | (1 << obj.layer)))
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(obj.transform.position, obj.transform.lossyScale);
            }
            else if (obj.GetComponent<PlayerTransformTrigger>() != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(obj.transform.position, obj.transform.lossyScale);
            }
        }
#endif
    }
}
