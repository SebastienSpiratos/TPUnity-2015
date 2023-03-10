// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Gets if agent currently positioned on an OffMeshLink. \n" +
		"NOTE: The Game Object must have a NavMeshAgent.")]
	public class GetAgentIsOnOffMeshLink : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store flag if agent currently positioned on an OffMeshLink.")]
		public FsmBool isOnOffMeshLink;		
		
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
			isOnOffMeshLink = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoGetIsOnOffMeshLink();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoGetIsOnOffMeshLink();
		}

		void DoGetIsOnOffMeshLink()
		{

			if (isOnOffMeshLink == null  || _agent == null) 
			{
				return;
			}
			
			isOnOffMeshLink.Value = _agent.isOnOffMeshLink;
						
		}

	}
}