using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

	private CharacterController ctrl = null;
	private Animation anim = null;
	private int state = (int)State.Invalid;
	private GameObject skillEffect = null;
	private float speed = 8.0f;

	private Camera gameCamera = null;
	private Vector3 cameraOffset = Vector3.zero; 

	public void setState(int state) {
		if (this.state == state) {
			return;
		}

		this.state = state;
		

		switch (state) {
			case (int)State.Idle:
				this.anim.CrossFade("free");
				break;

			case (int)State.Walk:
				this.anim.CrossFade("walk");
				break;
			case (int)State.Skill:
				this.anim.CrossFade("skill");
				this.skillEffect.SetActive(true);
				break;
		}
	}

	public void init() {
		this.gameCamera = Camera.main;
		this.cameraOffset = this.gameCamera.transform.position - this.transform.position;
		
		this.anim = this.gameObject.GetComponent<Animation>();
		this.ctrl = this.gameObject.GetComponent<CharacterController>();
		EventMgr.Instance.AddListener("JoyStick", this.onJoyStickEventUpdate);
		EventMgr.Instance.AddListener("SkillEvent", this.onSkillEvent);
		this.setState((int)State.Idle);

		this.skillEffect = GameObject.Instantiate(ResMgr.Instance.GetAssetCache<GameObject>("Effects/Prefabs/swords.prefab"));
		this.skillEffect.transform.SetParent(this.transform, false);
		this.skillEffect.transform.localPosition = Vector3.zero;
		this.skillEffect.SetActive(false);
	}

	private void onEndSkill() {
		this.skillEffect.SetActive(false);
		this.setState((int)State.Idle);
	}

	private void onSkillEvent(string uname, object udata) {
		if (this.state != (int)State.Idle && this.state != (int)State.Walk) {
			return;
		}

		this.setState((int)State.Skill);
		this.Invoke("onEndSkill", 1.5f);
	}

	private void onJoyStickEventUpdate(string uname, object udata) {
		Vector2 dir = (Vector2)udata;

		if (this.state != (int)State.Idle && this.state != (int)State.Walk) {
			return;
		}

		if (dir.x == 0 && dir.y == 0) {
			this.setState((int)State.Idle);
			return;
		}

		this.setState((int)State.Walk);
		float dt = Time.deltaTime;
		float r = Mathf.Atan2(dir.y, dir.x);
		float s = this.speed * dt;

		float offset = (-Mathf.PI * 0.25f);
		float sx = s * Mathf.Cos(r + offset);
		float sz = s * Mathf.Sin(r + offset);
		this.ctrl.Move(new Vector3(sx, 0, sz));

		float degree = r * 180 / Mathf.PI;
		offset = 45;
		degree = 360 - degree + 90 + offset;
		this.transform.localEulerAngles = new Vector3(0, degree, 0);
	}

	public void LateUpdate() {
		if (this.gameCamera == null) {
			return;
		}

		this.gameCamera.transform.position = this.transform.position + this.cameraOffset;
	}
}
