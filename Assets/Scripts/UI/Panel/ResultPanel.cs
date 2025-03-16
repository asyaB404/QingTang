// // ********************************************************************************************
// //     /\_/\                           @file       ResultPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031616
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using DG.Tweening;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class ResultPanel : BasePanel<ResultPanel>
    {
        [SerializeField] private Sprite[] result;

        public bool isWin;

        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            GetControl<Button>("ResultPanel").onClick.AddListener(() =>
            {
                HideMe();
                if (isWin)
                {
                    BattlePanel.Instance.HideMe();
                    DialogManager.Instance.Stop();
                    DialogManager.Instance.ReSet();
                }
                else
                {
                    MessagePanel.Instance
                        .ShowMessage("调解失败！",
                            () =>
                            {
                                MessagePanel.Instance.HideMe();
                                ResultPanel.Instance.HideMe();
                                BattlePanel.Instance.HideMe();
                                DialogManager.Instance.ReSet();
                                DialogManager.Instance.Load(6);
                            },
                            () =>
                            {
                                MessagePanel.Instance.HideMe();
                                ResultPanel.Instance.HideMe();
                                BattlePanel.Instance.HideMe();
                                DialogPanel.Instance.HideMe();
                                DialogManager.Instance.ReSet();
                            }).SetBtnName("重新调解", "重新收集线索");
                }
            });
        }

        public void ShowResult(bool isWin)
        {
            this.isWin = isWin;
            ShowMe();
            GetControl<Image>("resImg").gameObject.SetActive(true);
            GetControl<Image>("resImg").transform.localScale = Vector3.zero;
            GetControl<Image>("resImg").transform.DOScale(1, UIConst.UI_PANEL_ANIM);
            GetControl<Image>("resImg").sprite = isWin ? result[0] : result[1];
        }
    }
}