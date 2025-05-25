using UnityEngine;

public class PlayerInfiniteMapRangedAttack : PlayerRangedAttack
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
            InfiniteMapRangedAttack();
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

    protected override void InfiniteMapRangedAttack()
    {
        canAttack = false;
        counter = 0;

        Player.Anim.RangedAttack();
    }
}