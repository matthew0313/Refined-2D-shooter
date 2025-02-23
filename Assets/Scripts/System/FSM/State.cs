using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class State<T> where T : class
{
    protected T origin;
    protected Layer<T> parentLayer;
    public State(T m_origin, Layer<T> parent)
    {
        origin = m_origin;
        parentLayer = parent;
    }

    public virtual void OnStateEnter()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnFixedUpdate()
    {

    }
    public virtual void OnStateExit()
    {

    }

    //usage: return "--";
    public virtual string ViewState()
    {
        return "state";
    }
}
