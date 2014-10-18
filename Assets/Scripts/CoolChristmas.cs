using UnityEngine;
using System.Collections;

public class CoolChristmas : MonoBehaviour {

    Vector3 offs;
    int c = 0;

    float _baseY, _speed;

    void Awake()
    {
        _baseY = transform.position.y;
        _speed = 0.005f;
    }

    void Start () {
    
    }
    
    
    void Update () {
        c++;

        offs = transform.position;
        offs.y = _baseY + Mathf.Sin(c * 0.1f) * 0.25f;
        offs.x -= 1.0f * _speed;
        transform.position = offs;
    }

    void Respawn()
    {

    }
}
