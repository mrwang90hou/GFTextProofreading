namespace 公告文本校对程序
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pages_goto_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pages_text = new System.Windows.Forms.TextBox();
            this.font_to_small_btn = new System.Windows.Forms.Button();
            this.font_to_big_btn = new System.Windows.Forms.Button();
            this.result_text = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.previous_page_btn = new System.Windows.Forms.Button();
            this.next_page_btn = new System.Windows.Forms.Button();
            this.confirm_to_btn = new System.Windows.Forms.Button();
            this.pageNumber_label = new System.Windows.Forms.Label();
            this.work_record_btn = new System.Windows.Forms.Button();
            this.picture_path = new System.Windows.Forms.TextBox();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.select_picturePath_btn = new System.Windows.Forms.Button();
            this.select_txtPath_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.correct_btn = new System.Windows.Forms.Button();
            this.Saved_label = new System.Windows.Forms.Label();
            this.Save_workSpace = new System.Windows.Forms.Button();
            this.error_label = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.frist_page_btn = new System.Windows.Forms.Button();
            this.last_page_btn = new System.Windows.Forms.Button();
            this.open_thisFileFolder_btn = new System.Windows.Forms.Button();
            this.open_txtFiles_folder_btn = new System.Windows.Forms.Button();
            this.open_picture_folder_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.uctrl_ImgPro0 = new ImgeProClass.uctrl_ImagePro();
            this.uctrl_ImagePro1 = new ImgeProClass.uctrl_ImagePro();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pages_goto_btn
            // 
            this.pages_goto_btn.Location = new System.Drawing.Point(760, 672);
            this.pages_goto_btn.Name = "pages_goto_btn";
            this.pages_goto_btn.Size = new System.Drawing.Size(94, 34);
            this.pages_goto_btn.TabIndex = 2;
            this.pages_goto_btn.Text = "跳转";
            this.pages_goto_btn.UseVisualStyleBackColor = true;
            this.pages_goto_btn.Click += new System.EventHandler(this.pages_goto_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(721, 648);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "第";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(864, 646);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "页";
            // 
            // pages_text
            // 
            this.pages_text.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pages_text.Location = new System.Drawing.Point(753, 646);
            this.pages_text.MaxLength = 10;
            this.pages_text.Name = "pages_text";
            this.pages_text.Size = new System.Drawing.Size(109, 26);
            this.pages_text.TabIndex = 4;
            this.pages_text.TextChanged += new System.EventHandler(this.pages_text_TextChanged);
            this.pages_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pages_text_KeyDown);
            this.pages_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pages_text_KeyPress);
            // 
            // font_to_small_btn
            // 
            this.font_to_small_btn.Location = new System.Drawing.Point(1100, 661);
            this.font_to_small_btn.Name = "font_to_small_btn";
            this.font_to_small_btn.Size = new System.Drawing.Size(75, 23);
            this.font_to_small_btn.TabIndex = 1;
            this.font_to_small_btn.Text = "缩小";
            this.font_to_small_btn.UseVisualStyleBackColor = true;
            this.font_to_small_btn.Click += new System.EventHandler(this.font_to_small_btn_Click);
            // 
            // font_to_big_btn
            // 
            this.font_to_big_btn.Location = new System.Drawing.Point(1259, 661);
            this.font_to_big_btn.Name = "font_to_big_btn";
            this.font_to_big_btn.Size = new System.Drawing.Size(75, 23);
            this.font_to_big_btn.TabIndex = 1;
            this.font_to_big_btn.Text = "放大";
            this.font_to_big_btn.UseVisualStyleBackColor = true;
            this.font_to_big_btn.Click += new System.EventHandler(this.font_to_big_btn_Click);
            // 
            // result_text
            // 
            this.result_text.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.result_text.Location = new System.Drawing.Point(727, 136);
            this.result_text.Multiline = true;
            this.result_text.Name = "result_text";
            this.result_text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.result_text.Size = new System.Drawing.Size(623, 496);
            this.result_text.TabIndex = 4;
            this.result_text.WordWrap = false;
            this.result_text.TextChanged += new System.EventHandler(this.result_text_TextChanged);
            this.result_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.result_text_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1205, 666);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "字体";
            // 
            // previous_page_btn
            // 
            this.previous_page_btn.Location = new System.Drawing.Point(900, 656);
            this.previous_page_btn.Name = "previous_page_btn";
            this.previous_page_btn.Size = new System.Drawing.Size(81, 60);
            this.previous_page_btn.TabIndex = 5;
            this.previous_page_btn.Text = "上一页(PgUp)";
            this.previous_page_btn.UseVisualStyleBackColor = true;
            this.previous_page_btn.Click += new System.EventHandler(this.previous_page_btn_Click);
            // 
            // next_page_btn
            // 
            this.next_page_btn.Location = new System.Drawing.Point(987, 656);
            this.next_page_btn.Name = "next_page_btn";
            this.next_page_btn.Size = new System.Drawing.Size(81, 60);
            this.next_page_btn.TabIndex = 6;
            this.next_page_btn.Text = "下一页(PgDn)";
            this.next_page_btn.UseVisualStyleBackColor = true;
            this.next_page_btn.Click += new System.EventHandler(this.next_page_btn_Click);
            // 
            // confirm_to_btn
            // 
            this.confirm_to_btn.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confirm_to_btn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.confirm_to_btn.Location = new System.Drawing.Point(1100, 690);
            this.confirm_to_btn.Name = "confirm_to_btn";
            this.confirm_to_btn.Size = new System.Drawing.Size(234, 31);
            this.confirm_to_btn.TabIndex = 7;
            this.confirm_to_btn.Text = "保存（Alt）";
            this.confirm_to_btn.UseVisualStyleBackColor = true;
            this.confirm_to_btn.Click += new System.EventHandler(this.confirm_to_btn_Click);
            // 
            // pageNumber_label
            // 
            this.pageNumber_label.AutoSize = true;
            this.pageNumber_label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageNumber_label.ForeColor = System.Drawing.Color.Red;
            this.pageNumber_label.Location = new System.Drawing.Point(703, 720);
            this.pageNumber_label.Name = "pageNumber_label";
            this.pageNumber_label.Size = new System.Drawing.Size(120, 16);
            this.pageNumber_label.TabIndex = 8;
            this.pageNumber_label.Text = "显示页码信息！";
            // 
            // work_record_btn
            // 
            this.work_record_btn.Location = new System.Drawing.Point(1317, 12);
            this.work_record_btn.Name = "work_record_btn";
            this.work_record_btn.Size = new System.Drawing.Size(75, 23);
            this.work_record_btn.TabIndex = 9;
            this.work_record_btn.Text = "工作进度";
            this.work_record_btn.UseVisualStyleBackColor = true;
            this.work_record_btn.Click += new System.EventHandler(this.work_record_btn_Click);
            // 
            // picture_path
            // 
            this.picture_path.Location = new System.Drawing.Point(807, 43);
            this.picture_path.Name = "picture_path";
            this.picture_path.Size = new System.Drawing.Size(474, 21);
            this.picture_path.TabIndex = 10;
            // 
            // txt_path
            // 
            this.txt_path.Location = new System.Drawing.Point(807, 79);
            this.txt_path.Name = "txt_path";
            this.txt_path.Size = new System.Drawing.Size(474, 21);
            this.txt_path.TabIndex = 11;
            // 
            // select_picturePath_btn
            // 
            this.select_picturePath_btn.Location = new System.Drawing.Point(1285, 41);
            this.select_picturePath_btn.Name = "select_picturePath_btn";
            this.select_picturePath_btn.Size = new System.Drawing.Size(75, 23);
            this.select_picturePath_btn.TabIndex = 12;
            this.select_picturePath_btn.Text = "选择";
            this.select_picturePath_btn.UseVisualStyleBackColor = true;
            this.select_picturePath_btn.Click += new System.EventHandler(this.select_picturePath_btn_Click);
            // 
            // select_txtPath_btn
            // 
            this.select_txtPath_btn.Location = new System.Drawing.Point(1285, 79);
            this.select_txtPath_btn.Name = "select_txtPath_btn";
            this.select_txtPath_btn.Size = new System.Drawing.Size(75, 23);
            this.select_txtPath_btn.TabIndex = 13;
            this.select_txtPath_btn.Text = "选择";
            this.select_txtPath_btn.UseVisualStyleBackColor = true;
            this.select_txtPath_btn.Click += new System.EventHandler(this.select_txtPath_btn_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(752, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "图片路径";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(752, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "文本路径";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Fuchsia;
            this.label6.Location = new System.Drawing.Point(750, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(415, 33);
            this.label6.TabIndex = 16;
            this.label6.Text = "公告文本校对程序-最终V1.0";
            // 
            // correct_btn
            // 
            this.correct_btn.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.correct_btn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.correct_btn.Location = new System.Drawing.Point(725, 107);
            this.correct_btn.Name = "correct_btn";
            this.correct_btn.Size = new System.Drawing.Size(144, 23);
            this.correct_btn.TabIndex = 7;
            this.correct_btn.Text = "矫正/刷新(Ctrl+S)";
            this.correct_btn.UseVisualStyleBackColor = true;
            this.correct_btn.Click += new System.EventHandler(this.correct_btn_Click);
            // 
            // Saved_label
            // 
            this.Saved_label.AutoSize = true;
            this.Saved_label.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Saved_label.ForeColor = System.Drawing.Color.Red;
            this.Saved_label.Location = new System.Drawing.Point(1108, 104);
            this.Saved_label.Name = "Saved_label";
            this.Saved_label.Size = new System.Drawing.Size(178, 29);
            this.Saved_label.TabIndex = 8;
            this.Saved_label.Text = "Saved_label";
            // 
            // Save_workSpace
            // 
            this.Save_workSpace.Location = new System.Drawing.Point(1194, 12);
            this.Save_workSpace.Name = "Save_workSpace";
            this.Save_workSpace.Size = new System.Drawing.Size(117, 23);
            this.Save_workSpace.TabIndex = 9;
            this.Save_workSpace.Text = "保存工作路径";
            this.Save_workSpace.UseVisualStyleBackColor = true;
            this.Save_workSpace.Click += new System.EventHandler(this.Save_workSpace_btn_Click);
            // 
            // error_label
            // 
            this.error_label.AutoSize = true;
            this.error_label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.error_label.ForeColor = System.Drawing.Color.Red;
            this.error_label.Location = new System.Drawing.Point(710, 709);
            this.error_label.Name = "error_label";
            this.error_label.Size = new System.Drawing.Size(120, 16);
            this.error_label.TabIndex = 8;
            this.error_label.Text = "提示错误信息！";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(875, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "提示错误信息！";
            // 
            // frist_page_btn
            // 
            this.frist_page_btn.Image = global::公告文本校对程序.Properties.Resources.frist;
            this.frist_page_btn.Location = new System.Drawing.Point(730, 678);
            this.frist_page_btn.Name = "frist_page_btn";
            this.frist_page_btn.Size = new System.Drawing.Size(24, 23);
            this.frist_page_btn.TabIndex = 19;
            this.frist_page_btn.UseVisualStyleBackColor = true;
            this.frist_page_btn.Click += new System.EventHandler(this.frist_page_btn_Click);
            // 
            // last_page_btn
            // 
            this.last_page_btn.Image = global::公告文本校对程序.Properties.Resources.end;
            this.last_page_btn.Location = new System.Drawing.Point(860, 678);
            this.last_page_btn.Name = "last_page_btn";
            this.last_page_btn.Size = new System.Drawing.Size(24, 23);
            this.last_page_btn.TabIndex = 19;
            this.last_page_btn.UseVisualStyleBackColor = true;
            this.last_page_btn.Click += new System.EventHandler(this.last_page_btn_Click);
            // 
            // open_thisFileFolder_btn
            // 
            this.open_thisFileFolder_btn.Image = global::公告文本校对程序.Properties.Resources.图片1;
            this.open_thisFileFolder_btn.Location = new System.Drawing.Point(1326, 107);
            this.open_thisFileFolder_btn.Name = "open_thisFileFolder_btn";
            this.open_thisFileFolder_btn.Size = new System.Drawing.Size(24, 23);
            this.open_thisFileFolder_btn.TabIndex = 18;
            this.open_thisFileFolder_btn.UseVisualStyleBackColor = true;
            this.open_thisFileFolder_btn.Click += new System.EventHandler(this.open_thisFileFolder_btn_Click);
            // 
            // open_txtFiles_folder_btn
            // 
            this.open_txtFiles_folder_btn.Image = global::公告文本校对程序.Properties.Resources.图片1;
            this.open_txtFiles_folder_btn.Location = new System.Drawing.Point(1368, 79);
            this.open_txtFiles_folder_btn.Name = "open_txtFiles_folder_btn";
            this.open_txtFiles_folder_btn.Size = new System.Drawing.Size(24, 23);
            this.open_txtFiles_folder_btn.TabIndex = 17;
            this.open_txtFiles_folder_btn.UseVisualStyleBackColor = true;
            this.open_txtFiles_folder_btn.Click += new System.EventHandler(this.open_txtFiles_folder_btn_Click);
            // 
            // open_picture_folder_btn
            // 
            this.open_picture_folder_btn.Image = global::公告文本校对程序.Properties.Resources.图片1;
            this.open_picture_folder_btn.Location = new System.Drawing.Point(1368, 42);
            this.open_picture_folder_btn.Name = "open_picture_folder_btn";
            this.open_picture_folder_btn.Size = new System.Drawing.Size(24, 23);
            this.open_picture_folder_btn.TabIndex = 17;
            this.open_picture_folder_btn.UseVisualStyleBackColor = true;
            this.open_picture_folder_btn.Click += new System.EventHandler(this.open_picture_folder_btn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(25, 79);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 594);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // uctrl_ImgPro0
            // 
            this.uctrl_ImgPro0.AutoScroll = true;
            this.uctrl_ImgPro0.BIsStretch = false;
            this.uctrl_ImgPro0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uctrl_ImgPro0.Location = new System.Drawing.Point(25, 12);
            this.uctrl_ImgPro0.Name = "uctrl_ImgPro0";
            this.uctrl_ImgPro0.Size = new System.Drawing.Size(690, 704);
            this.uctrl_ImgPro0.TabIndex = 21;
            // 
            // uctrl_ImagePro1
            // 
            this.uctrl_ImagePro1.AutoScroll = true;
            this.uctrl_ImagePro1.BIsStretch = false;
            this.uctrl_ImagePro1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.uctrl_ImagePro1.Location = new System.Drawing.Point(119, 70);
            this.uctrl_ImagePro1.Name = "uctrl_ImagePro1";
            this.uctrl_ImagePro1.Size = new System.Drawing.Size(927, 608);
            this.uctrl_ImagePro1.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 741);
            this.Controls.Add(this.uctrl_ImgPro0);
            this.Controls.Add(this.frist_page_btn);
            this.Controls.Add(this.last_page_btn);
            this.Controls.Add(this.open_thisFileFolder_btn);
            this.Controls.Add(this.open_txtFiles_folder_btn);
            this.Controls.Add(this.open_picture_folder_btn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.select_txtPath_btn);
            this.Controls.Add(this.select_picturePath_btn);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.picture_path);
            this.Controls.Add(this.Save_workSpace);
            this.Controls.Add(this.work_record_btn);
            this.Controls.Add(this.Saved_label);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pageNumber_label);
            this.Controls.Add(this.correct_btn);
            this.Controls.Add(this.confirm_to_btn);
            this.Controls.Add(this.next_page_btn);
            this.Controls.Add(this.previous_page_btn);
            this.Controls.Add(this.result_text);
            this.Controls.Add(this.pages_text);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pages_goto_btn);
            this.Controls.Add(this.font_to_big_btn);
            this.Controls.Add(this.font_to_small_btn);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "公告文本校对程序";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button pages_goto_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pages_text;
        private System.Windows.Forms.Button font_to_small_btn;
        private System.Windows.Forms.Button font_to_big_btn;
        private System.Windows.Forms.TextBox result_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button previous_page_btn;
        private System.Windows.Forms.Button next_page_btn;
        private System.Windows.Forms.Button confirm_to_btn;
        private System.Windows.Forms.Label pageNumber_label;
        private System.Windows.Forms.Button work_record_btn;
        private System.Windows.Forms.TextBox picture_path;
        private System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Button select_picturePath_btn;
        private System.Windows.Forms.Button select_txtPath_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button open_picture_folder_btn;
        private System.Windows.Forms.Button open_txtFiles_folder_btn;
        private System.Windows.Forms.Button correct_btn;
        private System.Windows.Forms.Label Saved_label;
        private System.Windows.Forms.Button open_thisFileFolder_btn;
        private System.Windows.Forms.Button last_page_btn;
        private System.Windows.Forms.Button frist_page_btn;
        private ImgeProClass.uctrl_ImagePro uctrl_ImagePro1;
        private ImgeProClass.uctrl_ImagePro uctrl_ImgPro0;
        private System.Windows.Forms.Button Save_workSpace;
        private System.Windows.Forms.Label error_label;
        private System.Windows.Forms.Label label7;
    }
}

