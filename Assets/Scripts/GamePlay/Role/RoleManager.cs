// // ********************************************************************************************
// //     /\_/\                           @file       RoleManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030816
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlay
{
    public class RoleManager
    {
        private readonly Dictionary<int, Role> _dict = new();
        private readonly Transform _rolesParent;

        public RoleManager(Transform parent)
        {
            _rolesParent = parent;
            MyEventSystem.Instance.AddEventListener(CMDNAME.CLEAR,Clear);
        }

        private void Clear()
        {
            foreach (var role in _dict.Values.ToList())
            {
                Object.Destroy(role.gameObject);
            }
            _dict.Clear();
        }

        public Role GetRole(int id, string anchoredString)
        {
            Vector2 anchored = new Vector2(0.5f, 0.5f);
            if (anchoredString == "L")
            {
                anchored.x = 0;
            }
            else if (anchoredString == "R")
            {
                anchored.x = 1;
            }

            if (_dict.TryGetValue(id, out var role))
            {
                return role;
            }
            else
            {
                var objRes = Resources.Load<GameObject>("Prefabs/Roles/" + id);
                var obj = Object.Instantiate(objRes, _rolesParent, false);
                role = obj.GetComponent<Role>();
                var rectTransform = (RectTransform)role.transform;
                rectTransform.anchorMin = anchored;
                rectTransform.anchorMax = anchored;
                rectTransform.pivot = anchored;
                if (anchored.x < 0.5f)
                {
                    rectTransform.anchoredPosition =
                        new Vector2(-rectTransform.sizeDelta.x, rectTransform.anchoredPosition.y);
                }
                else if (anchored.x > 0.5f)
                {
                    rectTransform.anchoredPosition =
                        new Vector2(rectTransform.sizeDelta.x, rectTransform.anchoredPosition.y);
                }
                else
                {
                    rectTransform.anchoredPosition = Vector2.zero;
                }

                _dict.Add(id, role);
                return role;
            }
        }
    }
}