using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    private GameObject followThisGameObject;
    private Vector3 offset;
    private bool followGameObject = false;

    public void Setup(GameObject gameObject, Vector3 offset)
    {
        followThisGameObject = gameObject;
        this.offset = offset;
    }

    public void SetFollowGameobject(bool state)
    {
        followGameObject = state;
    }

    private void Update()
    {
        if (!followGameObject) return;

        if (followThisGameObject != null)
        {
            try
            {
                SetPosition(followThisGameObject.transform.position + offset);
            }
            catch (Exception e)
            {
                print(e.Message);
                SetFollowGameobject(false);
            }
        }
        else
        {
            SetFollowGameobject(false);
        }
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
