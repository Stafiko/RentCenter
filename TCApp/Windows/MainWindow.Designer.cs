namespace RentCenter.Window
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.Floors = new System.Windows.Forms.TabControl();
            this.Floor1 = new System.Windows.Forms.TabPage();
            this.Floor2 = new System.Windows.Forms.TabPage();
            this.Floor3 = new System.Windows.Forms.TabPage();
            this.GanttChart = new RentCenter.Gantt.GanttChart();
            this.Floors.SuspendLayout();
            this.SuspendLayout();

            this.Floors.Controls.Add(this.Floor1);
            this.Floors.Controls.Add(this.Floor2);
            this.Floors.Controls.Add(this.Floor3);
            this.Floors.Location = new System.Drawing.Point(11, 11);
            this.Floors.Margin = new System.Windows.Forms.Padding(2);
            this.Floors.Name = "Floors";
            this.Floors.SelectedIndex = 0;
            this.Floors.Size = new System.Drawing.Size(972, 403);
            this.Floors.TabIndex = 0;
            this.Floors.SelectedIndexChanged += new System.EventHandler(this.FloorSelected);

            this.Floor1.BackgroundImage = global::RentCenter.Window.Properties.Resources.floor1;
            this.Floor1.Location = new System.Drawing.Point(4, 22);
            this.Floor1.Margin = new System.Windows.Forms.Padding(2);
            this.Floor1.Name = "Floor1";
            this.Floor1.Padding = new System.Windows.Forms.Padding(2);
            this.Floor1.Size = new System.Drawing.Size(964, 377);
            this.Floor1.TabIndex = 0;
            this.Floor1.Text = "Этаж 1";
            this.Floor1.UseVisualStyleBackColor = true;

            this.Floor2.BackgroundImage = global::RentCenter.Window.Properties.Resources.floor2;
            this.Floor2.Location = new System.Drawing.Point(4, 22);
            this.Floor2.Margin = new System.Windows.Forms.Padding(2);
            this.Floor2.Name = "Floor2";
            this.Floor2.Padding = new System.Windows.Forms.Padding(2);
            this.Floor2.Size = new System.Drawing.Size(964, 377);
            this.Floor2.TabIndex = 1;
            this.Floor2.Text = "Этаж 2";
            this.Floor2.UseVisualStyleBackColor = true;

            this.Floor3.BackgroundImage = global::RentCenter.Window.Properties.Resources.floor3;
            this.Floor3.Location = new System.Drawing.Point(4, 22);
            this.Floor3.Margin = new System.Windows.Forms.Padding(2);
            this.Floor3.Name = "Floor3";
            this.Floor3.Size = new System.Drawing.Size(964, 377);
            this.Floor3.TabIndex = 2;
            this.Floor3.Text = "Этаж 3";
            this.Floor3.UseVisualStyleBackColor = true;

            this.GanttChart.AllowManualEditBar = false;
            this.GanttChart.BackColor = System.Drawing.Color.White;
            this.GanttChart.DateFont = new System.Drawing.Font("Verdana", 8F);
            this.GanttChart.FromDate = new System.DateTime(((long)(0)));
            this.GanttChart.Location = new System.Drawing.Point(11, 418);
            this.GanttChart.Margin = new System.Windows.Forms.Padding(2);
            this.GanttChart.Name = "GanttChart";
            this.GanttChart.RowFont = new System.Drawing.Font("Verdana", 8F);
            this.GanttChart.Size = new System.Drawing.Size(964, 153);
            this.GanttChart.TabIndex = 1;
            this.GanttChart.Text = "GanttChart";
            this.GanttChart.TimeFont = new System.Drawing.Font("Verdana", 8F);
            this.GanttChart.ToDate = new System.DateTime(((long)(0)));
            this.GanttChart.ToolTipText = ((System.Collections.Generic.List<string>)(resources.GetObject("GanttChart.ToolTipText")));
            this.GanttChart.ToolTipTextTitle = "";
            this.GanttChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BarSelected);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 582);
            this.Controls.Add(this.GanttChart);
            this.Controls.Add(this.Floors);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.Text = "Аренда помещений ТЦ \"Флагман\"";
            this.Floors.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl Floors;
        private System.Windows.Forms.TabPage Floor1;
        private System.Windows.Forms.TabPage Floor2;
        private System.Windows.Forms.TabPage Floor3;
        private RentCenter.Gantt.GanttChart GanttChart;
    }
}

