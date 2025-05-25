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

    [SerializeField] private int turns = 1;

    public int Turns
    {
        get => turns;
        set => turns = value;
    }

    [SerializeField] private int cycle = 1;

    public int Cycle
    {
        get => cycle;
        set => cycle = value;
    }

    [SerializeField] private bool pFisrt = true;
    [SerializeField] private bool pTurn = true;
    [SerializeField] private bool opTurn = false;
    [SerializeField] private bool endTurn = false;

    public bool EndTurn
    {
        get { return endTurn; }
        set { endTurn = value; }
    }

    [SerializeField] private bool canDrag = true;

    public bool CanDrag
    {
        get { return canDrag; }
        set { canDrag = value; }
    }

    [SerializeField] private float countDownTurn = 90;

    public float CountDownTurn
    {
        get { return countDownTurn; }
        set { countDownTurn = value; }
    }

    [SerializeField] private int turnCount = 1;

    public int TurnCount
    {
        get { return turnCount; }
        set { turnCount = value; }
    }

    [SerializeField] private bool botPlayed = false;

    public bool BotPlayed
    {
        get { return botPlayed; }
        set { botPlayed = value; }
    }

    [SerializeField] private DamageType playerNextDamage;

    public DamageType PlayerNextDamage
    {
        get { return playerNextDamage; }
        set { playerNextDamage = value; }
    }

    private Dictionary<TileEnum, int> tileCounter;

    public Dictionary<TileEnum, int> TileCounter => tileCounter;

    private Player player;
    private Opponent opponent;
    private PlayerBatlleMeleeAttack meleeAttack;
    private PlayerRangedAttack rangedAttack;
    private bool slashTile;
    private bool swordTile;

    [SerializeField] private bool end;
    [SerializeField] private bool show = true;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null) Debug.Log("Only 1 Battle is allowed to exist!");

        instance = this;
    }

    protected override void Start()
    {
        player = Game.Instance.Player;
        opponent = Game.Instance.Opponent;

        meleeAttack = (PlayerBatlleMeleeAttack)player.MeleeAttack;
        meleeAttack.OnMeleeHitTarget += DealTileDamage;
        rangedAttack = (PlayerBattleRangedAttack)player.RangedAttack;

        StartCoroutine(Fight());
    }

    private void Update()
    {
        if (end && show)
        {
            StopCoroutine(Fight());

            if (player.BattleStats.CurrentHP <= 0)
            {
                BattleResult.Instance.Lose.gameObject.SetActive(true);
                Game.Instance.MapData.Result = Result.LOST;
            }
            if (opponent.Stats.CurrentHP <= 0)
            {
                BattleResult.Instance.Win.gameObject.SetActive(true);
                Game.Instance.MapData.Result = Result.WIN;
            }
            BattleResult.Instance.OpacityBG.gameObject.SetActive(true);
            BattleResult.Instance.LoadMap.gameObject.SetActive(true);
            show = false;
        }
    }

    private IEnumerator Fight()
    {
        yield return new WaitForSeconds(1);

        while (player.BattleStats.CurrentHP > 0 && opponent.Stats.CurrentHP > 0)
        {
            if (pTurn)
            {
                countDownTurn = 90;
                canDrag = true;

                while (countDownTurn > 0)
                {
                    if (player.BattleStats.CurrentHP <= 0 || opponent.Stats.CurrentHP <= 0)
                    {
                        end = true;
                    }
                    if (endTurn) countDownTurn = 0;
                    countDownTurn -= Time.deltaTime;
                    CountDown.Instance.TextM.SetText($"{(int)countDownTurn}");

                    yield return null;
                }
            }

            if (player.BattleStats.CurrentHP <= 0 || opponent.Stats.CurrentHP <= 0)
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
                    if (player.BattleStats.CurrentHP <= 0 || opponent.Stats.CurrentHP <= 0)
                    {
                        end = true;
                    }
                    if (!botPlayed)
                    {
                        botPlayed = true;
                        Game.Instance.Opponent.OpponentDestroyAndFill.DestroyAndFill();
                    }

                    yield return null;
                }
            }

            if (player.BattleStats.CurrentHP <= 0 || opponent.Stats.CurrentHP <= 0)
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
            { TileEnum.VHEART, 0 }
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

        if (tileCounter[TileEnum.HEART] > 0)
        {
            if (pTurn) player.BattleStats.HPIns((int)(player.BattleStats.MaxHP * 0.005 * tileCounter[TileEnum.HEART]));
            if (opTurn) opponent.Stats.HPIns((int)(opponent.Stats.MaxHP * 0.005 * tileCounter[TileEnum.HEART]));
        }

        if (tileCounter[TileEnum.VHEART] > 0)
        {
            if (pTurn) player.BattleStats.VHPIns((int)(player.BattleStats.MaxHP * 0.01 * tileCounter[TileEnum.VHEART]));
            if (opTurn) opponent.Stats.VHPIns((int)(opponent.Stats.MaxHP * 0.01 * tileCounter[TileEnum.VHEART]));
        }

        if (tileCounter[TileEnum.MANA] > 0)
        {
            if (pTurn) player.BattleStats.ManaIns((int)(player.BattleStats.ManaRegen * tileCounter[TileEnum.MANA]));
            if (opTurn) opponent.Stats.ManaIns((int)(opponent.Stats.ManaRegen * tileCounter[TileEnum.MANA]));
        }

        if (tileCounter[TileEnum.SHEILD] > 0)
        {
            if (pTurn) player.BattleStats.SheildStack(tileCounter[TileEnum.SHEILD]);
            if (opTurn) opponent.Stats.SheildStack(tileCounter[TileEnum.SHEILD]);
        }

        if (tileCounter[TileEnum.SLASH] > 0)
        {

            if (pTurn)
            {
                slashTile = true;

                if (player.SwapWeapon.CurrentAttackRange == AttackRange.Melee)
                {
                    yield return StartCoroutine(meleeAttack.BattleMeleeAttack());
                }

                if (player.SwapWeapon.CurrentAttackRange == AttackRange.Ranged)
                {
                    yield return StartCoroutine(rangedAttack.BattleRangedAttack());
                }

                slashTile = false;
            }
        }

        if (tileCounter[TileEnum.SWORD] > 0)
        {
            if (pTurn)
            {
                StartCoroutine(player.Swordrain.SpawnSword(tileCounter[TileEnum.SWORD]));

                swordTile = true;

                if (player.SwapWeapon.CurrentAttackRange == AttackRange.Melee)
                {
                    yield return StartCoroutine(meleeAttack.BattleMeleeAttack());
                }

                if (player.SwapWeapon.CurrentAttackRange == AttackRange.Ranged)
                {
                    yield return StartCoroutine(rangedAttack.BattleRangedAttack());
                }

                swordTile = false;
            }
        }
    }

    public void DealSwordrainDamage(IEntityBattleStats dealer, IEntityBattleStats receiver)
    {
        int swordrainDamage = dealer.DamageCalculate(dealer.SwordrainDamage, receiver);
        int receiverVHP = receiver.VHP;
        int lostHP;

        if (swordrainDamage > receiverVHP)
        {
            lostHP = swordrainDamage - receiverVHP;
        }
        else
        {
            lostHP = 0;
        }

        receiver.VHPDes(swordrainDamage);

        receiver.HPDes(lostHP);

    }

    public void DealFlyObjectDamage(IEntityBattleStats dealer, IEntityBattleStats receiver)
    {
        int tileNum = 0;

        if (slashTile) tileNum = tileCounter[TileEnum.SLASH];
        if (swordTile) tileNum = tileCounter[TileEnum.SWORD];

        int swordrainDamage = dealer.DamageCalculate(tileNum * dealer.SwordrainDamage, receiver);
        int receiverVHP = receiver.VHP;
        int lostHP;

        if (swordrainDamage > receiverVHP)
        {
            lostHP = swordrainDamage - receiverVHP;
        }
        else
        {
            lostHP = 0;
        }

        receiver.VHPDes(swordrainDamage);

        receiver.HPDes(lostHP);
    }

    public void DealTileDamage()
    {
        int tileNum = 0;

        if (slashTile) tileNum = tileCounter[TileEnum.SLASH];
        if (swordTile) tileNum = tileCounter[TileEnum.SWORD];

        int pSlashDamage = player.BattleStats.DamageCalculate(tileNum * player.BattleStats.SlashDamage, opponent.Stats);
        int botVHP = opponent.Stats.VHP;
        int lostHP;

        if (pSlashDamage > botVHP)
        {
            lostHP = pSlashDamage - botVHP;
        }
        else
        {
            lostHP = 0;
        }

        opponent.Stats.VHPDes(pSlashDamage);
        opponent.Stats.HPDes(lostHP);

        playerNextDamage = DamageType.SlashDamage;
    }
}