using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameUI_UICtrl : UI_ctrl {

	public override void Awake() {
		base.Awake();

		this.add_button_listener("attack_opt/attack_skill1", this.onSkillClick);
	}

	private void onSkillClick() {
		EventMgr.Instance.Emit("SkillEvent", 1);
	}

	void Start() {
	}

}
