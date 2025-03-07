// // ********************************************************************************************
// //     /\_/\                           @file       SettingsPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030713
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

namespace UI.Panel
{
    public class SettingsPanel : BasePanel<SettingsPanel>
    {
        public override void Init()
        {
            base.Init();
            
        }

        public override void CallBackWhenHeadPop(IBasePanel popPanel)
        {
            popPanel?.HideAnim();
        }

        public override void CallBackWhenHeadPush(IBasePanel oldPanel)
        {
            ShowAnim();
        }
    }
}