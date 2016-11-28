using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public AudioSource musicChannel;
    public AudioSource efxChannel1; //footsteps
    public AudioSource efxChannel2; //Piece Sounds
    public AudioSource efxChannel3; //Wrong Move/Right move
    public AudioSource efxChannel4; //Obelisk magic
    public AudioSource efxChannel5; //Shots hitting Player
    public AudioSource efxChannel6; //Menu Stuff, Miscellaneous

    public AudioClip rotatePieceSound;
    public AudioClip rotationRejectSound;
    public AudioClip rotationAcceptSound;
    public AudioClip obeliskActivateSound;
    public AudioClip removePieceSound;
    public AudioClip footStepSound;
    public float footStepSoundCoolDownTime = 0.1f;
    float footStepSoundCoolDownTimeCurrent;
    public static SoundManager instance; 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        footStepSoundCoolDownTimeCurrent = footStepSoundCoolDownTime;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Will be called from our other scripts
    //sounds will be played in 1 of 6 different channels
    public void PlaySound(int channel, AudioClip clip)
    {
        switch (channel)
        {
            case 1:
                efxChannel1.clip = clip;
                efxChannel1.Play();
                break;
            case 2:
                efxChannel2.clip = clip;
                efxChannel2.Play();
                break;
            case 3:
                efxChannel3.clip = clip;
                efxChannel3.Play();
                break;
            case 4:
                efxChannel4.clip = clip;
                efxChannel4.Play();
                break;
            case 5:
                efxChannel5.clip = clip;
                efxChannel5.Play();
                break;
            case 6:
                efxChannel6.clip = clip;
                efxChannel6.Play();
                break;
        }

    }


    //Overloaded function, you can modify the volume of the sound with this
    public void PlaySound(int channel, int volume,  AudioClip clip)
    {
        switch (channel)
        {
            case 1:
                efxChannel1.clip = clip;
                efxChannel1.volume = volume/100.0f;
                efxChannel1.Play();
                break;
            case 2:
                efxChannel2.clip = clip;
                efxChannel2.volume = volume/100.0f;
                efxChannel2.Play();
                break;
            case 3:
                efxChannel3.clip = clip;
                efxChannel3.volume = volume / 100.0f;
                efxChannel3.Play();
                break;
            case 4:
                efxChannel4.clip = clip;
                efxChannel4.volume = volume / 100.0f;
                efxChannel4.Play();
                break;
            case 5:
                efxChannel5.clip = clip;
                efxChannel5.volume = volume / 100.0f;
                efxChannel5.Play();
                break;
            case 6:
                efxChannel6.clip = clip;
                efxChannel6.volume = volume / 100.0f;
                efxChannel6.Play();
                break;
        }

    }
    public void setMusic(AudioClip clip) {
        musicChannel.clip = clip;
    }

    public void setMusic(AudioClip clip, int volume)
    {
        musicChannel.volume = volume;
        musicChannel.clip = clip;
    }

    public void pauseMusic() {
        musicChannel.Pause();
    }
    public void unpauseMusic()
    {
        musicChannel.UnPause();
    }

    //Set a music first
    public void playMusic()
    {
        musicChannel.Play();
    }
    public void stopMusic()
    {
        musicChannel.Stop();
    }

    public void rotatePiece() {
        PlaySound(2,100, rotatePieceSound);
    }

    public void rejectPiece()
    {
        PlaySound(3, 100, rotationRejectSound);
    }

    public void acceptPiece()
    {
        PlaySound(3, 100, rotationAcceptSound);
    }

    public void removePiece()
    {
        PlaySound(2, 100, removePieceSound);
    }

    public void activateObelisk() {
        PlaySound(4, 100, obeliskActivateSound);

    }

    public void footStep() {
        if (footStepSoundCoolDownTimeCurrent <= 0)
        {
            footStepSoundCoolDownTimeCurrent = footStepSoundCoolDownTime;
            PlaySound(1, 20, footStepSound);
        }
        else {
            footStepSoundCoolDownTimeCurrent -= Time.deltaTime;
        }
    }

    public void stopStep()
    {
        efxChannel1.Stop();
    }
}
