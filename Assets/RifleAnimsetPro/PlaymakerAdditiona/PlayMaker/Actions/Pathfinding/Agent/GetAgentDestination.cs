// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Gets the destination of a NavMesh Agent. \nNOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class GetAgentDestination : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Store the destination of the navMesh Agent.")]
		[UIHint(UIHint.Variable)]
		public FsmVector3 storeResult;
		
		public bool everyFrame;
		
		private NavMeshAgent _agent;

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
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_getAgent();

			DoGetDestination();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetDestination();
		}

		void DoGetDestination()
		{
			if (storeResult == null || _agent == null) 
			{
				return;
			}

			storeResult.Value = _agent.destination;
		}

	}
}