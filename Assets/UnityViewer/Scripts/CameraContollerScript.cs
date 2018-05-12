using UnityEngine;
using System.Collections;

public class CameraContollerScript : MonoBehaviour
{
	private Transform target;
	public float spinSpeed = 1f;
	public float distance = 10f;
	public float rotatespeed = 2.4f;

	Vector3 nowPos;
	Vector3 pos = Vector3.zero;
	public Vector2 mouse;
	private bool zoomin = false;
	private bool zoomout = false;

	public float zoomin_lim = 3f;
	public float zoomout_lim = 12f;
	public float zoomspeed = 0.1f;
	private int count = 0;

	void Start()
	{
		target = transform.parent.gameObject.transform;
		//transform.position = new Vector3 (0,0,distance);

		// 初期位置の取得
		nowPos = transform.position;

		ControlView ();
	}

	void FixedUpdate()
	{
		if (Input.GetMouseButton (0)) {
			//タッチ直後に移動量めっちゃ大きくなるから何回かスルー
			if (count > 1) {
				mouse += new Vector2 (Input.GetAxis ("Mouse X") * rotatespeed, Input.GetAxis ("Mouse Y") * rotatespeed) * Time.deltaTime * spinSpeed;
			}
			count++;
		} else {
			count = 0;
		}

		ControlView ();
	}

	void ControlView(){
		if (zoomin)
			distance -= distance*zoomspeed;

		if (zoomout)
			distance += distance*zoomspeed;

		if (distance < zoomin_lim)
			distance = zoomin_lim;

		if (distance > zoomout_lim)
			distance = zoomout_lim;

		mouse.y = Mathf.Clamp(mouse.y, -0.49f + 0.5f, 0.49f + 0.5f);

		// 球面座標系変換
		pos.x = distance * Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Cos(mouse.x * Mathf.PI);
		pos.y = -distance * Mathf.Cos(mouse.y * Mathf.PI);
		pos.z = -distance * Mathf.Sin(mouse.y * Mathf.PI) * Mathf.Sin(mouse.x * Mathf.PI);

		pos *= nowPos.z;

		pos.y += nowPos.y;

		　　　　 // 座標の更新
		transform.position = pos + target.position;
		transform.LookAt(target.position);
	}

	public void ZoomInDown(){
		zoomin = true;
	}

	public void ZoomInUp(){
		zoomin = false;
	}

	public void ZoomOutDown(){
		zoomout = true;
	}

	public void ZoomOutUp(){
		zoomout = false;
	}

}