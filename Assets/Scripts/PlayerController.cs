using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float _movementSpeed = 4.0f;
    private Vector2 _direction;
    private Rigidbody2D _rigidbody;

    private float _dist;
    private float _leftBorder, _rightBorder, _upBorder, _bottomBorder;
	// Use this for initialization
	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();  
    }
	
    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
        Debug.Log(transform.position);

    }
	// Update is called once per frame
	void FixedUpdate ()
    {
        _direction = Vector3.zero;
        if (Input.GetKey("z"))
            _direction += (Vector2)transform.up;
        if (Input.GetKey("s"))
            _direction -= (Vector2)transform.up;
        if (Input.GetKey("q"))
            _direction -= (Vector2)transform.right;
        if (Input.GetKey("d"))
            _direction += (Vector2)transform.right;

        _direction.Normalize();
        _direction *= _movementSpeed;

        _rigidbody.velocity = _direction;
        Vector3 velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y);
    }
}
