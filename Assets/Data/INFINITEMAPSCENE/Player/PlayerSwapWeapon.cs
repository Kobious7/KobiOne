using UnityEngine;

namespace InfiniteMap
{
    public class PlayerSwapWeapon : PlayerAb
    {
        protected override void Start()
        {
            base.Start();
            Game.Instance.Inventory.EquipWearing.OnEquipWearing += SwapWeapon;

            Player.MeleeAttack.gameObject.SetActive(true);
            Player.AttackPoint.gameObject.SetActive(false);
            Player.RangedAttack.gameObject.SetActive(false);
        }

        public void SwapWeapon(InventoryEquip equip)
        {
            WeaponSO weapon = (WeaponSO)equip.EquipSO;

            switch(weapon.AttackRange)
            {
                case AttackRange.Melee:
                    Player.MeleeAttack.gameObject.SetActive(true);
                    Player.AttackPoint.gameObject.SetActive(false);
                    Player.RangedAttack.gameObject.SetActive(false);
                    break;
                case AttackRange.Ranged:
                    Player.MeleeAttack.gameObject.SetActive(false);
                    Player.AttackPoint.gameObject.SetActive(true);
                    Player.RangedAttack.gameObject.SetActive(true);
                    break;
            }
        }
    }
}