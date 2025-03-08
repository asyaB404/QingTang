// // ********************************************************************************************
// //     /\_/\                           @file       CommandManager.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030817
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

namespace GamePlay
{
    public enum CommandType
    {
        None,
        Stop
    }

    public class CommandManager
    {
        public CommandType CheckCommand(string content)
        {
            if (content.Contains("#stop"))
            {
                return CommandType.Stop;
            }
            return CommandType.None;
        }
    }
}