// // ********************************************************************************************
// //     /\_/\                           @file       MapPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031012
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using DG.Tweening;
using GamePlay;
using UnityEngine.UI;

namespace UI.Panel
{
    public class MapPanel : BasePanel<MapPanel>
    {
        public override void Init()
        {
            base.Init();
            GetControl<Button>("home").onClick.AddListener(() => { MessagePanel.Instance.ShowMessage("返回主界面？", MainPanel.Instance.HideMe);});
            GetControl<Button>("save").onClick.AddListener(HideMe);
            GetControl<Button>("back").onClick.AddListener(HideMe);
            GetControl<Button>("setting").onClick.AddListener(SettingsPanel.Instance.ShowMe);
            GetControl<Button>("main").onClick.AddListener(HideMe);
            GetControl<Button>("8").onClick.AddListener(() =>
            {
                if (SaveManager.Instance.CheckHasFinishedDialog(2))
                {
                    MessagePanel.Instance.ShowMessage("是否再次回看关键剧情？", () =>
                    {
                        MessagePanel.Instance.HideMe();
                        Game01.MainGame01.InitGame();
                    });
                    return;
                }

                Game01.MainGame01.InitGame();
            });
            GetControl<Button>("15").onClick.AddListener(() =>
            {
                if (SaveManager.Instance.CheckHasFinishedDialog(1))
                {
                    MessagePanel.Instance.ShowMessage("是否再次回看关键剧情？", () =>
                    {
                        MessagePanel.Instance.HideMe();
                        DialogManager.Instance.Load(1);
                    });
                    return;
                }

                DialogManager.Instance.Load(1);
            });
            GetControl<Button>("20").onClick.AddListener(() => { DialogManager.Instance.Load(3); });
            GetControl<Button>("25").onClick.AddListener(() => { DialogManager.Instance.Load(4); });
            GetControl<Button>("market").onClick.AddListener(() =>
            {
                MessagePanel.Instance.ShowMessage("这里人太多太嘈杂了，去居民区看看吧！");
            });
            GetControl<Button>("school").onClick.AddListener(() =>
            {
                MessagePanel.Instance.ShowMessage("工人正在抢修大门，真辛苦……原来监控也在抢修");
            });
            GetControl<Button>("cup").onClick.AddListener(() =>
            {
                MessagePanel.Instance.ShowMessage("三桥溪村的水非常干净，渴了就喝三桥溪矿泉水！营养好味……后面忘了！");
            });
            GetControl<Button>("square").onClick.AddListener(() =>
            {
                MessagePanel.Instance.ShowMessage("广场空荡荡的，去居民区看看吧！");
            });
        }

        public override void ShowAnim()
        {
            gameObject.SetActive(true);
            AudioMgr.Instance.PlayMusic("map");
            CanvasGroupInstance.DOKill(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UI_PANEL_ANIM);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOKill(true);
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}