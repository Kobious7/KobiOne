using System;
using System.Collections;
using UnityEngine;

public abstract class DestructiveObjectCollision : DestructiveObjectAb
{
    [SerializeField] protected Transform normalHitFX;
    [SerializeField] protected Transform currentNomarlHitFXObject;
    protected BSkill bSkill;
    protected BPlayerStats playerStats;
    protected BMonsterStats opStats;
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
        Debug.Log(DObject.Target.name);

        if (DObject.Target.name == "Tile")
        {
            if (other.transform.parent.name == "Tile")
            {
                TilePrefab tile = other.transform.parent.GetComponentInChildren<TilePrefab>();
                TilePrefab target = DObject.Target.GetComponentInChildren<TilePrefab>();

                if (tile.X == target.X && tile.Y == target.Y)
                {
                    SpawnTileHitFX(other.transform);
                    StartCoroutine(WaitHit(() =>
                    {
                        DestructiveObjectSpawner.Instance.Despawn(transform.parent);
                        DestructiveObjectSpawner.Instance.TileSpawnCount--;
                    }));
                }
            }
        }

        if (DObject.Target.parent.name == "Monster")
        {
            if (other.transform.name == "Monster")
            {
                SpawnOpHitFX(other.transform);
                StartCoroutine(WaitHit(() =>
                {
                    opAnim.BeingHit();
                    DealDamage();
                    DestructiveObjectSpawner.Instance.Despawn(transform.parent);
                    DestructiveObjectSpawner.Instance.OpSpawnCount--;
                }));
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

    protected virtual IEnumerator WaitHit(Action onFXDespawn)
    {
        BSkillFX skillFX = normalHitFX.GetComponent<BSkillFX>();

        DObject.Model.gameObject.SetActive(false);
        yield return StartCoroutine(skillFX.WaitHitFX());

        DespawnFX();
        onFXDespawn?.Invoke();

        DObject.Model.gameObject.SetActive(true);
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
        playerStats.DealDamage(rawSkillDamage, opStats);

        Debug.Log("Skill Damage");
        if (skill.skillSO is TileSkillSO tileSkillSO)
        {
            Battle.Instance.PlayerNextDamage = tileSkillSO.Damage.DamageType;
        }
        
        bSkill.SkillActivator.Activators[skill].ApplyDebuff(skill, opStats);
    }
}