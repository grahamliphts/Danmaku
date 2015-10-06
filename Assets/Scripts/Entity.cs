using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    int _speed = 1;

	public struct EntityStruct
    {
        public Vector2 position;
        public int speed;
        public bool isActive;
        public float angle;
    }

    public virtual void SetStruct(EntityStruct es)
    {

        transform.position = es.position;
        _speed = es.speed;
        transform.rotation = Quaternion.AngleAxis(es.angle, Vector3.right);
        gameObject.SetActive(es.isActive);
    }

    public virtual EntityStruct CreateStruct()
    {
        var es = new EntityStruct();
        es.position = transform.position;
        es.speed = _speed;
        es.isActive = gameObject.activeSelf;
        es.angle = transform.localRotation.eulerAngles.z;

        return es;
    }
}
