
using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuCanvas;

    void Start()
    {
        SpaceshipInputHandler.OnPause += TogglePause;
    }

    void OnDestroy()
    {
        SpaceshipInputHandler.OnPause -= TogglePause;
    }

    private Guid pauseHandle;
    private bool paused;
    public void TogglePause()
    {
        paused = !paused;
        PauseMenuCanvas?.SetActive(paused);
        if (paused) pauseHandle = PauseManager.Pause();
        if (!paused) PauseManager.Resume(pauseHandle);
    }

}
