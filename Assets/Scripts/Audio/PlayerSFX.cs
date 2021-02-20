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
    [SerializeField] float footstepVolume = 0.7f;
    [SerializeField] float footstepPitchMin = 0.9f;
    [SerializeField] float footstepPitchMax = 1.1f;

    [Header("Jump Sounds")]
    public List<PlayerSFXMaterials> jumpSounds = new List<PlayerSFXMaterials>();
    [SerializeField] int jumpSoundLastOmitValue = 1;
    [SerializeField] float jumpSoundVolume = 0.7f;
    [SerializeField] float jumpSoundPitchMin = 0.9f;
    [SerializeField] float jumpSoundPitchMax = 1.1f;

    [Header("Landing Sounds")]
    public List<PlayerSFXMaterials> landingSounds = new List<PlayerSFXMaterials>();
    [SerializeField] int landingSoundLastOmitValue = 1;

    [SerializeField] float landingSoundVolume = 0.7f;
    [SerializeField] float landingSoundPitchMin = 0.9f;
    [SerializeField] float landingSoundPitchMax = 1.1f;


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

        SelectAndPlayNextClip(currentFootSteps, footstepLastOmitValue, footstepVolume, footstepPitchMin, footstepPitchMax);
    }
    public void JumpSoundsPlayerSFX()
    {
        SelectAndPlayNextClip(currentJumpSounds, jumpSoundLastOmitValue, jumpSoundVolume, jumpSoundPitchMin, jumpSoundPitchMax);
    }
    public void LandingSoundsPlayerSFX()
    {
        SelectAndPlayNextClip(currentLandingSounds, landingSoundLastOmitValue, landingSoundVolume, jumpSoundPitchMin, jumpSoundPitchMax);
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

    public void SelectAndPlayNextClip(List<AudioClip> _audioClipList , int _lastOmitValue , float _volume , float _pitchMin , float _pitchMax)
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

            playerSFXEmitter.PlayOneShot(_audioClipList[0] , _volume);
            return;
        }

        int i = Random.Range(_lastOmitValue, _audioClipList.Count);

        nextClipToPlay = _audioClipList[i];
        _audioClipList.RemoveAt(i);
        _audioClipList.Insert(0, nextClipToPlay);


        playerSFXEmitter.pitch = Random.Range(_pitchMin, _pitchMax);
        if(nextClipToPlay == null)
        {
            if(debugOn)
            {
                Debug.Log("PLAYER SFX - next clip to play == null - so has just been RETURNED");
            }

            return;
        }

        playerSFXEmitter.PlayOneShot(nextClipToPlay , _volume);
        nextClipToPlay = null;


    }
   
}
