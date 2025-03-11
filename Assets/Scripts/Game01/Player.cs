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

        private void Update()
        {
            if (!_isStart) return;
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.y = Input.GetAxisRaw("Vertical");
            if (Input.GetMouseButton(1))
            {
                rb.velocity = moveDir * 4f;
            }
            else
            {
                rb.velocity = moveDir * 2f;
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