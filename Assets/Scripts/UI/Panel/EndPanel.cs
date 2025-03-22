// // ********************************************************************************************
// //     /\_/\                           @file       EndPanel.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025031721
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UI.Panel
{
    public class EndPanel:BasePanel<EndPanel>
    {
        [SerializeField] private VideoPlayer videoPlayer;
        public override void Init()
        {
            base.Init();
            UIManager.Instance.AddExcludedPanels(GetType());
            GetControl<Button>("Main").onClick.AddListener(End);
            MyEventSystem.Instance.AddEventListener<int>(CMDNAME.EVENT, (eId) =>
            {
                if (eId == 81)
                {
                    DialogPanel.Instance.HideMe();
                    ShowMe();
                }
            });
        }

        private void End()
        {
            GetControl<Button>("Main").enabled = false;
            GetControl<Image>("End").DOFade(1f, UIConst.UI_PANEL_ANIM * 10f).OnComplete(() =>
            {
                GetControl<Image>("End").color = Color.clear;
                PlayVideoAsync().Forget();
            });
        }

        public override void ShowAnim()
        {
            GetControl<Button>("Main").enabled = true;
            CanvasGroupInstance.DOKill(true);
            gameObject.SetActive(true);
            CanvasGroupInstance.interactable = true;
            CanvasGroupInstance.DOFade(1f, UIConst.UI_PANEL_ANIM);
        }

        public override void HideAnim()
        {
            CanvasGroupInstance.DOKill(true);
            CanvasGroupInstance.interactable = false;
            CanvasGroupInstance.DOFade(0f, UIConst.UI_PANEL_ANIM).OnComplete(() => { gameObject.SetActive(false); });
        }

        public override void OnPressedEsc()
        {
            if (!videoPlayer.isPlaying) return;
            videoPlayer.gameObject.SetActive(false);
            videoPlayer.Stop();
        }
        
        private async UniTask PlayVideoAsync()
        {
            Debug.Log("PlayVideoAsync 开始");
            AudioMgr.Instance.StopMusic();
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.Prepare();
            await UniTask.WaitUntil(() => videoPlayer.isPrepared);

            Debug.Log("Video 已准备，开始播放");

            videoPlayer.Play();
            await UniTask.WaitUntil(() => videoPlayer.isPlaying);

            Debug.Log("Video 正在播放");

            await UniTask.WaitUntil(() => !videoPlayer.isPlaying);

            Debug.Log("Video 播放结束");
            UIManager.Instance.ClearPanels();
            videoPlayer.gameObject.SetActive(false);
            GameStartPanel.Instance.ShowMe();
        }
    }
}