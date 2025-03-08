// // ********************************************************************************************
// //     /\_/\                           @file       DialogPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030818
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using Data;
using DG.Tweening;
using UnityEngine.UI;

namespace UI.Panel
{
    public class DialogPanel : BasePanel<DialogPanel>
    {
        public void SetBackGround()
        {
            GetControl<Image>("BG");
        }

        public override void ShowAnim()
        {
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, MyConst.PANEL_FADE);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, MyConst.PANEL_FADE);
        }
    }
}