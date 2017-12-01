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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.FunctionsTabs = new System.Windows.Forms.TabControl();
            this.Rents = new System.Windows.Forms.TabPage();
            this.Floors = new System.Windows.Forms.TabControl();
            this.Floor1 = new System.Windows.Forms.TabPage();
            this.Floor2 = new System.Windows.Forms.TabPage();
            this.Floor3 = new System.Windows.Forms.TabPage();
            this.Costs = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CostPercent = new System.Windows.Forms.NumericUpDown();
            this.PercentLabel = new System.Windows.Forms.Label();
            this.CostType = new System.Windows.Forms.ComboBox();
            this.TotalCounting = new System.Windows.Forms.DataGridView();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostPriceNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CostDateTo = new System.Windows.Forms.DateTimePicker();
            this.CostDateFrom = new System.Windows.Forms.DateTimePicker();
            this.CostInfoBox = new System.Windows.Forms.TextBox();
            this.AddCostButton = new System.Windows.Forms.Button();
            this.TotalCosts = new System.Windows.Forms.DataGridView();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.From = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.To = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PerMonth = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TotalProfit = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceFloor1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceFloor2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceFloor3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percents = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GanttChart = new RentCenter.Gantt.GanttChart();
            this.FunctionsTabs.SuspendLayout();
            this.Rents.SuspendLayout();
            this.Floors.SuspendLayout();
            this.Costs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CostPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalCounting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostPriceNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalCosts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalProfit)).BeginInit();
            this.SuspendLayout();
            // 
            // FunctionsTabs
            // 
            this.FunctionsTabs.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.FunctionsTabs.Controls.Add(this.Rents);
            this.FunctionsTabs.Controls.Add(this.Costs);
            this.FunctionsTabs.Location = new System.Drawing.Point(-2, -3);
            this.FunctionsTabs.Multiline = true;
            this.FunctionsTabs.Name = "FunctionsTabs";
            this.FunctionsTabs.SelectedIndex = 0;
            this.FunctionsTabs.Size = new System.Drawing.Size(1113, 412);
            this.FunctionsTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.FunctionsTabs.TabIndex = 4;
            // 
            // Rents
            // 
            this.Rents.BackColor = System.Drawing.Color.LightGray;
            this.Rents.Controls.Add(this.Floors);
            this.Rents.Location = new System.Drawing.Point(23, 4);
            this.Rents.Name = "Rents";
            this.Rents.Padding = new System.Windows.Forms.Padding(3);
            this.Rents.Size = new System.Drawing.Size(1086, 404);
            this.Rents.TabIndex = 0;
            this.Rents.Text = "Аренда";
            // 
            // Floors
            // 
            this.Floors.Controls.Add(this.Floor1);
            this.Floors.Controls.Add(this.Floor2);
            this.Floors.Controls.Add(this.Floor3);
            this.Floors.Location = new System.Drawing.Point(64, 0);
            this.Floors.Margin = new System.Windows.Forms.Padding(2);
            this.Floors.Name = "Floors";
            this.Floors.SelectedIndex = 0;
            this.Floors.Size = new System.Drawing.Size(972, 403);
            this.Floors.TabIndex = 1;
            // 
            // Floor1
            // 
            this.Floor1.BackgroundImage = global::RentCenter.Window.Properties.Resources.floor1;
            this.Floor1.Location = new System.Drawing.Point(4, 22);
            this.Floor1.Margin = new System.Windows.Forms.Padding(2);
            this.Floor1.Name = "Floor1";
            this.Floor1.Padding = new System.Windows.Forms.Padding(2);
            this.Floor1.Size = new System.Drawing.Size(964, 377);
            this.Floor1.TabIndex = 0;
            this.Floor1.Text = "Этаж 1";
            this.Floor1.UseVisualStyleBackColor = true;
            // 
            // Floor2
            // 
            this.Floor2.BackgroundImage = global::RentCenter.Window.Properties.Resources.floor2;
            this.Floor2.Location = new System.Drawing.Point(4, 22);
            this.Floor2.Margin = new System.Windows.Forms.Padding(2);
            this.Floor2.Name = "Floor2";
            this.Floor2.Padding = new System.Windows.Forms.Padding(2);
            this.Floor2.Size = new System.Drawing.Size(964, 377);
            this.Floor2.TabIndex = 1;
            this.Floor2.Text = "Этаж 2";
            this.Floor2.UseVisualStyleBackColor = true;
            // 
            // Floor3
            // 
            this.Floor3.BackgroundImage = global::RentCenter.Window.Properties.Resources.floor3;
            this.Floor3.Location = new System.Drawing.Point(4, 22);
            this.Floor3.Margin = new System.Windows.Forms.Padding(2);
            this.Floor3.Name = "Floor3";
            this.Floor3.Size = new System.Drawing.Size(964, 377);
            this.Floor3.TabIndex = 2;
            this.Floor3.Text = "Этаж 3";
            this.Floor3.UseVisualStyleBackColor = true;
            // 
            // Costs
            // 
            this.Costs.Controls.Add(this.label7);
            this.Costs.Controls.Add(this.label6);
            this.Costs.Controls.Add(this.CostPercent);
            this.Costs.Controls.Add(this.PercentLabel);
            this.Costs.Controls.Add(this.CostType);
            this.Costs.Controls.Add(this.TotalCounting);
            this.Costs.Controls.Add(this.CostPriceNumeric);
            this.Costs.Controls.Add(this.label4);
            this.Costs.Controls.Add(this.label3);
            this.Costs.Controls.Add(this.label2);
            this.Costs.Controls.Add(this.label1);
            this.Costs.Controls.Add(this.CostDateTo);
            this.Costs.Controls.Add(this.CostDateFrom);
            this.Costs.Controls.Add(this.CostInfoBox);
            this.Costs.Controls.Add(this.AddCostButton);
            this.Costs.Controls.Add(this.TotalCosts);
            this.Costs.Controls.Add(this.TotalProfit);
            this.Costs.Location = new System.Drawing.Point(23, 4);
            this.Costs.Name = "Costs";
            this.Costs.Padding = new System.Windows.Forms.Padding(3);
            this.Costs.Size = new System.Drawing.Size(1086, 404);
            this.Costs.TabIndex = 1;
            this.Costs.Text = "Доходы / Расходы";
            this.Costs.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1065, 329);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(780, 329);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "руб.";
            // 
            // CostPercent
            // 
            this.CostPercent.DecimalPlaces = 2;
            this.CostPercent.Location = new System.Drawing.Point(889, 327);
            this.CostPercent.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.CostPercent.Name = "CostPercent";
            this.CostPercent.Size = new System.Drawing.Size(170, 20);
            this.CostPercent.TabIndex = 18;
            // 
            // PercentLabel
            // 
            this.PercentLabel.AutoSize = true;
            this.PercentLabel.Location = new System.Drawing.Point(845, 329);
            this.PercentLabel.Name = "PercentLabel";
            this.PercentLabel.Size = new System.Drawing.Size(38, 13);
            this.PercentLabel.TabIndex = 17;
            this.PercentLabel.Text = "Налог";
            // 
            // CostType
            // 
            this.CostType.FormattingEnabled = true;
            this.CostType.Location = new System.Drawing.Point(619, 291);
            this.CostType.Name = "CostType";
            this.CostType.Size = new System.Drawing.Size(155, 21);
            this.CostType.TabIndex = 16;
            this.CostType.SelectedIndexChanged += new System.EventHandler(this.CostTypeSelectedIndexChanged);
            this.CostType.TextUpdate += new System.EventHandler(this.CostTypeTextChanged);
            // 
            // TotalCounting
            // 
            this.TotalCounting.AllowUserToAddRows = false;
            this.TotalCounting.AllowUserToDeleteRows = false;
            this.TotalCounting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TotalCounting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Data,
            this.Count});
            this.TotalCounting.Location = new System.Drawing.Point(6, 288);
            this.TotalCounting.Name = "TotalCounting";
            this.TotalCounting.ReadOnly = true;
            this.TotalCounting.Size = new System.Drawing.Size(531, 110);
            this.TotalCounting.TabIndex = 15;
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Data.HeaderText = "";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // Count
            // 
            this.Count.HeaderText = "";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            // 
            // CostPriceNumeric
            // 
            this.CostPriceNumeric.DecimalPlaces = 2;
            this.CostPriceNumeric.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.CostPriceNumeric.Location = new System.Drawing.Point(619, 327);
            this.CostPriceNumeric.Maximum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            0});
            this.CostPriceNumeric.Minimum = new decimal(new int[] {
            1661992959,
            1808227885,
            5,
            -2147483648});
            this.CostPriceNumeric.Name = "CostPriceNumeric";
            this.CostPriceNumeric.Size = new System.Drawing.Size(155, 20);
            this.CostPriceNumeric.TabIndex = 14;
            this.CostPriceNumeric.ThousandsSeparator = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(551, 329);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Стоимость";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(548, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Затраты на";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(774, 364);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "До";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(551, 364);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "С";
            // 
            // CostDateTo
            // 
            this.CostDateTo.Location = new System.Drawing.Point(802, 362);
            this.CostDateTo.Name = "CostDateTo";
            this.CostDateTo.Size = new System.Drawing.Size(200, 20);
            this.CostDateTo.TabIndex = 9;
            // 
            // CostDateFrom
            // 
            this.CostDateFrom.Location = new System.Drawing.Point(571, 362);
            this.CostDateFrom.Name = "CostDateFrom";
            this.CostDateFrom.Size = new System.Drawing.Size(200, 20);
            this.CostDateFrom.TabIndex = 8;
            // 
            // CostInfoBox
            // 
            this.CostInfoBox.Location = new System.Drawing.Point(780, 291);
            this.CostInfoBox.Name = "CostInfoBox";
            this.CostInfoBox.Size = new System.Drawing.Size(300, 20);
            this.CostInfoBox.TabIndex = 6;
            // 
            // AddCostButton
            // 
            this.AddCostButton.Location = new System.Drawing.Point(1008, 359);
            this.AddCostButton.Name = "AddCostButton";
            this.AddCostButton.Size = new System.Drawing.Size(72, 23);
            this.AddCostButton.TabIndex = 5;
            this.AddCostButton.Text = "Добавить";
            this.AddCostButton.UseVisualStyleBackColor = true;
            this.AddCostButton.Click += new System.EventHandler(this.AddCost);
            // 
            // TotalCosts
            // 
            this.TotalCosts.AllowUserToAddRows = false;
            this.TotalCosts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TotalCosts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Type,
            this.Info,
            this.From,
            this.To,
            this.Cost,
            this.Percent,
            this.Price,
            this.PerMonth});
            this.TotalCosts.Location = new System.Drawing.Point(543, 11);
            this.TotalCosts.Name = "TotalCosts";
            this.TotalCosts.RowHeadersWidth = 10;
            this.TotalCosts.Size = new System.Drawing.Size(538, 271);
            this.TotalCosts.TabIndex = 4;
            // 
            // Type
            // 
            this.Type.HeaderText = "Тип";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 60;
            // 
            // Info
            // 
            this.Info.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Info.HeaderText = "Инвестиции";
            this.Info.MinimumWidth = 100;
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            // 
            // From
            // 
            dataGridViewCellStyle31.Format = "d";
            dataGridViewCellStyle31.NullValue = null;
            this.From.DefaultCellStyle = dataGridViewCellStyle31;
            this.From.HeaderText = "С";
            this.From.Name = "From";
            this.From.ReadOnly = true;
            this.From.Width = 65;
            // 
            // To
            // 
            dataGridViewCellStyle32.Format = "d";
            dataGridViewCellStyle32.NullValue = null;
            this.To.DefaultCellStyle = dataGridViewCellStyle32;
            this.To.HeaderText = "До";
            this.To.Name = "To";
            this.To.ReadOnly = true;
            this.To.Width = 65;
            // 
            // Cost
            // 
            dataGridViewCellStyle33.Format = "C2";
            dataGridViewCellStyle33.NullValue = null;
            this.Cost.DefaultCellStyle = dataGridViewCellStyle33;
            this.Cost.HeaderText = "Стоимость";
            this.Cost.Name = "Cost";
            this.Cost.ReadOnly = true;
            this.Cost.Width = 80;
            // 
            // Percent
            // 
            dataGridViewCellStyle34.Format = "N2";
            dataGridViewCellStyle34.NullValue = null;
            this.Percent.DefaultCellStyle = dataGridViewCellStyle34;
            this.Percent.HeaderText = "Н/С";
            this.Percent.Name = "Percent";
            this.Percent.ReadOnly = true;
            this.Percent.Width = 40;
            // 
            // Price
            // 
            dataGridViewCellStyle35.Format = "C2";
            dataGridViewCellStyle35.NullValue = "0";
            this.Price.DefaultCellStyle = dataGridViewCellStyle35;
            this.Price.HeaderText = "Итого";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 80;
            // 
            // PerMonth
            // 
            this.PerMonth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PerMonth.HeaderText = "Е/м";
            this.PerMonth.Name = "PerMonth";
            // 
            // TotalProfit
            // 
            this.TotalProfit.AllowUserToAddRows = false;
            this.TotalProfit.AllowUserToDeleteRows = false;
            this.TotalProfit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TotalProfit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.PriceFloor1,
            this.PriceFloor2,
            this.PriceFloor3,
            this.Percents,
            this.Total});
            this.TotalProfit.Location = new System.Drawing.Point(6, 11);
            this.TotalProfit.Name = "TotalProfit";
            this.TotalProfit.ReadOnly = true;
            this.TotalProfit.RowHeadersWidth = 4;
            this.TotalProfit.Size = new System.Drawing.Size(531, 271);
            this.TotalProfit.TabIndex = 3;
            // 
            // Date
            // 
            this.Date.HeaderText = "Дата";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // PriceFloor1
            // 
            dataGridViewCellStyle36.Format = "C2";
            dataGridViewCellStyle36.NullValue = "0";
            this.PriceFloor1.DefaultCellStyle = dataGridViewCellStyle36;
            this.PriceFloor1.HeaderText = "1 этаж";
            this.PriceFloor1.Name = "PriceFloor1";
            this.PriceFloor1.ReadOnly = true;
            this.PriceFloor1.Width = 80;
            // 
            // PriceFloor2
            // 
            dataGridViewCellStyle37.Format = "C2";
            dataGridViewCellStyle37.NullValue = "0";
            this.PriceFloor2.DefaultCellStyle = dataGridViewCellStyle37;
            this.PriceFloor2.HeaderText = "2 этаж";
            this.PriceFloor2.Name = "PriceFloor2";
            this.PriceFloor2.ReadOnly = true;
            this.PriceFloor2.Width = 80;
            // 
            // PriceFloor3
            // 
            dataGridViewCellStyle38.Format = "C2";
            dataGridViewCellStyle38.NullValue = "0";
            this.PriceFloor3.DefaultCellStyle = dataGridViewCellStyle38;
            this.PriceFloor3.HeaderText = "3 этаж";
            this.PriceFloor3.Name = "PriceFloor3";
            this.PriceFloor3.ReadOnly = true;
            this.PriceFloor3.Width = 80;
            // 
            // Percents
            // 
            dataGridViewCellStyle39.Format = "C2";
            dataGridViewCellStyle39.NullValue = "0";
            this.Percents.DefaultCellStyle = dataGridViewCellStyle39;
            this.Percents.HeaderText = "Налог";
            this.Percents.Name = "Percents";
            this.Percents.ReadOnly = true;
            this.Percents.Width = 80;
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle40.Format = "C2";
            this.Total.DefaultCellStyle = dataGridViewCellStyle40;
            this.Total.HeaderText = "Всего";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // GanttChart
            // 
            this.GanttChart.AllowManualEditBar = false;
            this.GanttChart.BackColor = System.Drawing.Color.White;
            this.GanttChart.DateFont = new System.Drawing.Font("Verdana", 8F);
            this.GanttChart.FromDate = new System.DateTime(((long)(0)));
            this.GanttChart.Location = new System.Drawing.Point(21, 414);
            this.GanttChart.Margin = new System.Windows.Forms.Padding(2);
            this.GanttChart.Name = "GanttChart";
            this.GanttChart.RowFont = new System.Drawing.Font("Verdana", 8F);
            this.GanttChart.Size = new System.Drawing.Size(1090, 153);
            this.GanttChart.TabIndex = 5;
            this.GanttChart.Text = "GanttChart";
            this.GanttChart.TimeFont = new System.Drawing.Font("Verdana", 8F);
            this.GanttChart.ToDate = new System.DateTime(((long)(0)));
            this.GanttChart.ToolTipText = ((System.Collections.Generic.List<string>)(resources.GetObject("GanttChart.ToolTipText")));
            this.GanttChart.ToolTipTextTitle = "";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 571);
            this.Controls.Add(this.GanttChart);
            this.Controls.Add(this.FunctionsTabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainWindow";
            this.Text = "Аренда помещений ТЦ \"Флагман\"";
            this.FunctionsTabs.ResumeLayout(false);
            this.Rents.ResumeLayout(false);
            this.Floors.ResumeLayout(false);
            this.Costs.ResumeLayout(false);
            this.Costs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CostPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalCounting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CostPriceNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalCosts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalProfit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl FunctionsTabs;
        private System.Windows.Forms.TabPage Rents;
        private System.Windows.Forms.TabControl Floors;
        private System.Windows.Forms.TabPage Floor1;
        private System.Windows.Forms.TabPage Floor2;
        private System.Windows.Forms.TabPage Floor3;
        private System.Windows.Forms.TabPage Costs;
        private System.Windows.Forms.DataGridView TotalCosts;
        private System.Windows.Forms.DataGridView TotalProfit;
        private Gantt.GanttChart GanttChart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker CostDateTo;
        private System.Windows.Forms.DateTimePicker CostDateFrom;
        private System.Windows.Forms.TextBox CostInfoBox;
        private System.Windows.Forms.Button AddCostButton;
        private System.Windows.Forms.NumericUpDown CostPriceNumeric;
        private System.Windows.Forms.DataGridView TotalCounting;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.Label PercentLabel;
        private System.Windows.Forms.ComboBox CostType;
        private System.Windows.Forms.NumericUpDown CostPercent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceFloor1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceFloor2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceFloor3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percents;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Info;
        private System.Windows.Forms.DataGridViewTextBoxColumn From;
        private System.Windows.Forms.DataGridViewTextBoxColumn To;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PerMonth;
    }
}

