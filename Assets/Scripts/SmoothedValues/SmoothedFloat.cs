using System;
using UnityEngine;

namespace mpm_modules.SmoothedValues
{
    [Serializable]
    public class SmoothedFloat
    {
        public float smoothTime;
        public float maxSpeed;

        public float Value
        {
            get => currentValue;
            set => targetValue = value;
        }

        private float velocity;
        private float targetValue;
        private float currentValue;

        public SmoothedFloat(float smoothTime, float initialValue = 0, float maxSpeed = Mathf.Infinity)
        {
            this.smoothTime = smoothTime;
            this.maxSpeed = maxSpeed;
            currentValue = initialValue;
            targetValue = initialValue;
        }

        public void Update(float deltaTime)
        {
            currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref velocity, smoothTime, maxSpeed, deltaTime);
        }

        public override string ToString()
        {
            return
                $"{nameof(smoothTime)}: {smoothTime}, {nameof(maxSpeed)}: {maxSpeed}, {nameof(velocity)}: {velocity}, {nameof(targetValue)}: {targetValue}, {nameof(currentValue)}: {currentValue}";
        }

        public static implicit operator float(SmoothedFloat me)
        {
            return me.Value;
        }
    }
}
