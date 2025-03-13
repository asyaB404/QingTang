// // ********************************************************************************************
// //     /\_/\                           @file       GameManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030713
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Collections.Generic;
using GamePlay.Tips;

namespace GamePlay
{
    public class SaveManager
    {
        private readonly HashSet<int> _finishedDialog;
        private readonly List<Tip> _finishedTips;
        public HashSet<int> FinishedDialogs => _finishedDialog;
        public IReadOnlyList<Tip> FinishedTips => _finishedTips;

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

        public bool FinishDialog(int id)
        {
            return _finishedDialog.Add(id);
        }

        public bool CheckHasFinishedDialog(int id)
        {
            return _finishedDialog.Contains(id);
        }


        public bool FinishTip(Tip tip)
        {
            if (CheckHasFinishedTip(tip.Id)) return false;
            _finishedTips.Add(tip);
            return true;
        }

        public bool CheckHasFinishedTip(int id)
        {
            foreach (var tip in _finishedTips)
            {
                if (tip.Id == id) return true;
            }

            return false;
        }
    }
}