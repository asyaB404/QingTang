// // ********************************************************************************************
// //     /\_/\                           @file       Role.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030810
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class Role : MonoBehaviour
    {
        public string roleName;
        public Image roleImage;
        [SerializeField] private int curFace;
        [SerializeField] private Sprite[] idleSprites;
        [SerializeField] private Sprite[] faceSprites;
        [SerializeField] private float doAnimDuration = 5f;
        [SerializeField] private float faceDuration = 0.5f;
        private float _animTimer = 4f;


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

        /// <summary>
        /// 眨眼
        /// </summary>
        private async UniTask IdleFaceAnim()
        {
            int i = 0;
            float d = faceDuration / idleSprites.Length;
            while (curFace == 0 && i < idleSprites.Length && roleImage != null)
            {
                roleImage.sprite = idleSprites[i];
                i++;
                await UniTask.WaitForSeconds(d);
            }
        }

        public Role SetFace(int faceId)
        {
            curFace = faceId;
            faceId -= 1;
            if (faceId >= faceSprites.Length || faceId < 0) return this;
            roleImage.sprite = faceSprites[faceId - 1];
            return this;
        }

        public Role SetAnchor(string anchoredString, bool isInit = false)
        {
            if (string.IsNullOrEmpty(anchoredString)) return this;
            Vector2 anchored = new Vector2(0.5f, 0.5f);
            if (anchoredString == "L")
            {
                anchored.x = 0;
            }
            else if (anchoredString == "R")
            {
                anchored.x = 1;
            }

            var rectTransform = (RectTransform)transform;
            rectTransform.anchorMin = anchored;
            rectTransform.anchorMax = anchored;
            rectTransform.pivot = anchored;
            if (isInit)
            {
                if (anchored.x < 0.5f)
                {
                    rectTransform.anchoredPosition =
                        new Vector2(-rectTransform.sizeDelta.x, rectTransform.anchoredPosition.y);
                }
                else if (anchored.x > 0.5f)
                {
                    rectTransform.anchoredPosition =
                        new Vector2(rectTransform.sizeDelta.x, rectTransform.anchoredPosition.y);
                }
                else
                {
                    rectTransform.anchoredPosition = Vector2.zero;
                }
            }

            return this;
        }

        public Role Move(string type, float amount)
        {
            var rectTransform = (RectTransform)transform;
            if (type == "L" || type == "R")
            {
                amount *= 108;
                rectTransform.DOAnchorPosX(amount, MyConst.ROLE_MOVE);
            }
            else if (type == "S")
            {
                rectTransform.DOScale(amount, MyConst.ROLE_MOVE / 2f);
            }

            return this;
        }
    }
}