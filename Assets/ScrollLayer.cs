using UnityEngine;
using System.Collections;

public class ScrollLayer : MonoBehaviour
{
    public float Speed = 1.0f;
    private float _width, _height;
    private float _offset;
    private GameObject _paralaxLayer;

    void Awake()
    {
        _width = this.renderer.bounds.extents.x * 2;
        _height = this.renderer.bounds.extents.y * 2;

        this.transform.position = new Vector3( _width / 2, 0, 0 );

        _paralaxLayer = new GameObject( this.name + " Paralax Copy" );

        // Just to keep it neat, parent this to my parent
        _paralaxLayer.transform.parent = this.transform.parent;

        SpriteRenderer foo_spr = _paralaxLayer.AddComponent<SpriteRenderer>();
        SpriteRenderer my_spr = GetComponent<SpriteRenderer>();
        foo_spr.sprite = my_spr.sprite;

        _paralaxLayer.transform.position = new Vector3( transform.position.x + _width, 0, 0 );
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

}
