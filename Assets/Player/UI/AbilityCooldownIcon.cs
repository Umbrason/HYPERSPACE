using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownIcon : MonoBehaviour
{
    [SerializeField] private Image FillImageColored;
    [SerializeField] private Image BGImage;
    [SerializeField] private TMP_Text Text;

    [SerializeField] private MonoBehaviour AbilityComponent;
    private IPlayerAbilityWithCooldown Ability => AbilityComponent as IPlayerAbilityWithCooldown;

    void Update()
    {
        if (Ability == null) return;
        if (Ability.InUse) // in Use
        {
            FillImageColored.gameObject.SetActive(true);
            BGImage.color = new Color(1, 1, 1, .333f);
            FillImageColored.rectTransform.offsetMax = new(FillImageColored.rectTransform.offsetMax.x, ((RectTransform)FillImageColored.rectTransform.parent).rect.height);
            Text.enabled = false;
            return;
        }
        var t = Ability.CurrentCooldown / Ability.CooldownDuration;
        var CooldownActive = t > 0;
        if (FillImageColored.gameObject.activeSelf != CooldownActive)
            FillImageColored.gameObject.SetActive(CooldownActive);
        Text.enabled = CooldownActive;
        BGImage.color = CooldownActive ? new Color(1, 1, 1, .333f) : new Color(1, 1, 1, .666f);
        if (!CooldownActive) return;
        Text.text = $"{Mathf.CeilToInt(Ability.CurrentCooldown)}";
        FillImageColored.rectTransform.offsetMax = new(FillImageColored.rectTransform.offsetMax.x, ((RectTransform)FillImageColored.rectTransform.parent).rect.height * (1 - t));
    }
}
