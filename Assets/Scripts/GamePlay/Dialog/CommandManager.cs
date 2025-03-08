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
        Stop,
        Next,
        Event,
        Wait,
        Clear,
        Tip
    }

    public class CommandManager
    {
        public CommandType CheckCommand(string content)
        {
            if (content.Contains("#stop"))
                return CommandType.Stop;
            
            if (content.Contains("#next"))
                return CommandType.Next;
            
            if (content.Contains("#event"))
                return CommandType.Event;
            
            if (content.Contains("#wait"))
                return CommandType.Wait;

            if (content.Contains("#clear"))
                return CommandType.Clear;

            if (content.Contains("#tip"))
                return CommandType.Tip;
            
            return CommandType.None;
        }
    }
}