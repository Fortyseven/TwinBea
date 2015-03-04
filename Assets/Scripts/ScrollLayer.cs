using UnityEngine;

public class ScrollLayer : MonoBehaviour
{
    public float Speed = 1.0f;
    public bool IsMirrored = false;
    public bool CanTrackPlayer = true;

    private float _width, _height;
    private float _tick;
    private GameObject _paralax_layer;
    private GameObject _player;
    private int _sort_index;

    private Vector3 _base_pos;
    private bool _track_player;

    // leftmost == extents.x
    // rightmost == -extents.x
    // center = 0

    public void Awake()
    {
        _track_player = false;
        _base_pos = transform.root.position;

        _width = this.GetComponent<Renderer>().bounds.extents.x * 2;
        _height = this.GetComponent<Renderer>().bounds.extents.y * 2;

        this.transform.position = new Vector3( _width / 2, _base_pos.y, _base_pos.z );

        _paralax_layer = new GameObject( this.name + " Paralax Copy" );

        // Just to keep it neat, parent this to my parent
        _paralax_layer.transform.parent = this.gameObject.transform;

        SpriteRenderer foo_spr = _paralax_layer.AddComponent<SpriteRenderer>();
        SpriteRenderer my_spr = GetComponent<SpriteRenderer>();
        foo_spr.sprite = my_spr.sprite;
        foo_spr.sortingOrder = my_spr.sortingOrder;

        _paralax_layer.transform.position = new Vector3( _width * 1.50f, _base_pos.y, _base_pos.z );

        _sort_index = GetComponent<SpriteRenderer>().sortingOrder;
    }

    public void Start()
    {
        _player = GameObject.Find( "Maude" );
    }

    // Update is called once per frame
    public void Update()
    {
        _tick += Time.deltaTime * Speed;
        SetOffset();
    }

    public void SetOffset()
    {
        Vector3 pos;

        if ( _track_player ) {
            pos = new Vector3( -_tick % _width + _base_pos.x,
                               _player.transform.position.y + _base_pos.y * 0.05f * _sort_index,
                               _base_pos.z );
        }
        else {
            pos = new Vector3( -_tick % _width + _base_pos.x,
                               _base_pos.y * _sort_index,
                               _base_pos.z );
        }
        transform.position = pos;
    }

    public void SetTrackPlayer( bool track )
    {
        if ( CanTrackPlayer )
            _track_player = track;
    }
}
