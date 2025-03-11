// // ********************************************************************************************
// //     /\_/\                           @file       Target.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031119
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System;
using UI.Panel;
using UnityEngine;

namespace Game01
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private Transform[] paths;
        [SerializeField] private int curPath;
        [SerializeField] private float speed = 2.2f;
        [SerializeField] private float waitTime = 0.1f;
        private bool _isStart;

        private float _waitTimer;

        private void Start()
        {
            // 确保 curPath 初始化为 0
            curPath = 0;
            _waitTimer = waitTime;
        }

        public void StartGame()
        {
            _isStart = true;
        }

        private void Update()
        {
            if (!_isStart) return;
            if (paths.Length == 0) return;
            transform.position =
                Vector3.MoveTowards(transform.position, paths[curPath].position, speed * Time.deltaTime);

            // 检查是否到达当前路径点
            if (transform.position == paths[curPath].position)
            {
                // 开始等待
                if (_waitTimer > 0)
                {
                    _waitTimer -= Time.deltaTime;
                }
                else
                {
                    // 等待结束，移动到下一个路径点
                    curPath++;
                    if (curPath >= paths.Length)
                    {
                        Finish();
                        curPath = 0; // 回到第一个路径点
                    }

                    _waitTimer = waitTime; // 重置等待时间
                }
            }
        }

        private void Finish()
        {
            _isStart = false;
            Destroy(gameObject);
            MessagePanel.Instance.ShowMessage("跟丢老太太了....", () =>
            {
                MessagePanel.Instance.HideMe();
                MainGame01.GameInstance().Clear();
                MainGame01.InitGame();
            }, () =>
            {
                MessagePanel.Instance.HideMe();
                MainGame01.GameInstance().Clear();
            }).SetBtnName("重试", "离开");
        }
    }
}