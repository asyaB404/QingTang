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
            UIManager.Instance.ExcludedPanels.Add(GetType());
            GetControl<Button>("Result").onClick.AddListener(() =>
            {
                HideMe();
                if (isWin)
                {
                    BattlePanel.Instance.HideMe();
                }
                else
                {
                    MessagePanel.Instance
                        .ShowMessage("调解失败！",
                            () =>
                            {
                                MessagePanel.Instance.HideMe();
                                DialogManager.Instance.Load(6);
                            },
                            () =>
                            {
                                MessagePanel.Instance.HideMe();
                                DialogPanel.Instance.HideMe();
                            }).SetBtnName("重新调解", "重新收集线索");
                }
            });
        }

        private void CloseResult()
        {
        }

        public void ShowResult(bool isWin)
        {
            this.isWin = isWin;
            GetControl<Image>("Result").gameObject.SetActive(true);
            GetControl<Image>("Result").transform.localScale = Vector3.zero;
            GetControl<Image>("Result").transform.DOScale(1, UIConst.UI_PANEL_ANIM);
            GetControl<Image>("resImg").sprite = isWin ? result[0] : result[1];
        }
    }
}