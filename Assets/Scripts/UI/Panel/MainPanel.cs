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

            #region Event

            MyEventSystem.Instance.AddEventListener<int>(CMDNAME.EVENT, (eId) =>
            {
                if (eId == 1)
                {
                    btns[3].transform.GetChild(0).gameObject.SetActive(true);
                    event1 = true;
                }

                if (eId == 51)
                {
                    TipPanel.Instance.ShowTip("准备好了吗？");
                    btns[4].transform.GetChild(0).gameObject.SetActive(true);
                }
            });

            #endregion

            #region Btns

            btns[3].onClick.AddListener(() =>
            {
                btns[3].transform.GetChild(0).gameObject.SetActive(false);
                if (event1)
                {
                    DialogManager.Instance.UnStop();
                    event1 = false;
                }
            });
            btns[4].onClick.AddListener(() =>
            {
                btns[4].transform.GetChild(0).gameObject.SetActive(false);
                if (event1)
                {
                    MessagePanel.Instance.ShowMessage("先接一下电话吧！");
                    return;
                }
                TryBattlePanel.Instance.ShowMe();
            });
            btns[5].onClick.AddListener(() =>
            {
                if (event1)
                {
                    MessagePanel.Instance.ShowMessage("先接一下电话吧！");
                    return;
                }
                MapPanel.Instance.ShowMe();
            });
            btns[6].onClick.AddListener(() =>
            {
                SettingsPanel.Instance.ChangeMe();
            });
            btns[7].onClick.AddListener(() =>
            {
                MessagePanel.Instance.ShowMessage("返回主界面？", HideMe);
            });
            btns[8].onClick.AddListener(() =>
            {
                MessagePanel.Instance.ShowMessage("返回主界面？", HideMe);
            });
            btns[9].onClick.AddListener(() =>
            {
                MessagePanel.Instance.ShowMessage("返回主界面？", HideMe);
            });
            #endregion
        }

        private void OnUILoadFinish()
        {
            if (!SaveManager.Instance.CheckHasFinishedDialog(0) && DialogManager.Instance.CurDialogId != 0)
            {
                DialogManager.Instance.Load(0);
            }
            if (SaveManager.Instance.CheckHasFinishedDialog(1) && !SaveManager.Instance.CheckHasFinishedDialog(5))
            {
                btns[3].transform.GetChild(0).gameObject.SetActive(true);
                btns[3].onClick.AddListener(OnEvent51);
            }
        }

        private void OnEvent51()
        {
            btns[3].onClick.RemoveListener(OnEvent51);
            DialogManager.Instance.Load(5);
        }

        public override void OnPressedEsc()
        {
        }

        public override void ShowAnim()
        {
            CanvasGroupInstance.DOKill(true);
            gameObject.SetActive(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UI_PANEL_ANIM);
            OnUILoadFinish();
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.DOKill(true);
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}