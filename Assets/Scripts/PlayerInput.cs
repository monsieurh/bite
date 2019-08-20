using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool isDown;
    private Vector2 startPosition;
    private Vector2 currentPosition;

    public Vector2 Delta => isDown ? currentPosition - startPosition : Vector2.zero;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) OnUp(Input.mousePosition);
        if (Input.GetMouseButton(0)) OnSet(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) OnDown(Input.mousePosition);
    }

    private void OnUp(Vector2 mousePosition)
    {
        isDown = false;
        OnSet(mousePosition);
    }

    private void OnDown(Vector2 mousePosition)
    {
        isDown = true;
        startPosition = mousePosition;
        currentPosition = mousePosition;
    }

    private void OnSet(Vector2 mousePosition)
    {
        currentPosition = mousePosition;
    }
}
