using TMPro;
using UnityEngine;

public class CombatText : GMono
{
    [SerializeField] private TextMeshProUGUI combatText;
    [SerializeField] private RectTransform rectTransform;
    public float moveSpeed = 50f;
    public float fadeDuration = 1f;

    private float timer;
    private float fadeStartTime;
    private Color originalColor;
    private Vector2 moveDirection;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTMP();
        LoadRectTransform();
    }

    private void LoadTMP()
    {
        if (combatText != null) return;

        combatText = GetComponent<TextMeshProUGUI>();
    }

    private void LoadRectTransform()
    {
        if (rectTransform != null) return;

        rectTransform = GetComponent<RectTransform>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        timer = 0;
        fadeStartTime = fadeDuration * 0.6f;

        float xOffset = Random.Range(-30f, 30f);
        float yOffset = Random.Range(-10f, 10f);
        rectTransform.anchoredPosition += new Vector2(xOffset, yOffset);

        float randomSpeed = Random.Range(moveSpeed * 0.8f, moveSpeed * 1.2f);
        moveDirection = new Vector2(0f, randomSpeed);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        rectTransform.anchoredPosition += moveDirection * Time.deltaTime;

        if (timer >= fadeStartTime)
        {
            float fadeTime = timer - fadeStartTime;
            float fadeDurationOnly = fadeDuration - fadeStartTime;

            float alpha = Mathf.Clamp01(1f - (fadeTime / fadeDurationOnly));
            combatText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }

        if (timer >= fadeDuration)
        {
            CombatTextSpawner.Instance.Despawn(transform);
        }
    }

    public void SetDamageText(int damage, DamageType damageType, bool crit)
    {
        combatText.fontSize = 14;

        combatText.text = $"{damage}";

        if (damageType == DamageType.SlashDamage)
        {
            combatText.color = ColorUtility.TryParseHtmlString("#CD5C5C", out Color color) ? color : Color.white;
        }

        if (damageType == DamageType.SwordrainDamage)
        {
            combatText.color = ColorUtility.TryParseHtmlString("#E47FFF", out Color color) ? color : Color.white;
        }

        if (crit)
        {
            combatText.fontSize = 28;
        }
    }

    public void SetNoneDamageText(int amount, NonDamageType noneDamageType)
    {
        combatText.fontSize = 22;
        combatText.text = $"+{amount}";

        if (noneDamageType == NonDamageType.HP)
        {
            combatText.color = ColorUtility.TryParseHtmlString("#32CD32", out Color color) ? color : Color.white;
        }

        if (noneDamageType == NonDamageType.VHP)
        {
            combatText.color = ColorUtility.TryParseHtmlString("#D3D3D3", out Color color) ? color : Color.white;
        }

        if (noneDamageType == NonDamageType.SHIELD)
        {
            combatText.text = $"+{amount} Shield";
            combatText.color = Color.white;
        }

        if (noneDamageType == NonDamageType.MANA)
        {
            combatText.color = ColorUtility.TryParseHtmlString("#3399FF", out Color color) ? color : Color.white;
        }

        if (noneDamageType == NonDamageType.EXP)
        {
            combatText.text = $"+{amount}Exp";
            combatText.color = ColorUtility.TryParseHtmlString("#ADFF2F", out Color color) ? color : Color.white;
        }
    }
}