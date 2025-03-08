// // ********************************************************************************************
// //     /\_/\                           @file       TipPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030901
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using Cysharp.Threading.Tasks;
using DG.Tweening;
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
            rectTransform = GetComponent<RectTransform>();
        }

        public async UniTask ShowMeAsync(string content)
        {
            GetControl<TextMeshPro>("content").text = content;
            ShowMe();
            await UniTask.WaitForSeconds(UIConst.TipHide);
            HideMe();
        }

        public override void ShowAnim()
        {
            rectTransform.DOKill(true);
            rectTransform.DOAnchorPosY(0, UIConst.UIDuration);
        }

        public override void HideAnim()
        {
            rectTransform.DOKill(true);
            rectTransform.DOAnchorPosY(-rectTransform.sizeDelta.y, UIConst.UIDuration);
        }

        [ContextMenu("test")]
        private void Test()
        {
            ShowMeAsync("获得了线索！！获得了线索！！获得了线索！！").Forget();
        }
    }
}