// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Gets the relative vertical displacement of the owning GameObject. \n" +
		"NOTE: The Game Object must have a NavMeshAgent.")]
	public class GetAgentBaseOffset : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("the relative vertical displacement of the owning GameObject")]
		[UIHint(UIHint.Variable)]
		public FsmFloat baseOffset;
		
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
			baseOffset = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoGetBaseOffset();
			
			if (!everyFrame)
				Finish();		
		}

		public override void OnUpdate()
		{
			DoGetBaseOffset();
		}

		void DoGetBaseOffset()
		{

			if (_agent == null) 
			{
				return;
			}
			
			if (baseOffset!=null){
				baseOffset.Value = _agent.baseOffset;
			}		
		}

	}
}