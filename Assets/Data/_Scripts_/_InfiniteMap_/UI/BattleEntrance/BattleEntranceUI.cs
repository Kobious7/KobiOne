using System.Collections;
using UnityEngine;

public class BattleEntranceUI : GMono
{
    private static BattleEntranceUI instance;

    public static BattleEntranceUI Instance => instance;

    [SerializeField] private BattleEntranceInfoUI playerInfo, monsterInfo;
    [SerializeField] private Animator animator;

    protected override void Awake()
    {
        if (instance != null) Debug.LogError("Only 1 BattleEntranceUI");

        instance = this;
    }

    protected override void LoadComponents()
    {
        if (playerInfo == null) playerInfo = transform.Find("Player").GetComponent<BattleEntranceInfoUI>();
        if (monsterInfo == null) monsterInfo = transform.Find("Monster").GetComponent<BattleEntranceInfoUI>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();

        InfiniteMapManager.Instance.BattleEntrance.OnBattleEntranceActivating += ShowInfos;

        this.gameObject.SetActive(false);
    }

    private void ShowInfos(IMPlayer player, IMMonster monster)
    {
        playerInfo.ShowInfo(player.name, player.StatsSystem.Level);
        monsterInfo.ShowInfo(monster.name, monster.Stats.Level);
        this.gameObject.SetActive(true);

        StartCoroutine(WaitAnimToLoadBattle("enter_battle"));
    }

    public IEnumerator WaitAnimToLoadBattle(string name)
    {
        yield return new WaitForSeconds(GetAnimDuration(name));
        LoadScene(BATTLE);
    }

    public float GetAnimDuration(string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip.length;
            }
        }

        return 1;
    }
}