// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Gets the flag a NavMesh Agent if the current path stale. \nWhen true, the path may no longer be valid or optimal. \nThis flag will be set : \n--if there are any changes to the walkableMask, \n--if any off-mesh link is enabled or disabled, \n--or if the costs for the NavMeshLayers have been changed. \nNOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class GetAgentIsPathStale : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Store flag of the agent if the current path stale.")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
		
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

			DoGetIsPathStale();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetIsPathStale();
		}

		void DoGetIsPathStale()
		{
			if (storeResult == null || _agent == null) 
			{
				return;
			}

			storeResult.Value = _agent.isPathStale;
		}

	}
}