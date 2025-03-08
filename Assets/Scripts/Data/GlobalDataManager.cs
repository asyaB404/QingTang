// // ********************************************************************************************
// //     /\_/\                           @file       GlobalDataManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030810
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System;
using UnityEngine;

namespace Data
{
    public class GlobalDataManager : MonoBehaviour
    {
        public static GlobalDataManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}