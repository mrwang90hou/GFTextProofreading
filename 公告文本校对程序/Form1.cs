using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;

namespace 公告文本校对程序
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int linesNumber;
        public static string extension_name;
        public static string date_number;//期号hj 
        public static string path = System.IO.Directory.GetCurrentDirectory();//获取当前应用程序所在路径
        public static string error_picture_path = Path.Combine(path, @"error_picture.jpg");//生成“错误图片提示”的路径
        public static string totalPath = "";
        public static string totalPath2 = "";
        public static string recordPageNumber = "";
        //获取页码的上下范围
        public static int[] pagesSize = new int[2];
        public static bool[] save_history;
        private void Form1_Load(object sender, EventArgs e)
        {
            recordPageNumber = LoadSettingContent.Default.记录页码;
            //pages_text.Text = LoadSettingContent.Default.记录页码.ToString();
            picture_path.Text = LoadSettingContent.Default.图片路径;
            txt_path.Text = LoadSettingContent.Default.文本路径;
            asc.ControllInitializeSize(this);
            asc2.ControllInitializeSize(this);
            //设置双缓冲解决控件发生改变闪烁问题  
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.    
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲   
            
            result_text.Focus();
            //uctrl_ImgPro0.Image = 
            pageNumber_label.Hide();
            label7.Hide();
            Saved_label.Hide();
            //picture_path.Text = @"H:\王宁\国方C#程序设计\公告文本校对程序\测试样本\许可备案\图片";
            //txt_path.Text = @"H:\王宁\国方C#程序设计\公告文本校对程序\测试样本\许可备案\文本";
            //totalPath = txt_path.Text;
            //pages_text.Text = @"147236";

            //prepare();
            
            this.MouseWheel += Form1_MouseWheel;//在窗体load事件里注册鼠标滚轮事件
            
        }


        Form3 f3 = new Form3();
        private void Save_workSpace_btn_Click(object sender, EventArgs e)
        {
            totalPath2 = picture_path.Text;
            //判断图片个数与文本个数是否相同
            GetFilesName(picture_path.Text, out string[] filesPaths1, out string[] filesnames1, out date_number);
            GetFilesName(txt_path.Text, out string[] filesPaths2, out string[] filesnames2, out date_number);
            
            if (filesnames1.Length!= filesnames2.Length)
            {
                MessageBox.Show("资源路径的文件个数不相同！","错误提示！");
                MessageBox.Show("图片资源个数：" + filesnames1.Length + "\n文本资源个数：" + filesnames2.Length, "文件个数不相同!");
                return;
            }
            f3.ShowDialog();
            linesNumber = Form3.linesNumber;
            extension_name = Form3.extension_name;

            prepare();
            //MessageBox.Show(linesNumber+ extension_name);
            pages_goto_btn.PerformClick();
            
        }
        private void prepare()
        {
            //获取页码的上下范围
            if (txt_path.Text == ""||picture_path.Text=="")
            {
                MessageBox.Show("请先选择资源路径", "提示信息！");
                picture_path.Focus();
                return;
            }
            GetFilesName(txt_path.Text, out string[] filesPaths, out string[] filesnames, out date_number);
            string[] folderName_string = filesnames;
            //folderName_string.Split('_');
            ////得到期号
            //MessageBox.Show(date_number);
            //string[] strArray = folderName_string[0].Split('_');
            //date_number = strArray[0];

            //date_number =;
            //string[] strArray = folderName_string.Split('_');
            //pagesSize;
            //MessageBox.Show(string.Join("\n", filesnames));
            List<int> filesNames_toInt_list = new List<int>();
            try
            {
                foreach (var item in filesnames)
                {
                    filesNames_toInt_list.Add(int.Parse(item));
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(e.Message);
                throw;
            }

            int[] filesNames_toInt = filesNames_toInt_list.ToArray();
            pagesSize[1] = filesNames_toInt.Max();
            pagesSize[0] = filesNames_toInt.Min();
            //MessageBox.Show(pagesSize[0].ToString()+ "     "+ pagesSize[1].ToString());
            //缓存记录的初始化
            List<bool> save_history_list = new List<bool> { };
            int i = 0;
            for (; i < pagesSize[1] - pagesSize[0] + 1; i++)
            {
                save_history_list.Add(false);
            }
            save_history = save_history_list.ToArray();
            //result_text.Text = string.Join("\t", save_history);
            //MessageBox.Show(i.ToString());

            bool b = false;
            try
            {
                if (int.Parse(recordPageNumber) < pagesSize[0] || int.Parse(recordPageNumber) > pagesSize[1])
                {
                    b = true;
                }

            }
            catch (Exception)
            {
                b = true;
            }
            
            if (recordPageNumber == null|| recordPageNumber == ""|| b==true)
            {
                //MessageBox.Show("null");
                pages_text.Text =pagesSize[0].ToString();
            }
            else
            {
                //MessageBox.Show(recordPageNumber);
                pages_text.Text = recordPageNumber;
            }
            pageNumber_label.Text ="页码范围:"+ pagesSize[0]+ "—>"+pagesSize[1];
            pageNumber_label.Show();
        }
       
        #region 图片放大缩小显示

        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) //放大图片
            {
                pictureBox1.Size = new Size(pictureBox1.Width + 50, pictureBox1.Height + 50);
            }
            else
            {  //缩小图片
                pictureBox1.Size = new Size(pictureBox1.Width - 50, pictureBox1.Height - 50);
            }
            //设置图片在窗体居中
            //pictureBox1.Location = new Point((this.Width - pictureBox1.Width) / 2, (this.Height - pictureBox1.Height) / 2);
        }
        //等比例缩放图片 
        private Bitmap ZoomImage(Bitmap bitmap, int destHeight, int destWidth)
        {
            try
            {
                System.Drawing.Image sourImage = bitmap;
                int width = 0, height = 0;
                //按比例缩放             
                int sourWidth = sourImage.Width;
                int sourHeight = sourImage.Height;
                if (sourHeight > destHeight || sourWidth > destWidth)
                {
                    if ((sourWidth * destHeight) > (sourHeight * destWidth))
                    {
                        width = destWidth;
                        height = (destWidth * sourHeight) / sourWidth;
                    }
                    else
                    {
                        height = destHeight;
                        width = (sourWidth * destHeight) / sourHeight;
                    }
                }
                else
                {
                    width = sourWidth;
                    height = sourHeight;
                }
                Bitmap destBitmap = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);
                //设置画布的描绘质量           
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourImage, new Rectangle((destWidth - width) / 2, (destHeight - height) / 2, width, height), 0, 0, sourImage.Width, sourImage.Height, GraphicsUnit.Pixel);
                g.Dispose();
                //设置压缩质量       
                System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                sourImage.Dispose();
                return destBitmap;
            }
            catch
            {
                return bitmap;
            }
        }
        #endregion
        #region 选择文件夹路径
        /// <summary>
        /// 选择即将需要所处的文件夹路径
        /// </summary>
        private void select_picturePath_btn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            //folderBrowserDialog1.ShowDialog() == DialogResult.OK;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dialog.Description = "请选择图片路径";
                //folderBrowserDialog1.SelectedPath = @"e:\";
                string foldPath = dialog.SelectedPath;
                //弹出提示框，显示“已选择文件夹。。。。”
                MessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //选择路径显示在文本框中
                picture_path.Text = foldPath;
            }
        }
        private void select_txtPath_btn_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            //folderBrowserDialog1.ShowDialog() == DialogResult.OK;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderBrowserDialog1.Description = "请选择txt文件路径";
                string foldPath = folderBrowserDialog1.SelectedPath;
                //弹出提示框，显示“已选择文件夹。。。。”
                MessageBox.Show("已选择文件夹:" + foldPath, "选择文件夹提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //选择路径显示在文本框中
                txt_path.Text = foldPath;
            }
        }
        #endregion
        #region 文本结果的放大缩小显示
        //设置result_text字体大小
        int text_font = 15;
        private void font_to_small_btn_Click(object sender, EventArgs e)
        {

            if (text_font <= 0)
            {
                return;
            }
            result_text.Font = new Font(result_text.Font.FontFamily, text_font, result_text.Font.Style);
            
            text_font = text_font - 5;
            //result_text.Font = int32.Parse(textbox_FontSize);
        }
        private void font_to_big_btn_Click(object sender, EventArgs e)
        {
            text_font = text_font + 5;
            result_text.Font = new Font(result_text.Font.FontFamily, text_font, result_text.Font.Style);
        }
        #endregion
        
        System.Drawing.Bitmap GetResourceBitmap(string strImageName)
        {
            object obj = Properties.Resources.ResourceManager.GetObject(strImageName, Properties.Resources.Culture);
            return ((System.Drawing.Bitmap)(obj));
        }
        private System.Drawing.Image GetResourceImage(string strImageName)
        {
            object obj = Properties.Resources.ResourceManager.GetObject(strImageName, Properties.Resources.Culture);
            return ((System.Drawing.Image)(obj));
        }

        #region 跳转页面的按钮事件
        
        private void pages_goto_btn_Click(object sender, EventArgs e)
        {
            //prepare();
            if (picture_path.Text==""|| txt_path.Text=="")
            {
                MessageBox.Show("请先选择资源路径","提示信息！");
                picture_path.Focus();
                return;
            }
            //图片资源路径
            string picture_resoure_path = picture_path.Text;
            //文本资源路径
            string txtFile_resoure_path = txt_path.Text;
            if (pages_text.Text=="")
            {
                MessageBox.Show("请先输入需要跳转到的页码", "提示信息！");
                picture_path.Focus();
                return;
            }
            //判断
            PageJudge();
            //pictureBox1.Image = GetResourceImage("1574_147237");//不需要带后缀名
            //pictureBox1.Image = Image.FormFile(@"图片路径");

        }
        private void frist_page_btn_Click(object sender, EventArgs e)
        {
            pages_text.Text = pagesSize[0].ToString();
            pages_goto_btn.PerformClick();
        }

        private void last_page_btn_Click(object sender, EventArgs e)
        {
            pages_text.Text = pagesSize[1].ToString();
            pages_goto_btn.PerformClick();
        }

        #endregion

        #region 翻页的按钮事件
        private void previous_page_btn_Click(object sender, EventArgs e)//上一页
        {
            if (pages_text.Text=="")
            {
                MessageBox.Show("请先输入需要跳转到的页码", "提示信息！");
                picture_path.Focus();
                return;
            }
            string page_number = pages_text.Text;
            int page = int.Parse(page_number);
            page--;
            pages_text.Text = page.ToString();
            PageJudge();
            //bool b1 = LoadPicture(page.ToString());
            //bool b2 = LoadTxtFile(page.ToString());
            //pages_goto_btn.PerformClick();
            //if (b1|| b2)
            //{
            //    pages_text.Text = page.ToString();
            //}
        }
        private void next_page_btn_Click(object sender, EventArgs e)//下一页
        {

            if (pages_text.Text == "")
            {
                MessageBox.Show("请先输入需要跳转到的页码", "提示信息！");
                picture_path.Focus();
                return;
            }
            confirm_to_btn.PerformClick();
            
            /*取消下一页按钮
            string page_number = pages_text.Text;
            int page = int.Parse(page_number);
            page++;
            pages_text.Text = page.ToString();
            PageJudge();

            */
            
            //bool b1 = LoadPicture(page.ToString());
            //bool b2 = LoadTxtFile(page.ToString());
            //pages_goto_btn.PerformClick();
            //if (b1 || b2)
            //{
            //    pages_text.Text = page.ToString();
            //}
        }
        private void PageJudge()//"超出校对页码范围"的判断
        {
            string page_number = pages_text.Text;
            int number = int.Parse(page_number);
            if (number < pagesSize[0])
            {
                MessageBox.Show("超出校对页码范围！","警告");
                LoadPicture(page_number);
                LoadTxtFile(page_number);
                pages_text.Text = (++number).ToString();
                //pages_goto_btn.PerformClick();
            }
            if (number > pagesSize[1])
            {
                MessageBox.Show("超出校对页码范围！", "警告");
                LoadPicture(page_number);
                LoadTxtFile(page_number);
                pages_text.Text = (--number).ToString();
                //pages_goto_btn.PerformClick();
            }
            LoadPicture(page_number);
            LoadTxtFile(page_number);
        }
        #endregion
        #region pages_text更改刷新事件
        
        private void pages_text_TextChanged(object sender, EventArgs e)//实现更换页码的自动刷新
        {
            //if (picture_path.Text == "" || txt_path.Text == "")
            //{
            //    MessageBox.Show("请先选择资源路径", "提示信息！");
            //    picture_path.Focus();
            //    return;
            //}
            ////设置输入的页面在范围内
            //int num = int.Parse(pages_text.Text);
            ////MessageBox.Show(num.ToString());
            //string str = pages_text.Text;
            //if (num < pagesSize[0] || num > pagesSize[1])
            //{
            //    MessageBox.Show("超出校对页码范围！");
            //    pages_text.Text = str.Substring(0,str.Length - 1);
            //    pages_text.Focus();
            //    pages_text.SelectionStart = pages_text.Text.Length;
            //    return;
            //}
            ////图片资源路径
            //string picture_resoure_path = picture_path.Text;
            ////文本资源路径
            //string txtFile_resoure_path = txt_path.Text;
            
            ////当前页面数
            //string page_number = pages_text.Text;
            //LoadPicture(page_number);
            //LoadTxtFile(page_number);
            ////pictureBox1.Image = GetResourceImage("1574_147237");//不需要带后缀名
            ////pictureBox1.Image = Image.FormFile(@"图片路径");
        }
        private void pages_text_KeyPress(object sender, KeyPressEventArgs e)//控制输入字符为数字
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
        //pages_text按键盘事件
        private void pages_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pages_goto_btn.PerformClick();
            }
        }
        #endregion
        //保存按钮
        private void confirm_to_btn_Click(object sender, EventArgs e)
        {
            //为确保不会出现存储交叉，设计Save_to_txtFile()方法，传参：picture_name
            //MessageBox.Show(pictureBox1.ImageLocation);
            //string picture_name = Path.GetFileNameWithoutExtension(pictureBox1.ImageLocation);
            string picture_name = date_number.ToString()+"_"+pages_text.Text;
            //MessageBox.Show(picture_name);
            if (pages_text.Text==""|| result_text.Text=="")
            {
                return;
            }
            //验证文本内容是否异常的方法
            bool bl = Validate_textContent_error();
            if (bl == false)
            {
                MessageBox.Show("请先修改文本！");
                return;
            }
            Save_to_txtFile(picture_name);
            //Thread.Sleep(1000);暂停一面后执行
            int pagesNum = int.Parse(pages_text.Text);
            pagesNum++;
            pages_text.Text = pagesNum.ToString();
            pages_goto_btn.PerformClick();
        }
        
        private void result_text_TextChanged(object sender, EventArgs e)//result_text更改事件
        {
            //Saved_label.Hide();
            //Saved_label.Text = "已保存！";
            //Save_to_txtFile();
            //Saved_label.Text = "";
        }

        #region 快捷键事件
        private void result_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)//保存
            {
                e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
                confirm_to_btn.PerformClick();
            }
            if (e.KeyCode == Keys.PageDown)//往下翻页
            {
                e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
                next_page_btn.PerformClick();
            }
            if (e.KeyCode == Keys.PageUp)//往上翻页
            {
                e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
                previous_page_btn.PerformClick();
            }
            if (e.KeyCode == Keys.S && e.Control)//刷新修改后的txt并保存
            {
                e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
                correct_btn.PerformClick();
                //保存操作
                string picture_name = date_number.ToString() + "_" + pages_text.Text;
                //验证文本内容是否异常的方法
                bool bl = Validate_textContent_error();
                if (bl == false)
                {
                    MessageBox.Show("请先修改文本！");
                    return;
                }
                Save_to_txtFile(picture_name);
                //刷新操作
                pages_goto_btn.PerformClick();
                
                //confirm_to_btn.PerformClick();
                //previous_page_btn.PerformClick();
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //    if (e.KeyCode == Keys.PageDown && e.Control)
            //    {
            //        e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
            //        next_page_btn.PerformClick();
            //    }
            //if (e.KeyCode == Keys.PageUp)
            //{
            //    e.Handled = true; //将Handled设置为true，指示已经处理过KeyPress事件  
            //    previous_page_btn.PerformClick();
            //}
            //    if ((e.KeyCode == Keys.A) && e.Control)
            //        confirm_to_btn.PerformClick();
        }

        #endregion

        #region 加载（图片和文本）到显示窗体的主要方法
        
    
        //加载（图片和文本）到显示窗体的主要方法
        private void LoadPicture(string page_number)//加载图片的方法
        {
            try
            {
                string fileName = date_number+"_" + page_number + extension_name;
                string endpath = Path.Combine(picture_path.Text, fileName);
                //pictureBox1.ImageLocation = endpath;
                Bitmap bmpTmp = (Bitmap)Bitmap.FromFile(endpath);
                //uctrl_ImgPro0.Image = bmpTmp;
                uctrl_ImgPro0.Image = (Bitmap)Image.FromFile(endpath);
                uctrl_ImgPro0.ZoomToOriImgSize();//按照图像原始大小缩、放显示


                //uctrl_ImgPro0.Image = new Bitmap(endpath, 2, 2);
                //Bitmap bm = new Bitmap(endpath);
                //缩放：
                //Bitmap bm1 = new Bitmap(bm, 2, 2);
                

                pictureBox1.Image = Image.FromFile(endpath);

                //pictureBox1.Image = Image.FromFile(endpath);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message,"错误！");
                //MessageBox.Show(error_picture_path);
                uctrl_ImgPro0.Image = (Bitmap)Bitmap.FromFile(error_picture_path);
                pictureBox1.Image = Image.FromFile(error_picture_path);

                return;
                throw;
            }
        }
        //private void Bnt_ZoomToImgSize_Click(object sender, EventArgs e)
        //{
        //    uctrl_ImgPro0.ZoomToOriImgSize();
        //}
        private void LoadTxtFile(string page_number)//加载文本的方法
        {
            try
            {
                result_text.Text = "";
                string fileName = date_number+"_" + page_number + ".txt";
                string endpath = Path.Combine(txt_path.Text, fileName);
                string[] txt_content = Read_txtFile_content(endpath);
                result_text.ForeColor = System.Drawing.Color.Black;
                //第一种方法
                //foreach (var item in txt_content)
                //{
                //    result_text.AppendText(item + "\r\n");
                //}
                //第二种方法
                result_text.Text = string.Join("\r\n", txt_content);
                //pictureBox1.ImageLocation = totalpath;
                //pictureBox1.Image = Image.FromFile(totalpath);

                //验证文本内容是否异常的方法
                Validate_textContent_error();
                //载入缓存记录
                LoadHistoryMethod();
                
            }
            catch (Exception)
            {
                result_text.Text = "错误！";
                return;
                throw;
            }
        }
        #endregion

        
        #region 文本读取与存储的方法
        public static string[] Read_txtFile_content(string file)//获取文件内容存入数组的方法
        {
            //var file = File.Open("1574_159296.txt", FileMode.Open);
            try
            {
                int num = 0;//记录该文本的行数
                List<string> txt = new List<string>();
                using (var stream = new StreamReader(file))
                {
                    while (!stream.EndOfStream)  //判断是否为文本的结尾
                    {
                        txt.Add(stream.ReadLine());     //读取该行字符，并返回值
                        num++;
                    }
                    //MessageBox.Show(num.ToString());//判断该文本的行数
                }
                string[] string_array = new string[num];
                string line_txt = string.Empty;         //记录每一行的内容
                var line = 0;  //设置行数
                               //使用GB2312中文字符集
                               // StreamReader reader = new StreamReader(txtUrl, Encoding.GetEncoding("gb2312"));
                               //或使用默认编码格式
                               //StreamReader sR = new StreamReader(filePath, System.Text.Encoding.Default)
                using (StreamReader reader = new StreamReader(file,Encoding.Default))
                {
                    line_txt = reader.ReadLine();
                    //while (line_txt != "" && line_txt != null)
                    //var stream = new StreamReader(file);
                    //while (!stream.EndOfStream) 
                    for (int i = 0; i < num; i++)//解决读取TXT文本中，为null值的问题
                    {
                        string_array[line] = line_txt;
                        //Console.WriteLine(string_array[line]);
                        txt.Add(line_txt);     //读取下一行
                        line_txt = reader.ReadLine();
                        line++;
                    }
                }
                //Console.WriteLine(string_array.Length);
                return string_array;
            }
            catch (Exception)
            {
                //MessageBox.Show(file + "\n该文本内容存在错误排版！！！请认真核对", "错误警告！！！");
                throw;
            }
        }
        
        public void Save_to_txtFile(string fileName)//文本存储方法
        {
            //文本资源路径
            string txtFile_resoure_path = txt_path.Text;
            //当前页面数
            //string page_number = pages_text.Text;
            try
            {
                fileName = fileName + ".txt";
                string endpath = Path.Combine(txt_path.Text, fileName);
                //MessageBox.Show(endpath);
                if (!File.Exists(endpath))//判断日志文件路径是否存在
                {
                    File.Create(endpath).Close();
                }
                
                //MessageBox.Show(string.Join("\n",result_text.Lines), result_text.Lines.Length.ToString());
                List < string > lis = new List<string>();
                foreach (var item in result_text.Lines)
                {
                    lis.Add(item);
                    lis.Remove("\n");
                }
                string[] new_txt_content = lis.ToArray();
                //MessageBox.Show(string.Join("\n", new_txt_content));
                File.WriteAllLines(endpath, new_txt_content, Encoding.Default);
                //Write(endpath, new_txt_content);
                 
                ////保存的缓存记录
                SaveHistoryMethod();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //MessageBox.Show(error_picture_path);
                //pictureBox1.Image = Image.FromFile(error_picture_path);
                
                throw;
            }
        }
        public static void Write(string path, string[] name)//写入文件
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            foreach (var item in name)
            {
                sw.Write(item,Encoding.Default);
            }
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
        public void ClearTxt(String txtPath)//清空文件内容
        {
            String appDir = txtPath;
            FileStream stream = File.Open(appDir, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            stream.Close();
        }
        public void SaveFile(string str, String txtPath, bool saOrAp)
        {

            String appDir = txtPath;
            StreamWriter sw = new StreamWriter(appDir, saOrAp);//saOrAp表示覆盖或者是追加  
            sw.WriteLine(str);
            sw.Close();
        }

        private string[] GetFoldersName() //获取子文件夹名称的方法
        {
            string root_path = txt_path.Text;//根目录路径
            DirectoryInfo theFolder = new DirectoryInfo(root_path);
            DirectoryInfo[] folders_name = theFolder.GetDirectories();//返回当前目录的子目录
            int num = 0;
            //string[] folders_names = new string[folders_name.Length()];
            foreach (DirectoryInfo NextFolder in folders_name)//子文件夹名称
            {
                //listBox1.Items.Add(NextFolder.Name);
                //folders_name[] = 
                num++;
            }
            string[] folders_names = new string[num];
            for (int i = 0; i < num; i++)
            {
                folders_names[i] = folders_name[i].Name;
                //Console.WriteLine(folders_names[i]);
            }
            return folders_names;
        }
        private void GetFilesName(string Fpath, out string[] filesPaths, out string[] filesnames, out string date_number) //获取文件名字（路径）的方法
        {
            
                string filesPath = Fpath;   //文件夹路径
                                            //打开当前路径
                                            //string filesPath = @"D:\Program Files";
                                            //System.Diagnostics.Process.Start("explorer.exe", filesPath);
                                            //限定当前路径可见文件类型为*.txt
                                            //string filepath = Server.mapPath("某文件夹名");
                filesPaths = Directory.GetFiles(filesPath, "*", SearchOption.TopDirectoryOnly);//在搜索操作中包括当前目录及其所有的子目录
                string[] temp = new string[filesPaths.Length];
                string[] new_temp = new string[filesPaths.Length];
                int li_Index = 0; //变量声明
            int sum = 0;
                for (int i = 0; i < filesPaths.Length; i++)
                {
                    temp[i] = filesPaths[i].Replace(filesPath, "");
                    li_Index = temp[i].LastIndexOf("_");//获得_的位置
                    new_temp[i] = temp[i].Substring(li_Index + 1, temp[i].Length - 1 - li_Index);//获得目标字符串
                    new_temp[i] = new_temp[i].Replace(@".txt", "");
                sum++;
                }
            //MessageBox.Show(filesPaths.Length.ToString(), "filesPaths.Length");
            //MessageBox.Show(filesPaths[sum-1], "sum");
            //MessageBox.Show(sum.ToString(), "sum");
            filesnames = new_temp;
            string one_filesName = Path.GetFileNameWithoutExtension(filesPaths[0].ToString());
            string[] strArray = one_filesName.Split('_');
            date_number = strArray[0];
        }
        
        #endregion

        private void correct_btn_Click(object sender, EventArgs e)
        {
            if (Validate_textContent_error())
            {
                return;
            }
            //if (!error_content_string.Contains(result_text.Lines[0]))
            //{
            //    return;
            //}
            List<string> lis = new List<string>();
            foreach (var item in result_text.Lines)
            {
                lis.Add(item);
                lis.Remove("\n");
            }

            //错误文件类别列表
            //string[] error_content_string = { "注册号异常!", "类别号异常!", "转让人、受让人异常!" };
            if (result_text.Lines[0].IndexOf('!') > -1 || result_text.Lines[0].IndexOf('！') > -1)
            {
                lis.Remove(result_text.Lines[0]);//移除第一行
            }

            string[] new_txt_content = lis.ToArray();
            //result_text.Clear();
            //result_text.Text = string.Join("\r\n", new_txt_content);

            //【增添】
            List<string> lis2 = new List<string>();
            List<string> lis3 = new List<string>();
            List<string> lis4 = new List<string>();
            try
            {
                switch (linesNumber)
                {
                    /*
                     * 公告校对行规则提示！
                    《变更公告》行数：2
                    《许可备案》行数：12
                    《转让公告》行数：3
                     */
                    case 2://《变更公告》自动删除
                        List<int> record_num_list = new List<int>();
                        for (int i = 0; i < new_txt_content.Length; i++)
                        {
                            if (result_text.Lines[i].IndexOf('：') > -1)
                            {
                                record_num_list.Add(i);
                                //MessageBox.Show(record_num.ToString());
                            }
                        }
                        int[] record_num = record_num_list.ToArray();
                        foreach (var item in new_txt_content)
                        {
                            string tp;
                            if (item.IndexOf('：') > -1)
                            {
                                string[] temp = item.Split('：');
                                tp = temp[1];
                                //MessageBox.Show(tp);
                            }
                            else
                            {
                                tp = item;
                            }
                            lis2.Add(tp);
                        }
                        string[] str2 = lis2.ToArray();
                        //MessageBox.Show(string.Join("\n", str2));
                        for (int i = 1; i < record_num[0]; i++)
                        {
                            lis2.Remove(str2[i]);
                        }
                        string[] str3 = lis2.ToArray();
                        lis3.Add(str3[0] + str3[1]);
                        for (int i = 2; i < str3.Length; i++)
                        {
                            lis3.Add(str3[i]);
                        }
                        //for (int i = 0;i < str3.Length;i++)
                        //{
                        //    lis4.Add();
                        //}
                        //for (int i = 1; i < record_num + 1; i++)
                        //{
                        //    MessageBox.Show(i.ToString()+ "remove!!!");
                        //    lis2.Remove(lis2[i].ToString());
                        //}
                        break;
                    case 3://《转让公告》
                        List<int> record_markCode_space_list = new List<int>();
                        List<int> record_styleNumber_space_list = new List<int>();
                        for (int i = 0; i < new_txt_content.Length; i++)
                        {
                            if (MarkCode_check(new_txt_content[i]))
                            {
                                record_markCode_space_list.Add(i);
                                //MessageBox.Show((i + 1).ToString());
                            }
                            if (StyleNumber_check(new_txt_content[i]))
                            {
                                record_styleNumber_space_list.Add(i);
                            }
                        }
                        int[] record_markCode_space = record_markCode_space_list.ToArray();
                        int[] record_styleNumber_space = record_styleNumber_space_list.ToArray();
                        lis2 = new_txt_content.ToList();
                        for (int i = 0; i < record_styleNumber_space.Length; i++)
                        {
                            lis2.Remove(new_txt_content[record_styleNumber_space[i]]);
                        }
                        for (int i = 0; i < record_markCode_space.Length; i++)
                        {
                            lis2.Remove(new_txt_content[record_markCode_space[i] + 1]);
                        }
                        lis3 = lis2;
                        break;
                    case 12://《许可备案》

                        lis2.Add("/");
                        lis2.Add(new_txt_content[0]);
                        string end_str = new_txt_content[new_txt_content.Length - 1];
                        lis2.Add("/");
                        lis2.Add(end_str);
                        for (int i = 1; i < new_txt_content.Length - 1; i++)
                        {
                            lis2.Add("/");
                            lis2.Add(new_txt_content[i].Replace("年", "-").Replace("月", "-").Replace("日", "").Replace("曰", "").Replace(" ", ""));
                        }
                        lis2.Add("/");
                        //lis2 = new_txt_content.ToList();
                        lis3 = lis2;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
            
            string[] new_txt_content_end = lis3.ToArray();
            result_text.Clear();
            result_text.Text = string.Join("\r\n", new_txt_content_end);
        }
        #region 判断和加载：是否保存至TXT文件的缓存方法
        private void SaveHistoryMethod()//保存缓存的方法
        {
            try
            {
                int page_number = int.Parse(pages_text.Text);
                int temp = page_number - pagesSize[0];
                //MessageBox.Show(temp.ToString());
                save_history[temp] = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }
        private void LoadHistoryMethod()//加载缓存的方法
        {
            Saved_label.Show();
            int page_number = int.Parse(pages_text.Text);
            int temp = page_number - pagesSize[0];
            if (save_history[temp] == true)
            {

                Saved_label.ForeColor = System.Drawing.Color.Red;
                Saved_label.Text = "已修改保存！";
            }
            else
            {
                Saved_label.ForeColor = System.Drawing.Color.BlueViolet;
                Saved_label.Text = "未修改保存！";
            }
        }
        #endregion

        #region 判断文本文件result_txt窗口是否存在错误的方法
        
        private bool Validate_textContent_error()//判断文本文件result_txt窗口是否存在错误！
        {
            //错误文件类别列表
            string[] error_content_string = { "注册号异常!", "类别号异常!", "转让人、受让人异常!" };
            //验证文本内容是否异常的方法
            if (result_text.Lines[0].IndexOf('!') > -1 || result_text.Lines[0].IndexOf('！') > -1 || result_text.Lines.Length != linesNumber)
            {
                result_text.ForeColor = System.Drawing.Color.Red;
                label7.Show();
                label7.Text = "文本格式存在错误！";
                return false;
            }
            else
            {
                //MessageBox.Show(linesNumber.ToString());
                switch (linesNumber)
                {
                    /*
                     * 公告校对行规则提示！
                    《变更公告》行数：2
                    《转让公告》行数：3
                    《许可备案》行数：12
                     */
                    case 2://《变更公告》
                        //string str = GetMarkCode(result_text.Lines[0]);
                        if (!GetMarkCode(result_text.Lines[0]))//获取并判断注册号的方法
                        {
                            result_text.ForeColor = System.Drawing.Color.Red;
                            label7.Show();
                            label7.Text = "注册号异常！";
                            return false;
                        }
                        foreach (var item in result_text.Lines)//判断是否存在特殊字符
                        {
                            if (Have_special_char(item))
                            {
                                result_text.ForeColor = System.Drawing.Color.Red;
                                label7.Show();
                                label7.Text = "特殊字符异常！";
                                MessageBox.Show(item);
                                return false;
                            }
                        }
                        break;
                    case 3://《转让公告》
                        //MessageBox.Show(GetMarkCode(result_text.Lines[0]).ToString());
                        if (!GetMarkCode(result_text.Lines[0]) || !GetMarkCode(result_text.Lines[1]) || !GetMarkCode(result_text.Lines[2]))//获取并判断注册号的方法
                        {
                            result_text.ForeColor = System.Drawing.Color.Red;
                            label7.Show();
                            label7.Text = "注册号异常！";
                            //MessageBox.Show(GetMarkCode(result_text.Lines[0]).ToString());
                            return false;
                        }
                        foreach (var item in result_text.Lines)//判断是否存在特殊字符
                        {
                            if (Have_special_char(item))
                            {
                                result_text.ForeColor = System.Drawing.Color.Red;
                                label7.Show();
                                label7.Text = "特殊字符异常！";
                                MessageBox.Show(item);
                                return false;
                            }
                        }
                        break;
                    case 12://《许可备案》
                        if (result_text.Lines[0] != "/" || result_text.Lines[2] != "/" || result_text.Lines[4] != "/" || result_text.Lines[7] != "/" || result_text.Lines[9] != "/" || result_text.Lines[11] != "/" )
                        {
                            result_text.ForeColor = System.Drawing.Color.Red;
                            label7.Show();
                            label7.Text = "缺少分隔符：‘ / ’";
                            return false;
                        }
                        if (!GetMarkCode(result_text.Lines[1]))
                        {
                            result_text.ForeColor = System.Drawing.Color.Red;
                            label7.Show();
                            label7.Text = "注册号异常！";
                            return false;
                        }
                        if (!HaveEightNumber(result_text.Lines[5].Replace("-", "").Replace("至", "").Replace(" ", "")) || !HaveEightNumber(result_text.Lines[6].Replace("-", "").Replace(" ", "")))
                        {
                            //MessageBox.Show((result_text.Lines[5].Replace("-", "")).ToString());
                            result_text.ForeColor = System.Drawing.Color.Red;
                            label7.Show();
                            label7.Text = "日期异常！";
                            return false;
                        }
                        foreach (var item in result_text.Lines)//判断是否存在特殊字符
                        {
                            if (Have_special_char(item))
                            {
                                result_text.ForeColor = System.Drawing.Color.Red;
                                label7.Show();
                                label7.Text = "特殊字符异常！";
                                MessageBox.Show(item);
                                return false;
                            }
                        }
                        break;
                    default:
                        break;
                }
                label7.Hide();
                result_text.ForeColor = System.Drawing.Color.Black;
                return true;
            }
           
            //if (error_content_string.Contains(result_text.Lines[0])||result_text.Lines.Length!=linesNumber)//判断是否出现异常！
            //{
            //    result_text.ForeColor = System.Drawing.Color.Red;
            //    label7.Text = "文本格式存在错误！";
            //    return false;
            //}
            
        }

        #endregion

        #region 打开当前文件夹/当前文件所在文件夹

        private void open_picture_folder_btn_Click(object sender, EventArgs e)
        {
            string path = picture_path.Text;
            System.Diagnostics.Process.Start(path);
        }
        private void open_txtFiles_folder_btn_Click(object sender, EventArgs e)
        {
            string path = txt_path.Text;
            System.Diagnostics.Process.Start(path);
        }
        private void open_thisFileFolder_btn_Click(object sender, EventArgs e)
        {
            string filepath = txt_path.Text;
            string pages = pages_text.Text;
            string filename = "1574_" + pages+".txt";
            string filepathname = Path.Combine(filepath,filename);
            
            OpenFolderAndSelectFile(filepathname);
        }
        private void OpenFolderAndSelectFile(string fileFullName)//打开某文件所在文件夹，并在文件夹内选择该文件
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + fileFullName;
            System.Diagnostics.Process.Start(psi);
        }

        #endregion

        #region 新From窗口弹出！！！

        //新的弹出窗口
        Form2 f2 = new Form2();
        private void work_record_btn_Click(object sender, EventArgs e)
        {
            totalPath = txt_path.Text;
            Form newForm = new Form();
            f2.ShowDialog();
        }

        #endregion
        
        #region 窗体自适应效果
        //方案一
        AutoSizeFormClass asc = new AutoSizeFormClass();
        private void MainPage_Load(object sender, EventArgs e)
        {
            asc.ControllInitializeSize(this);

        }
        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            asc.ControlAutoSize(this);
        }

        //方案二
        AutoSizeFormClass2 asc2 = new AutoSizeFormClass2();
        //private bool width;

        /// <summary>  
        /// RegisterFrm大小改变时触发  
        /// </summary>  
        private void RegisterFrm_SizeChanged(object sender, EventArgs e)
        {
            //调用类的自适应方法，完成自适应  
            asc2.ControlAutoSize(this);
        }
        #endregion
        
        //PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    if (pictureBox1.Width < 1000)
            //    {
            //        pictureBox1.Width = Convert.ToInt32(pictureBox1.Width * 1.2);
            //        pictureBox1.Height = Convert.ToInt32(pictureBox1.Height * 1.2);
            //    }
            //}
            //if (e.Button == MouseButtons.Right)
            //{
            //    if (pictureBox1.Width > 600)
            //    {
            //        pictureBox1.Width = Convert.ToInt32(pictureBox1.Width / 1.2);
            //        pictureBox1.Height = Convert.ToInt32(pictureBox1.Height / 1.2);
            //    }
            //}
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadSettingContent.Default.图片路径 = picture_path.Text;
            LoadSettingContent.Default.文本路径 = txt_path.Text;
            LoadSettingContent.Default.记录页码 = pages_text.Text;
            LoadSettingContent.Default.Save();
        }
        
        private bool GetMarkCode(string str)//判断注册号
        {
            char[] c = str.ToArray();
            List<char> new_str_list = new List<char>();
            string number = null;
            foreach (char item in str)
            {
                //if (item >= 48 && item <= 58)
                if (item >= 0x4e00 && item <= 0x9fbb)//判断是否为中文    或者     存在连续英文
                {
                    break;
                }
                else
                {
                    number += item;
                }
            }
            //foreach (var item in c)
            //{
            //    if (item >= 0x4e00 && item <= 0x9fbb)//判断是否为中文    或者     存在连续英文
            //    {
            //        break;
            //    }
            //    new_str_list.Add(item);
            //}
            //char[] new_char = new_str_list.ToArray();
            ////方法一
            //string str = string.Join("", tempChar);
            ////方法二
            //string str = string.Concat<char>(tempChar);
            //方法三
            //string markcode = new string(new_char);
            string markcode = number;

            //MessageBox.Show(string.Join("", markcode));

            if (!MarkCode_check(markcode))
            {
                //MessageBox.Show(MarkCode_check(str).ToString());
                return false;
            }
            return true;
        }
        private bool MarkCode_check(string one_string)      //判断字符串中是否存在特殊字符
        {
            //注册号
            /*   (1)字符数大于等于4
                   (2)首字符只能为1-9 或 G 或 T
                   (3)中间字符只能为0-9
                     (4)尾字符只能为0-9或A-Z
             */
            //var IsTrueORfalse = false;
            //string AllNumIsSame = "~!@#$%^&*()_+`-=[]\\;'/{}|:\"<>?·~！@#￥%……&*（）——+—-=【】、；‘。、{}|：“《》？";
            if (one_string.Length <= 4)
            {
                return false;
            }
            char frist_char = one_string[0];
            string mindle_string;
            char end_char = one_string[one_string.Length - 1];
            bool char_number_pd = false, frist_char_pd = false, mindle_pd = false, end_char_pd = false;

            char[] string_to_char = one_string.ToCharArray();
            string one_string1 = one_string.Substring(0, one_string.Length - 1);//去掉最后一个字符
            //MessageBox.Show(one_string1);
            string one_string2 = one_string1.Substring(1, one_string1.Length - 1);//去掉第一个字
            //MessageBox.Show(one_string2);
            mindle_string = one_string2;
            //(1)字符数大于等于4
            if (string_to_char.Length > 4)
            {
                char_number_pd = true;
            }
            //(2)首字符只能为1 - 9 或 G 或 T
            string pattern1;
            pattern1 = @"1|2|3|4|5|6|7|8|9|G|T";
            Regex r1 = new Regex(pattern1); //正则表达式 表示数字的范围 ^符号是开始，9$是关闭
            frist_char_pd = r1.IsMatch(frist_char.ToString());
            //(3)中间字符只能为0-9
            //MessageBox.Show("mindle_string\t" + mindle_string);
            mindle_pd = validateNum(mindle_string);
            //(4)尾字符只能为0 - 9或A - Z
            //MessageBox.Show("end_char\t" + end_char.ToString());
            end_char_pd = IsNumAndEnCh(end_char.ToString());
            //MessageBox.Show("char_number_pd" + char_number_pd.ToString());
            //MessageBox.Show("frist_char_pd" + frist_char_pd.ToString());
            //MessageBox.Show("mindle_pd" + mindle_pd.ToString());
            //MessageBox.Show("end_char_pd" + end_char_pd.ToString());
            if (char_number_pd && frist_char_pd && mindle_pd && end_char_pd)
            {
                //MessageBox.Show("true!!!");
                return true;
            }
            return false;
            
        }
        #region 验证文本框输入为整数
        /// <summary>
        /// 验证文本框输入为整数
        /// </summary>
        /// <param name="strNum">输入字符</param>
        /// <returns>返回一个bool类型的值</returns>
        public static bool validateNum(string strNum)
        {
            //return Regex.IsMatch(strNum,@"^[+]?\d*$");

            //MessageBox.Show(Regex.IsMatch("23w2451", @"^[+]?\d*$").ToString());

            if (Regex.IsMatch(strNum, @"^[+]?\d*$"))//是否找到匹配项
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 验证字符串是否只包含数字和英文字母
        /// <summary>
        /// 判断输入的字符串是否只包含数字和英文字母
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumAndEnCh(string input)
        {
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }
        #endregion
        /// <summary>
        /// 类别号检查
        /// </summary>
        /// <param name="one_string">需要验证的注册号</param>
        /// <returns>MarkCode_check</returns>
        private bool StyleNumber_check(string one_string)
        {
            try
            {

                int a = int.Parse(one_string);
                if (a >= 1 && a <= 45)
                {
                    return true;//满足
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        private static bool Have_special_char(string one_string)      //判断字符串中是否存在特殊字符
        {
            //var IsTrueORfalse = false;
            string AllNumIsSame = "~!@$%^+=\\{}|<>?~！@￥%……+=【】。{}|？«の°ⅡΩ℃∧｀≈★ΠⅢ※ΠΣΦ^＆！—％。+‘’@『』▏＞–……～＜±÷∏…";
            char[] tempChar = AllNumIsSame.ToCharArray();
            for (int i = 0; i < tempChar.Length; i++)
            {
                if (one_string.IndexOf(tempChar[i]) != -1)
                {
                    //IsTrueORfalse = true;
                    MessageBox.Show(tempChar[i].ToString());
                    //break;
                    return true;
                }
            }
            return false;

        }

        public static bool HaveEightNumber(string one_string)//备案号检查：要求存在10个以上数字
        {
            string pattern = @"^[0-9]+$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(one_string) && one_string.Length == 8)
            {
                //MessageBox.Show("行中存在本不应该存在的数字！", "错误提示！");
                return true;
            }
            else
            {
                return false;
            };

            //char[] tempChar = one_string.ToCharArray();
            //for (int i = 0; i < tempChar.Length; i++)
            //{
            //    if (one_string.IndexOf(tempChar[i]) != -1)
            //    {
            //        //IsTrueORfalse = true;
            //        //MessageBox.Show("行中存在本不应该存在的不规则符号", "错误提示！");
            //        //break;
            //        return true;
            //    }
            //}
            //return true;//满足

            //return false;//不满足
        }
        //private string GetMarkCode(string str)
        //{



        //    return null;
        //}



    }
}