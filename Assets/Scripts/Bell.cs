using UnityEngine;

public class Bell : MonoBehaviour
{
    public float Speed = 0.75f;
    public Color[] Colors;
    int _cur_color;
    SpriteRenderer _rend;
    private AudioSource _audio;
    private Rigidbody2D _rigidbody2D;
    public enum BellType
    {
        POINTS,
        SPEED,
        SHOTS
    };

    // Use this for initialization
    public void Awake()
    {
        _rend = GetComponentInChildren<SpriteRenderer>();
        _cur_color = Random.Range( 0, Colors.Length );
        _rend.color = Colors[ _cur_color ];
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _audio.Play();
    }

    // Update is called once per frame
    public void Update()
    {
        _rigidbody2D.AddForce( new Vector2( -40.0f * Time.deltaTime, 0 ) );
    }

    public void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.CompareTag( "playerbullet" ) ) {
            _cur_color++;
            _cur_color = _cur_color % Colors.Length;
            _rend.color = Colors[ _cur_color ];
            _rigidbody2D.AddForce( new Vector2( 45.0f, 0 ) );
            Destroy( other.gameObject );
            _audio.Play();
        }
    }

    public BellType GetBellType()
    {
        return (BellType)_cur_color;
    }
}
