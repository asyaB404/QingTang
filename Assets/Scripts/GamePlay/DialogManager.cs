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
using QTConfig;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay
{
    public class DialogManager : MonoBehaviour, IPointerUpHandler
    {
        private RoleManager _roleManager = new();
        [SerializeField] private int curIndex = 0;
        [SerializeField] private DialogStringBuilder sb;
        [SerializeField] private TextMeshProUGUI roleName;
        [SerializeField] private List<DialogListInfoClass> dialogList;
        public static DialogManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void DoNext()
        {
            var info = dialogList[curIndex];
            roleName.text = GlobalConfig.IdToRoleName(info.roleId);
            if (!sb.isBuilding)
            {
                sb.SetText(info.dialog);
                curIndex++;
            }
            else
            {
                sb.Skip();
            }
        }

        [ContextMenu("test")]
        private void Test()
        {
            dialogList = GlobalConfig.Instance.GetDialogList(0).list;
        }
    }
}