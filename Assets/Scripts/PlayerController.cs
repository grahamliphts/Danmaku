using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float _movementSpeed = 4.0f;

    private float _dist;
    private float _leftBorder, _rightBorder, _upBorder, _bottomBorder;
	
    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
        if (Input.GetKey("z"))
            MoveUp();
        if (Input.GetKey("s"))
            MoveDown();
        if (Input.GetKey("q"))
            MoveLeft();
        if (Input.GetKey("d"))
            MoveRight();
    }

    void MoveUp()
    {
        transform.Translate((Vector2.up * _movementSpeed) * Time.deltaTime);
    }

    void MoveDown()
    {
        transform.Translate((-(Vector2.up) * _movementSpeed) * Time.deltaTime);
    }

    void MoveLeft()
    {
        transform.Translate((-(Vector2.right) * _movementSpeed) * Time.deltaTime);
    }

    void MoveRight()
    {
        transform.Translate((Vector2.right * _movementSpeed) * Time.deltaTime);
    }

}
