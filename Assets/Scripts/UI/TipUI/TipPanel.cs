// // ********************************************************************************************
// //     /\_/\                           @file       TipPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030900
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System;
using UnityEngine;

namespace UI.TipUI
{
    public class TipPanel : MonoBehaviour
    {
        public static TipPanel Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}