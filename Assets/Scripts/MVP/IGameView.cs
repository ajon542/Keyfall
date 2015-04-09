using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

/// <summary>
/// Abstract base class for a game view.
/// </summary>
public abstract class IGameView : MonoBehaviour
{
    /// <summary>
    /// Reference to the presenter object.
    /// </summary>
    private Presenter presenter;

    /// <summary>
    /// Each view can implement a method to handle incoming messages. By decorating
    /// that method with the RecvMsgMethod attribute, the method can now receive all
    /// messages of the given argument type.
    /// </summary>
    private Dictionary<Type, MethodInfo> recvMethods = new Dictionary<Type, MethodInfo>();

    /// <summary>
    /// The attribute for methods to receive a message of type BaseMsg.
    /// </summary>
    protected class RecvMsgMethod : Attribute
    {
    }

    /// <summary>
    /// Initialize the game view.
    /// </summary>
    /// <param name="presenter">The game presenter.</param>
    public virtual void Initialize(Presenter presenter)
    {
        this.presenter = presenter;

        // Search for any methods in the game view marked with the RecvMsgMethod attribute.
        foreach (MethodInfo method in GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(m => m.GetCustomAttributes(true).OfType<RecvMsgMethod>().Any()))
        {
            // Make sure the method has a single parameter.
            if (method.GetParameters().Length != 1)
            {
                Debug.LogError("RecvMethod " + method + " in class " + GetType() + " must have one parameter");
            }
            else
            {
                // Make sure the parameter is a subclass of BaseMsg.
                Type msgType = method.GetParameters()[0].ParameterType;
                if (!msgType.IsSubclassOf(typeof(BaseMsg)))
                {
                    Debug.LogError("RecvMethod " + method + " in class " + GetType() + ": msgType " + msgType + " must be subclass of BaseMsg");
                }
                else
                {
                    // Update the dictionary with the message handler.
                    recvMethods[msgType] = method;
                }
            }
        }
    }

    /// <summary>
    /// Update the view.
    /// </summary>
    public virtual void UpdateView()
    {
    }

    /// <summary>
    /// Publish a message to all other game views.
    /// </summary>
    /// <param name="msg">The message to publish.</param>
    protected void PublishMsg(BaseMsg msg)
    {
        if (presenter != null)
        {
            presenter.PublishMsg(msg);
        }
    }

    /// <summary>
    /// Receive a message.
    /// </summary>
    /// <param name="msg">The message to receive.</param>
    public void ReceiveMsg(BaseMsg msg)
    {
        // Invoke the appropriate handlers for the message type.
        MethodInfo method;
        if (recvMethods.TryGetValue(msg.GetType(), out method))
        {
            method.Invoke(this, new object[] { msg });
        }
    }
}
