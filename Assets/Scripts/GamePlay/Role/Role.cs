// // ********************************************************************************************
// //     /\_/\                           @file       Role.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030810
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class Role : MonoBehaviour
    {
        [SerializeField] private Image roleImage;
        [SerializeField] private int curFace;
        [SerializeField] private Sprite[] idleSprites;
        [SerializeField] private Sprite[] faceSprites;
        [SerializeField] private float doAnimDuration = 5f;
        [SerializeField] private float faceDuration = 0.5f;
        private float _animTimer;


        private void Update()
        {
            if (curFace == 0)
            {
                _animTimer += Time.deltaTime;
                if (_animTimer > doAnimDuration)
                {
                    IdleFaceAnim().Forget();
                    _animTimer = 0;
                }
            }
        }

        private async UniTask IdleFaceAnim()
        {
            int i = 0;
            float d = faceDuration / idleSprites.Length;
            while (curFace == 0 && i < idleSprites.Length)
            {
                roleImage.sprite = idleSprites[i];
                i++;
                await UniTask.WaitForSeconds(d);
            }
        }

        public void SetFace(int faceId)
        {
            roleImage.sprite = faceSprites[faceId];
            curFace = faceId;
        }
        
        
    }
}