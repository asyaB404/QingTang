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
using GamePlay;
using UnityEngine.UI;

namespace UI.Panel
{
    public class DialogPanel : BasePanel<DialogPanel>
    {
        public void SetBackGround()
        {
            GetControl<Image>("BG");
        }

        public override void OnPressedEsc()
        {
            
        }

        public override void ShowAnim()
        {
            CanvasGroupInstance.interactable = true;
            gameObject.SetActive(true);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.interactable = false;
            gameObject.SetActive(false);
        }
    }
}