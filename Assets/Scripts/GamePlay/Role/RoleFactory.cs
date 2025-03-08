// // ********************************************************************************************
// //     /\_/\                           @file       RoleFactory.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030810
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

namespace GamePlay.Role
{
    public class RoleFactory
    {
        private static RoleFactory _instance;

        public static RoleFactory Instance
        {
            get
            {
                _instance ??= new RoleFactory();
                return _instance;
            }
        }

        public void CreateRole()
        {
            
        }
    }
}