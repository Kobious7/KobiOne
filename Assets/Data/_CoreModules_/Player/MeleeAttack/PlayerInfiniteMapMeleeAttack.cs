using System.Collections;
using UnityEngine;

public class PlayerInfiniteMapMeleeAttack : PlayerMeleeAttack
{
    [SerializeField] private float timer = 1;
    [SerializeField] private float counter = 0;
    [SerializeField] private bool canAttack;

    private void Update()
    {
        if(Player.IsUIOpening) return;
        
        CountTime();

        if (InputManager.Instance.Fire1 > 0 && canAttack)
        {
            InfiniteMapMeleeAttack();
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

    protected override void InfiniteMapMeleeAttack()
    {
        canAttack = false;
        counter = 0;
        Player.Anim.MeleeAttack();
    }

    public override IEnumerator CheckHit()
    {
        Collider[] monsters = Physics.OverlapSphere(Player.CenterPoint.transform.position, range, layerMask);

        if (monsters.Length > 0)
        {
            PlayerEventsInAnim.Instance.Hit = true;
        }

        foreach (Collider monster in monsters)
        {
            Debug.Log("" + monster.transform.name);

            yield return new WaitForSeconds(1.5f);

            LoadScene("Battle");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(Player.CenterPoint.transform.position, range);
    }
}