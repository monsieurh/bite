using mpm_modules.Populate;
using mpm_modules.SmoothedValues;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Card : MonoBehaviour
{
    [FindObjectOfType] private PlayerInput input;
    [SerializeField] private float smoothTime = 1;
    [GetComponent] private Animator animator;
    private Vector2 initialPosition;
    private SmoothedFloat offset;
    private static readonly int X = Animator.StringToHash("x");
    private static readonly int OnDecision = Animator.StringToHash("onDecision");

    private void Awake()
    {
        initialPosition = transform.position;
        offset = new SmoothedFloat(smoothTime);
        this.Populate();
        input.onUp.AddListener(() => ValidateCard());
    }

    private void Update()
    {
        offset.Value = input.Delta.x;
        offset.Update(Time.deltaTime);
        transform.position = initialPosition + new Vector2(offset, 0);
        animator.SetFloat(X, offset);
    }

    private void ValidateCard()
    {
        animator.SetTrigger(OnDecision);
        if (offset > 0) Accept();
        else Decline();
    }

    private void Accept()
    {
        throw new System.NotImplementedException();
    }

    private void Decline()
    {
        throw new System.NotImplementedException();
    }
}
