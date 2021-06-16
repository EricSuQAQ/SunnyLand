using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCauculator : MonoBehaviour
{
    // Start is called before the first frame update

    public Image HPslot;
    public Image HPcurrent;
    private int hpCount;
    private int hpMax;
    public float HPRate = 1f;
    public Text HPtext;

    public Image CherrySlot;
    public Image CherryCurrent;
    public int cherryCount;
    private int cherryMax;
    public float cherryRate = 0f;
    public Text cherryText;

    void Awake()
    {
        cherryCount = 0;
        cherryMax = 10;
    }

    void Start()
    {
        hpMax = GameObject.Find("player/role").GetComponent<Player>().getHp();
    }

    // Update is called once per frame
    void Update()
    {
        hpCount = GameObject.Find("player/role").GetComponent<Player>().getHp();
        HPRate = (float)(hpCount)/(float)(hpMax);
        HPcurrent.fillAmount = HPRate;
        HPtext.text = hpCount + "  /  " + hpMax;
    }

    public void cherryAccumulate()
    {
        cherryCount += 1;
        cherryRate = (float)(cherryCount)/(float)(cherryMax);
        CherryCurrent.fillAmount = cherryRate;
        cherryText.text = cherryCount + "  /  " + cherryMax;
    }
}
