using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Layer<T> : State<T> where T : class
{
    protected Dictionary<string, State<T>> states = new Dictionary<string, State<T>>();
    protected State<T> defaultState, currentState;
    public Layer(T m_origin, Layer<T> parent) : base(m_origin, parent)
    {

    }
    
    protected void AddState(string name, State<T> state)
    {
        states.Add(name, state);
    }
    public override void OnStateEnter()
    {
        currentState = defaultState;
        currentState.OnStateEnter();
        base.OnStateEnter();
    }
    public override void OnUpdate()
    {
        currentState.OnUpdate();
        base.OnUpdate();
    }
    public override void OnFixedUpdate()
    {
        currentState.OnFixedUpdate();
        base.OnFixedUpdate();
    }
    public override void OnStateExit()
    {
        currentState.OnStateExit();
        base.OnStateExit();
    }
    public void SwitchState(string stateName)
    {
        if (!states.ContainsKey(stateName))
        {
            Debug.LogError(stateName + " State does not exist");
            return;
        }
        else
        {
            currentState.OnStateExit();
            currentState = states[stateName];
            states[stateName].OnStateEnter();
        }
    }

    //usage: return "--" + base.ViewState();
    public override string ViewState()
    {
        return "->" + currentState.ViewState();
    }
}
