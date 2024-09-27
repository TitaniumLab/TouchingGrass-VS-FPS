using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GrassVsFps
{
    public class CustomSlider : Slider, IEndDragHandler, IBeginDragHandler
    {
        private float _lastValue;
        private bool _isDragged = false;
        private bool _isValueChanged = false;
        [SerializeField] private CustomSliderEvent _onEndDragAndValueChanged = new CustomSliderEvent();
        /// <summary>
        /// Callback when the slider drag end to new value
        /// </summary>
        public CustomSliderEvent OnEndDragAndValueChanged { get { return _onEndDragAndValueChanged; } set { _onEndDragAndValueChanged = value; } }


        protected override void Start()
        {
            base.Start();
            _lastValue = value;
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (_lastValue != value)
            {
                _isValueChanged = true;
                _lastValue = value;
            }
        }


        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (!_isDragged && _isValueChanged)
            {
                _onEndDragAndValueChanged?.Invoke(value);
                _isValueChanged = false;
            }
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragged = true;
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            if (value != _lastValue)
            {
                _onEndDragAndValueChanged?.Invoke(value);
            }
            _isDragged = false;
        }


        [Serializable]
        public class CustomSliderEvent : UnityEvent<float> { }
    }
}
