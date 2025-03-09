// // ********************************************************************************************
// //     /\_/\                           @file       PrefMgr.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030916
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using UnityEngine;

namespace Data
{
    public static class PrefMgr
    {
        public static string GetPlayerName()
        {
            return PlayerPrefs.GetString("PlayerName", "我");
        }
        
        public static void SetPlayerName(string name)
        {
            PlayerPrefs.SetString("PlayerName", name);
        }
    }
}