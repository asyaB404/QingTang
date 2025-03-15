// // ********************************************************************************************
// //     /\_/\                           @file       TipsManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031321
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using UI.Panel;

namespace GamePlay.Tips
{
    public class TipsManager
    {
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
                    DialogManager.Instance.Stop();
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
    }
}