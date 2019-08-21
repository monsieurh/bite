using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField, Range(0, 1)] private float randomAmount;

    [SerializeField] private List<string> names = new List<string>
    {
        "Brenda",
        "Kathia",
        "Gurl",
        "April",
        "Rex",
        "Mai",
        "Star",
        "Ziggy",
        "Kiwi",
        "Diva",
        "Pepper",
        "Paris",
        "Sample",
        "Error",
        "Fifi",
        "Hula",
        "Rumor",
        "Peaches",
        "Polaris",
        "Harlow",
        "Ramona",
        "Chastity",
        "Florence",
        "Gertrude",
        "Beth",
        "Ethel",
        "Harlow",
        "Ramona",
        "Chastity",
        "Florence",
        "Gertrude",
        "Beth",
        "Ethel",
        "Margo"
    };

    public Card GenerateCard()
    {
        Card card = Instantiate(cardPrefab, transform);
        int derp = Random.Range(1, 4);
        if ((derp & 1) == 1) card.love = Random.Range(-randomAmount, randomAmount);
        if ((derp & 2) == 2) card.sex = Random.Range(-randomAmount, randomAmount);

        card.CharacterName = names[Random.Range(0, names.Count)];
        return card;
    }
}
