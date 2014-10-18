using UnityEngine;
using System.Collections;

public class ScrollLayer : MonoBehaviour
{
    public float Speed = 1.0f;
    public bool IsMirrored = false;
    public bool CanTrackPlayer = true;

    private float _width, _height;
    private float _tick;
    private GameObject _paralaxLayer;
    private GameObject _player;
    private int _sortIndex;

    private Vector3 _basePos;
    private bool _trackPlayer;

    // leftmost == extents.x
    // rightmost == -extents.x
    // center = 0

    void Awake()
    {
        _trackPlayer = false;
        _basePos = transform.root.position;

        _width = this.renderer.bounds.extents.x * 2;
        _height = this.renderer.bounds.extents.y * 2;

        this.transform.position = new Vector3( _width / 2, _basePos.y, _basePos.z );

        _paralaxLayer = new GameObject( this.name + " Paralax Copy" );

        // Just to keep it neat, parent this to my parent
        _paralaxLayer.transform.parent = this.gameObject.transform;

        SpriteRenderer foo_spr = _paralaxLayer.AddComponent<SpriteRenderer>();
        SpriteRenderer my_spr = GetComponent<SpriteRenderer>();
        foo_spr.sprite = my_spr.sprite;
        foo_spr.sortingOrder = my_spr.sortingOrder;

        _paralaxLayer.transform.position = new Vector3( _width * 1.50f, _basePos.y, _basePos.z );

        _sortIndex = GetComponent<SpriteRenderer>().sortingOrder;
    }

    void Start()
    {
        _player = GameObject.Find("Maude");
    }

    // Update is called once per frame
    void Update()
    {
        _tick += Time.deltaTime * Speed;
        SetOffset();
    }

    void SetOffset()
    {
        Vector3 pos;

        if (_trackPlayer) {
            pos = new Vector3(-_tick % _width + _basePos.x, 
                               _player.transform.position.y + _basePos.y * 0.05f * _sortIndex,
                               _basePos.z);
        } else {
            pos = new Vector3(-_tick % _width + _basePos.x, 
                               _basePos.y * _sortIndex,
                               _basePos.z);
        }
        transform.position = pos;
    }

    public void SetTrackPlayer(bool track)
    {
        if (CanTrackPlayer)
            _trackPlayer = track;
    }
}
