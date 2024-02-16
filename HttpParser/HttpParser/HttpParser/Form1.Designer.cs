namespace HttpParser
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nUDDeep = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.tBLink = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nUDPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLinks = new System.Windows.Forms.RichTextBox();
            this.tbInfo = new System.Windows.Forms.RichTextBox();
            this.tbStatus = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDDeep)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDPort)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.Firebrick;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonStart.Location = new System.Drawing.Point(297, 237);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(131, 34);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Запуск";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nUDDeep);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(17, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(420, 110);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ограничения";
            // 
            // nUDDeep
            // 
            this.nUDDeep.Location = new System.Drawing.Point(9, 58);
            this.nUDDeep.Margin = new System.Windows.Forms.Padding(4);
            this.nUDDeep.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUDDeep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDDeep.Name = "nUDDeep";
            this.nUDDeep.Size = new System.Drawing.Size(194, 31);
            this.nUDDeep.TabIndex = 2;
            this.nUDDeep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Глубина вложенности";
            // 
            // tBLink
            // 
            this.tBLink.Location = new System.Drawing.Point(9, 33);
            this.tBLink.Margin = new System.Windows.Forms.Padding(4);
            this.tBLink.Name = "tBLink";
            this.tBLink.Size = new System.Drawing.Size(343, 31);
            this.tBLink.TabIndex = 4;
            this.tBLink.Text = "http://imc.ssau.ru";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tBLink);
            this.groupBox2.Location = new System.Drawing.Point(17, 136);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(420, 82);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Введите доменное имя";
            // 
            // nUDPort
            // 
            this.nUDPort.Location = new System.Drawing.Point(26, 246);
            this.nUDPort.Margin = new System.Windows.Forms.Padding(4);
            this.nUDPort.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nUDPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUDPort.Name = "nUDPort";
            this.nUDPort.Size = new System.Drawing.Size(181, 31);
            this.nUDPort.TabIndex = 7;
            this.nUDPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 224);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "ПОРТ";
            // 
            // tbLinks
            // 
            this.tbLinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbLinks.ForeColor = System.Drawing.SystemColors.MenuText;
            this.tbLinks.Location = new System.Drawing.Point(463, 28);
            this.tbLinks.Margin = new System.Windows.Forms.Padding(4);
            this.tbLinks.Name = "tbLinks";
            this.tbLinks.Size = new System.Drawing.Size(754, 526);
            this.tbLinks.TabIndex = 10;
            this.tbLinks.Text = "";
            // 
            // tbInfo
            // 
            this.tbInfo.Location = new System.Drawing.Point(17, 304);
            this.tbInfo.Margin = new System.Windows.Forms.Padding(4);
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(418, 250);
            this.tbInfo.TabIndex = 11;
            this.tbInfo.Text = "";
            // 
            // tbStatus
            // 
            this.tbStatus.Location = new System.Drawing.Point(17, 594);
            this.tbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.Size = new System.Drawing.Size(1200, 89);
            this.tbStatus.TabIndex = 12;
            this.tbStatus.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 572);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 23);
            this.label1.TabIndex = 13;
            this.label1.Text = "Статус";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 285);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 23);
            this.label4.TabIndex = 14;
            this.label4.Text = "Информация";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(460, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 23);
            this.label5.TabIndex = 15;
            this.label5.Text = "Ссылки";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(1236, 698);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.tbInfo);
            this.Controls.Add(this.tbLinks);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nUDPort);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonStart);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "HttpParser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDDeep)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUDPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nUDDeep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tBLink;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nUDPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox tbLinks;
        private System.Windows.Forms.RichTextBox tbInfo;
        private System.Windows.Forms.RichTextBox tbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

