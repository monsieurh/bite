using System.Collections;
using mpm_modules.Populate;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CardGenerator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float delayBeforeNextCard;
    [SerializeField] private GameObject gameOverGui;
    public bool IsAlive => Sex > 0 && Sex < 1 && Love > 0 && Love < 1;
    [GetComponent] private CardGenerator cardGenerator;
    public float Sex { get; private set; } = 0.5f;
    public float Love { get; private set; } = 0.5f;

    private IEnumerator Start()
    {
        this.Populate();
        gameOverGui.SetActive(false);
        while (IsAlive)
        {
            Card currentCard = DrawCard();
            yield return new WaitUntil(() => currentCard.IsSwiped);
            Apply(currentCard);
            yield return new WaitForSecondsRealtime(delayBeforeNextCard);
        }

        gameOverGui.SetActive(true);
        yield return WaitForClick();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Apply(Card card)
    {
        if (card.IsAccepted)
        {
            Love += card.love;
            Sex += card.sex;
        }
        else
        {
            Love -= card.love;
            Sex -= card.sex;
        }
    }

    private Card DrawCard()
    {
        return cardGenerator.GenerateCard();
    }

    //TODO: you lazy bitch

    public float this[string statName]
    {
        get
        {
            switch (statName)
            {
                case "Love":
                    return Love;
                case "Sex":
                    return Sex;
                default:
                    return 0;
            }
        }
    }

    private static IEnumerator WaitForClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButton(0));
    }
}
