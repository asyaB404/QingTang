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

    public struct CommandInfo
    {
        public CommandType CommandType;
        public string TipContent;
        public float wait;
    }

    public class CommandManager
    {
        public CommandInfo CheckCommand(string content)
        {
            CommandInfo commandInfo = new CommandInfo
            {
                CommandType = CommandType.None
            };
            if (content.Contains("#stop"))
            {
                commandInfo.CommandType = CommandType.Stop;
            }
            if (content.Contains("#next"))
            {
                commandInfo.CommandType = CommandType.Next;
            }
            if (content.Contains("#event"))
            {
                commandInfo.CommandType = CommandType.Event;
                MyEventSystem.Instance.EventTrigger(content);
            }
            if (content.Contains("#wait"))
            {
                commandInfo.CommandType = CommandType.Wait;
                commandInfo.wait = float.Parse(content.Replace("#wait,", ""));
            }
            if (content.Contains("#clear"))
            {
                commandInfo.CommandType = CommandType.Clear;
            }
            if (content.Contains("#tip"))
            {
                commandInfo.CommandType = CommandType.Tip;
                commandInfo.TipContent = content.Replace("#tip ", "");
            }


            return commandInfo;
        }
    }
}