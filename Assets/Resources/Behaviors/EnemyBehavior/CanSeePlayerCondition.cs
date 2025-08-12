using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "canSeePlayer", story: "Using [DetectionScript] to detect [Player]", category: "Conditions", id: "fe65fae6c57629e6facb5815f364062b")]
public partial class CanSeePlayerCondition : Condition
{
    [SerializeReference] public BlackboardVariable<PlayerDetection> DetectionScript;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    public override bool IsTrue()
    {
        return true;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
