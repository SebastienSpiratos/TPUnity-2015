// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Gets the flag of NavMesh Agent if it attempts to acquire a new path if the existing path becomes invalid. \n" +
		"NOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class GetAgentHasPath : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Store flag of the agent if it attempts to acquire a new path if the existing path becomes invalid.")]
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

			DoGetAutoRepath();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetAutoRepath();
		}

		void DoGetAutoRepath()
		{
			if (storeResult == null || _agent == null) 
			{
				return;
			}

			storeResult.Value = _agent.autoRepath;
		}

	}
}