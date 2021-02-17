using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] bool debugOn = false;
    public MaterialID materialID;
    

    [Header("Current PlayerSFX")] 
    [SerializeField] List<AudioClip> currentFootSteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> currentJumpSounds = new List<AudioClip>();
    [SerializeField] List<AudioClip> currentLandingSounds = new List<AudioClip>();

    [Header("Footstep Sounds")]
    //public List<AudioClip> defaultFootsteps = new List<AudioClip>();
    public List<PlayerSFXMaterials> footSteps = new List<PlayerSFXMaterials>();
    [SerializeField] int footstepLastOmitValue = 1;
    
    [Header("Jump Sounds")]
    public List<PlayerSFXMaterials> jumpSounds = new List<PlayerSFXMaterials>();
    [SerializeField] int jumpSoundLastOmitValue = 1;

    [Header("Landing Sounds")]
    public List<PlayerSFXMaterials> landingSounds = new List<PlayerSFXMaterials>();
    [SerializeField] int landingSoundLastOmitValue = 1;

    private AudioSource playerSFXEmitter;
    private AudioClip nextClipToPlay = null;

    void Start()
    {
        playerSFXEmitter = GetComponent<AudioSource>();
        NewListsForEachPlayerSFX();
        
    }
    void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            FootstepPlayerSFX();
        }

        if (Input.GetKeyDown("o"))
        {
            JumpSoundsPlayerSFX();
        }
        if (Input.GetKeyDown("i"))
        {
            LandingSoundsPlayerSFX();
        }


    }


    #region public voids for calling playerSFX
    // public voids for calling playerSFX
    public void FootstepPlayerSFX()
    {
        SelectAndPlayNextClip(currentFootSteps, footstepLastOmitValue);
    }
    public void JumpSoundsPlayerSFX()
    {
        SelectAndPlayNextClip(currentJumpSounds, jumpSoundLastOmitValue);
    }
    public void LandingSoundsPlayerSFX()
    {
        SelectAndPlayNextClip(currentLandingSounds, landingSoundLastOmitValue);
    }
    #endregion


    private void NewListsForEachPlayerSFX()
    {
        NewListOfAudioClips(footSteps, currentFootSteps);
        NewListOfAudioClips(jumpSounds, currentJumpSounds);
        NewListOfAudioClips(landingSounds, currentLandingSounds);

        if(currentFootSteps.Count == 0 || currentJumpSounds.Count == 0 || currentLandingSounds.Count == 0)
        {
            materialID = MaterialID.Default;
            NewListOfAudioClips(footSteps, currentFootSteps);
            NewListOfAudioClips(jumpSounds, currentJumpSounds);
            NewListOfAudioClips(landingSounds, currentLandingSounds);

            if(debugOn)
            {
                Debug.Log("One of the current PlayerSFX was empty, so all PlayerSFX are set to Default Material");
            }
        }

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
    public void SelectAndPlayNextClip(List<AudioClip> _audioClipList , int _lastOmitValue)
    {
        if(_audioClipList.Count == 0) 
        { 
            if(debugOn)
            {
                Debug.Log("PLAYERSFX - _audioClipList is empty - so RETURNED");
            }
            return; 
        }

        if(_audioClipList.Count == 1)
        {
            playerSFXEmitter.PlayOneShot(_audioClipList[0]);
            return;
        }

        int i = Random.Range(_lastOmitValue, _audioClipList.Count);

        nextClipToPlay = _audioClipList[i];
        _audioClipList.RemoveAt(i);
        _audioClipList.Insert(0, nextClipToPlay);


        if(nextClipToPlay == null)
        {
            if(debugOn)
            {
                Debug.Log("PLAYER SFX - next clip to play == null - so has just been RETURNED");
            }

            return;
        }
        playerSFXEmitter.PlayOneShot(nextClipToPlay);
        nextClipToPlay = null;


    }
   
}
