// // ********************************************************************************************
// //     /\_/\                           @file       TipsManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031321
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Collections.Generic;
using UI.Panel;
using UnityEngine;

namespace GamePlay.Tips
{
    public class TipsManager
    {
        private int target = 1 ^ 21 ^ 63;
        public void OnGetTip(Tip tip)
        {
            if (tip.ColorId == 2)
            {
                MessagePanel.Instance.ShowMessage("针对这条发言：" + tip.Name, () =>
                {
                    MessagePanel.Instance.HideMe();
                    if (!SaveManager.Instance.FinishTip(tip)) return;
                    DialogPanel.Instance.ShowRedPoint();
                    DialogPanel.Instance.OnUpdateTip();
                }, () =>
                {
                    MessagePanel.Instance.HideMe();
                    BattlePanel.Instance.ShowMe();
                }).SetBtnName("采纳", "驳回");
                return;
            }

            if (SaveManager.Instance.FinishTip(tip))
            {
                DialogPanel.Instance.ShowRedPoint();
                DialogPanel.Instance.OnUpdateTip();
            }
        }

        public TipsManager()
        {
            MyEventSystem.Instance.AddEventListener<List<Tip>>("BattlePanel_Confirm", (tips) =>
            {
                int sum = 0;
                for (int i = 0; i < tips.Count; i++)
                {
                    var tip = tips[i];
                    if (tip == null)
                    {
                        MessagePanel.Instance.ShowMessage("请选择完3条关键线索！");
                        return;
                    }
                    sum ^= tip.Id;
                }

                if (sum == target)
                {
                    BattlePanel.Instance.HideMe();
                    DialogManager.Instance.Load(7);
                }
                else
                {
                    BattlePanel.Instance.HideMe();
                    ResultPanel.Instance.ShowResult(false);
                }
            });
            MyEventSystem.Instance.AddEventListener<int>(CMDNAME.EVENT, (id) =>
            {
                if (id == 11)
                {
                    
                }

                if (id == 71)
                {
                    ResultPanel.Instance.ShowResult(true);
                }
            });
        }
    }
}