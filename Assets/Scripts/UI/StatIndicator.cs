using mpm_modules.Populate;
using mpm_modules.SmoothedValues;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StatIndicator : MonoBehaviour
    {
        [SerializeField] private string statName;
        [SerializeField] private float smoothTime;
        [SerializeField] private Image filler;
        [FindObjectOfType] private Player player;
        private SmoothedFloat smoothedFloat;

        private void Awake()
        {
            this.Populate();
            smoothedFloat = new SmoothedFloat(smoothTime);
        }

        private void Update()
        {
            smoothedFloat.Value = player[statName];
            smoothedFloat.Update(Time.deltaTime);
            filler.fillAmount = Mathf.Clamp01(smoothedFloat);
        }
    }
}
