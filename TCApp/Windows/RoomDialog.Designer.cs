namespace RentCenter.Window
{
    partial class RoomDialog
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.RentList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RentStart = new System.Windows.Forms.DateTimePicker();
            this.RentEnd = new System.Windows.Forms.DateTimePicker();
            this.RentColor = new System.Windows.Forms.ComboBox();
            this.RentName = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.DelButton = new System.Windows.Forms.Button();
            this.RentPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.RentList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RentList.FormattingEnabled = true;
            this.RentList.Location = new System.Drawing.Point(103, 12);
            this.RentList.Name = "RentList";
            this.RentList.Size = new System.Drawing.Size(171, 21);
            this.RentList.TabIndex = 0;
            this.RentList.SelectedIndexChanged += new System.EventHandler(this.SelectRent);

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Арендатель";

            this.RentStart.Location = new System.Drawing.Point(103, 65);
            this.RentStart.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.RentStart.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.RentStart.Name = "RentStart";
            this.RentStart.Size = new System.Drawing.Size(171, 20);
            this.RentStart.TabIndex = 2;
            this.RentStart.Value = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);

            this.RentEnd.Location = new System.Drawing.Point(103, 93);
            this.RentEnd.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.RentEnd.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.RentEnd.Name = "RentEnd";
            this.RentEnd.Size = new System.Drawing.Size(171, 20);
            this.RentEnd.TabIndex = 3;
            this.RentEnd.Value = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);

            this.RentColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.RentColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RentColor.FormattingEnabled = true;
            this.RentColor.Location = new System.Drawing.Point(103, 119);
            this.RentColor.Name = "RentColor";
            this.RentColor.Size = new System.Drawing.Size(171, 21);
            this.RentColor.TabIndex = 4;

            this.RentName.Location = new System.Drawing.Point(103, 39);
            this.RentName.Name = "RentName";
            this.RentName.Size = new System.Drawing.Size(171, 20);
            this.RentName.TabIndex = 5;

            this.AddButton.Location = new System.Drawing.Point(15, 206);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(115, 23);
            this.AddButton.TabIndex = 6;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddRent);

            this.DelButton.Location = new System.Drawing.Point(150, 206);
            this.DelButton.Name = "DelButton";
            this.DelButton.Size = new System.Drawing.Size(125, 23);
            this.DelButton.TabIndex = 7;
            this.DelButton.Text = "Удалить";
            this.DelButton.UseVisualStyleBackColor = true;
            this.DelButton.Click += new System.EventHandler(this.DelRent);

            this.RentPrice.Enabled = false;
            this.RentPrice.Location = new System.Drawing.Point(103, 146);
            this.RentPrice.Name = "RentPrice";
            this.RentPrice.Size = new System.Drawing.Size(171, 20);
            this.RentPrice.TabIndex = 8;

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Имя";

            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Начало аренды";

            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Конец аренды";

            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Цвет";

            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Стоимость";

            this.ChangeButton.Location = new System.Drawing.Point(15, 177);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(260, 23);
            this.ChangeButton.TabIndex = 14;
            this.ChangeButton.Text = "Изменить";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeRent);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 240);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RentPrice);
            this.Controls.Add(this.DelButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.RentName);
            this.Controls.Add(this.RentColor);
            this.Controls.Add(this.RentEnd);
            this.Controls.Add(this.RentStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RentList);
            this.Name = "RoomDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Аренда помещения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox RentList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker RentStart;
        private System.Windows.Forms.DateTimePicker RentEnd;
        private System.Windows.Forms.ComboBox RentColor;
        private System.Windows.Forms.TextBox RentName;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DelButton;
        private System.Windows.Forms.TextBox RentPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ChangeButton;
    }
}