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
using UI.Panel;
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
        public int CurDialogId { get; private set; } = -1;

        private readonly CommandManager _commandManager = new();
        [SerializeField] private Transform rolesParent;
        private RoleManager _roleManager;
        private readonly HashSet<int> _finishedDialog = new();
        public HashSet<int> FinishedDialog => _finishedDialog;
        [SerializeField] private int curIndex = 0;
        public int CurDialogIndex => curIndex;
        [SerializeField] private CanvasGroup panel;

        [FormerlySerializedAs("sb")] [SerializeField]
        private DialogStringBuilder sbMgr;

        [SerializeField] private TextMeshProUGUI roleName;
        [SerializeField] private List<DialogListInfoClass> dialogList;
        public static DialogManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _roleManager = new(rolesParent);
            MyEventSystem.Instance.AddEventListener(CMDNAME.STOP, Stop);
            MyEventSystem.Instance.AddEventListener<float>(CMDNAME.WAIT, SetWait);
            MyEventSystem.Instance.AddEventListener<string>(CMDNAME.TIP, (string _) =>
            {
                curIndex++;
                Next();
            });
            MyEventSystem.Instance.AddEventListener(CMDNAME.NEXT, () =>
            {
                curIndex++;
                Next();
            });
            MyEventSystem.Instance.AddEventListener<int>(CMDNAME.EVENT, (int id) =>
            {
                curIndex++;
                Next();
            });
        }

        private void Update()
        {
            if (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
            }
        }


        public void SetDialogUI(bool isActive)
        {
            if (isActive)
            {
                DialogPanel.Instance.ShowMe();
            }
            else
            {
                DialogPanel.Instance.HideMe();
            }

            panel.DOFade(isActive ? 1f : 0f, MyConst.DIALOG_FADE);
            panel.blocksRaycasts = isActive;
            panel.interactable = isActive;
        }

        public void Load(int dialogId)
        {
            CurDialogId = dialogId;
            SetDialogUI(true);
            dialogList = GlobalConfig.Instance.GetDialogList(dialogId).list;
            curIndex = 0;
            Next();
        }

        public void Stop()
        {
            isStop = true;
            waitTimer = 0;
            SetDialogUI(false);
        }

        public void ReSet()
        {
            CurDialogId = -1;
            curIndex = 0;
            sbMgr.ReSet();
        }

        public void UnStop()
        {
            if (!isStop) return;
            isStop = false;
            SetDialogUI(true);
            curIndex++;
            Next();
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
                Finish(CurDialogId);
                ReSet();
                Stop();
                return;
            }

            var info = dialogList[curIndex];
            DialogPanel.Instance.SetBackGround(info.sceneId);
            string content = info.dialog;
            var infoMove = info.move;
            var split = infoMove.Split(',');
            if (info.roleId != 0)
            {
                Role role;
                if (split.Length >= 2)
                {
                    role = _roleManager.GetRole(info.roleId, split[0]);
                    role.MoveX(float.Parse(split[1]));
                }
                else
                {
                    role = _roleManager.GetRole(info.roleId);
                }

                roleName.text = role.roleName;
                role.SetFace(info.face);
            }
            else
            {
                roleName.text = PrefMgr.GetPlayerName();
            }

            _commandManager.CheckCommand(content);

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
            MyEventSystem.Instance.EventTrigger(CMDNAME.CLEAR);
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

        [ContextMenu("test2")]
        private void Test2()
        {
            UnStop();
        }

        #endregion
    }
}