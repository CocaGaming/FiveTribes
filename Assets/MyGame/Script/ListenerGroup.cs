using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerGroup
{
    List<Action<object>> actions = new List<Action<object>>();
    public void BroadCast(object value)//hàm bắn ra sự kiện
    {
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i](value);
        }
    }
    public void Attach(Action<object> action)//thêm vào sự kiện
    {
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i] == action)
                return;
        }
        actions.Add(action);
    }
    public void Detach(Action<object> action)//remove ra khỏi sự kiện 
    {
        for (int i = 0; i < actions.Count; i++)
        {
            if (actions[i] == action)
            {
                actions.Remove(action);
                break;
            }
        }
    }
}
