using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxTail
{
    [Serializable]
    public class ComponentData
    {

    }

    public class ComponentData<TYPE_ONE> : ComponentData where TYPE_ONE : AttackData
}
