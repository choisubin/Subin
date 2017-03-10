using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Information_Manager : MonoBehaviour {
    public int Info_day=1;            //n일째
    public int Info_level=1;          //n레벨
    public float Info_exp=0;          //경험치
    public float Info_satisfaction=50; //만족도
    public int Info_money=0;          //돈
    public int Info_time=600;           //시간

    public Text T_day;               //n일째
    public Text T_level;            //n레벨
    public Text T_exp;              //경험치
    public Text T_satisfaction;     //만족도
    public Text T_money;            //돈
    public Text T_time;             //시간

    public Image I_exp;             //경험치
    public Image I_satisfaction;    //만족도
    

    public bool IngameCheck=false;    //인게임인지 세팅창인지 구별
    public float cur_time;

    void Start () {
        beginning_set();//초기 세팅
        //Info_setting();//초기화
    }
	
	// Update is called once per frame
	void Update () {
        UpdateInfo_Text(); //정보 업데이트
        UpdateInfo_ImageBar();  //상단바들 업데이트
        
        if (IngameCheck==true)
        {
            cur_time += Time.deltaTime;
            if (cur_time >= 0.2f)
            {
                Info_time++;
                cur_time = 0;
            }

            if(Info_time>=1230)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            save_Info();
        }
    }

    void Info_setting()
    {
        PlayerPrefs.SetInt("Info day", 1);
        PlayerPrefs.SetInt("Info level", 1);
        PlayerPrefs.SetInt("Info time", 600);
        PlayerPrefs.SetFloat("Info exp", 0);
        PlayerPrefs.SetFloat("Info satisfaction", 50);
        PlayerPrefs.SetInt("Info money", 0);
    }

    void beginning_set()
    {
        Info_day = PlayerPrefs.GetInt("Info day");
        Info_level = PlayerPrefs.GetInt("Info level");
        Info_time = PlayerPrefs.GetInt("Info time");
        Info_exp = PlayerPrefs.GetFloat("Info exp");
        Info_satisfaction = PlayerPrefs.GetFloat("Info satisfaction");
        Info_money = PlayerPrefs.GetInt("Info money");

        PlayerPrefs.Save();
    }

    void save_Info()
    {
        PlayerPrefs.SetInt("Info day", Info_day);
        PlayerPrefs.SetInt("Info level", Info_level);
        PlayerPrefs.SetInt("Info time", Info_time);
        PlayerPrefs.SetFloat("Info exp", Info_exp);
        PlayerPrefs.SetFloat("Info satisfaction", Info_satisfaction);
        PlayerPrefs.SetInt("Info money", Info_money);
    }

    void UpdateInfo_Text()
    {
        T_day.text = Info_day.ToString() +" 일째";
        T_level.text = "Lv. " + Info_level.ToString();
        T_exp.text = Info_exp.ToString();
        T_satisfaction.text = Info_satisfaction.ToString();
        T_money.text = Info_money.ToString()+"원";
        T_time.text = (Info_time / 60).ToString() + " : " + (Info_time % 60);
    }

    void UpdateInfo_ImageBar()
    {
        I_exp.fillAmount = Info_exp/100;
        I_satisfaction.fillAmount = Info_satisfaction/100;
    }
}
