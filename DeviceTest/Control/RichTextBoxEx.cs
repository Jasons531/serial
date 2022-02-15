using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DeviceTest
{
    public partial class RichTextBoxEx : RichTextBox
    {
        public RichTextBoxEx()
        {
            InitializeComponent();
            ContextMenuStrip = contextMenuStrip1;
        }
        /* 在文本不滚动时，保存接收的文本数据 */
        private string NoShowStr = "";
        #region 属性
        /// <summary>
        /// 设置或获取控件在不使用时是否自动隐藏
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("Appearance")]
        public bool AutoHide { get; set; }

        /// <summary>
        /// 设置控件是否使用自动隐藏功能
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        [Category("Appearance")]
        public bool EnableAutoHide { get; set; }
        #endregion

        #region 重构
        //protected override void WndProc(ref System.Windows.Forms.Message m)
        //{
        //    base.WndProc(ref m);
        //    if (m.Msg == 15)
        //    {
        //        // raise the paint event
        //        Graphics graphic = base.CreateGraphics();
        //        Pen p = new Pen(Color.Red);
        //        graphic.DrawRectangle(p, base.DisplayRectangle);
        //        OnPaint(new PaintEventArgs(graphic,base.ClientRectangle));
        //    }

        //}
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            this.Text = "";
            g_IsShowNewLine = true;
            base.OnMouseDoubleClick(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            /* 重写OnMouseUp事件，添加代码Focus()，在右键单击时，让控件成为活动控件 */
            if (e.Button == MouseButtons.Right)
            {
                if (!Focused)
                    Focus();
                IsAutoHide.Visible = EnableAutoHide;
                if (AutoHide)
                {
                    IsAutoHide.Text = "总是显示";
                }
                else
                {
                    IsAutoHide.Text = "自动隐藏";
                }
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            /* 重写OnMouseUp事件，添加代码Focus()，在右键单击时，让控件成为活动控件 */
            if (AutoHide)
            {
                if (!Focused)
                    Visible = false;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            /* 重写OnMouseUp事件，添加代码Focus()，在右键单击时，让控件成为活动控件 */
            if (AutoHide)
            {
                Visible = false;
            }
            base.OnLeave(e);
        }

        #endregion

        #region 右键菜单
        /* 是否滚动 */
        private bool g_IsScroll = true;

        private bool g_IsShowTime = false;

        private bool g_IsShowNewLine = true;

        string g_SaveLogPath = null;
        private void SaveLogFunc(string str)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.OverwritePrompt = false;
            sf.CreatePrompt = false;
            sf.Filter = ("文本文件|*.txt");
            if (sf.ShowDialog() == DialogResult.OK)
            {
                g_SaveLogPath=sf.FileName;
                using (StreamWriter writer = new StreamWriter(sf.FileName, true, Encoding.GetEncoding("GB2312")))
                {
                    writer.Write(str);
                    writer.Flush();
                }
            }
        }
        private string Hex2Char(string str)
        {
            return Encoding.GetEncoding("GB2312").GetString(str.ToByteArray());
        }

        private string Char2Hex(string str)
        {
            byte[] arrClientSendMsg = Encoding.GetEncoding("GB2312").GetBytes(str);
            return BitConverter.ToString(arrClientSendMsg).Replace("-", " ") + " ";
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            RichTextBox rtb = (RichTextBox)this.contextMenuStrip1.SourceControl;
            switch (e.ClickedItem.Text)
            {
                case "停止滚动":
                    g_IsScroll = false;
                    e.ClickedItem.Text = "滚动文本";
                    break;
                case "滚动文本":
                    g_IsScroll = true;
                    e.ClickedItem.Text = "停止滚动";
                    break;

                case "黑底青字":
                    rtb.BackColor = Color.Black;
                    rtb.ForeColor = Color.MediumAquamarine;
                    e.ClickedItem.Text = "白底黑字";
                    break;
                case "白底黑字":
                    rtb.BackColor = SystemColors.Window;
                    rtb.ForeColor = Color.Black;
                    e.ClickedItem.Text = "黑底青字";
                    break;


                case "显示时间":
                    g_IsShowTime = true;
                    e.ClickedItem.Text = "关闭时间";
                    break;
                case "关闭时间":
                    g_IsShowTime = false;
                    e.ClickedItem.Text = "显示时间";
                    break;

                case "字符转HEX":
                    Text = Char2Hex(Text);
                    e.ClickedItem.Text = "HEX转字符";
                    break;
                case "HEX转字符":
                    g_IsShowTime = false;
                    Text = Hex2Char(Text);
                    e.ClickedItem.Text = "字符转HEX";
                    break;

                case "自动隐藏":
                    AutoHide = true;
                    break;
                case "总是显示":
                    AutoHide = false;
                    e.ClickedItem.Text = "自动隐藏";
                    break;

                case "保存日志":
                    SaveLogFunc(rtb.Text);
                    break;
                default:
                    break;
            }
        }


        #endregion


        private void SetText(string str, bool IsLineDown)
        {
            Action act = new Action(() =>
            {
                if (Lines.Count() > 1000)
                {
                    Text = "";
                }
                if (IsLineDown)
                {
                    if (NoShowStr != "")
                    {
                        AppendText(NoShowStr);
                        NoShowStr = "";
                    }
                    AppendText(str);
                    ScrollToCaret();
                }
                else
                {
                    NoShowStr += str;
                }
                if (g_SaveLogPath != null)
                {
                    using (StreamWriter writer = new StreamWriter(g_SaveLogPath, true, Encoding.GetEncoding("GB2312")))
                    {
                        writer.Write(str);
                        writer.Flush();
                    }
                }
            });
            if (this.InvokeRequired)
            {
                BeginInvoke(act);
            }
            else
            {
                act();
            }
        }
        public void ShowText(string str)
        {
            //SetTest(str, true);
            ShowTextCtrl(str);
        }

        private void prvShowTime(string s)
        {
            int EnterIndex;
            EnterIndex = s.IndexOf(Environment.NewLine);
            if (EnterIndex > -1)
            {
                while (EnterIndex > -1)
                {
                    string line;
                    if (g_IsShowNewLine)
                    {
                        line = DateTime.Now.ToString("[HH:mm:ss.fff] ") + s.Substring(0, EnterIndex + Environment.NewLine.Length);
                    }
                    else
                    {
                        line = s.Substring(0, EnterIndex + Environment.NewLine.Length);
                        g_IsShowNewLine = true;
                    }

                    SetText(line, g_IsScroll);
                    s = s.Substring(EnterIndex + Environment.NewLine.Length, s.Length - EnterIndex - Environment.NewLine.Length);
                    EnterIndex = s.IndexOf(Environment.NewLine);
                }
                if (s != "")
                {
                    SetText(DateTime.Now.ToString("[HH:mm:ss.fff] ") + s, g_IsScroll);
                    g_IsShowNewLine = false;
                }
            }
            else
            {
                if (g_IsShowNewLine)
                {
                    SetText(DateTime.Now.ToString("[HH:mm:ss.fff] ") + s, g_IsScroll);
                }
                else
                {
                    SetText(s, g_IsScroll);
                }
                g_IsShowNewLine = false;
            }
        }
        private void ShowTextCtrl(string s)
        {
            if (g_IsShowTime)
            {
                prvShowTime(s);
            }
            else
            {
                SetText(s, g_IsScroll);
            }
        }
    }
}
