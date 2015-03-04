using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float BaseSpeed = 2.0f;
    public GameObject BellPrefab;
    private float _speed;
    private bool _has_bell;

    // Use this for initialization
    public void Start()
    {
        _speed = BaseSpeed + Random.Range( BaseSpeed - 1.5f, BaseSpeed + 1.5f );
        _has_bell = true;
    }

    // Update is called once per frame
    public void Update()
    {
        transform.Translate( -1 * Time.deltaTime * _speed, 0, 0 );
        if ( transform.position.x <= -2.0f ) {
            Destroy( this.gameObject );
        }
    }

    public void OnTriggerEnter2D( Collider2D other )
    {
        if ( other.tag == "playerbullet" ) {
            HitByPlayer();
        }
    }

    private void HitByPlayer()
    {
        if ( _has_bell ) {
            _has_bell = false;
            Instantiate( BellPrefab, transform.position, Quaternion.identity );
        }
    }

    public void Kill()
    {
        Destroy( this.gameObject );
    }
}
