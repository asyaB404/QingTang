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
using GamePlay.Tips;
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
        [SerializeField] private bool waitForNext;

        private bool WaitForNext
        {
            get => waitForNext;
            set
            {
                waitForNext = value;
                Debug.Log("set" + value);
            }
        }

        public bool IsWaiting => waitTimer > 0;
        public int CurDialogId { get; private set; } = -1;

        private readonly TipsManager _tipsManager = new();
        private readonly CommandManager _commandManager = new();
        [SerializeField] private Transform rolesParent;
        private RoleManager _roleManager;
        private readonly HashSet<int> _finishedDialog = new();
        [SerializeField] private int curIndex = 0;
        public int CurDialogIndex => curIndex;
        [SerializeField] private CanvasGroup panel;

        [FormerlySerializedAs("sb")] [SerializeField]
        private DialogStringBuilder sbMgr;

        [SerializeField] private TextMeshProUGUI roleName;
        [SerializeField] private List<DialogListInfoClass> dialogList;
        public static DialogManager Instance { get; private set; }

        public void Init()
        {
            Instance = this;
            _roleManager = new(rolesParent);
            MyEventSystem.Instance.AddEventListener(CMDNAME.STOP, Stop);
            MyEventSystem.Instance.AddEventListener<float>(CMDNAME.WAIT, SetWait);
            MyEventSystem.Instance.AddEventListener<int[], string>(CMDNAME.GET_TIP, (id, tipName) =>
            {
                Tip tip = new Tip(id[0], tipName, id[1]);
                _tipsManager.OnGetTip(tip);
                curIndex++;
                Next();
            });
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
            MyEventSystem.Instance.AddEventListener(CMDNAME.CLEAR, () =>
            {
                _roleManager.Clear();
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
            else
            {
                if (!WaitForNext) return;
                WaitForNext = false;
                curIndex++;
                Next();
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
            isStop = false;
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
            _roleManager.Clear();
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
            if (isStop)
                return;

            if (IsWaiting)
            {
                SetWait(0);
                return;
            }

            if (curIndex >= dialogList.Count)
            {
                Finish(CurDialogId);
                Stop();
                return;
            }

            if (sbMgr.IsBuilding)
            {
                sbMgr.Skip();
                return;
            }

            var info = dialogList[curIndex];
            DialogPanel.Instance.SetBackGround(info.sceneId);
            string content = info.dialog;
            var infoMove = info.move;
            var split = infoMove.Split(',');
            if (info.roleId > 0)
            {
                Role role;
                if (split.Length >= 2)
                {
                    role = _roleManager.GetRole(info.roleId, split[0]);
                    role.Move(split[0], float.Parse(split[1]));
                }
                else
                {
                    role = _roleManager.GetRole(info.roleId);
                }

                roleName.text = role.roleName;
                role.SetFace(info.face);
            }
            else if (info.roleId == 0)
            {
                roleName.text = PrefMgr.GetPlayerName();
            }

            _commandManager.CheckCommand(content);

            if (content.Length <= 0 || content[0] == '#') return;
            sbMgr.SetText(content);
            curIndex++;
        }

        public void Finish(int id)
        {
            SaveManager.Instance.FinishDialog(id);
            ReSet();
        }

        private void SetWait(float duration)
        {
            waitTimer = duration;
            WaitForNext = true;
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

        [Space(50)] public int debugId = 0;

        #region Debug

        [ContextMenu("test")]
        private void Test()
        {
            Load(debugId);
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