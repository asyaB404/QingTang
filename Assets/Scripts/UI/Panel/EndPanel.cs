// // ********************************************************************************************
// //     /\_/\                           @file       EndPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031721
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using DG.Tweening;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class EndPanel:BasePanel<EndPanel>
    {
        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            GetControl<Button>("Main").onClick.AddListener(End);
            MyEventSystem.Instance.AddEventListener<int>(CMDNAME.EVENT, (eId) =>
            {
                if (eId == 81)
                {
                    DialogPanel.Instance.HideMe();
                    ShowMe();
                }
            });
        }

        private void End()
        {
            GetControl<Image>("End").DOFade(1f, UIConst.UI_PANEL_ANIM * 10f).OnComplete(() =>
            {
                GetControl<Image>("End").color = Color.clear;
                UIManager.Instance.ClearPanels();
                GameStartPanel.Instance.ShowMe();
            });
        }

        public override void ShowAnim()
        {
            CanvasGroupInstance.DOKill(true);
            gameObject.SetActive(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UI_PANEL_ANIM);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.DOKill(true);
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() => { gameObject.SetActive(false); });
        }

        public override void OnPressedEsc()
        {
            
        }
    }
}