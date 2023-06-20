using UnityEngine;
using UnityEngine.Rendering;

public class PauseBackgroundBlur : MonoBehaviour
{
    [SerializeField] private float fadeOutDuration = .3f;
    [SerializeField] private AnimationCurve fadeOutCurve = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private Volume DOFVolume;

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
        DOFVolume.weight = 0;
    }

    private float PauseStartTime = float.MaxValue;
    void Update()
    {
        var t = Time.unscaledTime - PauseStartTime;
        t /= fadeOutDuration;
        if (t < 0 || t >= 1) return;
        DOFVolume.weight = fadeOutCurve.Evaluate(t);
    }
}
