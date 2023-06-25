
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbar : MonoBehaviour
{
    public Image BarTemplate;
    public Health playerHealth;

    private const float RedTintDuration = IFrames.ImmunityDuration;

    private Image[] bars;

    void Start()
    {
        RefreshBarCount();
        playerHealth.OnHealthChanged += UpdateBars;
        lastHealth = playerHealth.MaxHP;

    }

    void OnDestroy()
    {
        playerHealth.OnHealthChanged -= UpdateBars;
    }


    private Color GoneHealthColor = new Color(.4f, .4f, .4f, 1f);
    private Color DefaultHealthColor = new Color(.8867924f, .8867924f, .8867924f, 1);
    private Color RecentlyLostHealthColor = Color.Lerp(Color.gray, Color.red, .5f);

    private float LastHitTime;
    private bool dirty;
    void Update()
    {
        if (!dirty) return;
        if (Time.time > LastHitTime + RedTintDuration)
            for (int i = 0; i < playerHealth.MaxHP; i++)
                bars[i].color = i < playerHealth.HP ? DefaultHealthColor : GoneHealthColor;
    }


    private int lastHealth;
    void UpdateBars(int health)
    {
        RefreshBarCount();
        LastHitTime = Time.time;
        dirty = true;
        for (int i = 0; i < playerHealth.MaxHP; i++)
            bars[i].color = i < playerHealth.HP ? DefaultHealthColor : i >= lastHealth ? GoneHealthColor : RecentlyLostHealthColor;
        lastHealth = health;
    }

    void RefreshBarCount()
    {
        //DestroyDummy
        for (int i = 1; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        bars = new Image[playerHealth.MaxHP];
        for (int i = 0; i < playerHealth.MaxHP; i++)
            bars[i] = Instantiate(BarTemplate, transform);
    }
}
