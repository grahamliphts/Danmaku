using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour
{
    int _speed;
    public int EID;

    public struct EntityStruct
    {
        public Vector3 position;
        public float speed;
        public bool isActive;
        public Vector3 direction;
        public Quaternion rotation;
        public int ID;
    }

    /*public virtual void SetStruct(EntityStruct es)
    {

        transform.position = es.position;
        _speed = es.speed;
        transform.rotation = Quaternion.AngleAxis(es.angle, Vector3.right);
        gameObject.SetActive(es.isActive);
    }*/

    public virtual EntityStruct CreateStruct(Vector3 position,Vector3 ADirection,int EID, bool isactive)
    {
        var es = new EntityStruct();
        es.position = position;
        es.speed = _speed;
        es.isActive = isactive;
        es.direction = ADirection;
        es.ID = EID;
        //es.angle = transform.localRotation.eulerAngles.z;
        //es.angle =  Random.Range(0, 360);

        return es;
    }

    
}
