// // ********************************************************************************************
// //     /\_/\                           @file       GameManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030713
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Collections.Generic;

namespace GamePlay
{
    public class SaveManager
    {
        private readonly HashSet<int> _finishedDialog;
        private readonly List<int> _finishedTips;
        public HashSet<int> FinishedDialogs => _finishedDialog;
        public IReadOnlyList<int> FinishedTips => _finishedTips;

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


        public bool FinishTip(int id)
        {
            if (CheckHasFinishedTip(id)) return false;
            _finishedTips.Add(id);
            return true;
        }

        public bool CheckHasFinishedTip(int id)
        {
            return _finishedTips.Contains(id);
        }
    }
}