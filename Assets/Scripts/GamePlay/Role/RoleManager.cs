// // ********************************************************************************************
// //     /\_/\                           @file       RoleManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030816
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Collections.Generic;
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
        }

        public void Clear()
        {
            foreach (var role in _dict.Values)
            {
                Object.Destroy(role.gameObject);
            }

            _dict.Clear();
        }

        public void SetHighLight(int id)
        {
            foreach (var item in _dict)
            {
                int curId = item.Key;
                Role role = item.Value;
                if (curId == 6)
                {
                    continue;
                }

                if (curId == id)
                {
                    role.roleImage.color = Color.white;
                }
                else
                {
                    role.roleImage.color = Color.gray;
                }
            }
        }

        public Role GetRole(int id, string anchoredString = "")
        {
            if (_dict.TryGetValue(id, out var role))
            {
                return role.SetAnchor(anchoredString);
            }

            var objRes = Resources.Load<GameObject>("Prefabs/Roles/" + id);
            var obj = Object.Instantiate(objRes, _rolesParent, false);
            role = obj.GetComponent<Role>();
            role.SetAnchor(anchoredString,true);
            _dict.Add(id, role);
            return role;
        }
    }
}