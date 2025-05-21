using System;
using UnityEngine;

namespace InfiniteMap
{
    public class PlayerAttackPoint : PlayerAb
    {
        [SerializeField] private float distance;

        private void Update()
        {
            LookMouse();
        }

        private void LookMouse()
        {
            Vector3 mousePos = to2DVec(InputManager.Instance.MousePos);
            Vector3 direction = mousePos - Player.CenterPoint.position;
            //transform.position = Player.CenterPoint.position + direction.normalized * distance;
            float rotZ = (float)(Math.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            Player.CenterPoint.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }
}