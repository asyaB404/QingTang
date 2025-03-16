// // ********************************************************************************************
// //     /\_/\                           @file       RolesPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031618
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using UnityEngine.UI;

namespace UI.Panel
{
    public class RolesPanel : BasePanel<RolesPanel>
    {
        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            GetControl<Button>("exit").onClick.AddListener(HideMe);
        }
    }
}