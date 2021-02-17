using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class PlayerSFX : MonoBehaviour
{
    public MaterialID materialID;
    
   [Header("Footstep Sounds")]
    //public List<AudioClip> defaultFootsteps = new List<AudioClip>();
    public List<PlayerSFXMaterials> footSteps = new List<PlayerSFXMaterials>();
    public List<AudioClip> currentFootSteps = new List<AudioClip>();

    [Header("Jump Sounds")]
    public List<PlayerSFXMaterials> jumpSounds = new List<PlayerSFXMaterials>();
    public List<AudioClip> currentJumpSounds = new List<AudioClip>();

    [Header("Landing Sounds")]
    public List<PlayerSFXMaterials> landingSounds = new List<PlayerSFXMaterials>();
    public List<AudioClip> currentLandingSounds = new List<AudioClip>();

    private AudioSource playerSFXEmitter;

    // Start is called before the first frame update
    void Start()
    {
        playerSFXEmitter = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            NewListOfAudioClips(footSteps , currentFootSteps);
           
        }
    }

    public void PlayOneShot(AudioClip _audioClip)
    {
        playerSFXEmitter.PlayOneShot(_audioClip);
    }

    public void SelectClip()
    {

    }

    private void NewListOfAudioClips(List<PlayerSFXMaterials> _clipLists , List<AudioClip> _currentAudioClipList)
    {
        _currentAudioClipList.Clear();

        foreach (PlayerSFXMaterials TypeOfFootstep in _clipLists)
        {
            if (TypeOfFootstep.customMaterialD == materialID)
            {
                for (int i = 0; i < TypeOfFootstep.audioClips.Count; i++)
                {
                    _currentAudioClipList.Add(TypeOfFootstep.audioClips[i]);
                }
            }
        }
    }
}
