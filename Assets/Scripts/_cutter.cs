using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _cutter : MonoBehaviour
{
    Camera MainCamera;
    TrailRenderer _cutterTrail;
    private Vector3 _cutterPosition;
    private Transform _knife;
    private Transform _cutterObject;
    //[SerializeField] ParticleSystem _cutEffect;

    public event Action _RopeCut;
    private void Start()
    {
        MainCamera = Camera.main;
        _cutterObject = transform.GetChild(0);
        _cutterTrail=_cutterObject.GetComponent<TrailRenderer>();
        _knife = transform.GetChild(1);
    }
    private void Update()
    {
        //move the knife everytime
        _cutterPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        _knife.position = new Vector2(_cutterPosition.x, _cutterPosition.y);

        if(Input.GetMouseButtonDown(0))
        {
            _cutterObject.gameObject.SetActive(true);
        }

        if(Input.GetMouseButton(0))
        {
            //move the cutter trail if button down
            _cutterObject.position = new Vector2(_cutterPosition.x, _cutterPosition.y);
            RaycastHit2D hit = Physics2D.Raycast(MainCamera.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
            if(hit.collider!=null)
            {
                GameObject collidedLink=hit.collider.gameObject;
                if(hit.collider.CompareTag("Link"))
                {
                    if(collidedLink.transform.parent.TryGetComponent<_rope>(out _rope r))
                    {
                        r.CutTheRope(collidedLink.transform);
                        //_cutEffect.Play();
                        if (_RopeCut != null) _RopeCut();
                    }
                    Destroy(hit.collider.gameObject);
                }
            }
        
        }
        if(Input.GetMouseButtonUp(0))
        {
            _cutterObject.gameObject.SetActive(false);
        }
    }
}
