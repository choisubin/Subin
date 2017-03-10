using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ingame_Tile : MonoBehaviour
{
    public enum Tool
    {
        CUT,
        COLOR,
        SETTING,
        PERM,
        WEDDING,
        BARIGGANG,
        DRY,
        MAKEUP,
        SPRAY,
        SHAMP,
        PERMMACHINE,
        HAND,
        NON,
        END
    }
    public enum Hair
    {
        CUT,            //일반컷        
        COLOR,          //염색
        CUT_COLOR,      //염색과컷
        PERM,           //파마
        STRAIGHT,       //스트레이트
        NOHAIR,         //빡빡머리
        MAKEUP,         //메이크업
        CUT_PERM,       //컷과파마
        CUT_COLOR_PERM, //컷과염색과파마
        FAIRY,          //요정헤어
        WOLFGIRL,       //늑대소녀헤어
        TWINT,          //트윈트양갈래헤어
        RIBBON,         //리본헤어
        ORORA,          //오로라헤어
    }
    #region const
    const int BG_WIDTH = 15;
    const int BG_HEIGHT = 6;
    const int Tile_Size = 1;
    const int START_X = 2;
    const int START_Y = 3;
    const int Guest_num = 14;
    const int Guest_stay =12;
    const int TOOL_NUM = 10;
    const float PERM_DELAY = 4.5f;
    #endregion
    #region classset
    public Tile[,] BackGround_Tile = new Tile[BG_HEIGHT, BG_WIDTH];
    public Tile[,] ObJect_Tile = new Tile[BG_HEIGHT, BG_WIDTH];
    public Guest_c[] guest_cc=new Guest_c[Guest_num];
    public Guest_ob[] guest_ob = new Guest_ob[Guest_stay];
    #endregion
    #region prefabs and parent
    public GameObject BG_prefabs;
    public GameObject BG_parent;

    public GameObject Player_prefabs;
    public GameObject Player_parent;

    public GameObject ObJect_Sofa_prefabs;
    public GameObject ObJect_TeaTable_prefabs;
    public GameObject ObJect_TV_prefabs;
    public GameObject ObJect_coffeetable_prefabs;
    public GameObject ObJect_perm_prefabs;
    public GameObject ObJect_cut_prefabs;
    public GameObject ObJect_shampoo_prefabs;
    public GameObject ObJect_parent;

    public GameObject GuestZUP_parent;
    public GameObject GuestZDOWN_parent;
    public GameObject[] Guest_prefabs;
    #endregion
    #region Player
    int Player_Tile_X;
    int Player_Tile_Y;
    string Player_cur;
    int Player_curHand= (int)Tool.NON;
    #endregion
    public Animator ani;
    GameObject Player_Tile;
    Vector2[] toolposition=new Vector2[TOOL_NUM];
    #region 기타변수들
    int result_num;
    int choice_guest_num;
    int cur_choicenum;
    int specialchance;
    float cur_time;
    float guest_cometime;
    float delay_time = 0;
    bool whilecheck = false;
    bool whilecheck1 = false;
    bool whilecheck2 = false;
    bool specialcheck = false;
    bool delaycheck = false;
    #endregion

    void Start()
    {
        Player_cur = "NON";
        set_BG_Tile();
        set_Player_Tile();
        set_ObJect_Tile();
        set_guest();
        set_guestob();
        ani = Player_Tile.GetComponent<Animator>();

    }


    void Update()
    {
        move_Player_Tile();
        cur_time += Time.deltaTime;
        guest_cometime+= Time.deltaTime;


        for (int i = 0; i < Guest_stay; i++)
        {
            if (guest_ob[i].nullcheck == false) //게스트가 존재할때
            {
               if(guest_ob[i].guest_permcheck==true)
                {
                    guest_ob[i].guest_permtime += Time.deltaTime;
                    if(guest_ob[i].guest_permtime>= PERM_DELAY)
                    {
                        guest_ob[i].Guest_OB.transform.FindChild("want").gameObject.SetActive(true);
                        guest_ob[i].Guest_OB.transform.FindChild("wantnum").gameObject.SetActive(true);
                        guest_ob[i].guest_permcheck = false;
                        guest_ob[i].guest_permtime = 0;
                        move_guest_to(guest_ob[i].i, guest_ob[i].X, guest_ob[i].Y, guest_ob[i].Tool);
                        guest_ob[i].Guest_OB.transform.FindChild("want").GetChild(guest_ob[i].Guest_process[guest_ob[i].guest_process_step]).gameObject.SetActive(true);
                        guest_ob[i].Guest_OB.transform.FindChild("wantnum").GetComponent<Text>().text = guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step].ToString();
                    }
                }

            }
        }

                if (delaycheck==true)
        {
            delay_time += Time.deltaTime;
            if(delay_time>=0.37f)
            {
                delaycheck = false;
                delay_time = 0;
            }
        }

        if (guest_cometime >= 5)
        {
            guest_cometime = 0;
            
            for (int h = 0; h < BG_HEIGHT; h++)
            {
                for (int w = 0; w < BG_WIDTH; w++)
                {
                    if( ObJect_Tile[h,w].Tile_ues==false && ObJect_Tile[h,w].Tile_Type=="SOFA" &&whilecheck==false)
                    {
                        specialchance = Random.Range(1,100);
                        if(specialchance<=5)
                        {
                            specialcheck = true;
                        }
                        else
                        {
                            specialcheck = false;
                        }
                        int random_num = Random.Range(1, 100);

                        #region 헤어 확률
                        if (specialcheck == false)   //일반 손님일 경우
                        {
                            if (random_num <= 20)
                            {
                                result_num = 0;
                            }
                            else if (random_num <= 35)
                            {
                                result_num = 1;
                            }
                            else if (random_num <= 50)
                            {
                                result_num = 2;
                            }
                            else if (random_num <= 60)
                            {
                                result_num = 3;
                            }
                            else if (random_num <= 70)
                            {
                                result_num = 4;
                            }
                            else if (random_num <= 80)
                            {
                                result_num = 5;
                            }
                            else if (random_num <= 85)
                            {
                                result_num = 6;
                            }
                            else if (random_num <= 93)
                            {
                                result_num = 7;
                            }
                            else if (random_num <= 100)
                            {
                                result_num = 8;
                            }
                        }
                        else
                        {
                            if(random_num<=30)
                            {
                                result_num = 9;
                            }
                            else if (random_num <= 50)
                            {
                                result_num = 10;
                            }
                            else if (random_num <= 90)
                            {
                                result_num = 11;
                            }
                            else if (random_num <= 96)
                            {
                                result_num = 12;
                            }
                            else if (random_num <= 100)
                            {
                                result_num = 13;
                            }
                        }
                        #endregion

                        for (int i = 0; i < Guest_stay; i++)
                        {
                            if (guest_ob[i].nullcheck == true)
                            {
                                guest_ob[i].Guest_OB = Instantiate(guest_cc[result_num].Guest_object, new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y), transform.rotation) as GameObject;
                                guest_ob[i].Guest_OB.transform.SetParent(GuestZUP_parent.transform);
                                guest_ob[i].guest_type = result_num;

                                for(int ii=0;ii< guest_cc[guest_ob[i].guest_type].Guest_processnum.Length;ii++)
                                {
                                    guest_ob[i].Guest_process = guest_cc[guest_ob[i].guest_type].Guest_process;
                                    guest_ob[i].Guest_processnum[ii] = guest_cc[guest_ob[i].guest_type].Guest_processnum[ii];
                                }
                                guest_ob[i].guest_process_step = 0; 
                                guest_ob[i].nullcheck = false;
                                guest_ob[i].guest_permcheck = false;
                                guest_ob[i].guest_permtime = 0;

                                guest_ob[i].Guest_OB.transform.FindChild("want").GetChild(guest_ob[i].Guest_process[guest_ob[i].guest_process_step]).gameObject.SetActive(true);
                                guest_ob[i].Guest_OB.transform.FindChild("wantnum").GetComponent<Text>().text = guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step].ToString();

                                break;
                            }
                        }
                        whilecheck = true;
                        ObJect_Tile[h, w].Tile_ues = true;
                        

                    }
                }
                whilecheck = false;
            }
        }
        
    }

    void set_BG_Tile()
    {
        for (int h = 0; h < BG_HEIGHT; h++)
        {
            for (int w = 0; w < BG_WIDTH; w++)
            {
                BackGround_Tile[h, w] = new Tile();

                BackGround_Tile[h, w].Tile_Type = "BACKGROUND";
                BackGround_Tile[h, w].Tile_being = 0;
                BackGround_Tile[h, w].Tile_Position.x = -7 + (Tile_Size * w);
                BackGround_Tile[h, w].Tile_Position.y = -3.5f + (Tile_Size * h);

                GameObject BG_tile = Instantiate(BG_prefabs, new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y), transform.rotation) as GameObject;
                BG_tile.transform.SetParent(BG_parent.transform);
            }
        }
    }

    void set_Player_Tile()
    {
        Player_Tile = Instantiate(Player_prefabs, new Vector2(BackGround_Tile[START_Y, START_X].Tile_Position.x, BackGround_Tile[START_Y, START_X].Tile_Position.y), transform.rotation) as GameObject;
        Player_Tile.transform.SetParent(Player_parent.transform);
        Player_Tile_X = START_X;
        Player_Tile_Y = START_Y;
        for(int i=0;i<TOOL_NUM;i++)
        {
            toolposition[i] = Player_Tile.transform.FindChild("toolbox").FindChild("toolposition").GetChild(i).gameObject.transform.position;
        }
    }

    void set_ObJect_Tile()
    {
        for (int h = 0; h < BG_HEIGHT; h++)
        {
            for (int w = 0; w < BG_WIDTH; w++)
            {
                ObJect_Tile[h, w] = new Tile();
                ObJect_Tile[h, w].Tile_Position.x = -7 + (Tile_Size * w);
                ObJect_Tile[h, w].Tile_Position.y = -3.5f + (Tile_Size * h);
                ObJect_Tile[h, w].Tile_ues = false;
            }
        }
        #region setOBTile
        setOBTile(2, 1, "SOFA");
        setOBTile(2, 2, "SOFA");
        setOBTile(2, 3, "SOFA");

        setOBTile(1, 2, "TEATABLE");

        setOBTile(0, 2, "TV");

        setOBTile(1, 5, "COFFEETABLE");

        setOBTile(5, 6, "CUT");
        setOBTile(5, 8, "CUT");
        setOBTile(5, 10, "CUT");
        setOBTile(5, 12, "CUT");
        
        setOBTile(1, 14, "PERM");
        setOBTile(3, 14, "PERM");

        setOBTile(0, 7, "SHAMPOO");
        setOBTile(0, 9, "SHAMPOO");
        setOBTile(0, 11, "SHAMPOO");

        setOBTile(4, 6, "CUTSOFA");
        setOBTile(4, 8, "CUTSOFA");
        setOBTile(4, 10, "CUTSOFA");
        setOBTile(4, 12, "CUTSOFA");
        #endregion
        #region Tile setting
        for (int h = 0; h < BG_HEIGHT; h++)
        {
            for (int w = 0; w < BG_WIDTH; w++)
            {
                if (ObJect_Tile[h, w].Tile_Type == "SOFA")
                {
                    GameObject ObJect_Sofa = Instantiate(ObJect_Sofa_prefabs, new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y), transform.rotation) as GameObject;
                    ObJect_Sofa.transform.SetParent(ObJect_parent.transform);
                }
                else if (ObJect_Tile[h, w].Tile_Type == "TEATABLE")
                {
                    GameObject ObJect_TeaTable = Instantiate(ObJect_TeaTable_prefabs, new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y), transform.rotation) as GameObject;
                    ObJect_TeaTable.transform.SetParent(ObJect_parent.transform);
                }
                else if (ObJect_Tile[h, w].Tile_Type == "TV")
                {
                    GameObject ObJect_TV = Instantiate(ObJect_TV_prefabs, new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y), transform.rotation) as GameObject;
                    ObJect_TV.transform.SetParent(ObJect_parent.transform);
                }
                else if (ObJect_Tile[h, w].Tile_Type == "COFFEETABLE")
                {
                    GameObject ObJect_CoffeeTable = Instantiate(ObJect_coffeetable_prefabs, new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y), transform.rotation) as GameObject;
                    ObJect_CoffeeTable.transform.SetParent(ObJect_parent.transform);
                }
                else if (ObJect_Tile[h, w].Tile_Type == "CUT")
                {
                    GameObject ObJect_Cut = Instantiate(ObJect_cut_prefabs, new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y), transform.rotation) as GameObject;
                    ObJect_Cut.transform.SetParent(ObJect_parent.transform);
                }
                else if (ObJect_Tile[h, w].Tile_Type == "SHAMPOO")
                {
                    GameObject ObJect_Shampoo = Instantiate(ObJect_shampoo_prefabs, new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y), transform.rotation) as GameObject;
                    ObJect_Shampoo.transform.SetParent(ObJect_parent.transform);
                }
                else if (ObJect_Tile[h, w].Tile_Type == "PERM")
                {
                    GameObject ObJect_Perm = Instantiate(ObJect_perm_prefabs, new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y), transform.rotation) as GameObject;
                    ObJect_Perm.transform.SetParent(ObJect_parent.transform);
                }
            }
        }
        #endregion
    }
    void set_guest()
    {
        for (int i = 0; i < Guest_num; i++)
        {
            guest_cc[i] = new Guest_c();
            guest_cc[i].Guest_type = i;
            guest_cc[i].Guest_object = Guest_prefabs[i];
            guest_cc[i].Guest_waittime = 10;
            guest_cc[i].Guest_come_max = 20 * (i + 1);
            guest_cc[i].Guest_come_min = 20 * (i + 1) - 19;

        }
        setguest_cc();

    }

    void setguest_cc()
    {
        #region 이런 우라질 노가다 같으니라고 머리하는 순서설정입니다.
        //일반컷
        guest_cc[0].Guest_process = new int[] { (int)Tool.CUT, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.END };
        guest_cc[0].Guest_processnum = new int[] { 3, 3, 3, 0 };
        //염색
        guest_cc[1].Guest_process = new int[] { (int)Tool.COLOR, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.END };
        guest_cc[1].Guest_processnum = new int[] { 5, 3, 3, 0 };
        //염색과 컷
        guest_cc[2].Guest_process = new int[] { (int)Tool.CUT, (int)Tool.COLOR, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.END };
        guest_cc[2].Guest_processnum = new int[] { 3, 5, 3, 3, 0 };
        //파마
        guest_cc[3].Guest_process = new int[] { (int)Tool.PERM, (int)Tool.PERMMACHINE, (int)Tool.PERM, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.END };
        guest_cc[3].Guest_processnum = new int[] { 5, 1, 5, 3, 3, 0 };
        //스트레이트
        guest_cc[4].Guest_process = new int[] { (int)Tool.SETTING, (int)Tool.PERMMACHINE, (int)Tool.SETTING, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.END };
        guest_cc[4].Guest_processnum = new int[] { 5, 1, 5, 3, 3, 0 };
        //빡빡머리
        guest_cc[5].Guest_process = new int[] { (int)Tool.BARIGGANG, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.END };
        guest_cc[5].Guest_processnum = new int[] { 3, 3, 3, 0 };
        //메이크업
        guest_cc[6].Guest_process = new int[] { (int)Tool.MAKEUP, (int)Tool.END };
        guest_cc[6].Guest_processnum = new int[] { 3, 0 };
        //컷과파마
        guest_cc[7].Guest_process = new int[] { (int)Tool.CUT, (int)Tool.PERM, (int)Tool.PERMMACHINE, (int)Tool.PERM, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.END };
        guest_cc[7].Guest_processnum = new int[] { 3, 5, 1, 5, 3, 3, 0 };
        //컷과 염색과 파마
        guest_cc[8].Guest_process = new int[] { (int)Tool.CUT, (int)Tool.COLOR, (int)Tool.PERM, (int)Tool.PERMMACHINE, (int)Tool.PERM, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.END };
        guest_cc[8].Guest_processnum = new int[] { 3, 5, 5, 1, 5, 3, 3, 0 };
        //요정헤어
        guest_cc[9].Guest_process = new int[] { (int)Tool.PERM, (int)Tool.PERMMACHINE, (int)Tool.PERM, (int)Tool.COLOR, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.MAKEUP, (int)Tool.END };
        guest_cc[9].Guest_processnum = new int[] { 5, 1, 5, 5, 3, 3, 3, 0 };
        //늑대소녀헤어
        guest_cc[10].Guest_process = new int[] { (int)Tool.CUT, (int)Tool.SETTING, (int)Tool.PERMMACHINE, (int)Tool.SETTING, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.HAND, (int)Tool.END };
        guest_cc[10].Guest_processnum = new int[] { 3, 5, 1, 5, 3, 3, 3, 0 };
        //트윈트 양갈래 헤어
        guest_cc[11].Guest_process = new int[] { (int)Tool.CUT, (int)Tool.COLOR, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.HAND, (int)Tool.END };
        guest_cc[11].Guest_processnum = new int[] { 3, 5, 3, 3, 3, 0 };
        //리본송이
        guest_cc[12].Guest_process = new int[] { (int)Tool.CUT, (int)Tool.PERM, (int)Tool.PERMMACHINE, (int)Tool.PERM, (int)Tool.SHAMP, (int)Tool.COLOR, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.SPRAY, (int)Tool.MAKEUP, (int)Tool.END };
        guest_cc[12].Guest_processnum = new int[] { 3, 5, 1, 5, 3, 5, 3, 3, 3, 3, 0 };
        //오로라 헤어
        guest_cc[13].Guest_process = new int[] { (int)Tool.CUT, (int)Tool.PERM, (int)Tool.PERMMACHINE, (int)Tool.PERM, (int)Tool.SHAMP, (int)Tool.COLOR, (int)Tool.SHAMP, (int)Tool.DRY, (int)Tool.SPRAY, (int)Tool.HAND, (int)Tool.MAKEUP, (int)Tool.END };
        guest_cc[13].Guest_processnum = new int[] { 3, 5, 1, 5, 3, 5, 3, 3, 3, 3, 3, 0 };
        #endregion
    }                            

    void set_guestob()
    {
        for( int i=0 ; i < Guest_stay ; i++ )
        {
            guest_ob[i] = new Guest_ob();
            guest_ob[i].nullcheck = true;
            guest_ob[i].Guest_process = new int[13];
            guest_ob[i].Guest_processnum = new int[13];
        }
    }

    void move_guest_to(int guestnum,int X,int Y,int type)
    {
        bool whilecheck = false;
        for (int h = 0; h < BG_HEIGHT-1; h++)
        {
            for (int w = 0; w < BG_WIDTH; w++)
            {
                if (ObJect_Tile[h+1, w].Tile_ues == false && ObJect_Tile[h, w].Tile_Type == "SHAMPOO" && whilecheck == false && type == (int)Tool.SHAMP)
                {
                    guest_ob[guestnum].Guest_OB.transform.position = new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y);
                    whilecheck = true;
                    ObJect_Tile[h+1, w].Tile_ues = true;
                    ObJect_Tile[Y, X].Tile_ues = false;
                }
                else if (ObJect_Tile[h+1, w].Tile_ues == false && ObJect_Tile[h, w].Tile_Type == "PERM" && whilecheck == false && type == (int)Tool.PERMMACHINE)
                {
                    guest_ob[guestnum].Guest_OB.transform.position = new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y);
                    whilecheck = true;
                    ObJect_Tile[h+1, w].Tile_ues = true;
                    ObJect_Tile[Y, X].Tile_ues = false;
                }
                else if (ObJect_Tile[h, w].Tile_ues == false && ObJect_Tile[h, w].Tile_Type == "CUTSOFA" && whilecheck == false && type == (int)Tool.DRY)
                {
                    guest_ob[guestnum].Guest_OB.transform.position = new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y);
                    whilecheck = true;
                    ObJect_Tile[h, w].Tile_ues = true;
                    ObJect_Tile[Y, X].Tile_ues = false;
                }
            }
        }
        whilecheck = false;
    }
    
    void setOBTile(int y,int x,string type)
    {
        #region setTiletouchType
        if (type == "SOFA"||type=="CUT"||type=="CUTSOFA") //1칸차지
        {
            ObJect_Tile[y, x].Tile_Type = type;
            ObJect_Tile[y, x].Tile_being = 1;

            ObJect_Tile[y, x].Tile_touch_Type = type;
        }
        else if (type == "TEATABLE"|| type == "TV") //가로 3칸차지
        {
            ObJect_Tile[y, x].Tile_Type = type;
            ObJect_Tile[y, x].Tile_being = 1;
            ObJect_Tile[y, x + 1].Tile_being = 1;
            ObJect_Tile[y, x - 1].Tile_being = 1;

            ObJect_Tile[y, x].Tile_touch_Type = type;
            ObJect_Tile[y, x + 1].Tile_touch_Type = type;
            ObJect_Tile[y, x - 1].Tile_touch_Type = type;
        }
        else if(type=="COFFEETABLE")//세로 3칸차지
        {
            ObJect_Tile[y, x].Tile_Type = type;
            ObJect_Tile[y, x].Tile_being = 1;
            ObJect_Tile[y + 1, x].Tile_being = 1;
            ObJect_Tile[y - 1, x].Tile_being = 1;

            ObJect_Tile[y, x].Tile_touch_Type = type;
            ObJect_Tile[y + 1, x].Tile_touch_Type = type;
            ObJect_Tile[y - 1, x].Tile_touch_Type = type;
        }
        else if(type=="SHAMPOO"||type=="PERM")//세로 2칸 차지
        {
            ObJect_Tile[y, x].Tile_Type = type;
            ObJect_Tile[y, x].Tile_being = 1;
            ObJect_Tile[y + 1, x].Tile_being = 1;
            
            ObJect_Tile[y + 1, x].Tile_touch_Type = type;
        }
        #endregion
    }

    void move_Player_Tile_set(int X,int Y,int addposition,string type)
    {
        #region 손님 머리자르는곳 이동
        if (ObJect_Tile[Y, X].Tile_touch_Type == "SOFA" && ObJect_Tile[Y, X].Tile_ues == true)
        {
            for (int h = 0; h < BG_HEIGHT; h++)
            {
                for (int w = 0; w < BG_WIDTH; w++)
                {

                    if (ObJect_Tile[h, w].Tile_ues == false && ObJect_Tile[h, w].Tile_Type == "CUTSOFA" && whilecheck1 == false)
                    {
                        if (type == "X")
                        {
                            for (int i = 0; i < Guest_stay; i++)
                            {
                                if (guest_ob[i].nullcheck == false)
                                {
                                    if ((guest_ob[i].Guest_OB.transform.position.x == BackGround_Tile[Y, X].Tile_Position.x) && (guest_ob[i].Guest_OB.transform.position.y == BackGround_Tile[Y, X].Tile_Position.y))
                                    {
                                        guest_ob[i].Guest_OB.transform.position = new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y);
                                        guest_ob[i].Guest_OB.transform.SetParent(GuestZDOWN_parent.transform);
                                        ObJect_Tile[Y, X].Tile_ues = false;
                                        ObJect_Tile[h, w].Tile_ues = true;
                                        whilecheck1 = true;
                                        break;
                                    }

                                }
                            }
                        }
                        else if (type == "Y")
                        {
                            for (int i = 0; i < Guest_stay; i++)
                            {
                                if (guest_ob[i].nullcheck == false)
                                {

                                    if ((guest_ob[i].Guest_OB.transform.position.x == BackGround_Tile[Y, X].Tile_Position.x) && (guest_ob[i].Guest_OB.transform.position.y == BackGround_Tile[Y, X].Tile_Position.y))
                                    {
                                        guest_ob[i].Guest_OB.transform.position = new Vector2(BackGround_Tile[h, w].Tile_Position.x, BackGround_Tile[h, w].Tile_Position.y);
                                        guest_ob[i].Guest_OB.transform.SetParent(GuestZDOWN_parent.transform);
                                        ObJect_Tile[Y, X].Tile_ues = false;
                                        ObJect_Tile[h, w].Tile_ues = true;
                                        whilecheck1 = true;
                                        break;
                                    }

                                }
                            }
                        }
                    }

                }
                whilecheck1 = false;
            }
        }
        #endregion
        #region 머리자르는 쇼파에서 하는 행동들
        else if (ObJect_Tile[Y, X].Tile_touch_Type == "CUTSOFA" && ObJect_Tile[Y, X].Tile_ues == true)  //머리자르는 쇼파에서 하는 행동들
        {
            if (type == "X")
            {
                for (int i = 0; i < Guest_stay; i++)
                {
                    if (guest_ob[i].nullcheck == false) //게스트가 존재할때
                    {
                        if ((guest_ob[i].Guest_OB.transform.position.x == BackGround_Tile[Y, X].Tile_Position.x) && (guest_ob[i].Guest_OB.transform.position.y == BackGround_Tile[Y, X].Tile_Position.y))
                        {
                            if (delaycheck == false)
                            {
                                delaycheck = true;
                                if (guest_ob[i].Guest_process[guest_ob[i].guest_process_step] == Player_curHand)//요구조건과 플레이어 손에있는게 똑같을때
                                {
                                    guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step]--;
                                    guest_ob[i].Guest_OB.transform.FindChild("wantnum").GetComponent<Text>().text = guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step].ToString();
                                    #region 애니메이션
                                    if (addposition == 1)
                                    {
                                        if (Player_curHand == (int)Tool.CUT)
                                        {
                                            ani.Play("P_cut_r");
                                        }
                                        else if (Player_curHand == (int)Tool.SETTING)
                                        {
                                            ani.Play("P_setting_r");
                                        }
                                        else if (Player_curHand == (int)Tool.PERM)
                                        {
                                            ani.Play("P_perm_r");
                                        }
                                        else if (Player_curHand == (int)Tool.BARIGGANG)
                                        {
                                            ani.Play("P_bariggang_r");
                                        }
                                        else if (Player_curHand == (int)Tool.DRY)
                                        {
                                            ani.Play("P_dry_r");
                                        }
                                        else if (Player_curHand == (int)Tool.MAKEUP)
                                        {
                                            ani.Play("P_makeup_r");
                                        }
                                        else if (Player_curHand == (int)Tool.SPRAY)
                                        {
                                            ani.Play("P_spray_r");
                                        }
                                        else if (Player_curHand == (int)Tool.HAND)
                                        {
                                            ani.Play("P_hand_r");
                                        }
                                        else if (Player_curHand == (int)Tool.COLOR)
                                        {
                                            ani.Play("P_color_r");
                                        }
                                    }
                                    else
                                    {
                                        if (Player_curHand == (int)Tool.CUT)
                                        {
                                            ani.Play("P_cut_l");
                                        }
                                        else if (Player_curHand == (int)Tool.SETTING)
                                        {
                                            ani.Play("P_setting_l");
                                        }
                                        else if (Player_curHand == (int)Tool.PERM)
                                        {
                                            ani.Play("P_perm_l");
                                        }
                                        else if (Player_curHand == (int)Tool.BARIGGANG)
                                        {
                                            ani.Play("P_bariggang_l");
                                        }
                                        else if (Player_curHand == (int)Tool.DRY)
                                        {
                                            ani.Play("P_dry_l");
                                        }
                                        else if (Player_curHand == (int)Tool.MAKEUP)
                                        {
                                            ani.Play("P_makeup_l");
                                        }
                                        else if (Player_curHand == (int)Tool.SPRAY)
                                        {
                                            ani.Play("P_spray_l");
                                        }
                                        else if (Player_curHand == (int)Tool.HAND)
                                        {
                                            ani.Play("P_hand_l");
                                        }
                                        else if (Player_curHand == (int)Tool.COLOR)
                                        {
                                            ani.Play("P_color_l");
                                        }
                                    }
                                    #endregion

                                    if (guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step] == 0) //횟수 다했을때 다음 요구조건으로 넘어감
                                    {
                                        guest_ob[i].Guest_OB.transform.FindChild("want").GetChild(guest_ob[i].Guest_process[guest_ob[i].guest_process_step]).gameObject.SetActive(false);
                                        guest_ob[i].guest_process_step++;
                                        if (guest_ob[i].Guest_process[guest_ob[i].guest_process_step] != (int)Tool.END)
                                        {
                                            guest_ob[i].Guest_OB.transform.FindChild("want").GetChild(guest_ob[i].Guest_process[guest_ob[i].guest_process_step]).gameObject.SetActive(true);
                                            guest_ob[i].Guest_OB.transform.FindChild("wantnum").GetComponent<Text>().text = guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step].ToString();
                                        }
                                        else
                                        {
                                            guest_ob[i].nullcheck = true;
                                            guest_ob[i].Guest_OB.transform.FindChild("wantnum").gameObject.SetActive(false);
                                            ObJect_Tile[Y, X].Tile_ues = false;
                                            Destroy(guest_ob[i].Guest_OB);
                                        }

                                        if (guest_ob[i].Guest_process[guest_ob[i].guest_process_step] == (int)Tool.SHAMP)
                                        {
                                            move_guest_to(i, X, Y, (int)Tool.SHAMP);
                                        }
                                        else if (guest_ob[i].Guest_process[guest_ob[i].guest_process_step] == (int)Tool.PERMMACHINE)
                                        {
                                            move_guest_to(i, X, Y, (int)Tool.PERMMACHINE);
                                        }
                                    }

                                    break;
                                }
                                else if (guest_ob[i].Guest_process[guest_ob[i].guest_process_step] == (int)Tool.SHAMP)
                                {
                                    move_guest_to(i, X, Y, (int)Tool.SHAMP);
                                }
                                else if (guest_ob[i].Guest_process[guest_ob[i].guest_process_step] == (int)Tool.PERMMACHINE)
                                {
                                    move_guest_to(i, X, Y, (int)Tool.PERMMACHINE);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region 머리감겨주기
        else if (ObJect_Tile[Y, X].Tile_touch_Type == "SHAMPOO" && ObJect_Tile[Y, X].Tile_ues == true)  //머리자르는 쇼파에서 하는 행동들
        {
            if (type == "X")
            {
                for (int i = 0; i < Guest_stay; i++)
                {
                    if (guest_ob[i].nullcheck == false) //게스트가 존재할때
                    {
                        if ((guest_ob[i].Guest_OB.transform.position.x == BackGround_Tile[Y-1, X].Tile_Position.x) && (guest_ob[i].Guest_OB.transform.position.y == BackGround_Tile[Y - 1, X].Tile_Position.y))
                        {
                            if (delaycheck == false)
                            {
                                delaycheck = true;
                                if (guest_ob[i].Guest_process[guest_ob[i].guest_process_step] == (int)Tool.SHAMP)
                                {
                                    guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step]--;
                                    guest_ob[i].Guest_OB.transform.FindChild("wantnum").GetComponent<Text>().text = guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step].ToString();
                                    if (addposition == 1)
                                    {
                                        ani.Play("P_play_r");
                                    }
                                    else
                                    {
                                        ani.Play("P_play_l");
                                    }

                                    if (guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step] == 0) //횟수 다했을때 다음 요구조건으로 넘어감
                                    {
                                        guest_ob[i].Guest_OB.transform.FindChild("want").GetChild(guest_ob[i].Guest_process[guest_ob[i].guest_process_step]).gameObject.SetActive(false);
                                        guest_ob[i].guest_process_step++;
                                        if (guest_ob[i].Guest_process[guest_ob[i].guest_process_step] != (int)Tool.END)
                                        {
                                            guest_ob[i].Guest_OB.transform.FindChild("want").GetChild(guest_ob[i].Guest_process[guest_ob[i].guest_process_step]).gameObject.SetActive(true);
                                            guest_ob[i].Guest_OB.transform.FindChild("wantnum").GetComponent<Text>().text = guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step].ToString();
                                        }
                                        else
                                        {
                                            guest_ob[i].nullcheck = true;
                                            guest_ob[i].Guest_OB.transform.FindChild("wantnum").gameObject.SetActive(false);
                                            ObJect_Tile[Y, X].Tile_ues = false;
                                            Destroy(guest_ob[i].Guest_OB);
                                        }

                                        move_guest_to(i, X, Y, (int)Tool.DRY);
                                    }
                                }
                                else
                                {
                                    move_guest_to(i, X, Y, (int)Tool.DRY);
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #region 파마기 작동
        else if (ObJect_Tile[Y, X].Tile_touch_Type == "PERM" && ObJect_Tile[Y, X].Tile_ues == true)  //머리자르는 쇼파에서 하는 행동들
        {
            if (type == "X")
            {
                for (int i = 0; i < Guest_stay; i++)
                {
                    if (guest_ob[i].nullcheck == false) //게스트가 존재할때
                    {
                        if ((guest_ob[i].Guest_OB.transform.position.x == BackGround_Tile[Y - 1, X].Tile_Position.x) && (guest_ob[i].Guest_OB.transform.position.y == BackGround_Tile[Y - 1, X].Tile_Position.y))
                        {
                            if (delaycheck == false)
                            {
                                delaycheck = true;
                                if (guest_ob[i].Guest_process[guest_ob[i].guest_process_step] == (int)Tool.PERMMACHINE)
                                {
                                    guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step]--;
                                    guest_ob[i].Guest_OB.transform.FindChild("wantnum").GetComponent<Text>().text = guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step].ToString();

                                    if (guest_ob[i].Guest_processnum[guest_ob[i].guest_process_step] == 0&&guest_ob[i].guest_permcheck==false) //횟수 다했을때 다음 요구조건으로 넘어감
                                    {
                                        guest_ob[i].Guest_OB.transform.FindChild("want").GetChild(guest_ob[i].Guest_process[guest_ob[i].guest_process_step]).gameObject.SetActive(false);
                                        guest_ob[i].guest_process_step++;
                                        guest_ob[i].guest_permcheck = true;
                                        guest_ob[i].guest_permtime = 0;
                                        guest_ob[i].i = i;
                                        guest_ob[i].X = X;
                                        guest_ob[i].Y = Y;
                                        guest_ob[i].Tool = (int)Tool.DRY;

                                        guest_ob[i].Guest_OB.transform.FindChild("want").gameObject.SetActive(false);
                                        guest_ob[i].Guest_OB.transform.FindChild("wantnum").gameObject.SetActive(false);
                                        
                                    }
                                }
                                else
                                {
                                    if (guest_ob[i].guest_permcheck == false)
                                    {
                                        move_guest_to(i, X, Y, (int)Tool.DRY);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
        else if (ObJect_Tile[Y, X].Tile_being == 1)
        {
             Debug.Log(ObJect_Tile[Y, X].Tile_touch_Type);
        }                                                                     
        else
        {
            if (type == "X")
            {
                Player_Tile_X += addposition;
            }
            else if (type == "Y")
            {
                Player_Tile_Y += addposition;
            }
            Player_Tile.transform.position = new Vector2(BackGround_Tile[Y, X].Tile_Position.x, BackGround_Tile[Y, X].Tile_Position.y);
        }
    }
     
    void move_Player_Tile()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Player_cur == "NON")
            {
                if ((Player_Tile_X + 1) == BG_WIDTH) return;
                move_Player_Tile_set(Player_Tile_X + 1, Player_Tile_Y, 1, "X");
            }
            else if(Player_cur == "CHOICE")
            {
                if (cur_choicenum + 1 < 10)
                {
                    cur_choicenum++;
                    Player_Tile.transform.FindChild("toolbox").FindChild("toolchoice").gameObject.transform.position = new Vector3(toolposition[cur_choicenum].x, toolposition[cur_choicenum].y, -2);
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Player_cur == "NON")
            {
                if ((Player_Tile_X - 1) == -1) return;
                move_Player_Tile_set(Player_Tile_X - 1, Player_Tile_Y, -1, "X");
            }
            else if (Player_cur == "CHOICE")
            {
                if (cur_choicenum - 1 >= 0)
                {
                    cur_choicenum--;
                    Player_Tile.transform.FindChild("toolbox").FindChild("toolchoice").gameObject.transform.position = new Vector3(toolposition[cur_choicenum].x, toolposition[cur_choicenum].y, -2);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Player_cur == "NON")
            {
                if ((Player_Tile_Y + 1) == BG_HEIGHT) return;
                move_Player_Tile_set(Player_Tile_X, Player_Tile_Y + 1, 1, "Y");
            }
            else if (Player_cur == "CHOICE")
            {
                if(cur_choicenum - 5 >=0)
                {
                    cur_choicenum -= 5;
                    Player_Tile.transform.FindChild("toolbox").FindChild("toolchoice").gameObject.transform.position = new Vector3(toolposition[cur_choicenum].x, toolposition[cur_choicenum].y, -2);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Player_cur == "NON")
            {
                if ((Player_Tile_Y - 1) == -1) return;
                move_Player_Tile_set(Player_Tile_X, Player_Tile_Y - 1, -1, "Y");
            }
            else if (Player_cur == "CHOICE")
            {
                if(cur_choicenum + 5 < 10)
                {
                    cur_choicenum += 5;
                    Player_Tile.transform.FindChild("toolbox").FindChild("toolchoice").gameObject.transform.position = new Vector3(toolposition[cur_choicenum].x, toolposition[cur_choicenum].y, -2);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Player_cur == "NON")
            {
                for (int i = 0; i < TOOL_NUM; i++)
                {
                    toolposition[i] = Player_Tile.transform.FindChild("toolbox").FindChild("toolposition").GetChild(i).gameObject.transform.position;
                }
                Player_cur = "CHOICE";
                Player_Tile.transform.FindChild("toolbox").gameObject.SetActive(true);
                Player_Tile.transform.FindChild("toolbox").FindChild("toolchoice").gameObject.transform.position = new Vector3(toolposition[0].x, toolposition[0].y, -2);
                cur_choicenum = 0;
            }
            else if (Player_cur == "CHOICE")
            {
                Player_cur = "NON";
                Player_Tile.transform.FindChild("toolbox").gameObject.SetActive(false);

                if (cur_choicenum == 0)
                {
                    Player_curHand = (int)Tool.CUT;
                }
                else if (cur_choicenum == 1)
                {
                    Player_curHand = (int)Tool.COLOR;
                }
                else if (cur_choicenum == 2)
                {
                    Player_curHand = (int)Tool.SETTING;
                }
                else if (cur_choicenum == 3)
                {
                    Player_curHand = (int)Tool.PERM;
                }
                else if (cur_choicenum == 4)
                {
                    Player_curHand = (int)Tool.WEDDING;
                }
                else if (cur_choicenum == 5)
                {
                    Player_curHand = (int)Tool.BARIGGANG;
                }
                else if (cur_choicenum == 6)
                {
                    Player_curHand = (int)Tool.DRY;
                }
                else if (cur_choicenum == 7)
                {
                    Player_curHand = (int)Tool.MAKEUP;
                }
                else if (cur_choicenum == 8)
                {
                    Player_curHand = (int)Tool.SPRAY;
                }
                else if (cur_choicenum == 9)
                {
                    Player_curHand = (int)Tool.HAND;
                }
            }
        }
    }
}
