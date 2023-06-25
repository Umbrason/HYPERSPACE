
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DestroyOnDie : MonoBehaviour
{
    [SerializeField] private Transform Debris;
    [SerializeField] private Transform SFX;
    [SerializeField] private float DebrisLingerTime = 2f;

    private Health cached_Health;
    private Health Health => cached_Health ??= GetComponent<Health>();

    void Start()
    {
        if (Health != null) Health.Die += OnDie;
    }

    void OnDestroy()
    {
        if (Health != null) Health.Die -= OnDie;
    }

    void OnDie()
    {
        if (Debris != null)
        {
            Debris.SetParent(null);
            Debris.gameObject.SetActive(true);
            Destroy(Debris.gameObject, DebrisLingerTime);
        }
        if (SFX != null)
        {
            SFX.SetParent(null);
            Destroy(SFX.gameObject, 3f);
        }
        Destroy(this.gameObject);
    }
}
