using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float Speed = 2.0f;
    public GameObject BulletPrefab;
    public GameObject BulletSpawnPoint;
    public GameObject Jaw;

    private const float FIRE_TIMEOUT = 0.200f;

    private Animation _JawAnim;
    private float _fireTimeout;

    void Awake()
    {
        _JawAnim = Jaw.GetComponentInChildren<Animation>();
        _fireTimeout = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp( pos.x + Input.GetAxis( "Horizontal" ) * Time.deltaTime * Speed, -1.7f, -0.3f );
        pos.y = Mathf.Clamp( pos.y + Input.GetAxis( "Vertical" ) * Time.deltaTime * Speed, -1.1f, 1.1f );
        transform.position = pos;

        if ( Input.GetButton( "Fire1" ) && Time.time > _fireTimeout )
        {
            Instantiate( BulletPrefab, BulletSpawnPoint.transform.position, Quaternion.identity );
            _JawAnim.Play();
            _fireTimeout = Time.time + FIRE_TIMEOUT;
        }
    }

    public void OnTriggerEnter2D( Collider2D other )
    {
        if (other.tag == "bell") {
            // switch color bell type
            PowerUp(other);
        }
    }

    public void PowerUp(Collider2D other)
    {
        audio.Play();
        Destroy(other.gameObject);
    }
}
