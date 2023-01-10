using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class _halfRopeSimulation : MonoBehaviour
{
    [SerializeField] List<Transform> _links = new List<Transform>();
    [SerializeField] LineRenderer _linerenderer;
    public void Init(List<Transform> links,LineRenderer _line)
    {
        _links = links;

        _linerenderer= gameObject.AddComponent<LineRenderer>();
        _linerenderer.startWidth = _line.startWidth;
        _linerenderer.startColor = _line.startColor;
        _linerenderer.material = _line.material;
        _linerenderer.sortingOrder = 1;
        _linerenderer.numCapVertices = 8;

        int _linePoints = _links.Count;
        _linerenderer.positionCount = _linePoints;
    }
    private void Update()
    {
        for(int i=0;i<_links.Count;i++)
        {
            _linerenderer.SetPosition(i, _links[i].position);
        }
    }

}
