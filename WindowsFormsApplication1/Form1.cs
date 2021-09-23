using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class Mainform : Form
    {

        public string biliurl = "http://api.bilibili.com/x/relation/followers?vmid=";

        public Mainform()
        {
            InitializeComponent();
            FansDataView.Columns[3].Width = this.Size.Width - 105 - FansDataView.Columns[0].Width - FansDataView.Columns[1].Width - FansDataView.Columns[2].Width - FansDataView.Columns[4].Width;

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            FansIfmRequest fan = new FansIfmRequest();
            //DataTable dt = (DataTable)FansDataView.DataSource;
            //if(dt != null && dt.Rows != null && dt.Rows.Count > 0)
            //  dt.Rows.Clear();
            FansDataView.Rows.Clear();

           // FansDataView.DataSource = dt;
            fan.refresh(pagetips,FansDataView, textBoxUID.Text, textBoxData.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (FansDataView.Rows.Count == 0)
            {
                MessageBox.Show("当前无数据可导出!");
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Title = "请选择要导出的位置";
            saveFileDialog1.Filter = "Text文件| *.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string str;

                FileStream aFile = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(aFile);
                // Write data to file.

                str = "";
                //生成字段名称bai
                for (int i = 0; i < FansDataView.ColumnCount; i++)
                {
                    str += "\"" + FansDataView.Columns[i].HeaderText +"\"";
                    if (i < FansDataView.ColumnCount - 1)
                        str += ",";
                    else
                        str += "\r\n";

                }
                sw.Write(str);
                //填充数据
                for (int i = 0; i < FansDataView.RowCount - 1; i++) //循环行
                {
                    str = "";
                    for (int j = 0; j < FansDataView.ColumnCount; j++) //循环列
                    {
                        str += "\"" + FansDataView.Rows[i].Cells[j].Value.ToString() + "\"";

                        if (j < FansDataView.ColumnCount - 1)
                            str += ",";
                        else
                            str += "\r\n";
                    }
                    sw.Write(str);


                }

                sw.Close();



#if AAA
            

                if (saveFileDialog1.FileName != "")
                {
                    //创建Excel文件的对象
                    NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                    //添加一个sheet
                    NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
                    //获取list数据
                    //List<TB_STUDENTINFOModel> listRainInfo = m_BLL.GetSchoolListAATQ(schoolname);
                    // DataTable listRainInfo = mymssqlConnet.DAL_SelectDT_Par("EnrollmentGroup", mySqlParameters);
                    //给sheet1添加第一行的头部标题
                    NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
                    row1.CreateCell(0).SetCellValue("编码");
                    row1.CreateCell(1).SetCellValue("名称");
                    row1.CreateCell(3).SetCellValue("型号");
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                        rowtemp.CreateCell(0).SetCellValue(dgv.Rows[i].Cells["Fixed_Assets_Code"].Value.ToString());
                        rowtemp.CreateCell(1).SetCellValue(dgv.Rows[i].Cells["Capital_assets_Name_G"].Value.ToString());
                        rowtemp.CreateCell(2).SetCellValue(dgv.Rows[i].Cells["Capital_assets_Code_G"].Value.ToString());
                        rowtemp.CreateCell(3).SetCellValue(dgv.Rows[i].Cells["Capital_assets_Model_G"].Value.ToString());

                    }
                    FileStream ms = File.OpenWrite(saveFileDialog1.FileName.ToString());
                    try
                    {
                        book.Write(ms);
                        ms.Seek(0, SeekOrigin.Begin);
                        MessageBox.Show("导出成功");
                    }
                    catch
                    {
                        MessageBox.Show("导出失败!");
                    }
                    finally
                    {
                        if (ms != null)
                        {
                            ms.Close();
                        }
                    }
                    hidepanelEx15();
                    panelEx15.Text = "正在加载。。。";
                }
#endif
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {

            tabcontroller.Width = this.Size.Width - 40;
            tabcontroller.Height = this.Size.Height - 60;
            FansDataView.Width = this.Size.Width - 60;
            FansDataView.Height = this.Size.Height - 240;
            panel1.Top = this.Size.Height - 230;
            panel1.Width = this.Size.Width - 60;
            FansDataView.Columns[3].Width = this.Size.Width - 105 - FansDataView.Columns[0].Width - FansDataView.Columns[1].Width - FansDataView.Columns[2].Width - FansDataView.Columns[4].Width;
            //if(this.Size.Width < 960 || this.Size.Height < 540)
            //{
            //    this.Width = 960;
            //    this.Height = 540;
            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FansDataView_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            FansDataView.Columns[3].Width = this.Size.Width - 105 - FansDataView.Columns[0].Width - FansDataView.Columns[1].Width - FansDataView.Columns[2].Width - FansDataView.Columns[4].Width;

        }

        private void FansDataView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {

            new 帮助SESSDATA().ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Process.Start("https://www.bilibili.com/read/cv6542828");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            new 帮助SESSDATA().ShowDialog();

        }

        private void label9_Click(object sender, EventArgs e)
        {
            Process.Start("https://space.bilibili.com/271218438");

        }

        private void label11_Click(object sender, EventArgs e)
        {
            Process.Start("https://space.bilibili.com/393479166");

        }

        private void label13_Click(object sender, EventArgs e)
        {
            Process.Start("https://space.bilibili.com/271218438");

        }

        private void FansDataView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (FansDataView.SelectedRows.Count == 0) return;

            string selectedUid = FansDataView.SelectedRows[0].Cells[2].Value.ToString();
            Process.Start("https://space.bilibili.com/" + selectedUid);
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/SocialSisterYi/bilibili-API-collect");

        }
    }
}
