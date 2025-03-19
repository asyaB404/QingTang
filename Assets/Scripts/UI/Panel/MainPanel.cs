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

                if (eId == -1)
                {
                    PhonePanel.Instance.HideMe();
                    DialogManager.Instance.UnStop();
                    event1 = false;
                }

                if (eId == 51)
                {
                    TipPanel.Instance.ShowTip("准备好了吗？");
                    btns[4].transform.GetChild(0).gameObject.SetActive(true);
                }

                if (eId == 71)
                {
                    icons[0].DOFillAmount(0.8f, UIConst.UI_PANEL_ANIM);
                    btns[2].transform.GetChild(0).gameObject.SetActive(true);
                }
            });

            #endregion

            #region Btns

            GetControl<Button>("Mail").onClick.AddListener(() =>
            {
                GetControl<Button>("Mail").gameObject.SetActive(false);
                GetControl<Image>("Front").rectTransform.DOAnchorPosY(0, UIConst.UI_PANEL_ANIM);
                DialogManager.Instance.Load(0);
            });
            btns[0].onClick.AddListener(() => { InternetPanel.Instance.ShowMe(); });
            btns[1].onClick.AddListener(() => { RolesPanel.Instance.ShowMe(); });
            btns[2].onClick.AddListener(() =>
            {
                btns[2].transform.GetChild(0).gameObject.SetActive(false);
                AchievePanel.Instance.ShowMe();
            });
            btns[3].onClick.AddListener(() =>
            {
                btns[3].transform.GetChild(0).gameObject.SetActive(false);
                if (event1)
                    PhonePanel.Instance.ShowMe();
            });
            btns[4].onClick.AddListener(() =>
            {
                btns[4].transform.GetChild(0).gameObject.SetActive(false);
                if (event1)
                {
                    MessagePanel.Instance.ShowMessage("先打一下电话吧！");
                    return;
                }

                // TryBattlePanel.Instance.ShowMe();
                MessagePanel.Instance.ShowMessage("信息是否收集全？", () =>
                {
                    MessagePanel.Instance.HideMe();
                    if (SaveManager.Instance.CheckHasFinishedDialog(1) &&
                        SaveManager.Instance.CheckHasFinishedDialog(5))
                    {
                        DialogManager.Instance.Load(6);
                    }
                    else
                    {
                        TipPanel.Instance.ShowTip("线索收集不足");
                    }
                });
            });
            btns[5].onClick.AddListener(() =>
            {
                if (event1)
                {
                    MessagePanel.Instance.ShowMessage("先打一下电话吧！");
                    return;
                }

                MapPanel.Instance.ShowMe();
            });
            btns[6].onClick.AddListener(() => { SettingsPanel.Instance.ChangeMe(); });
            btns[7].onClick.AddListener(() => { MessagePanel.Instance.ShowMessage("返回主界面？", HideMe); });
            btns[8].onClick.AddListener(() => { MessagePanel.Instance.ShowMessage("返回主界面？", HideMe); });
            btns[9].onClick.AddListener(() => { MessagePanel.Instance.ShowMessage("返回主界面？", HideMe); });

            #endregion
        }

        private void OnUILoadFinish()
        {
            AudioMgr.Instance.PlayMusic("Music/" + "main");
            if (!SaveManager.Instance.CheckHasFinishedDialog(0) && DialogManager.Instance.CurDialogId != 0)
            {
                OnFirst();
            }

            if (SaveManager.Instance.CheckHasFinishedDialog(1) && !SaveManager.Instance.CheckHasFinishedDialog(5))
            {
                AudioMgr.Instance.PlaySFX("SFX/" + "phone");
                btns[3].transform.GetChild(0).gameObject.SetActive(true);
                btns[3].onClick.AddListener(OnEvent51);
            }
        }

        private void OnFirst()
        {
            GetControl<Image>("Front").rectTransform.localPosition = new Vector3(0, -350, 0);
            GetControl<Button>("Mail").gameObject.SetActive(true);
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