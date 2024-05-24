using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour {

	// 放角色，放NPC
	public void init() {

		GameObject playerPrefab = ResMgr.Instance.GetAssetCache<GameObject>("Charactors/Monsters/Jinglingnan_6.prefab");
		GameObject player = GameObject.Instantiate(playerPrefab);
		player.name = playerPrefab.name;
		player.transform.SetParent(this.transform, false);
		player.transform.position = this.transform.Find("entryA").position;
		player.AddComponent<PlayerCtrl>().init();
		
	}


}
