// // ********************************************************************************************
// //     /\_/\                           @file       Player.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031119
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using GamePlay;
using UnityEngine;

namespace Game01
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Vector2 moveDir;
        [SerializeField] private Rigidbody2D rb;
        private bool _isStart;
        private float lastInputTime = -1f;
        private float doubleClickTimeThreshold = 0.3f; // 双击时间阈值，单位为秒
        private Vector2 lastMoveDir;
        private void Update()
        {
            if (!_isStart) return;

            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.y = Input.GetAxisRaw("Vertical");

            if (moveDir != Vector2.zero)
            {
                if (lastMoveDir == moveDir && Time.time - lastInputTime < doubleClickTimeThreshold)
                {
                    rb.velocity = moveDir * 4f; // 双击方向键时加速
                }
                else
                {
                    rb.velocity = moveDir * 2f; // 单击方向键时正常速度
                }

                lastMoveDir = moveDir;
                lastInputTime = Time.time;
            }
            else
            {
                rb.velocity = Vector2.zero; // 没有输入时停止移动
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name == "p2")
            {
                _isStart = false;
                MainGame01.GameInstance().Clear();
                DialogManager.Instance.Load(2);
            }
        }

        public void StartGame()
        {
            _isStart = true;
        }
    }
}