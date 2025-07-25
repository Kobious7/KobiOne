using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : GMono
{
    private static Battle instance;

    public static Battle Instance => instance;

    public event Action OnTurnChange;
    public event Action OnCycleChange;
    public event Action OnPlayerLost;
    public event Action OnMonsterLost;
    [SerializeField] private int turns = 1;
    [SerializeField] private int cycle = 1;
    [SerializeField] private bool pFisrt = true;
    [SerializeField] private bool pTurn = true;
    [SerializeField] private bool opTurn = false;
    public bool OpTurn => opTurn;
    [SerializeField] private bool endTurn = false;
    [SerializeField] private bool canDrag = true;
    [SerializeField] private float countDownTurn = 90;
    [SerializeField] private int turnCount = 1;
    [SerializeField] private bool botPlayed = false;
    [SerializeField] private DamageType playerNextDamage;
    [SerializeField] private bool playerCrit;
    private Dictionary<TileEnum, int> tileCounter;
    public Dictionary<TileEnum, int> TileCounter => tileCounter;
    private BPlayer player;
    private BMonster monster;
    private BattleManager battleManager;
    private bool slashTile;
    private bool swordTile;
    [SerializeField] private bool end;
    [SerializeField] private bool show = true;
    [SerializeField] private int collectedExp = 0;

    #region Battle element getters and setters
    public int Turns
    {
        get => turns;
        set => turns = value;
    }

    public int Cycle
    {
        get => cycle;
        set => cycle = value;
    }

    public bool EndTurn
    {
        get { return endTurn; }
        set { endTurn = value; }
    }

    public bool CanDrag
    {
        get { return canDrag; }
        set { canDrag = value; }
    }

    public float CountDownTurn
    {
        get { return countDownTurn; }
        set { countDownTurn = value; }
    }

    public int TurnCount
    {
        get { return turnCount; }
        set { turnCount = value; }
    }

    public bool BotPlayed
    {
        get { return botPlayed; }
        set { botPlayed = value; }
    }

    public DamageType PlayerNextDamage
    {
        get { return playerNextDamage; }
        set { playerNextDamage = value; }
    }

    public bool PlayerCrit
    {
        get { return playerCrit; }
        set { playerCrit = value; }
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.Log("Only 1 Battle is allowed to exist!");

        instance = this;
    }

    protected override void Start()
    {
        battleManager = BattleManager.Instance;
        player = battleManager.Player;
        monster = battleManager.Monster;
        player.MeleeAttack.OnMeleeHitTarget += DealTileDamage;
        monster.MeleeAttack.OnMeleeHitTarget += DealTileDamage;

        StartCoroutine(Fight());
    }

    private void Update()
    {
        if (end && show)
        {
            StopCoroutine(Fight());

            if (player.Stats.CurrentHP <= 0)
            {
                OnPlayerLost?.Invoke();
                BattleResult.Instance.Lose.gameObject.SetActive(true);
                battleManager.MapData.Result = Result.LOST;
            }
            if (monster.Stats.CurrentHP <= 0)
            {
                OnMonsterLost?.Invoke();
                BattleResult.Instance.Win.gameObject.SetActive(true);
                battleManager.MapData.Result = Result.WIN;
            }

            battleManager.MapData.PlayerInfo.ExpFromBattle += collectedExp;

            BattleResult.Instance.OpacityBG.gameObject.SetActive(true);
            BattleResult.Instance.LoadMap.gameObject.SetActive(true);
            show = false;
        }
    }

    private IEnumerator Fight()
    {
        yield return new WaitForSeconds(1);

        while (player.Stats.CurrentHP > 0 && monster.Stats.CurrentHP > 0)
        {
            if (pTurn)
            {
                countDownTurn = 90;
                canDrag = true;

                while (countDownTurn > 0)
                {
                    if (player.Stats.CurrentHP <= 0 || monster.Stats.CurrentHP <= 0)
                    {
                        end = true;
                    }
                    if (endTurn) countDownTurn = 0;
                    countDownTurn -= Time.deltaTime;
                    CountDown.Instance.TextM.SetText($"{(int)countDownTurn}");

                    yield return null;
                }
            }

            if (player.Stats.CurrentHP <= 0 || monster.Stats.CurrentHP <= 0)
            {
                end = true;
            }
            else
            {
                pTurn = false;
                opTurn = true;
                endTurn = false;
                turnCount = 1;

                if (!pFisrt)
                {
                    cycle++;
                    OnCycleChange?.Invoke();
                }
            }


            if (opTurn)
            {
                while (!endTurn)
                {
                    if (player.Stats.CurrentHP <= 0 || monster.Stats.CurrentHP <= 0)
                    {
                        end = true;
                    }
                    if (!botPlayed)
                    {
                        botPlayed = true;
                        battleManager.Monster.DestroyAndFill.DestroyAndFill();
                    }

                    yield return null;
                }
            }

            if (player.Stats.CurrentHP <= 0 || monster.Stats.CurrentHP <= 0)
            {
                end = true;
            }
            else
            {
                pTurn = true;
                opTurn = false;
                endTurn = false;
                turnCount = 1;
                if (pFisrt)
                {
                    cycle++;
                    OnCycleChange?.Invoke();
                }
            }

            yield return null;
        }
    }

    public void NewTileCounter()
    {
        tileCounter = new()
        {
            { TileEnum.SWORD, 0 },
            { TileEnum.SLASH, 0 },
            { TileEnum.MANA, 0 },
            { TileEnum.SHEILD, 0 },
            { TileEnum.HEART, 0 },
            { TileEnum.VHEART, 0 },
            { TileEnum.EXP, 0 }
        };
    }

    public void TurnChange()
    {
        if (opTurn) botPlayed = false;

        if (turnCount <= 0) endTurn = true;
        else
        {
            if (pTurn)
            {
                canDrag = true;
                countDownTurn = 90;
            }

            if (opTurn) botPlayed = false;
        }

        turns++;
        OnTurnChange?.Invoke();
    }

    public IEnumerator TileHandling()
    {
        //foreach(KeyValuePair<TileEnum, int> keyValuePair in tileCounter)
        //{
        //    Debug.Log($"{keyValuePair.Key} = {keyValuePair.Value}");
        //}
        if (tileCounter[TileEnum.EXP] > 0)
        {
            if (pTurn)
            {
                int flatExp = player.Stats.ExpFlatBonus * tileCounter[TileEnum.EXP];
                int amount = (int)(flatExp + flatExp * player.Stats.ExpPercentBonus / 100);
                collectedExp += amount;
                player.Stats.EXPIns(amount);
            }
        }

        if (tileCounter[TileEnum.HEART] > 0 && !end)
        {
            if (pTurn)
            {
                int insAmount = (int)(player.Stats.MaxHP * 0.005 * tileCounter[TileEnum.HEART]);
                player.Stats.HPIns(insAmount);
            }

            if (opTurn)
            {
                int insAmount = (int)(monster.Stats.MaxHP * 0.005 * tileCounter[TileEnum.HEART]);
                monster.Stats.HPIns(insAmount);
            }
        }

        if (tileCounter[TileEnum.VHEART] > 0 && !end)
        {
            if (pTurn)
            {
                int insAmount = (int)(player.Stats.MaxHP * 0.01 * tileCounter[TileEnum.VHEART]);
                player.Stats.VHPIns(insAmount);
            }
            if (opTurn)
            {
                int insAmount = (int)(monster.Stats.MaxHP * 0.01 * tileCounter[TileEnum.VHEART]);
                monster.Stats.VHPIns(insAmount);
            }
        }

        if (tileCounter[TileEnum.MANA] > 0 && !end)
        {
            if (pTurn) player.Stats.ManaIns((int)(player.Stats.ManaRegen * tileCounter[TileEnum.MANA]));
            if (opTurn) monster.Stats.ManaIns((int)(monster.Stats.ManaRegen * tileCounter[TileEnum.MANA]));
        }

        if (tileCounter[TileEnum.SHEILD] > 0 && !end)
        {
            if (pTurn) player.Stats.SheildStack(tileCounter[TileEnum.SHEILD]);
            if (opTurn) monster.Stats.SheildStack(tileCounter[TileEnum.SHEILD]);
        }

        if (tileCounter[TileEnum.SLASH] > 0 && !end)
        {

            if (pTurn)
            {
                slashTile = true;

                if (player.AttackRange.Current == AttackRange.Melee)
                {
                    yield return StartCoroutine(player.MeleeAttack.MeleeAttack());
                }

                if (player.AttackRange.Current == AttackRange.Ranged)
                {
                    yield return StartCoroutine(player.RangedAttack.RangedAttack());
                }

                slashTile = false;
            }

            if (opTurn)
            {
                slashTile = true;

                if (monster.AttackRange.Current == AttackRange.Melee)
                {
                    yield return StartCoroutine(monster.MeleeAttack.MeleeAttack());
                }

                if (monster.AttackRange.Current == AttackRange.Ranged)
                {
                    yield return StartCoroutine(monster.RangedAttack.RangedAttack());
                }

                slashTile = false;
            }
        }

        if (tileCounter[TileEnum.SWORD] > 0 && !end)
        {
            if (pTurn)
            {
                StartCoroutine(player.Swordrain.SpawnSword(tileCounter[TileEnum.SWORD]));

                swordTile = true;

                if (player.AttackRange.Current == AttackRange.Melee)
                {
                    yield return StartCoroutine(player.MeleeAttack.MeleeAttack());
                }

                if (player.AttackRange.Current == AttackRange.Ranged)
                {
                    yield return StartCoroutine(player.RangedAttack.RangedAttack());
                }

                swordTile = false;
            }

            if (opTurn)
            {
                StartCoroutine(monster.Swordrain.SpawnSword(tileCounter[TileEnum.SWORD]));

                swordTile = true;

                if (monster.AttackRange.Current == AttackRange.Melee)
                {
                    yield return StartCoroutine(monster.MeleeAttack.MeleeAttack());
                }

                if (monster.AttackRange.Current == AttackRange.Ranged)
                {
                    yield return StartCoroutine(monster.RangedAttack.RangedAttack());
                }

                swordTile = false;
            }
        }

        BSkill.Instance.SkillActivator.IsCasting = false;
    }

    public void DealSwordrainDamage(BEntityStats dealer, BEntityStats receiver)
    {
        if (receiver.ShieldStack > 0)
        {
            StartCoroutine(receiver.ShieldBreak());
            return;
        }
        
        dealer.DealDamage(dealer.SwordrainDamage, receiver, DamageType.SwordrainDamage);

        playerNextDamage = DamageType.SwordrainDamage;
    }

    public void DealTileDamage(BEntityStats dealer, BEntityStats receiver)
    {
        if (receiver.ShieldStack > 0)
        {
            StartCoroutine(receiver.ShieldBreak());
            return;
        }

        int tileNum = 0;

        if (slashTile) tileNum = tileCounter[TileEnum.SLASH];
        if (swordTile) tileNum = tileCounter[TileEnum.SWORD];

        dealer.DealDamage(tileNum * dealer.SlashDamage, receiver, DamageType.SlashDamage);

        playerNextDamage = DamageType.SlashDamage;
    }
}