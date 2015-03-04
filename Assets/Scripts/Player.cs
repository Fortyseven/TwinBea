using UnityEngine;

public class Player : MonoBehaviour
{
    public float BaseSpeed = 1.0f;
    public GameObject BulletPrefab;
    public GameObject BulletSpawnPoint;
    public GameObject Jaw;
    public Sprite FloaterBellScore, FloaterBellSpeed, FloaterBellShot;
    public AudioClip[] Clips;

    private const float NORMAL_SPEED = 0.05f;
    private const float POWEREDUP_SPEED = 1.5f;

    private  float _modfied_speed = 1.0f;
    private AudioSource _audio;

    enum Clip
    {
        CLIP_BELLCOLLECT = 0,
        CLIP_MAUDEDIE
    }

    private const float FIRE_TIMEOUT = 0.200f;
    private Animation _jaw_anim;
    private float _fire_timeout;

    public void Awake()
    {
        _jaw_anim = Jaw.GetComponentInChildren<Animation>();
        _audio = GetComponent<AudioSource>();
        _fire_timeout = Time.time;
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp( pos.x + ( Input.GetAxis( "Horizontal" ) * BaseSpeed ) * Time.deltaTime * _modfied_speed,
                                -1.7f, -0.3f );
        pos.y = Mathf.Clamp( pos.y + ( Input.GetAxis( "Vertical" ) * BaseSpeed ) * Time.deltaTime * _modfied_speed,
                                -1.1f, 1.1f );

        transform.position = pos;

        if ( Input.GetButton( "Fire1" ) && Time.time > _fire_timeout ) {
            Instantiate( BulletPrefab, BulletSpawnPoint.transform.position, Quaternion.identity );
            _jaw_anim.Play();
            _fire_timeout = Time.time + FIRE_TIMEOUT;
        }
    }

    public void OnTriggerEnter2D( Collider2D other )
    {
        //Debug.Log( other.name );
        if ( other.tag == "bell" ) {
            // switch color bell type
            PowerUp( other );
        }
        else if ( other.tag == "enemy" ) {
            _audio.PlayOneShot( Clips[ (int)Clip.CLIP_MAUDEDIE ] );
        }
    }

    public void PowerUp( Collider2D other )
    {
        _audio.PlayOneShot( Clips[ (int)Clip.CLIP_BELLCOLLECT ] );

        Bell bell = other.gameObject.GetComponent<Bell>();
        if ( bell == null )
            throw new UnityException( "No Bell component in " + other.name );

        switch ( bell.GetBellType() ) {
            case Bell.BellType.POINTS:
                Game.instance.AdjustScore( 1000 );
                Game.instance.SpawnFloater( other.gameObject, FloaterBellScore );
                break;
            case Bell.BellType.SHOTS:
                Game.instance.SpawnFloater( other.gameObject, FloaterBellShot );
                break;
            case Bell.BellType.SPEED:
                Game.instance.SpawnFloater( other.gameObject, FloaterBellSpeed );
                _modfied_speed = POWEREDUP_SPEED;
                break;
        }

        Destroy( other.gameObject );
    }

    public void SoftReset() // for life lost
    {
        _modfied_speed = NORMAL_SPEED;
    }

    public void OnAnimIntroFinished()
    {
        ScrollLayer[] layers = GameObject.Find( "Background" ).GetComponentsInChildren<ScrollLayer>();
        for ( int i = 0; i < layers.Length; i++ ) {
            layers[ i ].SetTrackPlayer( true );
        }
    }
}
