// // ********************************************************************************************
// //     /\_/\                           @file       PhonePanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031913
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using DG.Tweening;
using GamePlay;
using UnityEngine.UI;

namespace UI.Panel
{
    public class PhonePanel : BasePanel<PhonePanel>
    {
        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            GetControl<Button>("exit").onClick.AddListener(HideMe);
            GetControl<Button>("001").onClick.AddListener(() =>
            {
                MyEventSystem.Instance.EventTrigger<int>(CMDNAME.EVENT,-1);
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
    }
}