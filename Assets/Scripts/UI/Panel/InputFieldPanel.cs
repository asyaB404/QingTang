// // ********************************************************************************************
// //     /\_/\                           @file       InputPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030921
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using Data;
using DG.Tweening;
using GamePlay;
using TMPro;
using UnityEngine.UI;

namespace UI.Panel
{
    public class InputFieldPanel : BasePanel<InputFieldPanel>
    {
        public TMP_InputField inputField;

        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            MyEventSystem.Instance.AddEventListener<int>(CMDNAME.EVENT, (int eId) =>
            {
                if (eId == 2)
                {
                    ShowMe();
                }
            });
            inputField.onSubmit.AddListener(Confirm);
            GetControl<Button>("confirm").onClick.AddListener(() => Confirm(inputField.text));
            GetControl<Button>("exit").onClick.AddListener(OnPressedEsc);
        }

        private void Confirm(string content)
        {
            HideMe();
            if (string.IsNullOrEmpty(content))
                content = "王小明";
            PrefMgr.SetPlayerName(content);
            DialogManager.Instance.UnStop();
        }

        public override void OnPressedEsc()
        {
            Confirm(inputField.text);
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
            CanvasGroupInstance.DOFade(0f, UIConst.UIDuration).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}