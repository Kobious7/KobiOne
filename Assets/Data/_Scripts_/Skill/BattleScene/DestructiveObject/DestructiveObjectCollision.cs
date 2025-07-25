using System;
using System.Collections;
using UnityEngine;

public abstract class DestructiveObjectCollision : DestructiveObjectAb
{
    [SerializeField] protected Transform normalHitFX;
    [SerializeField] protected Transform currentNomarlHitFXObject;
    protected BSkill bSkill;
    protected BEntityStats playerStats;
    protected BEntityStats opStats;
    protected BMonsterAnim opAnim;

    protected override void Start()
    {
        base.Start();

        bSkill = BSkill.Instance;
        playerStats = BattleManager.Instance.Player.Stats;
        opStats = BattleManager.Instance.Monster.Stats;
        opAnim = BattleManager.Instance.Monster.Anim as BMonsterAnim;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (DObject.Target.name == "Tile")
        {
            if (other.transform.parent.name == "Tile")
            {
                TileProperties tile = other.transform.parent.GetComponentInChildren<TileProperties>();
                TileProperties target = DObject.Target.GetComponentInChildren<TileProperties>();

                if (tile.X == target.X && tile.Y == target.Y)
                {
                    SpawnTileHitFX(other.transform);
                    WaitHit(() =>
                    {
                        DestructiveObjectSpawner.Instance.Despawn(transform.parent);
                        DestructiveObjectSpawner.Instance.TileSpawnCount--;
                    });
                }
            }
        }

        if (DObject.Target.parent.name == "Monster")
        {
            if (other.transform.name == "Monster")
            {
                SpawnOpHitFX(other.transform);
                WaitHit(() =>
                {
                    opAnim.BeingHit();
                    DealDamage();
                    DestructiveObjectSpawner.Instance.Despawn(transform.parent);
                    DestructiveObjectSpawner.Instance.OpSpawnCount--;
                });
            }
        }
    }

    protected virtual void SpawnTileHitFX(Transform other)
    {
        Vector3 offsetPos = new Vector3(other.transform.parent.position.x - DObject.OffsetValue,
                                        other.transform.parent.position.y + DObject.OffsetValue,
                                        other.transform.parent.position.z);
        Transform hitFx = DestructiveObjectFXSpawner.Instance.Spawn(normalHitFX, offsetPos, Quaternion.identity);

        hitFx.gameObject.SetActive(true);

        currentNomarlHitFXObject = hitFx;
    }

    protected virtual void SpawnOpHitFX(Transform other)
    {
        Vector3 pos = other.GetComponent<BMonster>().CenterPoint.position;
        Transform hitFX = DestructiveObjectFXSpawner.Instance.Spawn(normalHitFX, pos, Quaternion.identity);

        hitFX.gameObject.SetActive(true);

        currentNomarlHitFXObject = hitFX;
    }

    protected void WaitHit(Action onFXDespawn)
    {
        BSkillFX skillFX = normalHitFX.GetComponent<BSkillFX>();

        DObject.Model.gameObject.SetActive(false);
        StartCoroutine(WaitToDespawnFX(skillFX));
        onFXDespawn?.Invoke();

        DObject.Model.gameObject.SetActive(true);
    }

    protected IEnumerator WaitToDespawnFX(BSkillFX skillFX)
    {
        yield return StartCoroutine(skillFX.WaitHitFX());
        DespawnFX();
    }

    protected virtual void DespawnFX()
    {
        DestructiveObjectFXSpawner.Instance.Despawn(currentNomarlHitFXObject);
    }

    private void DealDamage()
    {
        if (DObject.SkillButton == SkillButton.Q)
        {
            CalculateDamage(bSkill.QSkill);
        }

        if (DObject.SkillButton == SkillButton.E)
        {
            CalculateDamage(bSkill.ESkill);
        }

        if (DObject.SkillButton == SkillButton.Space)
        {
            CalculateDamage(bSkill.SpaceSkill);
        }
    }

    private void CalculateDamage(SkillNode skill)
    {
        int rawSkillDamage = bSkill.Calculator.SkillDamageCalculate(playerStats, skill);
        DamageType damageType = DamageType.None;

        Debug.Log("Skill Damage");
        if (skill.SkillSO is TileSkillSO tileSkillSO)
        {
            damageType = tileSkillSO.Damage.DamageType;
        }

        playerStats.DealDamage(rawSkillDamage, opStats, damageType);
        
        bSkill.SkillActivator.Activators[skill].ApplyDebuff(skill, opStats);
    }
}