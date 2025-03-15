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
            if (tip.Id == 2)
            {
                MessagePanel.Instance.ShowMessage("针对这条发言：" + tip.Name, () =>
                {
                    if (!SaveManager.Instance.FinishTip(tip)) return;
                    DialogPanel.Instance.ShowRedPoint();
                    DialogPanel.Instance.OnUpdateTip();
                }, () =>
                {
                    DialogManager.Instance.Stop();
                    //TODO:
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