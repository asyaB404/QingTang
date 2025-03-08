// // ********************************************************************************************
// //     /\_/\                           @file       GlobalDataManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030810
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System;
using QTConfig;
using UnityEngine;

namespace Data
{
    public class GlobalConfig : MonoBehaviour
    {
        public static GlobalConfig Instance { get; private set; }
        [SerializeField] private DialogList[] dialogLists;

        private void Awake()
        {
            Instance = this;
        }

        public DialogList GetDialogList(int id)
        {
            return dialogLists[id];
        }

        public static string IdToRoleName(int id)
        {
            return "";
        }
    }
}