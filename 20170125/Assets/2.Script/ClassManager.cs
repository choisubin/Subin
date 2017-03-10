using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClassManager
{

}
public class Tile       //타일 클래스
{
    public string Tile_Type;        //타일 종류
    public string Tile_touch_Type;  //타일 인식 타입
    public int Tile_being;          //타일 유or무
    public int Tile_num;            //타일 번호
    public Vector2 Tile_Position;   //타일 좌표
    public bool Tile_ues;           //타일 사용 유무
}
public class Guest_c      //손님 클래스
{
    public int Guest_type;          //손님 종류
    public GameObject Guest_object; //손님 오브젝트
    public float Guest_waittime;    //손님 대기시간
    public Vector2 Guest_Position;  //손님 좌표
    public float Guest_come_min;    //손님 등장확률 min
    public float Guest_come_max;    //손님 등장확률 max
    public int[] Guest_process;     //머리 과정
    public int[] Guest_processnum;  //몇번인지
}

public class Guest_ob
{
    public GameObject Guest_OB;
    public bool nullcheck=true;
    public int guest_type;
    public int[] Guest_process;     //머리 과정
    public int[] Guest_processnum;  //몇번인지
    public int guest_process_step;
    public bool guest_permcheck = false;
    public float guest_permtime = 0;
    public int i=0;
    public int X=0;
    public int Y=0;
    public int Tool=0;
}

