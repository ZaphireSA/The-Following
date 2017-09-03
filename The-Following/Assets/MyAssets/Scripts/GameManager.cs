using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int SoulsCollected = 0;
    public float TimeLeft = 60f;
    [SerializeField] Text txtLife;

    private void Update()
    {
        TimeLeft -= Time.deltaTime;
        txtLife.text = TimeLeft.ToString("0");
    }
}
