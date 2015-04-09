using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class IGameView : MonoBehaviour
{
    private Presenter presenter;

    private Dictionary<Type, MethodInfo> recvMethods = new Dictionary<Type, MethodInfo>();

    protected class RecvMsgMethod : Attribute
    {
    }

    public virtual void Initialize(Presenter presenter)
    {
        this.presenter = presenter;

        foreach (MethodInfo method in GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => m.GetCustomAttributes(true).OfType<RecvMsgMethod>().Any()))
        {
            if (method.GetParameters().Length != 1)
            {
                Debug.LogError("RecvMethod " + method + " in class " + GetType() + " must have one parameter");
            }
            else
            {
                Type msgType = method.GetParameters()[0].ParameterType;
                if (!msgType.IsSubclassOf(typeof(BaseMsg)))
                {
                    Debug.LogError("RecvMethod " + method + " in class " + GetType() + ": msgType " + msgType + " must be subclass of BaseMsg");
                }
                else
                {
                    recvMethods[msgType] = method;
                }
            }
        }
    }

    public virtual void UpdateView()
    {
        
    }

    public void PublishMsg(BaseMsg msg)
    {
        if (presenter != null)
        {
            presenter.PublishMsg(msg);
        }
    }

    public void ReceiveMsg(BaseMsg msg)
    {
        MethodInfo method;
        if (recvMethods.TryGetValue(msg.GetType(), out method))
        {
            method.Invoke(this, new object[] { msg });
        }
    }
}
