// // ********************************************************************************************
// //     /\_/\                           @file       GOUSHI.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025032112
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using UI.Panel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class GOUSHI:MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string goushi;
        public void OnPointerEnter(PointerEventData eventData)
        {
            MapPanel.Instance.SetMapName(goushi);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MapPanel.Instance.SetMapName(string.Empty);
        }
    }
}