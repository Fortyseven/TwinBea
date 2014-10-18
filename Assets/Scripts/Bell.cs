using UnityEngine;
using System.Collections;

public class Bell : MonoBehaviour
{
    public float Speed = 0.75f;
    public Color[] Colors;
    int _curColor;
    SpriteRenderer _rend;

    public enum BellType {
        POINTS,
        SPEED,
        SHOTS
    };

    // Use this for initialization
    void Awake()
    {
        _rend = GetComponentInChildren<SpriteRenderer>();
        _curColor = Random.Range( 0, Colors.Length );
        _rend.color = Colors[ _curColor ];
        audio.Play();
    }
    
    // Update is called once per frame
    void Update()
    {
        rigidbody2D.AddForce( new Vector2( -40.0f * Time.deltaTime, 0 ) );
    }

    public void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.CompareTag( "playerbullet" ) )
        {
            _curColor++;
            _curColor = _curColor % Colors.Length;
            _rend.color = Colors[ _curColor ];
            rigidbody2D.AddForce( new Vector2( 45.0f, 0 ) );
            Destroy( other.gameObject );
            audio.Play();
        }
    }

    public BellType GetBellType()
    {
        return (BellType) _curColor;
    }
}
