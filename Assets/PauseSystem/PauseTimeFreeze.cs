using UnityEngine;


public class PauseTimeFreeze : MonoBehaviour
{
    [SerializeField] private float fadeOutDuration = .3f;
    [SerializeField] private AnimationCurve fadeOutCurve = AnimationCurve.Linear(0, 0, 1, 1);
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
        Time.timeScale = 1;
    }

    private float PauseStartTime = float.MaxValue;
    void Update()
    {
        var t = Time.unscaledTime - PauseStartTime;
        t /= fadeOutDuration;
        if (t >= 1) Time.timeScale = 0f;
        if (t < 0 || t >= 1) return;
        Time.timeScale = 1 - fadeOutCurve.Evaluate(t);
    }
}
