using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSpawner : MonoBehaviour
{
	[SerializeField]
	Button normalB;
	[SerializeField]
	Button innerB;
	[SerializeField]
	Button outerB;

	BlockInfo info;

	GameObject[] variations;
	GameObject[] innerCorners;
	GameObject[] outerCorners;

	public void SetTile (TileType tile, BlockInfo info)
	{
		this.info = info;
		variations = tile.variations;
		innerCorners = tile.innerCorners;
		outerCorners = tile.outerCorners;

		if (tile.variations.Length == 0) {
			normalB.interactable = false;
		}
		if (tile.innerCorners.Length == 0) {
			innerB.interactable = false;

		}
		if (tile.outerCorners.Length == 0) {
			outerB.interactable = false;
		}
	}

	public void SetTile (GameObject[] tile, BlockInfo info)
	{
		this.info = info;
		variations = tile;
		innerB.interactable = false;
		outerB.interactable = false;
	}

	public void SpawnNormal ()
	{
		if (variations != null && variations.Length > 0) {
			var go = Instantiate (variations [info.Variation]);
			var pos = GridPlaneControl.LookIntersect ();
			pos = new Vector3 (Round.ToFour (pos.x), Round.ToThreePointSix (pos.y), Round.ToFour (pos.z));
			go.transform.position = pos;
			var iOnBlock = go.AddComponent<InfoOnBlock> ();
			var newInfo = info;
			newInfo.MyTileKind = TileKind.Normal;
			iOnBlock.Info = newInfo;
			BlockManipulator.SelectItem (go);
		}
	}

	public void SpawnInner ()
	{
		if (innerCorners != null && innerCorners.Length > 0) {
			var go = Instantiate (innerCorners [info.Variation]);
			var pos = GridPlaneControl.LookIntersect ();
			pos = new Vector3 (Round.ToFour (pos.x), Round.ToThreePointSix (pos.y), Round.ToFour (pos.z));
			go.transform.position = pos;
			var iOnBlock = go.AddComponent<InfoOnBlock> ();
			var newInfo = info;
			newInfo.MyTileKind = TileKind.InnerCorner;
			iOnBlock.Info = newInfo;

			BlockManipulator.SelectItem (go);

		}
	}

	public void SpawnOuter ()
	{
		if (outerCorners != null && outerCorners.Length > 0) {
			var go = Instantiate (outerCorners [info.Variation]);
			var pos = GridPlaneControl.LookIntersect ();
			pos = new Vector3 (Round.ToFour (pos.x), Round.ToThreePointSix (pos.y), Round.ToFour (pos.z));
			go.transform.position = pos;
			var iOnBlock = go.AddComponent<InfoOnBlock> ();
			var newInfo = info;
			newInfo.MyTileKind = TileKind.OuterCorner;
			iOnBlock.Info = newInfo;

			BlockManipulator.SelectItem (go);

		}
	}

	public static void SpawnFull (BlockInfo info, Vector3 pos, float[] Blendshapes, Vector3 rot, bool addSelect = true)
	{
		var block = ListBlockTypes.AllBlocks.types [info.BlockId];
		GameObject[] tile = null;
		switch (info.TileTypeId) {
		case 0:
			switch (info.MyTileKind) {
			case TileKind.Normal:
				tile = block.wallTiles [info.UpperVariation].variations;
				break;
			case TileKind.InnerCorner:
				tile = block.wallTiles [info.UpperVariation].innerCorners;
				break;
			case TileKind.OuterCorner:
				tile = block.wallTiles [info.UpperVariation].outerCorners;
				break;
			default:
				break;
			}
			break;
		case 1:
			switch (info.MyTileKind) {
			case TileKind.Normal:
				tile = block.floorTiles [info.UpperVariation].variations;
				break;
			case TileKind.InnerCorner:
				tile = block.floorTiles [info.UpperVariation].innerCorners;
				break;
			case TileKind.OuterCorner:
				tile = block.floorTiles [info.UpperVariation].outerCorners;
				break;
			default:
				break;
			}
			break;
		case 2:
			switch (info.MyTileKind) {
			case TileKind.Normal:
				tile = block.ceilingTiles [info.UpperVariation].variations;
				break;
			case TileKind.InnerCorner:
				tile = block.ceilingTiles [info.UpperVariation].innerCorners;
				break;
			case TileKind.OuterCorner:
				tile = block.ceilingTiles [info.UpperVariation].outerCorners;
				break;
			default:
				break;
			}
			break;
		case 3:
			tile = block.traps;
			break;
		case 4:
			tile = block.puzzles;
			break;
		case 5:
			tile = block.other;
			break;
		default:
			break;
		}

		if (tile.Length == 0)
			return;

		info.Variation += 10 * tile.Length;
		info.Variation %= tile.Length;

		var go = Instantiate (tile [info.Variation]);
		go.transform.position = pos;
		if (rot != Vector3.zero)
			go.transform.rotation = Quaternion.Euler (rot);
		var iOb = go.AddComponent<InfoOnBlock> ();
		iOb.Info = info;
		iOb.Blendshapes = Blendshapes;
		if (iOb.Blendshapes != null)
			BlendshapeEditor.ApplyAll (iOb.GetComponent<SkinnedMeshRenderer> (), iOb);

		if (addSelect)
			BlockManipulator.AddSelectItem (go);

	}

	public static void Spawn (BlockInfo info, Vector3 pos)
	{
		SpawnFull (info, pos, null, Vector3.zero);
	}
		
}
