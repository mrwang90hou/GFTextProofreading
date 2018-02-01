using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using 公告文本校对程序;

namespace ImgeProClass
{
    public partial class uctrl_ImagePro : UserControl
    {

        Form3 f3 = new Form3();
        public static int linesNumber;
        private Bitmap image = null;
        /// <summary>
        /// 要显示的图像
        /// </summary>
        /// 
        public Bitmap Image
        {
            set
            {
                using (Bitmap bmpTemp = (Bitmap)value.Clone())
                {
                    if (image != null) using (image) { }
                    image = (Bitmap)bmpTemp.Clone();
                }
                fitToScreen();
            }
            get
            {
                return (Bitmap)image.Clone();
            }
        }
        /// <summary>
        /// 是否拉伸图像
        /// </summary>
        ///
        [Browsable(true), Category("IsStretch"), Description("Is Stretch")]
        public bool BIsStretch
        {
            get;
            set;
        }
        private double dZoom = 1;
        public uctrl_ImagePro()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
            this.SuspendLayout();
            this.ResumeLayout(false);
            this.AutoScroll = false;
            //this.SetScrollState();
            //this.SetAutoScrollMargin(1,1);
            Size scrollOffset = new Size();
            scrollOffset.Width = 200;
            scrollOffset.Height = 200;
            this.AutoScrollPosition = new Point(scrollOffset);
            //this.AutoScrollOffset = scrollOffset;
        }
        // Paint image
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (image != null)
                {
                    using (Bitmap imageToDraw = (Bitmap)image.Clone())
                    {
                        Graphics g = e.Graphics;
                        Rectangle rc = ClientRectangle;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        int width = (int)(this.Width * dZoom);
                        int height = (int)(this.Height * dZoom);
                        if (!BIsStretch)
                        {
                            double aspectRatio = ((double)imageToDraw.Width) / imageToDraw.Height;
                            if (width >= (int)(height * aspectRatio))
                            {
                                width = (int)(height * aspectRatio);
                            }
                            else
                            {
                                height = (int)((double)width / aspectRatio);
                            }
                        }
                        int x = (rc.Width < width) ? this.AutoScrollPosition.X : (rc.Width - width) / 2;//使图像显示在中间
                        int y = (rc.Height < height) ? this.AutoScrollPosition.Y : (rc.Height - height) / 2;
                        // draw image
                        g.DrawImage(imageToDraw, new Rectangle(x, y, width, height));
                    }

                }
            }
            finally
            {
                base.OnPaint(e);
            }
        }

        /// <summary>
        /// 按放大比列显示
        /// </summary>
        private void updateZoom()
        {
            int width = (int)(this.Width * dZoom);
            int height = (int)(this.Height * dZoom);
            if (!BIsStretch)
            {
                double aspectRatio = ((double)image.Width) / image.Height;
                if (width >= (int)(height * aspectRatio))
                {
                    width = (int)(height * aspectRatio);
                }
                else
                {
                    height = (int)((double)width / aspectRatio);
                }
            }
            this.AutoScrollMinSize = new Size(width, height);
            this.Invalidate();
        }
        /// <summary>
        /// 按控件大小显示
        /// </summary>
        private void fitToScreen()
        {
            dZoom = Math.Min((double)(this.ClientRectangle.Width) / (this.Width), (double)(this.ClientRectangle.Height) / (this.Height));
            updateZoom();
            //dZoom = Math.Min((double)(this.ClientRectangle.Width) / (this.Width), (double)(this.ClientRectangle.Height) / (this.Height));
            //updateZoom();
        }
        /// <summary>
        ///按图像的原始大小显示
        /// </summary>
        public void ZoomToOriImgSize()
        {
            linesNumber = Form3.linesNumber;
            if (linesNumber == 3)
            {
                dZoom = Math.Min((double)(image.Width) * 0.8 / (this.Width), (double)(image.Height) * 0.7 / (this.Height));
                updateZoom();
                Size scrollOffset = new Size();
                scrollOffset.Width = 0;
                scrollOffset.Height = 196;
                this.AutoScrollPosition = new Point(scrollOffset);
            }
            else
            {
                dZoom = Math.Min((double)(image.Width) * 1.2 / (this.Width), (double)(image.Height) * 1.2 / (this.Height));
                updateZoom();
                Size scrollOffset = new Size();
                scrollOffset.Width = 300;
                scrollOffset.Height = 196;
                this.AutoScrollPosition = new Point(scrollOffset);
            }
        }
        /// <summary>
        /// 重载滚轮事件，实现用滚轮放缩
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            double numberOfTextLinesToMove = ((double)e.Delta * SystemInformation.MouseWheelScrollLines) / 1000;
            if (dZoom + numberOfTextLinesToMove >= (Math.Min((double)(this.ClientRectangle.Width) / (this.Width), (double)(this.ClientRectangle.Height) / (this.Height))) && dZoom + numberOfTextLinesToMove <= 30.00)
            {
                dZoom += numberOfTextLinesToMove;
                updateZoom();

                if (ptMoveStart.X == 0 && ptMoveStart.Y == 0)
                {
                    this.AutoScrollPosition = this.PointToScreen(new Point(ptMoveStart.X, ptMoveStart.Y));
                }
                else
                {
                    this.AutoScrollPosition = new Point(ptCurScrollPos.X + ptImgToPnl(ptCurPosInImg).X - ptMoveStart.X, ptCurScrollPos.Y + ptImgToPnl(ptCurPosInImg).Y - ptMoveStart.Y);
                }
                ptCurScrollPos = new Point(this.HorizontalScroll.Value, this.VerticalScroll.Value);
            }
        }
        /// <summary>
        /// 根据鼠标移动的距离 移动图像 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uctrl_ImagePro_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if ((this.HorizontalScroll.Value + ((int)((ptMoveStart.X - e.X) / 5))) <= this.HorizontalScroll.Maximum && this.HorizontalScroll.Value + ((int)((ptMoveStart.X - e.X) / 5)) >= this.HorizontalScroll.Minimum)
                {
                    this.HorizontalScroll.Value += (int)((ptMoveStart.X - e.X) / 5);
                }
                if ((this.VerticalScroll.Value + ((int)((ptMoveStart.Y - e.Y) / 5))) <= this.VerticalScroll.Maximum && this.VerticalScroll.Value + ((int)((ptMoveStart.Y - e.Y) / 5)) >= this.VerticalScroll.Minimum)
                {
                    this.VerticalScroll.Value += (int)((ptMoveStart.Y - e.Y) / 5);
                }
                this.AdjustFormScrollbars(true);
                this.Invalidate();
            }
        }

        //记录鼠标拖动的开始位置
        private Point ptMoveStart = Point.Empty;
        //记录鼠标点击时鼠标在图像上的坐标
        private Point ptCurPosInImg = Point.Empty;
        private Point ptCurScrollPos = Point.Empty;
        private void uctrl_ImagePro_MouseDown(object sender, MouseEventArgs e)
        {
            //ptMoveStart = e.Location;
            //ptCurPosInImg = ptPlToImg(e.Location);
            //ptCurScrollPos = new Point(this.HorizontalScroll.Value, this.VerticalScroll.Value);
        }
        /// <summary>
        /// 获得图像上的坐标对应的控件上的坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point ptImgToPnl(Point point)
        {
            Point desPoint = Point.Empty;
            Rectangle rc = this.ClientRectangle;
            int width = this.Width;
            int height = this.Height;
            if (!BIsStretch)
            {
                double aspectRatio = ((double)image.Width) / image.Height;
                if (width >= (int)(height * aspectRatio))
                {
                    width = (int)(height * aspectRatio);
                }
                else
                {
                    height = (int)((double)width / aspectRatio);
                }
            }
            width = (int)(width * dZoom);
            height = (int)(height * dZoom);
            int x = (rc.Width < width) ? this.AutoScrollPosition.X : (rc.Width - width) / 2;
            int y = (rc.Height < height) ? this.AutoScrollPosition.Y : (rc.Height - height) / 2;
            desPoint = new Point((int)(point.X * dZoom) + x, (int)(point.Y * dZoom) + y);
            return desPoint;
        }
        /// <summary>
        /// 获得控件上的坐标对应到图像上的坐标
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point ptPlToImg(Point point)
        {
            Point desPoint = Point.Empty;
            Rectangle rc = this.ClientRectangle;
            int width = this.Width;
            int height = this.Height;
            if (!BIsStretch)
            {
                double aspectRatio = ((double)image.Width) / image.Height;
                if (width >= (int)(height * aspectRatio))
                {
                    width = (int)(height * aspectRatio);
                }
                else
                {
                    height = (int)((double)width / aspectRatio);
                }
            }
            width = (int)(width * dZoom);
            height = (int)(height * dZoom);
            int x = (rc.Width < width) ? this.AutoScrollPosition.X : (rc.Width - width) / 2;
            int y = (rc.Height < height) ? this.AutoScrollPosition.Y : (rc.Height - height) / 2;
            if ((point.X >= x) && (point.Y >= y) && (point.X < x + width) && (point.Y < y + height))
            {
                desPoint = new Point((int)(((double)(point.X - x)) / dZoom), (int)((double)((point.Y - y)) / dZoom));
            }
            else
            {
                desPoint = Point.Empty;
            }
            return desPoint;
        }
        /// <summary>
        ///  拦截Esc键  实现按控件界面大小显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uctrl_ImagePro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                fitToScreen();
            }
        }
        
        private void uctrl_ImagePro_Load(object sender, EventArgs e)
        {
            //this.SetScrollState(100,true);
            //this.SetAutoScrollMargin(100,100);
            //y = 100;
            //Size scrollOffset = new Size(this.AutoScrollPosition);
            //scrollOffset.Width = 20;
            //scrollOffset.Height = 20;
            //this.AutoScrollPosition = new Point(scrollOffset);
            //MessageBox.Show(scrollOffset.ToString());
            //MessageBox.Show("加载uctrl_ImagePro");
        }
        
        private void uctrl_ImagePro_Paint(object sender, PaintEventArgs e)
        {
            //this.VerticalScroll.Value = y;
        }

        private void Uctrl_ImagePro_Scroll(object sender, ScrollEventArgs e)
        {
            //y = this.VerticalScroll.Value;
        }
        
    }
}