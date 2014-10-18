using UnityEngine;
using System.Collections;

public class Walter : MonoBehaviour {
    public float BaseSpeed = 0.05f;
    float _speed;
    float _baseY;
    float _c, _yspeed;

    // Use this for initialization
    void Start () {
        _speed = BaseSpeed * Random.Range( 0.01f, 1.0f );
        _baseY = transform.position.y;
        _c = Random.Range(0, 100);
        _yspeed = Random.Range(0.05f, 0.005f);
    }

    public void OnTriggerEnter2D( Collider2D other )
    {
        if (other.tag == "playerbullet") {
            Kill();
            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        transform.Translate( -1 * Time.deltaTime * _speed, 0, 0 );  
        Vector3 pos = transform.position;
        pos.y = _baseY + Mathf.Sin(_c * _yspeed) * 1.0f;
        transform.position = pos;
        _c++;

        if (transform.position.x <= -2.0f) {
            Destroy(this.gameObject);
        }
    }


    void Kill()
    {
        audio.Play();
//        enabled = false;
        collider2D.enabled = false;
        renderer.enabled = false;
        Game.instance.AdjustScore(100);
        Destroy(this.gameObject,0.5f);
    }
}
