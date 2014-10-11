using UnityEngine;
using System.Collections;

public class ScrollLayer : MonoBehaviour
{
    public float Speed = 1.0f;
    public bool IsMirrored = false;
    private float _width, _height;
    private float _tick;
    private GameObject _paralaxLayer;
    private GameObject _player;
    private int _sortIndex;

    // leftmost == extents.x
    // rightmost == -extents.x
    // center = 0

    void Awake()
    {
        _width = this.renderer.bounds.extents.x * 2;
        _height = this.renderer.bounds.extents.y * 2;

        this.transform.position = new Vector3( _width / 2, 0, 0 );

        _paralaxLayer = new GameObject( this.name + " Paralax Copy" );

        // Just to keep it neat, parent this to my parent
        _paralaxLayer.transform.parent = this.gameObject.transform;

        SpriteRenderer foo_spr = _paralaxLayer.AddComponent<SpriteRenderer>();
        SpriteRenderer my_spr = GetComponent<SpriteRenderer>();
        foo_spr.sprite = my_spr.sprite;
        foo_spr.sortingOrder = my_spr.sortingOrder;

        _paralaxLayer.transform.position = new Vector3( _width * 1.50f, 0, 0 );

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
        Vector3 pos = new Vector3(-_tick % _width, 0 + _player.transform.position.y * 0.05f * _sortIndex,0);
        transform.position = pos;
    }
}
