using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class _rope : MonoBehaviour
{
    [SerializeField] Rigidbody2D _linkPrefab;
    [SerializeField] Rigidbody2D _hook;
    [SerializeField] float _connectorRate;

    [SerializeField] int _links;
    private int _lineRendererCount;
    [Header("line renderer")]
     LineRenderer _ropeLineRenderer;
    [SerializeField] int _lineRenderRate;

    [SerializeField] _weight _weightRB;

    private _cutter cutter;
    
    private void Start()
    {
        _ropeLineRenderer = GetComponent<LineRenderer>();
        cutter=GameObject.FindObjectOfType<_cutter>();
        CreateRope();
    }
    private void CreateRope()
    {
        float _mainDistance = (transform.position - _weightRB.transform.position).magnitude;
        _links = (int)(Mathf.Ceil(_mainDistance / _connectorRate));

        Rigidbody2D previousRB = _hook;
        for (int i = 0; i < _links;i++)
        {
            Rigidbody2D newLink= Instantiate(_linkPrefab, transform);
            newLink.name = "Link " + i.ToString();
            HingeJoint2D joint = newLink.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;

            previousRB = newLink;
        }
        _ropeLineRenderer.positionCount = (_links) + (_links*_lineRenderRate);
        _lineRendererCount = transform.childCount - 1;

        if (_weightRB != null)
            _weightRB.ConnectRopeEnd(transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>());
        else
            Debug.LogError("weight is not asssigned");
    }
    private void FixedUpdate()
    {
        int index = 0;
        for(int i=0;i<_lineRendererCount;i++,index++)
        {
            _ropeLineRenderer.SetPosition(index, transform.GetChild(i).position);

            Transform currentPosition = transform.GetChild(i);
            Transform targetPosition = transform.GetChild(i + 1);
            Vector3 dir = (targetPosition.position - currentPosition.position).normalized;
            float distance = Vector3.Distance(transform.GetChild(i).position, transform.GetChild(i + 1).position);
            float rate = distance / _lineRenderRate;
            for(int j=1;j<=_lineRenderRate;j++)
            {
                _ropeLineRenderer.SetPosition(++index, currentPosition.position + dir * rate*j);
            }
        }
        
    }
    private void Update()
    {
        
    }
    public void CutTheRope(Transform cutPoint)
    {
        int cutPointIndex = cutPoint.transform.GetSiblingIndex();
        Debug.Log(cutPoint);
        _lineRendererCount = cutPointIndex-1;
        _ropeLineRenderer.positionCount = (_lineRendererCount) + (_lineRendererCount * _lineRenderRate);

        //check if the rope is already cut.
        

        // creating another line renderer
        List<Transform> _remainingLinks = new List<Transform>();
        for (int i=cutPointIndex+1;i<transform.childCount;i++)
        {
            if (transform.GetChild(i).TryGetComponent<_halfRopeSimulation>(out _halfRopeSimulation h))
                break;
            _remainingLinks.Add(transform.GetChild(i));
            transform.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;         //can't cut the remaining rope
        
        }
        Debug.Log(_remainingLinks.Count);
        if(_remainingLinks.Count>0 )
        {
        transform.GetChild(cutPointIndex + 1).AddComponent<_halfRopeSimulation>().Init(_remainingLinks,_ropeLineRenderer);
        }
        else
        {
            Debug.Log("Last link is cut");
        }    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, _weightRB.transform.position);
    }
}
