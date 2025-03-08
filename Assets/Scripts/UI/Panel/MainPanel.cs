// // ********************************************************************************************
// //     /\_/\                           @file       MainPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030713
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using Data;
using DG.Tweening;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class MainPanel : BasePanel<MainPanel>
    {
        [SerializeField] private Button[] btns;
        [SerializeField] private Image[] icons;

        public override void OnPressedEsc()
        {
            
        }

        public override void ShowAnim()
        {
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, MyConst.PANEL_FADE);
            if (!DialogManager.Instance.FinishedDialog.Contains(0))
            {
                DialogManager.Instance.Load(0);
            }
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, MyConst.PANEL_FADE);
        }
    }
}