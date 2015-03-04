using UnityEngine;

public class CoolChristmas : MonoBehaviour
{

    Vector3 _offs;
    float  _c = 0;

    float _base_y, _speed;

    public void Awake()
    {
        _base_y = transform.position.y;
        _speed = 0.005f;
    }

    public void Start()
    {

    }


    public void Update()
    {
        _c += Time.deltaTime * 100.0f;

        _offs = transform.position;
        _offs.y = _base_y + Mathf.Sin( _c * 0.1f ) * 0.25f;
        _offs.x -= 1.0f * _speed;
        transform.position = _offs;
    }

    public void Respawn()
    {

    }
}
