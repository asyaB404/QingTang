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
    public class Tip
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ColorId { get; set; }

        public bool HasRedPoint { get; set; }

        public Tip(int id, string name, int colorId)
        {
            Id = id;
            Name = name;
            ColorId = colorId;
            HasRedPoint = true;
        }
        
        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}