using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float Speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate( Speed * Time.deltaTime, 0, 0 );
        if ( transform.position.x > 2.0f )
        {
            Destroy( this.gameObject );
        }
    }
}
