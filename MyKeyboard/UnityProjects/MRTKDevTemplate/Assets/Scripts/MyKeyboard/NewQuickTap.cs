using MixedReality.Toolkit;
using MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewQuickTap : MonoBehaviour
{
    private PressableButton pressableButton;
    private bool isPressedLastFrame=false;
    public float timer=0;
    public float ConfirmTime=0.2f;
    public float CancelTime=2f;
    public GameObject frontPlane;
    public Vector3 relativeVec;
    // Start is called before the first frame update
    void Start()
    {
        pressableButton=GetComponent<PressableButton>();
    }

    // Update is called once per frame
    void Update()
    {
        IPokeInteractor pokeInteractor=pressableButton.TryGetFarthestPressDistance(out float pokeDistance);
        if(pokeInteractor!=null)relativeVec=frontPlane.transform.InverseTransformPoint(pokeInteractor.PokeTrajectory.End);
        bool isPoked = pressableButton.TryGetPressProgress(out float pokeAmount);
        if(isPressedLastFrame&&isPoked)
        {
            timer+=Time.deltaTime;
        }
        else if(isPoked&&!isPressedLastFrame)
        {
            timer=0;
        }
        else if(isPressedLastFrame&&!isPoked)
        {
            if(timer>=ConfirmTime&&timer<CancelTime&&relativeVec.z<=0)
            {
                pressableButton.NewQuickTap_Click();
            }
            timer=0;
        }
        isPressedLastFrame=isPoked;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        
    }
}
