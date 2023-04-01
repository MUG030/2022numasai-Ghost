using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera virtualCamera;

	private int defaultPriority;

	void Start()
	{
		defaultPriority = virtualCamera.Priority;
	}

	//カメラスイッチコライダーに入った時vcamDownに切り替え
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			virtualCamera.Priority = 100;
		}
	}

	//カメラスイッチコライダーから出た時vcamUpに切り替え
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			virtualCamera.Priority = defaultPriority;
		}
	}
}
