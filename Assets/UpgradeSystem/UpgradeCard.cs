
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private TMP_Text m_Text;
    [SerializeField] private Image m_Icon;

    public Sprite Icon { get => m_Icon.sprite; set { m_Icon.sprite = value; } }
    public string Text { get => m_Text.text; set { m_Text.text = value; } }

    public event Action OnClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
            OnClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = 1.2f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        targetRotation = Vector3.zero;
        targetScale = 1f;
    }

    void OnDisable()
    {
        targetScale = 1f;
        currentScale = 1f;

        targetRotation = default;
        currentRotation = default;
    }

    private float targetScale = 1f;
    private float currentScale = 1f;
    private Vector3 targetRotation;
    private Vector3 currentRotation;
    void Update()
    {
        currentRotation = Vector3.MoveTowards(currentRotation, targetRotation, 180f * Time.unscaledDeltaTime);
        transform.rotation = Quaternion.Euler(currentRotation);

        currentScale = Mathf.MoveTowards(currentScale, targetScale, 1 * Time.unscaledDeltaTime);
        transform.localScale = Vector3.one * currentScale;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        var rect = ((RectTransform)transform).rect;
        var localPos = eventData.position - (Vector2)transform.position;
        var normalizedPos = ((Vector2)localPos - rect.min) / rect.size;
        targetRotation = new(Mathf.Lerp(10, -10, normalizedPos.y), Mathf.Lerp(-15, 15, normalizedPos.x), 0);
        //transform.rotation = ;
    }

}
