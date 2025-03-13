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
    public class CMDNAME
    {
        public const string GET_TIP = "#getTip";
        public const string STOP = "#stop";
        public const string NEXT = "#next";
        public const string EVENT = "#event";
        public const string WAIT = "#wait";
        public const string CLEAR = "#clear";
        public const string TIP = "#tip";
    }

    public class CommandManager
    {
        public void CheckCommand(string content)
        {
            if (content.Contains(CMDNAME.GET_TIP))
            {
                string[] split = content.Replace(CMDNAME.GET_TIP, "").Trim().Split(',');
                int tipId = int.Parse(split[0]);
                string tipName = split[1];
                MyEventSystem.Instance.EventTrigger<int,string>(CMDNAME.GET_TIP, tipId, tipName);
            }

            if (content.Contains(CMDNAME.STOP))
            {
                MyEventSystem.Instance.EventTrigger(CMDNAME.STOP);
            }

            if (content.Contains(CMDNAME.WAIT))
            {
                float wait = float.Parse(content.Replace(CMDNAME.WAIT, ""));
                MyEventSystem.Instance.EventTrigger<float>(CMDNAME.WAIT, wait);
            }

            if (content.Contains(CMDNAME.NEXT))
            {
                MyEventSystem.Instance.EventTrigger(CMDNAME.NEXT);
            }

            if (content.Contains(CMDNAME.EVENT))
            {
                content = content.Replace(CMDNAME.EVENT, "");
                MyEventSystem.Instance.EventTrigger<int>(CMDNAME.EVENT, int.Parse(content));
            }

            if (content.Contains(CMDNAME.CLEAR))
            {
                MyEventSystem.Instance.EventTrigger(CMDNAME.CLEAR);
            }

            if (content.Contains(CMDNAME.TIP))
            {
                string tipContent = content.Replace(CMDNAME.TIP, "").Trim();
                MyEventSystem.Instance.EventTrigger<string>(CMDNAME.TIP, tipContent);
            }
        }
    }
}