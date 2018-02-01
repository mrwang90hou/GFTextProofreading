using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 公告文本校对程序
{
    public partial class Form3 : Form
    {
        public static int linesNumber = 0;
        public static string extension_name = "";
        public Form3()
        {
            InitializeComponent();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            string[] filesPaths = Directory.GetFiles(Form1.totalPath2);
            List<string> extension_names_list = new List<string>();
            foreach (var item in filesPaths)
            {
                extension_names_list.Add(Path.GetExtension(item));
            }
            string[] extension_names = extension_names_list.ToArray();
            //MessageBox.Show(string.Join("\n", extension_names));
            extension_name = TongJi(extension_names);
            //MessageBox.Show(extension_name);
            switch (extension_name)
            {
                case ".tif":
                    comboBox1.SelectedIndex = 0;
                    break;
                case ".jpg":
                    comboBox1.SelectedIndex = 1;
                    break;
                case ".bmp":
                    comboBox1.SelectedIndex = 2;
                    break;
                default:
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(comboBox1.SelectedItem.ToString());
                extension_name = comboBox1.SelectedItem.ToString();
                comboBox2.Focus();
                int a = comboBox2.SelectedIndex;
                //MessageBox.Show(a.ToString());
                switch (a)
                {
                    case 0:
                        linesNumber = 2;
                        //MessageBox.Show("2");
                        break;
                    case 1:
                        linesNumber = 12;
                        break;
                    case 2:
                        linesNumber = 3;
                        break;
                    default:
                        break;
                }
                //linesNumber = int.Parse(textBox1.Text);
                if (linesNumber == 0 || extension_name == "")
                {
                    MessageBox.Show("请先进行准备工作！");
                    return;
                }
                string Fpath = Form1.totalPath2;
                //MessageBox.Show(Fpath);
                GetFilesName(Fpath, out string[] filesPaths, out string[] filesnames);
                if (filesPaths.Length ==0)
                {
                    MessageBox.Show("在资源路径下未找到扩展名为： "+ extension_name+"  的文件！", "图片扩展类型选择错误！");
                    return;
                }
                this.Close();
                MessageBox.Show("保存工作路径成功！");
            }
            catch (Exception)
            {
                MessageBox.Show("异常：请先进行准备工作！");
                return;
                //throw;
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//控制输入字符为数字
        {
            //if (!Char.IsNumber(e.KeyChar))
            //{
            //    e.Handled = true;
            //}
            //char Key_Char = e.KeyChar;//判斷按鍵的 Keychar 
            //MessageBox.Show(((int)(Key_Char)).ToString());//轉成整數顯示 
            //48 - 57代表数字0 - 9的范围
            // 允许输入:数字、退格键(8)、全选(1)、复制(3)、粘贴(22)
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 &&
            e.KeyChar != 1 && e.KeyChar != 3 && e.KeyChar != 22)
            {
                e.Handled = true;
            }
        }

        private void GetFilesName(string Fpath, out string[] filesPaths, out string[] filesnames) //获取文件名字（路径）的方法
        {

            string filesPath = Fpath;   //文件夹路径
                                        //打开当前路径
                                        //string filesPath = @"D:\Program Files";
                                        //System.Diagnostics.Process.Start("explorer.exe", filesPath);
                                        //限定当前路径可见文件类型为*.txt
                                        //string filepath = Server.mapPath("某文件夹名");string.Join("*",extension_name),
            string str = "*" + extension_name;
            filesPaths = Directory.GetFiles(filesPath, str, SearchOption.TopDirectoryOnly);//在搜索操作中包括当前目录及其所有的子目录
            //MessageBox.Show(string.Join("\n",filesPaths).ToString());
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



        private string TongJi(string[] str)
        {
            // 待统计的整型数组
            //int[] a = { 1, 1, 1, 3, 1, 2, 2, 1, 3, 4, 2, 1, 5, 3, 4 };

            // 集合 dic 用于存放统计结果
            Dictionary<string, ItemInfo> dic =
                new Dictionary<string, ItemInfo>();

            // 开始统计每个元素重复次数
            foreach (string v in str)
            {
                if (dic.ContainsKey(v))
                {
                    // 数组元素再次，出现次数增加 1
                    dic[v].RepeatNum += 1;
                }
                else
                {
                    // 数组元素首次出现，向集合中添加一个新项
                    // 注意 ItemInfo类构造函数中，已经将重复
                    // 次数设置为 1
                    dic.Add(v, new ItemInfo(v));
                }
            }
            List<int> num_list = new List<int>();
            List<string> name_str_list = new List<string>();
            foreach (ItemInfo info in dic.Values)
            {
                //MessageBox.Show("文件类型： "+ info.Value+ "\t出现的次数为  " + info.RepeatNum);
                num_list.Add(info.RepeatNum);
                name_str_list.Add(info.Value);
            }
            int[] num = num_list.ToArray();
            string[] name_str = name_str_list.ToArray();
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i]==num_list.Max())
                {
                    return name_str[i];
                }
            }
            return null;
        }

        
    }
    class ItemInfo
    {
        /// <summary>
        /// ItemInfo 类记录数组元素重复次数
        /// </summary>
        /// <param name="value">数组元素值</param>
        public ItemInfo(string value)
        {
            Value = value;
            RepeatNum = 1;
        }
        /// <summary>
        /// 数组元素的值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 数组元素重复的次数
        /// </summary>
        public int RepeatNum { get; set; }
    }
}