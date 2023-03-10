// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Set the destination of a NavMesh Agent to a gameObject. \nNOTE: The Game Object must have a NavMeshAgentcomponent attached.")]
	public class SetAgentDestinationAsGameObject : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Set the destination to navigate towards.")]
		public FsmGameObject destination;
		
		[RequiredField]
		[Tooltip("Define the gameObject minimum movement distance to retrigger path computation.")]
		public FsmFloat triggerThreshold;
		
		private NavMeshAgent _agent;
		
		private Vector3 lastPosition;
		
		private float lastDistance;
		
		private float squareDistance;
		
		private void _getAgent()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_agent =  go.GetComponent<NavMeshAgent>();
		}	
		
		public override void Reset()
		{
			gameObject = null;
			destination = null;
			triggerThreshold = 1;
			
		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoSetDestination();

		}

		public override void OnUpdate()
		{
			DoSetDestination();
		}

		void DoSetDestination()
		{
			if (destination == null || _agent == null) 
			{
				return;
			}
			if (lastDistance!= triggerThreshold.Value){
				lastDistance = triggerThreshold.Value;
				squareDistance = lastDistance*lastDistance;
			}
		
			Vector3 newPosition = destination.Value.transform.position;
			Vector3 deltaPosition = newPosition-lastPosition;
			if (deltaPosition.sqrMagnitude>squareDistance)
			{
				_agent.SetDestination(newPosition);
				lastPosition = destination.Value.transform.position;
			}

		}
	

	}
}