// // ********************************************************************************************
// //     /\_/\                           @file       TipsDict.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031321
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************


namespace GamePlay.Tips
{
    public struct Tip
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public Tip(int id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}