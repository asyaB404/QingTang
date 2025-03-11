// // ********************************************************************************************
// //     /\_/\                           @file       TipPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030901
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using GamePlay;
using TMPro;
using UnityEngine;

namespace UI.Panel
{
    public class TipPanel : BasePanel<TipPanel>
    {
        [SerializeField] private RectTransform rectTransform;

        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            rectTransform = GetComponent<RectTransform>();
            MyEventSystem.Instance.AddEventListener<string>(CMDNAME.TIP, ShowTip);
        }

        public void ShowTip(string content)
        {
            ShowMeAsync(content).Forget();
        }

        public async UniTask ShowMeAsync(string content)
        {
            GetControl<TextMeshProUGUI>("content").text = content;
            ShowMe();
            await UniTask.WaitForSeconds(MyConst.TIP_PANEL_HIDE);
            HideMe();
        }

        public override void ShowAnim()
        {
            rectTransform.DOKill(true);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.sizeDelta.y);
            rectTransform.DOAnchorPosY(0, UIConst.UI_PANEL_ANIM * 2f);
        }

        public override void HideAnim()
        {
            rectTransform.DOKill(true);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);
            rectTransform.DOAnchorPosY(rectTransform.sizeDelta.y, UIConst.UI_PANEL_ANIM);
        }

        [ContextMenu("test")]
        private void Test()
        {
            ShowMeAsync("获得了线索！！获得了线索！！获得了线索！！").Forget();
        }
    }
}