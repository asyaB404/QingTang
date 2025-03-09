// // ********************************************************************************************
// //     /\_/\                           @file       SettingsPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030713
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using UnityEngine.UI;

namespace UI.Panel
{
    public class SettingsPanel : BasePanel<SettingsPanel>
    {
        public override void Init()
        {
            base.Init();
            GetControl<Slider>("s1").onValueChanged.AddListener((v) => { AudioMgr.Instance.SetMusicVolume(v); });
            GetControl<Slider>("s2").onValueChanged.AddListener((v) => { AudioMgr.Instance.SetSFXVolume(v); });
            GetControl<Button>("exit").onClick.AddListener(OnPressedEsc);
        }
    }
}