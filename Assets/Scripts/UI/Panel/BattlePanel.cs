// // ********************************************************************************************
// //     /\_/\                           @file       TryBattle.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031420
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Collections.Generic;
using DG.Tweening;
using GamePlay;
using GamePlay.Tips;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panel
{
    public class BattlePanel : BasePanel<BattlePanel>
    {
        [SerializeField] private RectTransform tipBtnsParent;
        [SerializeField] private List<TipButton> tipButtons;
        [SerializeField] private List<Tip> choices = new() { null, null, null };
        [SerializeField] private TextMeshProUGUI[] choicesText;

        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            MyEventSystem.Instance.AddEventListener<int>(CMDNAME.EVENT, (id) =>
            {
                if (id == 61)
                {
                    ShowMe();
                }
            });
            GetControl<Button>("confirm").onClick.AddListener(() =>
            {
                MyEventSystem.Instance.EventTrigger<List<Tip>>("BattlePanel_Confirm", choices);
            });
            GetControl<Button>("cancel").onClick.AddListener(() =>
            {
                for (int i = 0; i < choices.Count; i++)
                {
                    choices[i] = null;
                }

                UpdateChoices();
            });
            GetControl<Button>("exit").onClick.AddListener(Exit);
            foreach (Transform child in tipBtnsParent)
            {
                TipButton tipButton = new TipButton()
                {
                    redPoint = child.GetChild(0).gameObject,
                    color = child.GetChild(1).GetComponent<Image>(),
                    btn = child.GetComponent<Button>(),
                    img = child.GetComponent<Image>(),
                    text = child.GetComponentInChildren<TextMeshProUGUI>()
                };
                tipButton.btn.onClick.AddListener(() =>
                {
                    if (tipButton.tip == null) return;
                    tipButton.tip.HasRedPoint = false;
                    tipButton.redPoint.SetActive(false);
                    SetChoice(tipButton.tip.ColorId, tipButton.tip);
                });
                tipButtons.Add(tipButton);
            }
        }

        public override void OnPressedEsc()
        {
            Exit();
        }

        private void Exit()
        {
            HideMe();
            ResultPanel.Instance.ShowResult(false);
        }

        private void SetChoice(int colorId, Tip tip)
        {
            choices[colorId] = tip;
            UpdateChoices();
        }

        private void UpdateChoices()
        {
            for (int i = 0; i < choices.Count; i++)
            {
                var tip = choices[i];
                if (tip == null)
                {
                    choicesText[i].text = string.Empty;
                }
                else
                {
                    choicesText[tip.ColorId].text = tip.Name;
                }
            }
        }

        public override void ShowAnim()
        {
            OnUpdateTip();
            UpdateChoices();
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

        public void OnUpdateTip()
        {
            var finishedTips = SaveManager.Instance.FinishedTips;
            foreach (var tipButton in tipButtons)
            {
                tipButton.Reset();
            }

            int i = 0;
            foreach (var tip in finishedTips)
            {
                tipButtons[i].tip = tip;
                tipButtons[i].text.text = tip.Name;
                tipButtons[i].SetColor(tip.ColorId);
                tipButtons[i].redPoint.SetActive(tip.HasRedPoint);
                i++;
            }
        }
        

        public void CloseResult()
        {
            GetControl<Image>("Result").gameObject.SetActive(false);
        }
    }
}