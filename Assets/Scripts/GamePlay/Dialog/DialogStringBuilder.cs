// // ********************************************************************************************
// //     /\_/\                           @file       DialogStringBuilder.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030816
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Text;
using Cysharp.Threading.Tasks;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay
{
    public class DialogStringBuilder : MonoBehaviour
    {
        public bool IsBuilding => isBuilding;
        [SerializeField] private bool isBuilding;
        [SerializeField] private TextMeshProUGUI dialog;
        [SerializeField] private float charDuration = 0.05f;
        private readonly StringBuilder _stringBuilder = new();
        private string _curContent;
        private int _uniId = 0;

        public void SetText(string content)
        {
            _curContent = content;
            _stringBuilder.Clear();
            content = content.Replace("{playerName}", PrefMgr.GetPlayerName());
            BuildStringAsync(content).Forget();
        }

        public void Skip()
        {
            _uniId++;
            dialog.text = _curContent;
            isBuilding = false;
        }

        public void ReSet()
        {
            _uniId = 0;
            _curContent = string.Empty;
            dialog.text = string.Empty;
            _stringBuilder.Clear();
        }

        private async UniTask BuildStringAsync(string content)
        {
            _uniId++;
            int uuid = _uniId;
            int i = 0;
            isBuilding = true;
            while (uuid == _uniId && i < content.Length)
            {
                AudioMgr.Instance.PlaySFX("SFX/str");
                _stringBuilder.Append(content[i]);
                dialog.text = _stringBuilder.ToString();
                i++;
                await UniTask.WaitForSeconds(charDuration);
            }

            isBuilding = false;
        }

        [ContextMenu("test1")]
        private void Test1()
        {
            SetText("一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五" +
                    "一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五" +
                    "一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五一二三四五" +
                    "一二三四五一二三四五一二三四五一二三四五一二三四五");
        }

        [ContextMenu("test2")]
        private void Test2()
        {
            Skip();
        }
    }
}