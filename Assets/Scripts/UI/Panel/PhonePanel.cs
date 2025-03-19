// // ********************************************************************************************
// //     /\_/\                           @file       PhonePanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031913
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using GamePlay;
using UnityEngine.UI;

namespace UI.Panel
{
    public class PhonePanel : BasePanel<PhonePanel>
    {
        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            GetControl<Button>("exit").onClick.AddListener(HideMe);
            GetControl<Button>("001").onClick.AddListener(() =>
            {
                MyEventSystem.Instance.EventTrigger<int>(CMDNAME.EVENT,-1);
            });
        }
    }
}