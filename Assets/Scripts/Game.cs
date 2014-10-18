using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject CloudPrefab;
    public GameObject WalterPrefab;
    public static Game instance = null;
    public Text GUI_Score;
    private int _score, _lives;

    void Awake()
    {
        Game.instance = this;
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        Reset();
    }

    void Reset()
    {
        _score = 0;
        _lives = 3;
    }

    void Update()
    {   
        if ( Random.Range( 0, 1000 ) < 10 )
        {
            SpawnCloud();
        }
        if ( Random.Range( 0, 1000 ) < 10 )
        {
            SpawnWalter();
        }

        GUI_Score.text = _score.ToString();
    }

    void SpawnCloud()
    {
        Instantiate( CloudPrefab, new Vector3( 2.0f, Random.Range( -1.0f, 1.0f ), 0 ), Quaternion.identity );
    }

    void SpawnWalter()
    {
        Instantiate( WalterPrefab, new Vector3( 2.0f, Random.Range( -1.0f, 1.0f ), 0 ), Quaternion.identity );
    }

    public void AdjustScore( int amount )
    {
        _score += amount;
    }

    public void AdjustLives( int amount )
    {
        _lives += amount;
        switch( Mathf.Abs( amount ) )
        {
            case -1:
                OnLifeLost();
                break;
            case 1:
                OnLifeGained();
                break;
            default:
                break;
        }
    }

    private void OnLifeLost()
    {
        Debug.Log( "life lost" );
    }

    private void OnLifeGained()
    {
        Debug.Log( "1-up" );
    }

    public void SpawnFloater( GameObject obj, Sprite spr )
    {
        object[] foo = new object[2]{obj, spr};
        StartCoroutine( "StartFloater", foo );
    }

    public IEnumerator StartFloater( object[] param )
    {
        GameObject targ = ( GameObject )param[ 0 ];

        GameObject flot = new GameObject( "floater" );
        flot.transform.position = targ.transform.position;

        SpriteRenderer rend = flot.AddComponent<SpriteRenderer>();
        rend.sprite = ( Sprite )param[ 1 ];
        rend.sortingOrder = 9999;

        yield return new WaitForSeconds( 1.0f );
        Destroy( flot );
    }

}
