using UnityEngine;

public class CombatTextSpawner : Spawner
{
    private static CombatTextSpawner instance;

    public static CombatTextSpawner Instance => instance;
    private BattleManager battleManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (instance != null) Debug.LogError("Only 1 CombatTextSpawner");

        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        battleManager = BattleManager.Instance;
        battleManager.Player.Stats.OnDealDamage += SpawnDamageText;
        battleManager.Monster.Stats.OnDealDamage += SpawnDamageText;
        battleManager.Player.Stats.OnNoneDamageStatInscrease += SpawnNoneDamageText;
        battleManager.Monster.Stats.OnNoneDamageStatInscrease += SpawnNoneDamageText;
    }

    public void SpawnDamageText(int damage, BEntityStats receiver, DamageType damageType, bool crit)
    {
        Vector3 spawnPos = GetReceiverSpawnPos(receiver);

        Transform newText = Spawn(prefabs[0], spawnPos, Quaternion.identity);
        newText.transform.localScale = Vector3.one;

        CombatText combatText = newText.GetComponent<CombatText>();
        combatText.SetDamageText(damage, damageType, crit);

        newText.gameObject.SetActive(true);
    }

    public void SpawnNoneDamageText(int amount, BEntityStats receiver, NonDamageType hPType)
    {
        Vector3 spawnPos = GetReceiverSpawnPos(receiver);

        Transform newText = Spawn(prefabs[0], spawnPos, Quaternion.identity);
        newText.transform.localScale = Vector3.one;

        CombatText combatText = newText.GetComponent<CombatText>();
        combatText.SetNoneDamageText(amount, hPType);

        newText.gameObject.SetActive(true);
    }

    public Vector3 GetReceiverSpawnPos(BEntityStats receiver)
    {
        BPlayer player = receiver.GetComponentInParent<BPlayer>();
        BMonster monster = receiver.GetComponentInParent<BMonster>();

        if (player != null)
        {
            return player.TextDamagePoint.position;
        }

        if (monster != null)
        {
            return monster.TextDamagePoint.position;
        }

        return Vector3.zero;
    }
}