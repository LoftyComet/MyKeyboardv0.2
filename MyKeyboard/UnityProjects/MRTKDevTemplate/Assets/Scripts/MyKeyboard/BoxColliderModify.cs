using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderModify : MonoBehaviour
{
    private BoxCollider boxCollider;
    private bool isPressedLastFrame=false;
    public Vector3 EnlargedColliderSiz=new Vector3(0.042f,0.042f,0.01f);
    // Start is called before the first frame update
    void Start()
    {
        boxCollider=GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        bool isPoked = gameObject.GetComponent<PressableButton>().TryGetPressProgress(out float pokeAmount);
        if(isPressedLastFrame&&!isPoked)
        {
            BoxColliderShrink();
        }
        else if(isPoked&&!isPressedLastFrame)
        {
            BoxColliderExpand();
        }
        isPressedLastFrame=isPoked;
    }

    private void BoxColliderExpand()
    {
        boxCollider.size=EnlargedColliderSiz;
    }

    private void BoxColliderShrink()
    {
        boxCollider.size=new Vector3(0.032f,0.032f,0.01f);
    }
}
