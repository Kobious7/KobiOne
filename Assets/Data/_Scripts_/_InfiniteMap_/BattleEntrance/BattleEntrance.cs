using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleEntrance : GMono
{
    public event Action<IMPlayer, IMMonster> OnBattleEntranceActivating;
    [SerializeField] private Camera playerCam, monsterCam;
    [SerializeField] private IMPlayer player;
    [SerializeField] private Vector2 playerOffset;
    [SerializeField] private List<MonsterOffsetCam> monsters;
    private InfiniteMapManager infiniteMapManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        playerCam = transform.Find("PlayerCamera").GetComponent<Camera>();
        monsterCam = transform.Find("MonsterCamera").GetComponent<Camera>();
    }

    protected override void Start()
    {
        base.Start();
        infiniteMapManager = InfiniteMapManager.Instance;
        player = infiniteMapManager.Player;
        player.OnBattlePreparing += FixOffsetAndCallEvents;
    }

    private void FixOffsetAndCallEvents(IMMonster monsterObj)
    {
        playerCam.transform.position = new Vector3(player.transform.position.x + playerOffset.x, player.transform.position.y + playerOffset.y, -10);

        int layer = LayerMask.NameToLayer("MonsterBE");
        SetLayerForMonster(monsterObj.RigModel.transform, layer);

        MonsterOffsetCam mon = monsters.FirstOrDefault(m => m.MonsterName == monsterObj.transform.name);
        Vector2 monsterOffset = monsterObj.Stats.Tier == MonsterTier.Rampage ? new Vector2(mon.Offset.x, mon.Offset.y + 1)
                                : monsterObj.Stats.Tier == MonsterTier.Elite ? new Vector2(mon.Offset.x, mon.Offset.y + 1) : mon.Offset;
        monsterCam.transform.position = new Vector3(monsterObj.transform.position.x + monsterOffset.x, monsterObj.transform.position.y + monsterOffset.y, -10);

        OnBattleEntranceActivating?.Invoke(player, monsterObj);
    }

    private void SetLayerForMonster(Transform obj, int newLayer)
    {
        if (obj == null)
            return;

        obj.gameObject.layer = newLayer;

        foreach (Transform child in obj)
        {
            child.gameObject.layer = newLayer;
        }
    }
}