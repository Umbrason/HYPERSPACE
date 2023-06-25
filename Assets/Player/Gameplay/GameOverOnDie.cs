
using System;
using UnityEngine;

public class GameOverOnDie : MonoBehaviour
{
    [SerializeField] private Health health;

    public GameObject GameOverCanvas;


    private Guid PauseHandle;

    void Start()
    {
        if (health) health.Die += GameOver;
    }

    void GameOver()
    {
        PauseHandle = PauseManager.Pause();
        GameOverCanvas.SetActive(true);
    }

    void OnDestroy()
    {
        if (health) health.Die -= GameOver;
        PauseManager.Resume(PauseHandle);
    }
}
