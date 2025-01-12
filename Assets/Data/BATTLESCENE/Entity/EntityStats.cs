using UnityEngine;

namespace Battle
{
    public class EntityStats : EntityAb
    {
        [SerializeField] protected int currentHP = 0;

        public int CurrentHP
        {
            get { return currentHP; }
            set { currentHP = value; }
        }

        [SerializeField] protected int maxHP = 50;

        public int MaxHP
        {
            get { return maxHP; }
            set { maxHP = value; }
        }

        [SerializeField] protected int vHP = 0;

        public int VHP
        {
            get { return vHP; }
            set { vHP = value; }
        }

        [SerializeField] protected int mana = 0;

        public int Mana
        {
            get { return mana; }
            set { mana = value; }
        }

        [SerializeField] protected int slashDamage = 2;

        public int SlashDamage
        {
            get { return slashDamage; }
            set { slashDamage = value; }
        }

        [SerializeField] protected int swordrainDamage = 1;

        public int SwordrainDamage
        {
            get { return swordrainDamage; }
            set { swordrainDamage = value; }
        }

        [SerializeField] protected int shieldCount = 0;

        public int ShieldCount
        {
            get { return shieldCount; }
            set { shieldCount = value; }
        }

        [SerializeField] protected int shieldStack = 0;

        public int ShieldStack
        {
            get { return shieldStack; }
            set { shieldStack = value; }
        }

        public void HPIns(int amount)
        {
            currentHP += amount;

            if (currentHP > maxHP) currentHP = maxHP;
        }

        public void HPDes(int amount)
        {
            currentHP -= amount;

            if (currentHP < 0) currentHP = 0;
        }

        public void VHPIns(int amount)
        {
            vHP += amount;

            if (vHP > maxHP / 2) vHP = maxHP / 2;
        }

        public void VHPDes(int amount)
        {
            vHP -= amount;

            if (VHP < 0) vHP = 0;
        }

        public void ManaIns(int percent)
        {
            mana += percent;

            if (mana > 100) mana = 100;
        }

        public void ManaDes(int percent)
        {
            mana -= percent;

            if (mana < 0) mana = 0;
        }

        public void SheildStack(int count)
        {
            shieldCount += count;

            while (shieldCount >= 2)
            {
                shieldStack++;
                shieldCount -= 2;
            }
        }
    }
}