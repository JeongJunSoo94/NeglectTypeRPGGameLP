using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JJS.BT
{
    public interface IBlackboard
    {
        virtual void Create() { }
    }

    [System.Serializable]
    public abstract class Blackboard : MonoBehaviour, IBlackboard
    {

    }
}
