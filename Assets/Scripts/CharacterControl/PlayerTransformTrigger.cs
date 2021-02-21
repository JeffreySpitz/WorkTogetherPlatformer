using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformTrigger : MonoBehaviour
{
    public float target_z = 0.0f;
    public float target_rot = 90.0f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.target_rotation = target_rot;
            player.target_z_coord = target_z;
            player.force_transform = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.target_rotation = 90.0f;
            player.target_z_coord = 0.0f;
            player.force_transform = false;
        }
    }
}
