using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle
{
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
        private Bot bot;

        [SerializeField] private bool end;
        [SerializeField] private bool show = true;

        protected override void Awake()
        {
            base.Awake();
            if(instance != null) Debug.Log("Only 1 Battle is allowed to exist!");

            instance = this;
        }

        private void Update()
        {
            if(end && show)
            {
                StopCoroutine(Fight());

                if(player.Stats.CurrentHP <= 0) 
                {
                    BattleResult.Instance.Lose.gameObject.SetActive(true);
                    Game.Instance.MapData.Result = Result.LOST;
                }
                if(bot.Stats.CurrentHP <= 0)
                {
                    BattleResult.Instance.Win.gameObject.SetActive(true);
                    Game.Instance.MapData.Result = Result.WIN;
                }
                BattleResult.Instance.OpacityBG.gameObject.SetActive(true);
                BattleResult.Instance.LoadMap.gameObject.SetActive(true);
                show = false;
            }
        }

        protected override void Start()
        {
            player = Game.Instance.Player;
            bot = Game.Instance.Bot;

            StartCoroutine(Fight());
        }

        private IEnumerator Fight()
        {
            yield return new WaitForSeconds(1);

            while(player.Stats.CurrentHP > 0 && bot.Stats.CurrentHP > 0)
            {
                if(pTurn)
                {
                    countDownTurn = 90;
                    canDrag = true;

                    while(countDownTurn > 0)
                    {
                        if(player.Stats.CurrentHP <= 0 || bot.Stats.CurrentHP <= 0)
                        {
                            end = true;
                        }
                        if(endTurn) countDownTurn = 0;
                        countDownTurn -= Time.deltaTime;
                        CountDown.Instance.TextM.SetText($"{(int)countDownTurn}");
                        
                        yield return null;
                    }              
                }

                if(player.Stats.CurrentHP <= 0 || bot.Stats.CurrentHP <= 0)
                {
                    end = true;
                }
                else
                {
                    pTurn = false;
                    opTurn = true;
                    endTurn = false;
                    turnCount = 1;

                    if(!pFisrt)
                    {
                        cycle++;
                        OnCycleChange?.Invoke();
                    }
                }               


                if(opTurn)
                {
                    while(!endTurn)
                    {
                        if(player.Stats.CurrentHP <= 0 || bot.Stats.CurrentHP <= 0)
                        {
                            end = true;
                        }
                        if(!botPlayed)
                        {
                            botPlayed = true;
                            Game.Instance.Bot.BotDestroyAndFill.DestroyAndFill();
                        }

                        yield return null;
                    }
                }

                if(player.Stats.CurrentHP <= 0 || bot.Stats.CurrentHP <= 0)
                {
                    end = true;
                }
                else
                {
                    pTurn = true;
                    opTurn = false;
                    endTurn = false;
                    turnCount = 1;
                    if(pFisrt)
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
            if(opTurn) botPlayed = false;

            if(turnCount <= 0) endTurn = true;
            else
            {
                if(pTurn)
                {
                    canDrag = true;
                    countDownTurn = 90;
                }
                
                if(opTurn) botPlayed = false;
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

            if(tileCounter[TileEnum.HEART] > 0)
            {
                if(pTurn) player.Stats.HPIns((int)(player.Stats.MaxHP * 0.005 * tileCounter[TileEnum.HEART]));
                if(opTurn) bot.Stats.HPIns((int)(bot.Stats.MaxHP * 0.005 * tileCounter[TileEnum.HEART]));
            }

            if(tileCounter[TileEnum.VHEART] > 0)
            {
                if(pTurn) player.Stats.VHPIns((int)(player.Stats.MaxHP * 0.01 * tileCounter[TileEnum.VHEART]));
                if(opTurn) bot.Stats.VHPIns((int)(bot.Stats.MaxHP * 0.01 * tileCounter[TileEnum.VHEART]));
            }

            if(tileCounter[TileEnum.MANA] > 0)
            {
                if(pTurn) player.Stats.ManaIns((int)(player.Stats.ManaRegen * tileCounter[TileEnum.MANA]));
                if(opTurn) bot.Stats.ManaIns((int)(bot.Stats.ManaRegen * tileCounter[TileEnum.MANA]));
            }

            if(tileCounter[TileEnum.SHEILD] > 0)
            {
                if(pTurn) player.Stats.SheildStack(tileCounter[TileEnum.SHEILD]);
                if(opTurn) bot.Stats.SheildStack(tileCounter[TileEnum.SHEILD]);
            }

            if(tileCounter[TileEnum.SLASH] > 0)
            {
                if(pTurn)
                {
                    int pSlashDamage = player.Stats.DamageCalculate(tileCounter[TileEnum.SLASH] * player.Stats.SlashDamage, bot.Stats);
                    int botVHP = bot.Stats.VHP;
                    int lostHP;

                    if(pSlashDamage > botVHP)
                    {
                        lostHP = pSlashDamage - botVHP;                  
                    }
                    else
                    {
                        lostHP = 0;
                    }

                    yield return StartCoroutine(player.Moving.MoveToTarget());
                    yield return StartCoroutine(player.Atack.MeleeAttack());

                    bot.Stats.VHPDes(pSlashDamage);
                    bot.Stats.HPDes(lostHP);

                    playerNextDamage = DamageType.SlashDamage;

                    yield return StartCoroutine(player.Moving.MoveBack());

                    player.Anim.IdleAnim();
                }

                if(opTurn)
                {
                    int opSlashDamage = bot.Stats.DamageCalculate(tileCounter[TileEnum.SLASH] * bot.Stats.SlashDamage, player.Stats);
                    int pVHP = player.Stats.VHP;
                    int lostHP;

                    if(opSlashDamage > pVHP)
                    {
                        lostHP = opSlashDamage - pVHP;
                    }
                    else
                    {
                        lostHP = 0;
                    }

                    yield return StartCoroutine(bot.Moving.MoveToTarget());
                    yield return StartCoroutine(bot.Atack.MeleeAttack());

                    player.Stats.VHPDes(opSlashDamage);
                    player.Stats.HPDes(lostHP);

                    yield return StartCoroutine(bot.Moving.MoveBack());

                    bot.Anim.IdleAnim();
                }
            }

            if(tileCounter[TileEnum.SWORD] > 0)
            {
                if(pTurn)
                {
                    int pSlashDamage = player.Stats.DamageCalculate(tileCounter[TileEnum.SLASH] * player.Stats.SlashDamage, bot.Stats);
                    int botVHP = bot.Stats.VHP;
                    int lostHP;

                    if(pSlashDamage > botVHP)
                    {
                        lostHP = pSlashDamage - botVHP;
                    }
                    else
                    {                  
                        lostHP = 0;
                    }

                    StartCoroutine(player.ESwordrain.SpawnSword(tileCounter[TileEnum.SWORD]));

                    yield return StartCoroutine(player.Moving.MoveToTarget());
                    yield return StartCoroutine(player.Atack.MeleeAttack());

                    bot.Stats.VHPDes(pSlashDamage);
                    bot.Stats.HPDes(lostHP);

                    playerNextDamage = DamageType.SlashDamage;

                    yield return StartCoroutine(player.Moving.MoveBack());

                    player.Anim.IdleAnim();
                }

                if(opTurn)
                {
                    int opSlashDamage = bot.Stats.DamageCalculate(tileCounter[TileEnum.SLASH] * bot.Stats.SlashDamage, player.Stats);
                    int pVHP = player.Stats.VHP;
                    int lostHP;

                    if(opSlashDamage > pVHP)
                    {
                        lostHP = opSlashDamage - pVHP;
                    }
                    else
                    {
                        lostHP = 0;
                    }

                    StartCoroutine(bot.ESwordrain.SpawnSword(tileCounter[TileEnum.SWORD]));

                    yield return StartCoroutine(bot.Moving.MoveToTarget());
                    yield return StartCoroutine(bot.Atack.MeleeAttack());

                    player.Stats.VHPDes(opSlashDamage);
                    player.Stats.HPDes(lostHP);

                    yield return StartCoroutine(bot.Moving.MoveBack());

                    bot.Anim.IdleAnim();
                }
            }

            //yield return new WaitForSeconds(10);
        }

        public void DealSwordrainDamage(Entity dealer, Entity receiver)
        {
            int swordrainDamage = dealer.Stats.DamageCalculate(dealer.Stats.SwordrainDamage, receiver.Stats);
            int receiverVHP = receiver.Stats.VHP;
            int lostHP;

            if(swordrainDamage > receiverVHP)
            {
                lostHP = swordrainDamage - receiverVHP;
            }
            else
            {
                lostHP = 0;
            }

            receiver.Stats.VHPDes(swordrainDamage);

            receiver.Stats.HPDes(lostHP);

        }
    }
}