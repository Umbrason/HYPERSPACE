
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealerEnemy : MonoBehaviour
{
    [SerializeField] private float HealFrequency = .1f;
    public int healAmount = 5;
    public float radius = 10f;

    private float lastHealBurst = 0;
    void FixedUpdate()
    {
        if (Time.fixedTime - lastHealBurst <= 1f / HealFrequency) return;
        lastHealBurst = Time.fixedTime;

        var hits = Physics.OverlapSphere(transform.position, radius);
        var healthComponents = new HashSet<Health>();
        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject || hit.GetComponent<HealerEnemy>() != null) continue;
            var collection = hit.GetComponentsInParent<Health>().Concat(hit.GetComponentsInChildren<Health>().Where(h => h.gameObject != hit.gameObject));
            foreach (var health in collection)
                healthComponents.Add(health);
        }
        foreach (var component in healthComponents)
        {
            component.Heal(healAmount);
            Debug.Log(component);
        }
    }
}
