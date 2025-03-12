// // ********************************************************************************************
// //     /\_/\                           @file       GameManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030713
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System;
using System.Collections.Generic;

namespace GamePlay
{
    [Serializable]
    public class SaveManager
    {
        private readonly HashSet<int> _finishedDialog;
        private readonly HashSet<int> _finishedTips;
        public HashSet<int> FinishedDialogs => _finishedDialog;
        public HashSet<int> FinishedTips => _finishedTips;

        private static SaveManager instance { get; set; }

        public static SaveManager Instance
        {
            get
            {
                instance ??= new SaveManager();
                return instance;
            }
        }

        private SaveManager()
        {
            _finishedDialog = new HashSet<int>();
        }

        public void FinishDialog(int id)
        {
            _finishedDialog.Add(id);
        }

        public bool CheckHasFinishedDialog(int id)
        {
            return _finishedDialog.Contains(id);
        }


        public void FinishTip(int id)
        {
            _finishedTips.Add(id);
        }

        public bool CheckHasFinishedTip(int id)
        {
            return _finishedTips.Contains(id);
        }
    }
}