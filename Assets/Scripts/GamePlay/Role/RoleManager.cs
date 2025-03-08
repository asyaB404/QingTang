// // ********************************************************************************************
// //     /\_/\                           @file       RoleManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030816
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System.Collections.Generic;

namespace GamePlay
{
    public class RoleManager
    {
        private Dictionary<int, Role> _dict = new();
        public Role CreateRole()
        {
            return new Role();
        }
    }
}