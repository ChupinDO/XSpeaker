namespace XSpeaker
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.BuddyList = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.служебнаяИнформацияКлиентаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.чеНитьБудетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьСписокПользователейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.режимадминистратораToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выйтиИзРежимаАдминистратораToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.руководствоПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.показатьОкноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.AdminModLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StateTimer = new System.Windows.Forms.Timer(this.components);
            this.ConnectingTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BuddyList
            // 
            this.BuddyList.ContextMenuStrip = this.contextMenuStrip1;
            this.BuddyList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BuddyList.FullRowSelect = true;
            this.BuddyList.GridLines = true;
            this.BuddyList.Location = new System.Drawing.Point(9, 27);
            this.BuddyList.Name = "BuddyList";
            this.BuddyList.Size = new System.Drawing.Size(227, 223);
            this.BuddyList.TabIndex = 0;
            this.BuddyList.UseCompatibleStateImageBehavior = false;
            this.BuddyList.View = System.Windows.Forms.View.List;
            this.BuddyList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.BuddyList_ItemSelectionChanged);
            this.BuddyList.SelectedIndexChanged += new System.EventHandler(this.BuddyList_SelectedIndexChanged);
            this.BuddyList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BuddyList_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.служебнаяИнформацияКлиентаToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(206, 54);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 22);
            this.toolStripMenuItem1.Text = "Клиент";
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // служебнаяИнформацияКлиентаToolStripMenuItem
            // 
            this.служебнаяИнформацияКлиентаToolStripMenuItem.Name = "служебнаяИнформацияКлиентаToolStripMenuItem";
            this.служебнаяИнформацияКлиентаToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.служебнаяИнформацияКлиентаToolStripMenuItem.Text = "Информация о клиенте";
            this.служебнаяИнформацияКлиентаToolStripMenuItem.Click += new System.EventHandler(this.служебнаяИнформацияКлиентаToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.чеНитьБудетToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(242, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // чеНитьБудетToolStripMenuItem
            // 
            this.чеНитьБудетToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьСписокПользователейToolStripMenuItem,
            this.режимадминистратораToolStripMenuItem,
            this.выйтиИзРежимаАдминистратораToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripMenuItem3});
            this.чеНитьБудетToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.чеНитьБудетToolStripMenuItem.Name = "чеНитьБудетToolStripMenuItem";
            this.чеНитьБудетToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.чеНитьБудетToolStripMenuItem.Text = "Файл";
            // 
            // обновитьСписокПользователейToolStripMenuItem
            // 
            this.обновитьСписокПользователейToolStripMenuItem.Name = "обновитьСписокПользователейToolStripMenuItem";
            this.обновитьСписокПользователейToolStripMenuItem.ShortcutKeyDisplayString = "(F5)";
            this.обновитьСписокПользователейToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.обновитьСписокПользователейToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.обновитьСписокПользователейToolStripMenuItem.Text = "Обновить список клиентов";
            this.обновитьСписокПользователейToolStripMenuItem.ToolTipText = "Обновление списка клиентов, доступных для звонка.";
            this.обновитьСписокПользователейToolStripMenuItem.Click += new System.EventHandler(this.обновитьСписокПользователейToolStripMenuItem_Click);
            // 
            // режимадминистратораToolStripMenuItem
            // 
            this.режимадминистратораToolStripMenuItem.Name = "режимадминистратораToolStripMenuItem";
            this.режимадминистратораToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.режимадминистратораToolStripMenuItem.Text = "Режим администратора";
            this.режимадминистратораToolStripMenuItem.ToolTipText = "В режиме администратора доступно добавление/удаление клиентов.";
            this.режимадминистратораToolStripMenuItem.Click += new System.EventHandler(this.админмодToolStripMenuItem_Click);
            // 
            // выйтиИзРежимаАдминистратораToolStripMenuItem
            // 
            this.выйтиИзРежимаАдминистратораToolStripMenuItem.Name = "выйтиИзРежимаАдминистратораToolStripMenuItem";
            this.выйтиИзРежимаАдминистратораToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.выйтиИзРежимаАдминистратораToolStripMenuItem.Text = "Выйти из режима администратора";
            this.выйтиИзРежимаАдминистратораToolStripMenuItem.Visible = false;
            this.выйтиИзРежимаАдминистратораToolStripMenuItem.Click += new System.EventHandler(this.выйтиИзРежимаАдминистратораToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(262, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.ShortcutKeyDisplayString = "(Ctrl+E)";
            this.toolStripMenuItem3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.toolStripMenuItem3.Size = new System.Drawing.Size(265, 22);
            this.toolStripMenuItem3.Text = "Выход";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.руководствоПользователяToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.справкаToolStripMenuItem.Text = "Помощь";
            // 
            // руководствоПользователяToolStripMenuItem
            // 
            this.руководствоПользователяToolStripMenuItem.Name = "руководствоПользователяToolStripMenuItem";
            this.руководствоПользователяToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.руководствоПользователяToolStripMenuItem.Text = "Руководство пользователя";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip2;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.показатьОкноToolStripMenuItem,
            this.toolStripSeparator3,
            this.выходToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(155, 54);
            // 
            // показатьОкноToolStripMenuItem
            // 
            this.показатьОкноToolStripMenuItem.Name = "показатьОкноToolStripMenuItem";
            this.показатьОкноToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.показатьОкноToolStripMenuItem.Text = "Показать окно";
            this.показатьОкноToolStripMenuItem.Click += new System.EventHandler(this.показатьОкноToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(151, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.progressBar1.Location = new System.Drawing.Point(9, 227);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(227, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // AdminModLabel
            // 
            this.AdminModLabel.AutoSize = true;
            this.AdminModLabel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.AdminModLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AdminModLabel.Location = new System.Drawing.Point(135, 3);
            this.AdminModLabel.Name = "AdminModLabel";
            this.AdminModLabel.Size = new System.Drawing.Size(105, 15);
            this.AdminModLabel.TabIndex = 5;
            this.AdminModLabel.Text = "[администратор]";
            this.AdminModLabel.Visible = false;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 337);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Состояние:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(85, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "текущее состояние";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 315);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(242, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(69, 17);
            this.toolStripStatusLabel1.Text = "Состояние:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(158, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "...";
            // 
            // StateTimer
            // 
            this.StateTimer.Enabled = true;
            this.StateTimer.Interval = 1000;
            this.StateTimer.Tick += new System.EventHandler(this.StateTimer_Tick);
            // 
            // ConnectingTimer
            // 
            this.ConnectingTimer.Interval = 10000;
            this.ConnectingTimer.Tick += new System.EventHandler(this.ConnectingTimer_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.BackgroundImage = global::XSpeaker.Properties.Resources.microphoneblue_5728;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(9, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 57);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(242, 337);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AdminModLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BuddyList);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView BuddyList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem чеНитьБудетToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem служебнаяИнформацияКлиентаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обновитьСписокПользователейToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.ToolStripMenuItem режимадминистратораToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem выйтиИзРежимаАдминистратораToolStripMenuItem;
        public System.Windows.Forms.Label AdminModLabel;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem показатьОкноToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Timer StateTimer;
        private System.Windows.Forms.Timer ConnectingTimer;
        private System.Windows.Forms.ToolStripMenuItem руководствоПользователяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
    }
}

