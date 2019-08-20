using System;
using UnityEngine;

namespace mpm_modules.SmoothedValues
{
    [Serializable]
    public class SmoothedVector
    {
        public float smoothTime;
        public float maxSpeed;

        public Vector3 Value
        {
            get => currentValue;
            set => targetValue = value;
        }

        private Vector3 velocity;
        private Vector3 targetValue;
        private Vector3 currentValue;

        public SmoothedVector(float smoothTime, Vector3 initialValue, float maxSpeed = float.PositiveInfinity)
        {
            this.smoothTime = smoothTime;
            this.maxSpeed = maxSpeed;
            currentValue = initialValue;
            targetValue = initialValue;
        }

        public SmoothedVector(float smoothTime, float maxSpeed = float.PositiveInfinity)
        {
            this.smoothTime = smoothTime;
            this.maxSpeed = maxSpeed;
            currentValue = Vector3.zero;
            targetValue = Vector3.zero;
        }

        public void Update(float deltaTime)
        {
            currentValue = Vector3.SmoothDamp(currentValue, targetValue, ref velocity, smoothTime, maxSpeed, deltaTime);
        }

        public override string ToString()
        {
            return
                $"{nameof(smoothTime)}: {smoothTime}, {nameof(maxSpeed)}: {maxSpeed}, {nameof(velocity)}: {velocity}, {nameof(targetValue)}: {targetValue}, {nameof(currentValue)}: {currentValue}";
        }

        public static implicit operator Vector3(SmoothedVector me)
        {
            return me.Value;
        }
    }
}
