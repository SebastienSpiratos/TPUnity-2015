// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Get flag to automate movement onto and off of OffMeshLinks. \nNOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class GetAgentAutoTraverseOffMeshLink : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Store the flag for automatic movement onto and off of OffMeshLinks")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
		
		
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
		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoGetAutoTraverseOffMeshLink();

			Finish();		
		}

		void DoGetAutoTraverseOffMeshLink()
		{
			if (storeResult == null || _agent==null)
			{
				return;
			}
			
			storeResult.Value = _agent.autoTraverseOffMeshLink;			
		}

	}
}