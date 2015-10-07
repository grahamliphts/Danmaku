using UnityEngine;
using System.Collections;

public class PlayerController : Entity
{
    private int _movementSpeed = 4;

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

        //WorldState ws = Step(WorldState state, Time.deltaTime);
    }

    public void MoveUp()
    {
        //transform.Translate((Vector2.up * _movementSpeed) * 2);
        gameObject.transform.Translate(Vector3.up * 0.1F);
    }

    public void MoveDown()
    {
        //transform.Translate((-(Vector2.up) * _movementSpeed) * 2);
        gameObject.transform.Translate(-Vector3.up * 0.1F);
    }
    public void MoveRight()
    {
        //transform.Translate((Vector2.right * _movementSpeed) * 2);
        gameObject.transform.Translate(Vector3.right * 0.1F);
    }

    public void MoveLeft()
    {
        // transform.Translate((-(Vector2.right) * _movementSpeed) * 2);
        gameObject.transform.Translate(-Vector3.right * 0.1F);
    }

}