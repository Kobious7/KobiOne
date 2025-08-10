using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class IMPlayerMeleeAttack : EntityComponent, IEntityMeleeAttack
{
    [SerializeField] protected float range = 1;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] private float timer = 1;
    [SerializeField] private float counter = 0;
    [SerializeField] private bool canAttack;
    private IMPlayer player;
    private IMPlayerAnim anim;

    protected override void Start()
    {
        base.Start();

        player = Entity as IMPlayer;
        anim = Entity.Anim as IMPlayerAnim;
    }

    private void Update()
    {
        if (InfiniteMapManager.Instance.IsUIOpening) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        CountTime();

        if (InputManager.Instance.Fire1 > 0 && canAttack)
        {
            MeleeAttack();
        }
    }

    private void CountTime()
    {
        if (counter >= timer)
        {
            canAttack = true;
            return;
        }

        counter += Time.deltaTime;
    }

    protected void MeleeAttack()
    {
        canAttack = false;
        counter = 0;

        anim.MeleeAttack();
    }

    public IEnumerator CheckHit()
    {
        Collider[] monsters = Physics.OverlapSphere(player.CenterPoint.position, range, layerMask);

        if (monsters.Length > 0)
        {
            IMMonster monsterCom = monsters[0].transform.parent.GetComponent<IMMonster>();
            IMMonsterAnim monsterAnim = monsterCom.Anim as IMMonsterAnim;
            PlayerAnimationEvents.Instance.Hit = true;
            monsterAnim.BeingHit();
            yield return new WaitForSeconds(1f);

            monsterCom.IsBeingHit = true;

            player.CanLockMovement = true;
            player.CallOnBattlePreparingEvent(monsterCom);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(Entity.CenterPoint.position, range);
    }
}