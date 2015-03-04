using UnityEngine;

public class Walter : MonoBehaviour
{
    public float BaseSpeed = 0.05f;
    float _speed;
    float _base_y;
    float _c, _yspeed;

    private AudioSource _audio;
    private Collider2D _collider_2d;
    private Renderer _renderer;

    // Use this for initialization
    public void Start()
    {
        _speed = BaseSpeed * Random.Range( 0.01f, 1.0f );
        _base_y = transform.position.y;
        _c = Random.Range( 0, 100 );
        _yspeed = Random.Range( 0.05f, 0.005f );

        _audio = GetComponent<AudioSource>();
        _collider_2d = GetComponent<Collider2D>();
        _renderer = GetComponent<Renderer>();
    }

    public void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.tag == "playerbullet" ) {
            Kill();
            Destroy( other.gameObject );
        }
    }

    // Update is called once per frame
    public void Update()
    {
        transform.Translate( -1 * Time.deltaTime * _speed, 0, 0 );
        Vector3 pos = transform.position;
        pos.y = _base_y + Mathf.Sin( _c * ( _yspeed * 75.0f ) ) * 1.0f;
        transform.position = pos;
        //   _c += Time.deltaTime;
        _c += Time.deltaTime;

        if ( transform.position.x <= -2.0f ) {
            Destroy( this.gameObject );
        }
    }

    void Kill()
    {
        _audio.Play();
        //        enabled = false;
        _collider_2d.enabled = false;
        _renderer.enabled = false;
        Game.instance.AdjustScore( 100 );
        Destroy( this.gameObject, 0.5f );
    }
}
