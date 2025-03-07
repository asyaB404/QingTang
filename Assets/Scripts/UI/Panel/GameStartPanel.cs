using UI;
using UnityEngine;
using UnityEngine.UI;

public class GameStartPanel : BasePanel<GameStartPanel>
{
    [SerializeField] private Button[] btns;

    public override void Init()
    {
        base.Init();
        btns[0].onClick.AddListener(() => { });
        btns[1].onClick.AddListener(() => { });
        btns[2].onClick.AddListener(Application.Quit);
    }
}
