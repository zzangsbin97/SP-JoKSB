using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{

    public Slider hpSlider;
    public Teammate teammate;

    public void Initialize(Teammate teammate) {
        this.teammate = teammate;
        hpSlider.maxValue = teammate.maxHP;
        UpdateUI();

    }

    void UpdateUI() {
        hpSlider.value = this.teammate.currentHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
