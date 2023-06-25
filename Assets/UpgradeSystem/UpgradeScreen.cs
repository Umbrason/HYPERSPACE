
using System;
using UnityEngine;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private WaveManager manager;
    private WaveManager.ContinueWaves continueCallback;
    public GameObject UpgradeCanvas;
    [SerializeField] private UpgradeCard[] cards;
    [SerializeField] private Upgrade[] upgradePool;


    void Start()
    {
        if (manager) manager.OnWaveFinished += OnWaveEnd;
        for (int i = 0; i < cards.Length; i++)
        {
            var index = i;
            cards[i].OnClick += () => PickUpgrade(index);
        }
    }

    void OnDestroy()
    {
        if (manager) manager.OnWaveFinished -= OnWaveEnd;
        for (int i = 0; i < cards.Length; i++)
        {
            var index = i;
            cards[i].OnClick -= () => PickUpgrade(index);
        }
    }

    private Guid pauseHandle;
    void OnWaveEnd(WaveManager.ContinueWaves continueCallback)
    {
        this.continueCallback = continueCallback;
        pauseHandle = PauseManager.Pause();
        UpgradeCanvas.SetActive(true);
        currentOffers = upgradePool.RandomElementsNoDuplicates(cards.Length);
        for (int i = 0; i < currentOffers.Length; i++)
        {
            cards[i].Icon = currentOffers[i].Icon;
            cards[i].Text = currentOffers[i].Text;
        }
    }

    private Upgrade[] currentOffers;
    void PickUpgrade(int index)
    {
        if (currentOffers == null) return;
        currentOffers[index].Apply();
        currentOffers = null;
        continueCallback.Invoke();
        PauseManager.Resume(pauseHandle);
        UpgradeCanvas.SetActive(false);
    }
}
