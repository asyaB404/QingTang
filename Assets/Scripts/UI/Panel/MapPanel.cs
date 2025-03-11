// // ********************************************************************************************
// //     /\_/\                           @file       MapPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031012
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using DG.Tweening;

namespace UI.Panel
{
    public class MapPanel:BasePanel<MapPanel>
    {
        public override void ShowAnim()
        {
            gameObject.SetActive(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UI_PANEL_ANIM);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}