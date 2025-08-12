using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Detecting Player", story: "Using [DetectionScript] to detect [target] in range", category: "Action", id: "e0f928e3ff5c32766c6688f3ff044084")]
public partial class DetectingPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<PlayerDetection> DetectionScript;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    
    protected override Status OnUpdate()
    {
        var detectedTarget = DetectionScript.Value.DetectTarget();
        Target.Value = detectedTarget;
        return Target != null ? Status.Success : Status.Failure;
    }

}

