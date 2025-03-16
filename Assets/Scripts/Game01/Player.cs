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
        private float lastTapTime = 0f;
        private KeyCode lastKey;
        private bool isDoubleTapped = false;
        public float normalSpeed = 2f;
        public float sprintSpeed = 4f;
        public float doubleTapTime = 0.3f; // 双击的时间间隔
        void Update()
        {
            if(!_isStart) return;
            HandleInput();
        }

        void FixedUpdate()
        {
            float speed = isDoubleTapped ? sprintSpeed : normalSpeed;
            rb.velocity = moveDir * speed;
        }

        void HandleInput()
        {
            Vector2 inputDir = Vector2.zero;
            KeyCode currentKey = KeyCode.None;

            if (Input.GetKey(KeyCode.A)) { inputDir = Vector2.left; currentKey = KeyCode.A; }
            if (Input.GetKey(KeyCode.D)) { inputDir = Vector2.right; currentKey = KeyCode.D; }
            if (Input.GetKey(KeyCode.W)) { inputDir = Vector2.up; currentKey = KeyCode.W; }
            if (Input.GetKey(KeyCode.S)) { inputDir = Vector2.down; currentKey = KeyCode.S; }

            moveDir = inputDir;
            if (inputDir != Vector2.zero)
            {

                // 处理双击逻辑
                if (Input.GetKeyDown(currentKey))
                {
                    if (lastKey == currentKey && Time.time - lastTapTime <= doubleTapTime)
                    {
                        isDoubleTapped = true;
                    }
                    lastTapTime = Time.time;
                    lastKey = currentKey;
                }
            }

            // 如果方向键松开，则重置双击状态
            if (Input.GetKeyUp(currentKey))
            {
                isDoubleTapped = false;
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