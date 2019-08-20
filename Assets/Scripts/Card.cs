#pragma warning disable 649
using mpm_modules.Populate;
using mpm_modules.SmoothedValues;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Card : MonoBehaviour
{
    [SerializeField] private float smoothTime = 1;
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
    private static readonly int X = Animator.StringToHash("x");
    private static readonly int OnDecision = Animator.StringToHash("onDecision");
    public float love;
    public float sex;


    private void Awake()
    {
        initialPosition = transform.position;
        offset = new SmoothedFloat(smoothTime);
        this.Populate();
        input.onUp.AddListener(ValidateCard);
    }

    private void Update()
    {
        if (!IsSwiped) offset.Value = input.Delta.x;
        offset.Update(Time.deltaTime);
        transform.position = initialPosition + new Vector2(offset, 0);
        animator.SetFloat(X, offset);
    }

    private void ValidateCard()
    {
        if (IsSwiped) return;
        IsSwiped = true;
        animator.SetTrigger(OnDecision);
        IsAccepted = offset > 0;
        Destroy(gameObject, 0.5f);
    }
}
