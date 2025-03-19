// // ********************************************************************************************
// //     /\_/\                           @file       Role.cs
// //    ( o.o )                          @brief     QingTang
// //     > ^ <                           @author     Basya
// //    /     \
// //   (       )                         @Modified   2025030810
// //   (___)___)                         @Copyright  Copyright (c) 2025, Basya
// // ********************************************************************************************

using System;
using Cysharp.Threading.Tasks;
using Data;
using DG.Tweening;
using UI.Panel;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class Role : MonoBehaviour
    {
        public string roleName;
        public Image roleImage;
        public Image win;
        [SerializeField] private int curFace;
        [SerializeField] private Sprite[] idleSprites;
        [SerializeField] private Sprite[] faceSprites;
        [SerializeField] private float doAnimDuration = 5f;
        [SerializeField] private float faceDuration = 0.5f;
        private float _animTimer = 4f;

        [SerializeField] private Image fire;

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

        private void OnDestroy()
        {
            if (win != null)
                Destroy(win.gameObject);
        }

        public Role SetFace(int faceId)
        {
            if (faceId == 99)
            {
                transform.DOLocalMoveY(-100f, 0.1f) 
                    .SetLoops(8, LoopType.Yoyo) 
                    .SetEase(Ease.OutQuad);
                DialogPanel.Instance.CupAnim();
                faceId = 0;
            }

            if (faceId == 1 && roleName[0] == '李')
            {
                win.gameObject.SetActive(true);
                win.transform.SetParent(transform.parent);
                win.transform.SetAsFirstSibling();
                win.rectTransform.DOAnchorPosX(284, MyConst.ROLE_MOVE);
            }

            curFace = faceId;
            if (curFace == 0)
            {
                roleImage.sprite = idleSprites[0];
                roleImage.SetNativeSize();
            }

            faceId -= 1;
            if (faceId >= faceSprites.Length || faceId < 0) return this;
            roleImage.sprite = faceSprites[faceId];
            roleImage.SetNativeSize();
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
            else if (anchoredString == "C")
            {
                anchored.x = 0.5f;
                if (isInit)
                {
                    MyEventSystem.Instance.EventTrigger<bool>("phone", true);
                }
            }
            else
            {
                return this;
            }

            var rectTransform = (RectTransform)transform;
            rectTransform.anchorMin = anchored;
            rectTransform.anchorMax = anchored;
            rectTransform.pivot = anchored;
            if (isInit)
            {
                roleImage.color = new Color(1, 1, 1, 0);
                roleImage.DOFade(1, MyConst.ROLE_MOVE * 1.5f);
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
                    rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y);
                }
            }

            return this;
        }

        public Role Move(string type, float amount)
        {
            var rectTransform = (RectTransform)transform;
            if (type == "L" || type == "R")
            {
                amount *= 150;
                rectTransform.DOAnchorPosX(amount, MyConst.ROLE_MOVE);
            }
            else if (type == "S")
            {
                rectTransform.DOScale(amount, MyConst.ROLE_MOVE / 2f);
                if (amount >= 1.5f)
                {
                    fire.gameObject.SetActive(true);
                }
            }
            else if (type == "F")
            {
                roleImage.DOFade(amount, MyConst.ROLE_MOVE * 1.5f);
                MyEventSystem.Instance.EventTrigger<bool>("phone", false);
            }

            return this;
        }

        [ContextMenu("12")]
        private void Test()
        {
            SetFace(99);
        }
    }
}