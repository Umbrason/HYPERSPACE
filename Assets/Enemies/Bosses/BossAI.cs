
using System.Linq;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] AbilityComponents;    

    private float TimeBetweenAbilities = 5f;
    private bool UsingAbility;
    private float AbilityStopTime;

    private bool CanUseAbilities => Time.fixedTime - AbilityStopTime >= TimeBetweenAbilities;

    void FixedUpdate()
    {
        if (UsingAbility || !CanUseAbilities) return;
        var availableAbilities = AbilityComponents.Select(comp => (IBossAbility)comp).Where(abl => abl.CanUse && abl.RemainingCooldown <= 0).OrderBy(_ => Random.value);
        var targetAbility = availableAbilities.FirstOrDefault();
        Debug.Log(targetAbility);
        if (targetAbility == null) return;
        UseAbility(targetAbility);
    }


    private void UseAbility(IBossAbility ability)
    {
        if (UsingAbility || !ability.CanUse) return;
        UsingAbility = true;
        AbilityStopTime = float.MaxValue;
        ability.OnUse(() => { UsingAbility = false; AbilityStopTime = Time.fixedTime; });
    }
}
