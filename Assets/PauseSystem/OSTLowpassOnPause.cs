using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class OSTLowpassOnPause : MonoBehaviour
{
    [SerializeField] private float fadeOutDuration = .3f;
    [SerializeField] private AnimationCurve fadeOutCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private AudioMixer Mixer;


    const float minHz = 500;
    const float maxHz = 22000;

    void OnEnable()
    {
        PauseManager.OnPause += Pause;
        PauseManager.OnResume += Resume;
    }

    void OnDisable()
    {
        PauseManager.OnPause -= Pause;
        PauseManager.OnResume -= Resume;
    }

    private void Pause()
    {
        PauseStartTime = Time.unscaledTime;
    }

    private void Resume()
    {
        PauseStartTime = float.MaxValue;
        Mixer.SetFloat("OSTLowpass", maxHz);
    }

    private float PauseStartTime = float.MaxValue;
    void Update()
    {
        var t = Time.unscaledTime - PauseStartTime;
        t /= fadeOutDuration;
        if (t < 0 || t >= 1) return;
        var hz = Mathf.Lerp(maxHz, minHz, fadeOutCurve.Evaluate(t));
        Mixer.SetFloat("OSTLowpass", hz);
    }
}
