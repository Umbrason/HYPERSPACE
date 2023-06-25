
using UnityEngine;

public class Godmode : MonoBehaviour
{
    [SerializeField] private IFrames Iframes;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioClip enterSFX;
    [SerializeField] private AudioClip exitSFX;

    void Start()
    {
        SpaceshipInputHandler.OnGodmodeToggled += Toggle;
    }

    void OnDestroy()
    {
        SpaceshipInputHandler.OnGodmodeToggled -= Toggle;
    }
    
    void Toggle()
    {
        if (!Iframes.godmode) SFXSource.PlayOneShot(enterSFX);
        else SFXSource.PlayOneShot(exitSFX);
        Iframes.godmode = !Iframes.godmode;
    }
}
