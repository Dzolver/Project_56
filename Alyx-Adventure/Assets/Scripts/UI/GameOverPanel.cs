using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOverPanel : MonoBehaviour {

    public TextMeshProUGUI scoreText;
	// Use this for initialization
	void Start () {
        LeanTween.value(0f, GameData.Instance.CurrentScore,2f).setOnUpdate(OnUpdate);
	}

    private void OnUpdate(float value)
    {
        scoreText.text = (int)value + "";
    }

    // Update is called once per frame
    void Update () {
		
	}
}
