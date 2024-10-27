using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Jobs;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
  
    public List<Target> targets = new List<Target>();
    public Target CurrentTarget{ get; private set; }

    private Camera MainCamera;

    private void Start()
    {
        MainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
       Target target = other.GetComponent<Target>();
        if (target == null)
        {
            return;
        }
        targets.Add(target);
        target.OnDestryed += RemoveTarget;
    }
    private void OnTriggerExit(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target == null)
        {
            return;
        }
        targets.Remove(target);
        RemoveTarget(target);  
    }
    public bool SelectTarget()
    {
        if (targets.Count == 0)
        {
            return false;
        }
        Target ClosestTarget = null;
        float ClosestTargetDistance = Mathf.Infinity;

        foreach(Target target in targets)
        {
            Vector2 viewPos = MainCamera.WorldToViewportPoint(target.transform.position);
            if ( viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1) 
            {
                continue;
            }
            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if (toCenter.sqrMagnitude > ClosestTargetDistance )
            {
                ClosestTarget = target;
                ClosestTargetDistance = toCenter.sqrMagnitude;
            }
        }
        CurrentTarget = targets[0];

        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }
    public void Cancel()
    {   
        if (CurrentTarget == null) { return; }

        cineTargetGroup.RemoveMember(CurrentTarget.transform);

        CurrentTarget = null;
        
    }
    public void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform); 
            CurrentTarget = null;
        }
        target.OnDestryed -= RemoveTarget;
        targets.Remove(target);
    }
}
