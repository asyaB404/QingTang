// // ********************************************************************************************
// //     /\_/\                           @file       MainPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030713
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using DG.Tweening;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class MainPanel : BasePanel<MainPanel>
    {
        [SerializeField] private Button[] btns;
        [SerializeField] private Image[] icons;
        [SerializeField] private bool event1 = false;

        public override void Init()
        {
            base.Init();
            MyEventSystem.Instance.AddEventListener<int>(CMDNAME.EVENT, (eId) =>
            {
                if (eId == 1)
                {
                    btns[3].transform.GetChild(0).gameObject.SetActive(true);
                    event1 = true;
                }
            });
            btns[3].onClick.AddListener(() =>
            {
                if (event1)
                {
                    btns[3].transform.GetChild(0).gameObject.SetActive(false);
                    DialogManager.Instance.UnStop();
                    event1 = false;
                }
            });
            btns[4].onClick.AddListener(() => { MapPanel.Instance.ShowMe(); });
        }

        public override void OnUILoadFinish()
        {
            if (!DialogManager.Instance.CheckHasFinishedDialog(0) && DialogManager.Instance.CurDialogId != 0)
            {
                DialogManager.Instance.Load(0);
            }
        }

        public override void OnPressedEsc()
        {
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
            CanvasGroupInstance.DOKill(true);
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}