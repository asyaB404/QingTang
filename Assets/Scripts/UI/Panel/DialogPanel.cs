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
using UnityEngine.UI;

namespace UI.Panel
{
    public class DialogPanel : BasePanel<DialogPanel>
    {
        public void SetBackGround(int sceneId)
        {
            GetControl<Image>("BG").sprite = GlobalConfig.Instance.GetBackGround(sceneId);
        }

        public override void OnPressedEsc()
        {
        }

        public override void ShowAnim()
        {
            gameObject.SetActive(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UIDuration);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UIDuration).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}