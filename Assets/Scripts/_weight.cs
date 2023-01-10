using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class _weight : MonoBehaviour
{
    //private HingeJoint2D _weightHingeJoint2D;
    [SerializeField] float _chainDistance;
    private void Start()
    {
        //_weightHingeJoint2D=GetComponent<HingeJoint2D>();     //not getting the hinge joint but adding new one each time
    }
    public void ConnectRopeEnd(Rigidbody2D endRB)
    {
        HingeJoint2D joint= gameObject.AddComponent<HingeJoint2D>();
        joint.connectedBody = endRB;
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2(0f, -_chainDistance);
        
    }

}
