using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : UnitySingleton<GameApp>
{

	// 游戏逻辑入口
	public void GameStart() {
		this.EnterGame();
	}

	public void EnterGame() {
		// 释放游戏地图
		GameObject mapPrefab = ResMgr.Instance.GetAssetCache<GameObject>("Maps/sgyd/sgyd.prefab");
		GameObject map = GameObject.Instantiate(mapPrefab);
		map.name = mapPrefab.name;
		// end

		// 释放游戏逻辑场景
		GameObject gamePrefab = ResMgr.Instance.GetAssetCache<GameObject>("Maps/sgyd/game.prefab");
		GameObject game = GameObject.Instantiate(gamePrefab);
		game.name = gamePrefab.name;
		game.AddComponent<SceneMgr>().init();
		// end

		// 释放游戏UI
		UIMgr.Instance.ShowUIView("GameUI");
		// end
	}
}
