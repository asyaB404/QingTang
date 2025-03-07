using System;
using Cysharp.Threading.Tasks;
using QTConfig;
using UI;
using UI.Panel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameStartPanel : BasePanel<GameStartPanel>
{
    [SerializeField] private Button[] btns;
    [SerializeField] private VideoPlayer videoPlayer;

    public override void Init()
    {
        base.Init();
        btns[0].onClick.AddListener(() => { PlayVideoAsync().Forget(); });
        btns[1].onClick.AddListener(() => { SettingsPanel.Instance.ShowMe(); });
        btns[2].onClick.AddListener(Application.Quit);
    }

    public override void OnPressedEsc()
    {
        if (!videoPlayer.isPlaying) return;
        videoPlayer.gameObject.SetActive(false);
        videoPlayer.Stop();
    }

    public override void CallBackWhenHeadPop(IBasePanel popPanel)
    {
        if (popPanel is SettingsPanel)
        {
            popPanel?.HideAnim();
            return;
        }
        popPanel?.HideAnim();
        ShowAnim();
    }

    private async UniTask PlayVideoAsync()
    {
        Debug.Log("PlayVideoAsync 开始");
    
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Prepare();
        await UniTask.WaitUntil(() => videoPlayer.isPrepared);

        Debug.Log("Video 已准备，开始播放");

        videoPlayer.Play();
        await UniTask.WaitUntil(() => videoPlayer.isPlaying);

        Debug.Log("Video 正在播放");

        await UniTask.WaitUntil(() => !videoPlayer.isPlaying);

        Debug.Log("Video 播放结束");

        videoPlayer.gameObject.SetActive(false);
    }
}