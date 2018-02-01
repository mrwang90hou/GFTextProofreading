using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace 公告文本校对程序
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {


            //listBox1.scrollalwayvisible = true;


            listBox1.Items.Clear();

            string Fpath=Form1.totalPath;
            string[] filesPaths = new string[] { };
            string[] filesnames = new string[] { };
            GetFilesName(Fpath,out filesPaths, out filesnames);

            //MessageBox.Show(string.Join("", filesnames[0]));
            //MessageBox.Show(string.Join("", Form1.save_history));

            //string[] result_string = new string[] { };
            List<string> result_string_list = new List<string>();
            string temp = "";
            
            for (int i = 0; i < filesnames.Length; i++)
            {
                temp= filesnames[i] +"\t"+ Form1.save_history[i].ToString();
                result_string_list.Add(temp);
                
                //listBox1.Items.Add(result_string[i].ToString());
            }
            string[] result_string = result_string_list.ToArray();

            //MessageBox.Show(string.Join("", result_string));
            int k = 0;
            foreach (var item in result_string)
            {
                listBox1.Items.Add(item);


                //Brush mybsh = Brushes.Black;
                
                //if (listBox1.Items[k].ToString().IndexOf("False") != -1)
                //{
                //    mybsh = Brushes.Red;
                //}
                k++;
            }
            
            listBox2.Items.Clear();

            int true_number = 0;
            int false_number = 0;
            for (int i = 0; i < Form1.save_history.Length; i++)
            {
                if (Form1.save_history[i]==true)
                {
                    true_number++;
                }
                else
                {
                    false_number++;
                }
            }

            listBox2.Items.Add("已完成个数：" + true_number.ToString());
            listBox2.Items.Add("未完成个数：" + false_number.ToString());
            listBox2.Items.Add("总计个数：" + (true_number+ false_number).ToString());

            //foreach (var item in Form1.save_history)
            //{
            //    listBox2.Items.Add(item.ToString());
            //}


            //MessageBox.Show("");
        }
        private void GetFilesName(string Fpath, out string[] filesPaths, out string[] filesnames) //获取文件名字（路径）的方法
        {
            
                string filesPath = Fpath;   //文件夹路径
                                            //打开当前路径
                                            //string filesPath = @"D:\Program Files";
                                            //System.Diagnostics.Process.Start("explorer.exe", filesPath);
                                            //限定当前路径可见文件类型为*.txt
                                            //string filepath = Server.mapPath("某文件夹名");
                filesPaths = Directory.GetFiles(filesPath, "*.txt", SearchOption.TopDirectoryOnly);//在搜索操作中包括当前目录及其所有的子目录
                
                //string[] temp = new string[filesPaths.Length];
                string[] new_temp = new string[filesPaths.Length];
                //int li_Index = 0; //变量声明
                for (int i = 0; i < filesPaths.Length; i++)
                {

                    new_temp[i] = Path.GetFileNameWithoutExtension(filesPaths[i]);
                    //temp[i] = filesPaths[i].Replace(filesPath, "");
                    //li_Index = temp[i].LastIndexOf("_");//获得_的位置
                    //new_temp[i] = temp[i].Substring(li_Index + 1, temp[i].Length - 1 - li_Index);//获得目标字符串
                    //new_temp[i] = new_temp[i].Replace(@".txt", "");
                }
                filesnames = new_temp;
        }
         
        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e) // ListBox DrawItem事件响应函数
        {
            if (e.Index >= 0)
            {
                e.DrawBackground();
                Brush mybsh = Brushes.Black;
                // 判断是什么类型的标签  
                //if (listBox1.Items[e.Index].ToString().IndexOf("false") != -1)
                //{
                //    mybsh = Brushes.Green;
                //}
                if (listBox1.Items[e.Index].ToString().IndexOf("False") != -1)
                {
                    mybsh = Brushes.Red;
                }
                // 焦点框  
                e.DrawFocusRectangle();
                //文本   
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, mybsh, e.Bounds, StringFormat.GenericDefault);
            }
        }
        
    }
}
