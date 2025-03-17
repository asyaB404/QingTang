// // ********************************************************************************************
// //     /\_/\                           @file       AchievePanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031618
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using DG.Tweening;
using GamePlay;
using UnityEngine.UI;

namespace UI.Panel
{
    public class AchievePanel : BasePanel<AchievePanel>
    {
        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            GetControl<Button>("exit").onClick.AddListener(HideMe);
            GetControl<Button>("01").onClick.AddListener(() =>
            {
                GetControl<Button>("01").transform.GetChild(0).gameObject.SetActive(false);
                if (SaveManager.Instance.CheckHasFinishedDialog(8)) return;
                DialogManager.Instance.Load(8);
                TipPanel.Instance.ShowTip("老王好感度降低，老李好感升高。居民声望增高，指标达成+1");
            });
            MyEventSystem.Instance.AddEventListener<int>(CMDNAME.EVENT, (eId) =>
            {
                if (eId == 71)
                {
                    GetControl<Button>("01").gameObject.SetActive(true);
                }
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