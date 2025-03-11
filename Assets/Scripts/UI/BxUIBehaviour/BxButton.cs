using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.BxUIBehaviour
{
    public class BxButton : Button, IBxUIBehaviour
    {
        public event Action OnButtonDown;
        public event Action OnButtonUp;
        public event Action OnButtonEnter;
        public event Action OnButtonExit;

        public event Action OnDoubleClick;
        private float _lastClickTime = 0f;

        public event Action OnButtonHold;
        public event Action OnButtonHoldRelease;
        public event Action OnButtonHoldFinishedRelease;

        private bool _isHolding = false;
        private float _holdTime = 0f;
        public float holdThreshold = 0f;

        private void Update()
        {
            if (!_isHolding || holdThreshold <= 0f) return;
            _holdTime += Time.deltaTime;
            if (_holdTime >= holdThreshold)
            {
                OnButtonHold?.Invoke();
                _isHolding = false;
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            _lastClickTime = Time.time;
            if (Time.time - _lastClickTime <= UIConst.BTN_DOUBLE_CLICK)
            {
                OnDoubleClick?.Invoke();
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnButtonEnter?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            OnButtonExit?.Invoke();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _isHolding = true;
            _holdTime = 0f;
            OnButtonDown?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnButtonUp?.Invoke();
            if (_isHolding && _holdTime < holdThreshold)
            {
                OnButtonHoldRelease?.Invoke();
            }
            else if (_isHolding && _holdTime >= holdThreshold)
            {
                OnButtonHoldFinishedRelease?.Invoke();
            }

            _isHolding = false;
            _holdTime = 0f;
        }
    }
}