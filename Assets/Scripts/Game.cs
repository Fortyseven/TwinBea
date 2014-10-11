using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public GameObject CloudPrefab;
    public GameObject WalterPrefab;

    void Start()
    {
        Application.targetFrameRate = 60;
    }
    
    void Update()
    {   
        if ( Random.Range( 0, 1000 ) < 10 ) {
            SpawnCloud();
        }
        if ( Random.Range( 0, 1000 ) < 10 ) {
            SpawnWalter();
        }
    }

    void SpawnCloud()
    {
        Instantiate( CloudPrefab, new Vector3( 2.0f, Random.Range( -1.0f, 1.0f ), 0 ), Quaternion.identity );
    }

    void SpawnWalter()
    {
        Instantiate( WalterPrefab, new Vector3( 2.0f, Random.Range( -1.0f, 1.0f ), 0 ), Quaternion.identity );
    }
}
