using mpm_modules.Populate;
using mpm_modules.SmoothedValues;
using UnityEngine;

public class Card : MonoBehaviour
{
    [FindObjectOfType] private PlayerInput input;
    [SerializeField] private float smoothTime = 1;
    private Vector2 initialPosition;
    private SmoothedFloat offset;

    private void Awake()
    {
        initialPosition = transform.position;
        offset = new SmoothedFloat(smoothTime);
        this.Populate();
    }

    private void Update()
    {
        offset.Value = input.Delta.x;
        offset.Update(Time.deltaTime);
        transform.position = initialPosition + new Vector2(offset, 0);
    }
}
