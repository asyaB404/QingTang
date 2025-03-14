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
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Panel
{
    public class DialogPanel : BasePanel<DialogPanel>
    {
        [SerializeField] private DialogManager dialogManager;
        [SerializeField] private RectTransform tipPanelTransform;

        public override void Init()
        {
            base.Init();
            dialogManager.Init();
            GetControl<Button>("tipBtn").onClick.AddListener(ChangeTipPanel);
            GetControl<Button>("exit").onClick.AddListener(ChangeTipPanel);
        }

        public void SetBackGround(int sceneId)
        {
            GetControl<Image>("BG").sprite = GlobalConfig.Instance.GetBackGround(sceneId);
        }

        public void ChangeTipPanel()
        {
            GetControl<Button>("tipBtn").transform.GetChild(0).gameObject.SetActive(false);
            if (tipPanelTransform.anchoredPosition.x > 0)
            {
                tipPanelTransform.DOAnchorPosX(0, UIConst.UI_PANEL_ANIM);
            }
            else
            {
                tipPanelTransform.DOAnchorPosX(tipPanelTransform.sizeDelta.x, UIConst.UI_PANEL_ANIM);
            }
        }

        public void OnUpdateTip()
        {
        }

        public override void OnPressedEsc()
        {
        }

        public override void ShowAnim()
        {
            gameObject.SetActive(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UI_PANEL_ANIM);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}