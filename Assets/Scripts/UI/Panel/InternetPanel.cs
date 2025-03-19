// // ********************************************************************************************
// //     /\_/\                           @file       InternetPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031913
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using UnityEngine.UI;

namespace UI.Panel
{
    public class InternetPanel:BasePanel<InternetPanel>
    {
        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            GetControl<Button>("exit").onClick.AddListener(HideMe);
            GetControl<Button>("btn1").onClick.AddListener(() =>
            {
                GetControl<Image>("01").gameObject.SetActive(false);
                GetControl<Image>("02").gameObject.SetActive(true);
            });
            GetControl<Button>("btn2").onClick.AddListener(() =>
            {
                GetControl<Image>("01").gameObject.SetActive(true);
                GetControl<Image>("02").gameObject.SetActive(false);
            });
        }
    }
}