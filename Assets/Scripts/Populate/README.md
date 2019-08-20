#Populate

## usage

The following code works as expected. You can call `Populate` on any class deriving `MonoBehaviour`. 
You can call `Populate` in `OnValidate` or wherever you see fit.
```C#
public class MyBehaviour : MonoBehaviour
{
    [GetComponent] public Image image;
    [GetComponentInChildren] public Text message;
    [FindObjectOfType] public Player player;
    private void Awake()
    {
        this.Populate();
    }
}

```
