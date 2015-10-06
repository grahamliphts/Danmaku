using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

    public Scrollbar lifeBossValue;
    public RawImage lifeBarBoss;

    //color/value of life
    private Color _lifeBarColor;
    private float _life;
    private float _lifeBarValue;

    public BossLife lifeBoss;
    void Update()
    {
        if (lifeBoss.GetLife() >= 0)
        {
            _lifeBarValue = lifeBoss.GetLife() / 100;
            lifeBossValue.size = _lifeBarValue;
            SetColorLife();
            lifeBarBoss.color = _lifeBarColor;
        }
    }
    void SetColorLife()
    {
        if (_lifeBarValue >= 0.5f)
            _lifeBarColor = Color.green;
        else if (_lifeBarValue >= 0.25f && _lifeBarValue < 0.5f)
            _lifeBarColor = Color.yellow;
        else if (_lifeBarValue < 0.25)
            _lifeBarColor = Color.red;
    }
}
