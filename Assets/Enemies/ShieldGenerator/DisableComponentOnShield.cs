
using UnityEngine;

public class DisableComponentOnShield : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] components;
    private ShieldBeamTarget ShieldBeamTarget => cached_shieldBeamTarget ??= GetComponent<ShieldBeamTarget>();
    private ShieldBeamTarget cached_shieldBeamTarget;

    void Awake()
    {
        ShieldBeamTarget.OnRecieveShield += OnRecieveShield;
        ShieldBeamTarget.OnLoseShield += OnLoseShield;
    }
    void OnDestroy()
    {
        ShieldBeamTarget.OnRecieveShield -= OnRecieveShield;
        ShieldBeamTarget.OnLoseShield -= OnLoseShield;
    }

    public void OnRecieveShield()
    {
        foreach (var component in components)
            if (component != null) component.enabled = false;
    }

    public void OnLoseShield()
    {
        foreach (var component in components)
            if (component != null) component.enabled = true;
    }
}

