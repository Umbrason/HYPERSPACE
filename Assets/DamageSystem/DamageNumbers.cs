using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Pool;
public class DamageNumbers : MonoBehaviour
{
    public float Duration = .5f;
    public AnimationCurve HeightCurve = AnimationCurve.Constant(0, 1, 0);

    private static DamageNumbers Instance;
    void Awake() => Instance = this;
    public TMP_Text textTemplate;
    private ObjectPool<TMP_Text> m_textPool;
    private ObjectPool<TMP_Text> TextPool => m_textPool ??=
    new(createFunc: () => { var instance = Instantiate<TMP_Text>(textTemplate); instance.transform.SetParent(this.transform); return instance; },
        actionOnGet: (text) => text.gameObject.SetActive(true),
        actionOnRelease: (text) => text.gameObject.SetActive(false),
        defaultCapacity: 100);

    private static List<DamageNumberInstance> activeTexts = new();

    public static void Spawn(int number, Vector3 position)
    {
        var text = Instance.TextPool.Get();
        text.text = number.ToString();
        activeTexts.Add(new(text, position, Time.time));
    }

    private struct DamageNumberInstance
    {
        public TMP_Text text;
        public Vector3 startPos;
        public float startTime;

        public DamageNumberInstance(TMP_Text text, Vector3 startPos, float startTime)
        {
            this.text = text;
            this.startPos = startPos;
            this.startTime = startTime;
        }
    }


    void Update()
    {
        foreach (var textInstance in activeTexts)
        {
            var t = (Time.time - textInstance.startTime) / Duration;
            if (t >= 1) { TextPool.Release(textInstance.text); continue; }
            var h = HeightCurve.Evaluate(t);
            textInstance.text.transform.position = textInstance.startPos + Vector3.up * h;
        }
        activeTexts.RemoveAll(instance => instance.startTime + Duration <= Time.time);
    }
}