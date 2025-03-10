// // ********************************************************************************************
// //     /\_/\                           @file       MessagePanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031020
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Panel
{
    public class MessagePanel : BasePanel<MessagePanel>
    {
        [SerializeField] private RectTransform rectTransform;

        public void ShowMessage(string message)
        {
            GetControl<TextMeshProUGUI>("content").text = message;
            ShowMe();
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
            rectTransform.DOAnchorPosY(0, UIConst.UIDuration * 2f);
        }

        public override void HideAnim()
        {
            rectTransform.DOKill(true);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);
            rectTransform.DOAnchorPosY(rectTransform.sizeDelta.y, UIConst.UIDuration);
        }
    }
}