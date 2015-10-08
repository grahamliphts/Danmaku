using UnityEngine;
using System.Collections;

public class PathPlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private int _speed = 4;

    void Update()
    {
        //Debug.Log("Update");
        transform.Translate(Vector2.up * Time.deltaTime * _speed);
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x >= 1 || pos.y >= 1 || pos.x <= 0 || pos.y <= 0)
            reset();
    }

    public void reset()
    {
        gameObject.SetActive(false);
        //Debug.Log("Reset");
    }

    public void play()
    {
        //Debug.Log("Play");
        gameObject.SetActive(true);
        transform.position = _player.transform.position;

        //transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        reset();
    }
}
