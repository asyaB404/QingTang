// // ********************************************************************************************
// //     /\_/\                           @file       MessagePanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031020
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Panel
{
    public class MessagePanel : BasePanel<MessagePanel>
    {
        [SerializeField] private RectTransform rectTransform;

        public MessagePanel ShowMessage(string message, UnityAction confirm = null)
        {
            GetControl<TextMeshProUGUI>("content").text = message;
            if (confirm != null)
                GetControl<Button>("confirm").onClick.AddListener(confirm);
            else
                GetControl<Button>("confirm").onClick.AddListener(HideMe);
            GetControl<Button>("exit").onClick.AddListener(HideMe);
            ShowMe();
            return this;
        }

        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            rectTransform = GetComponent<RectTransform>();
        }

        public override void ShowAnim()
        {
            rectTransform.DOKill(true);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.sizeDelta.y);
            rectTransform.DOAnchorPosY(0, UIConst.UI_PANEL_ANIM * 2f);
        }

        public override void HideAnim()
        {
            GetControl<Button>("confirm").onClick.RemoveAllListeners();
            rectTransform.DOKill(true);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);
            rectTransform.DOAnchorPosY(rectTransform.sizeDelta.y, UIConst.UI_PANEL_ANIM);
        }

        [ContextMenu("test")]
        private void Test()
        {
            ShowMessage("消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示");
        }
    }
}