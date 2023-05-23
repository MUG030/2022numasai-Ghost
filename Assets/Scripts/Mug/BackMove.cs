using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float leftLimit;

    private bool isPaused = false;

    void Update()
    {
        // Rキーを押した場合にシーンをリロードする
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        if (!isPaused)
        {
            transform.position -= new Vector3(Time.deltaTime * moveSpeed, 0);
            if (transform.position.x <= leftLimit)
            {
                moveSpeed = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;

            if (!isPaused)
            {
                // 再開時に移動速度を初期化する
                moveSpeed = 1f;
            }
        }
    }

    private void ReloadScene()
    {
        // 現在のシーンのインデックスを取得し、再読み込みする
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
