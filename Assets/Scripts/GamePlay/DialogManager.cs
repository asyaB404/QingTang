// // ********************************************************************************************
// //     /\_/\                           @file       DialogManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030816
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using QTConfig;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace GamePlay
{
    public class DialogManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private bool isStop;
        [SerializeField] private float waitTimer;
        public bool IsWaiting => waitTimer > 0;
        private int _curDialogId = -1;
        private readonly CommandManager _commandManager = new();
        private readonly RoleManager _roleManager = new();
        private readonly HashSet<int> _finishedDialog = new();
        public HashSet<int> FinishedDialog => _finishedDialog;
        [SerializeField] private int curIndex = 0;
        [SerializeField] private CanvasGroup panel;

        [FormerlySerializedAs("sb")] [SerializeField]
        private DialogStringBuilder sbMgr;

        [SerializeField] private TextMeshProUGUI roleName;
        [SerializeField] private List<DialogListInfoClass> dialogList;
        public static DialogManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            MyEventSystem.Instance.AddEventListener(CMDNAME.STOP, Stop);
            MyEventSystem.Instance.AddEventListener<float>(CMDNAME.WAIT, SetWait);
            MyEventSystem.Instance.AddEventListener(CMDNAME.NEXT, Next);
        }

        private void Update()
        {
            if (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
            }
        }

        public void Load(int dialogId)
        {
            _curDialogId = dialogId;
            panel.DOFade(1f, MyConst.DIALOG_FADE);
            panel.interactable = true;
            dialogList = GlobalConfig.Instance.GetDialogList(dialogId).list;
            curIndex = 0;
            Next();
        }

        public void Stop()
        {
            isStop = true;
            waitTimer = 0;
            panel.DOFade(0f, MyConst.DIALOG_FADE);
            panel.interactable = false;
            curIndex++;
        }

        public void ReSet()
        {
            _curDialogId = -1;
            sbMgr.ReSet();
        }

        public void Next()
        {
            if (IsWaiting)
            {
                SetWait(0);
                return;
            }

            if (curIndex >= dialogList.Count)
            {
                Finish(_curDialogId);
                ReSet();
                Stop();
                return;
            }

            if (isStop)
            {
                isStop = false;
                panel.DOFade(1f, MyConst.DIALOG_FADE);
                panel.interactable = true;
            }

            var info = dialogList[curIndex];
            string content = info.dialog;
            _commandManager.CheckCommand(content);
            roleName.text = GlobalConfig.IdToRoleName(info.roleId);

            if (content.Length <= 0 || content[0] == '#') return;

            if (!sbMgr.IsBuilding)
            {
                sbMgr.SetText(content);
                curIndex++;
            }
            else
            {
                sbMgr.Skip();
            }
        }

        public void Finish(int id)
        {
            _finishedDialog.Add(id);
        }

        private void SetWait(float duration)
        {
            waitTimer = duration;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("click");
            Next();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("down");
        }


        #region Debug

        [ContextMenu("test")]
        private void Test()
        {
            Load(0);
        }

        [ContextMenu("test1")]
        private void Test1()
        {
            Next();
        }

        #endregion
    }
}