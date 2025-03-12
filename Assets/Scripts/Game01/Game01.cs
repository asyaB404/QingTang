// // ********************************************************************************************
// //     /\_/\                           @file       Game.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031119
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System;
using UI.Panel;
using Unity.VisualScripting;
using UnityEngine;

namespace Game01
{
    public class MainGame01 : MonoBehaviour
    {
        public static MainGame01 Instance { get; private set; }

        [SerializeField] private Player player;
        [SerializeField] private Target target;

        public static MainGame01 GameInstance()
        {
            if (Instance != null) return Instance;
            var load = Resources.Load<GameObject>("Prefabs/Games/01");
            Instance = Instantiate(load).GetComponent<MainGame01>();
            return Instance;
        }
        
        public static void InitGame()
        {
            GameInstance();
            Game01UIPanel.Instance.ShowMe();
            MessagePanel.Instance.ShowMessage("老人家可别摔着了，追上那个老太太", () =>
            {
                MessagePanel.Instance.HideMe();
                GameInstance().StartGame();
            },  () =>
            {
                MessagePanel.Instance.HideMe();
                GameInstance().Clear();
            });
        }

        public void StartGame()
        {
            player.StartGame();
            target.StartGame();
        }

        public void Clear()
        {
            Instance = null;
            Destroy(gameObject);
            Game01UIPanel.Instance.HideMe();
        }
    }
}