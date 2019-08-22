#pragma warning disable 649
using mpm_modules.Populate;
using mpm_modules.SmoothedValues;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Card : MonoBehaviour
{
    [SerializeField] private float smoothTime = 1;
    [SerializeField] private float deadZoneScreen = .5f;
    [SerializeField, Range(0f, 1f)] private float cardEndingTargetOffset = .5f;
    [FindObjectOfType] private PlayerInput input;
    [GetComponent] private Animator animator;
    [GetComponentInChildren] private Text nameText;
    public bool IsSwiped { get; private set; }
    public bool IsAccepted { get; private set; }

    public string CharacterName
    {
        set => nameText.text = value;
    }

    private Vector2 initialPosition;
    private SmoothedFloat offset;
    private static readonly int OnDecision = Animator.StringToHash("onDecision");
    [HideInInspector] public float love;
    [HideInInspector] public float sex;
    private static readonly int SwipingLeft = Animator.StringToHash("IsSwipingLeft");
    private static readonly int SwipingRight = Animator.StringToHash("IsSwipingRight");
    private float deadZone;

    private void Awake()
    {
        initialPosition = transform.position;
        offset = new SmoothedFloat(smoothTime);
        if (!this.Populate()) enabled = false;
        input.onUp.AddListener(ValidateCard);
        deadZone = Screen.width * deadZoneScreen;
    }

    private void Update()
    {
        if (!IsSwiped) offset.Value = input.Delta.x;
        offset.Update(Time.deltaTime);
        transform.position = initialPosition + new Vector2(offset, 0);
        animator.SetBool(SwipingLeft, IsSwipingLeft());
        animator.SetBool(SwipingRight, IsSwipingRight());
    }

    private bool IsSwiping()
    {
        return Mathf.Abs(offset) > deadZone;
    }

    public bool IsSwipingRight()
    {
        return IsSwiping() && offset > 0;
    }

    public bool IsSwipingLeft()
    {
        return IsSwiping() && offset < 0;
    }

    private void ValidateCard()
    {
        if (IsSwiped || !IsSwiping()) return;
        IsSwiped = true;
        offset.Value = IsSwipingLeft() ? -cardEndingTargetOffset : cardEndingTargetOffset;
        animator.SetTrigger(OnDecision);
        IsAccepted = offset > 0;
        Destroy(gameObject, 0.5f);
    }

    public float Bonus(string statName)
    {
        float multiplier = 0;
        if (IsSwipingLeft()) multiplier = -1f;
        if (IsSwipingRight()) multiplier = 1f;

        switch (statName)
        {
            case "Love":
                return love * multiplier;
            case "Sex":
                return sex * multiplier;
            default:
                return 0;
        }
    }
}
