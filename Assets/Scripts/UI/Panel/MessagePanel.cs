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

        public MessagePanel ShowMessage(string message, UnityAction confirm = null, UnityAction cancel = null)
        {
            GetControl<TextMeshProUGUI>("confirmText").text = "确认";
            GetControl<TextMeshProUGUI>("cancelText").text = "取消";
            GetControl<TextMeshProUGUI>("content").text = message;
            if (confirm != null)
                GetControl<Button>("confirm").onClick.AddListener(confirm);
            else
                GetControl<Button>("confirm").onClick.AddListener(HideMe);
            if (cancel != null)
            {
                GetControl<Button>("cancel").onClick.AddListener(cancel);
                GetControl<Button>("exit").onClick.AddListener(cancel);
            }
            else
            {
                GetControl<Button>("cancel").onClick.AddListener(HideMe);
                GetControl<Button>("exit").onClick.AddListener(HideMe);
            }

            ShowMe();
            return this;
        }

        public MessagePanel SetBtnName(string confirmBtnName = "确认", string cancelBtnName = "取消")
        {
            GetControl<TextMeshProUGUI>("confirmText").text = confirmBtnName;
            GetControl<TextMeshProUGUI>("cancelText").text = cancelBtnName;
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
            gameObject.SetActive(true);
            CanvasGroupInstance.DOKill(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UI_PANEL_ANIM);
        }

        public override void HideAnim()
        {
            GetControl<Button>("confirm").onClick.RemoveAllListeners();
            CanvasGroupInstance.DOKill(true);
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() => { gameObject.SetActive(false); });
        }

        [ContextMenu("test")]
        private void Test()
        {
            ShowMessage("消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示消息提示");
        }
    }
}