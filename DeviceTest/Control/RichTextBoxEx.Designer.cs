using System.Windows.Forms;

namespace DeviceTest
{
    partial class RichTextBoxEx
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

        #region 组件设计器生成的代码

        //private ContextMenuStrip contextMenuStrip1;
        //private System.Windows.Forms.ToolStripMenuItem CopyItem;
        //private System.Windows.Forms.ToolStripMenuItem PasterItem;
        //private System.Windows.Forms.ToolStripMenuItem UndoItem;
        //private System.Windows.Forms.ToolStripMenuItem CutItem;
        //private System.Windows.Forms.ToolStripMenuItem ClearItem;
        //private System.Windows.Forms.ToolStripMenuItem SelectAllItem;
        //private System.Windows.Forms.ToolStripMenuItem RedoItem;
        //private ToolStripSeparator toolStripSeparator1;
        //private ToolStripSeparator toolStripSeparator2;

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.StopScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveLog = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowTime = new System.Windows.Forms.ToolStripMenuItem();
            this.Look = new System.Windows.Forms.ToolStripMenuItem();
            this.toHex = new System.Windows.Forms.ToolStripMenuItem();
            this.IsAutoHide = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StopScroll,
            this.SaveLog,
            this.ShowTime,
            this.Look,
            this.toHex,
            this.IsAutoHide});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 136);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // StopScroll
            // 
            this.StopScroll.Name = "StopScroll";
            this.StopScroll.Size = new System.Drawing.Size(136, 22);
            this.StopScroll.Text = "停止滚动";
            // 
            // SaveLog
            // 
            this.SaveLog.Name = "SaveLog";
            this.SaveLog.Size = new System.Drawing.Size(136, 22);
            this.SaveLog.Text = "保存日志";
            // 
            // ShowTime
            // 
            this.ShowTime.Name = "ShowTime";
            this.ShowTime.Size = new System.Drawing.Size(136, 22);
            this.ShowTime.Text = "显示时间";
            // 
            // Look
            // 
            this.Look.Name = "Look";
            this.Look.Size = new System.Drawing.Size(136, 22);
            this.Look.Text = "黑底青字";
            // 
            // toHex
            // 
            this.toHex.Name = "toHex";
            this.toHex.Size = new System.Drawing.Size(136, 22);
            this.toHex.Text = "字符转HEX";
            // 
            // IsAutoHide
            // 
            this.IsAutoHide.Name = "IsAutoHide";
            this.IsAutoHide.Size = new System.Drawing.Size(136, 22);
            this.IsAutoHide.Text = "自动隐藏";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem StopScroll;
        private ToolStripMenuItem SaveLog;
        private ToolStripMenuItem ShowTime;
        private ToolStripMenuItem Look;
        private ToolStripMenuItem toHex;
        private ToolStripMenuItem IsAutoHide;
    }
}
