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
        private CommandManager _commandManager = new();
        private RoleManager _roleManager = new();
        [SerializeField] private int curIndex = 0;
        [SerializeField] private CanvasGroup panel;
        [FormerlySerializedAs("sb")] [SerializeField] private DialogStringBuilder sbMgr;
        [SerializeField] private TextMeshProUGUI roleName;
        [SerializeField] private List<DialogListInfoClass> dialogList;
        public static DialogManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Load(int dialogId)
        {
            panel.DOFade(1f, MyConst.DIALOG_FADE);
            panel.interactable = true;
            dialogList = GlobalConfig.Instance.GetDialogList(dialogId).list;
            curIndex = 0;
            Next();
        }

        public void Stop()
        {
            isStop = true;
            panel.DOFade(0f, MyConst.DIALOG_FADE);
            panel.interactable = false;
        }

        public void ReSet()
        {
            sbMgr.ReSet();
        }

        public void Next()
        {
            if (isStop)
            {
                isStop = false;
                panel.DOFade(1f, MyConst.DIALOG_FADE);
                panel.interactable = true;
            }

            if (curIndex >= dialogList.Count)
            {
                Debug.LogError("ir1");
                return;
            }

            var info = dialogList[curIndex];
            string content = info.dialog;
            CommandType commandType = _commandManager.CheckCommand(content);
            switch (commandType)
            {
                case CommandType.None:
                    break;
                case CommandType.Stop:
                    Stop();
                    return;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            roleName.text = GlobalConfig.IdToRoleName(info.roleId);
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

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("click");
            Next();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("down");
        }
    }
}