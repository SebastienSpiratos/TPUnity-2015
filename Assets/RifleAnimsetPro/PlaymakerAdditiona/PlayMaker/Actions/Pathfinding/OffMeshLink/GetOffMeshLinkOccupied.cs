// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.NavMesh)]
	[Tooltip("Gets the occupied state of an OffMeshLink. \n" +
		"NOTE: The Game Object must have an OffMeshLink component attached.")]
	public class GetOffMeshLinkOccupied : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The Game Object to work with. NOTE: The Game Object must have an OffMeshLink component attached.")]
		[CheckForComponent(typeof(OffMeshLink))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("Store flag of the offLinkMesh occupied state")]
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
		
		public bool everyFrame;
		
		private OffMeshLink _offMeshLink;

		private void _getOffMeshLink()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) 
			{
				return;
			}
			
			_offMeshLink =  go.GetComponent<OffMeshLink>();
		}
		
		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			_getOffMeshLink();

			DoGetOccupied();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetOccupied();
		}

		void DoGetOccupied()
		{
			if (storeResult == null || _offMeshLink == null) 
			{
				return;
			}

			storeResult.Value = _offMeshLink.occupied;
		}

	}
}