using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public class CPersoninf
    {
        public long mid;
        public long attribute;
        public string uname;
        public string face;
        public string sign;
    }

    public class CDatastruct
    {
        public List<CPersoninf> list;
        public long re_version;
        public long total;
    }

    public class CFansInformationResposse
    {
        public long code;
        public string message;
        public CDatastruct data;
    }


    public class FansIfmRequest
    {
        public void refresh(Label lbl,DataGridView dgv,string inputUid, string inputSData)
        {
//            string state[7] = { "未关注", "未知", "已关注", "未知", "未知", "未知", "已互粉" };
            string[] state = new string[7];
            state[0] = "未关注";
            state[1] = "未知";
            state[2] = "已关注";
            state[3] = "未知";
            state[4] = "未知";
            state[5] = "未知";
            state[6] = "已互粉";


            //获取输入
            
            string cookie = "SESSDATA=" + inputSData;
            int page = 1;
            int row = 0;
            //循环
            while (page > 0)
            {


                //设置请求网址
                string biliurl = "http://api.bilibili.com/x/relation/followers?vmid=" + inputUid + "&pn=" + page;

                //创建一个请求
                HttpWebRequest myrequest = (HttpWebRequest)WebRequest.Create(biliurl);

                //设置请求
                myrequest.Headers.Add("Cookie", cookie);
                myrequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.93 Safari/537.36";
                myrequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                myrequest.ContentType = "application/x-www-form-urlencoded";
                myrequest.Method = "GET";

                //请求并读取返回数据流
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myrequest.GetResponse();
                StreamReader reader = new StreamReader(myHttpWebResponse.GetResponseStream(), Encoding.UTF8);
                string fback = reader.ReadToEnd();

                //实例化
                CFansInformationResposse myfan = new CFansInformationResposse();
                myfan = JsonConvert.DeserializeObject<CFansInformationResposse>(fback);

                if (myfan == null || myfan.data == null || myfan.data.list == null)
                {
                    MessageBox.Show("SESSDATA未填写或已经过期，请重新获取！");
                    break;
                }
                else if (myfan.data.list.Count == 0)
                {
                    break;
                }
                else
                {
                    foreach (CPersoninf fan in myfan.data.list)
                    {
                        row = dgv.Rows.Add();
                        dgv.Rows[row].Cells[0].Value = row + 1;
                        dgv.Rows[row].Cells[1].Value = fan.uname;
                        dgv.Rows[row].Cells[2].Value = fan.mid;
                        dgv.Rows[row].Cells[3].Value = fan.sign.Replace("\n", "");
                        dgv.Rows[row].Cells[4].Value = fan.face;
                        dgv.Rows[row].Cells[5].Value = state[fan.attribute];

                        lbl.Text = "正在获取第 " + ( row + 1 ) + " 位/第 " + page + " 页";
                        lbl.Refresh();
                    }
                }

                page++;
                Thread.Sleep(100);
            }

            lbl.Text = "总共获取了 " + ( row + 1 ) + " 位/ " + page + " 页";




            //GameObject.Find("Canvas/Panel/Scroll View/Viewport/Content/Text").GetComponent<Text>().text = fback;

        }




    }

}