// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMeshAgent)]
	[Tooltip("Clears the current path of a NavMesh Agent. \n" +
		"NOTE: that the agent will not start looking for a new path until SetDestination is called. \n" +
		"NOTE: The Game Object must have a NavMeshAgent component attached.")]
	public class AgentResetPath : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have a NavMeshAgent component attached.")]
		[CheckForComponent(typeof(NavMeshAgent))]
		public FsmOwnerDefault gameObject;
		
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
		}

		public override void OnEnter()
		{
			_getAgent();
			
			DoResetPath();

			Finish();		
		}
		
		void DoResetPath()
		{
			if (_agent == null) 
			{
				return;
			}
			
			_agent.ResetPath();
		}

	}
}