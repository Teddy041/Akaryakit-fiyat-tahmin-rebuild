namespace FuelPricePrediction.Forms
{
    partial class frmAnaSayfa
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlForecast = new System.Windows.Forms.Panel();
            this.pnlForecastCard = new System.Windows.Forms.Panel();
            this.pnlChartHost = new System.Windows.Forms.Panel();
            this.chartPredictions = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnlMetrics = new System.Windows.Forms.Panel();
            this.lblMetrics = new System.Windows.Forms.Label();
            this.pnlForecastInfo = new System.Windows.Forms.Panel();
            this.lblForecastTitle = new System.Windows.Forms.Label();
            this.dtpDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnPredict = new Guna.UI2.WinForms.Guna2Button();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblPriceValue = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.lblRangeNote = new System.Windows.Forms.Label();
            this.pnlLive = new System.Windows.Forms.Panel();
            this.pnlLiveCard = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblLiveEmpty = new System.Windows.Forms.Label();
            this.pnlLiveTop = new System.Windows.Forms.Panel();
            this.lblLiveTitle = new System.Windows.Forms.Label();
            this.lblLiveHint = new System.Windows.Forms.Label();
            this.comboBoxCities = new System.Windows.Forms.ComboBox();
            this.btnGetir = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlForecast.SuspendLayout();
            this.pnlForecastCard.SuspendLayout();
            this.pnlChartHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPredictions)).BeginInit();
            this.pnlMetrics.SuspendLayout();
            this.pnlForecastInfo.SuspendLayout();
            this.pnlLive.SuspendLayout();
            this.pnlLiveCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlLiveTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 16, 24, 8);
            this.pnlHeader.Size = new System.Drawing.Size(1120, 72);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(42)))), ((int)(((byte)(51)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(168, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Akaryakıt fiyatı";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(108)))), ((int)(((byte)(120)))));
            this.lblSubtitle.Location = new System.Drawing.Point(24, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(520, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Solda seçilen güne ait tahmin ve yıl grafiği, sağda güncel pompa fiyatı.";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.pnlBody.Controls.Add(this.pnlForecast);
            this.pnlBody.Controls.Add(this.pnlLive);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 72);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(20, 0, 20, 20);
            this.pnlBody.Size = new System.Drawing.Size(1120, 628);
            this.pnlBody.TabIndex = 1;
            // 
            // pnlForecast
            // 
            this.pnlForecast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.pnlForecast.Controls.Add(this.pnlForecastCard);
            this.pnlForecast.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForecast.Location = new System.Drawing.Point(20, 0);
            this.pnlForecast.Name = "pnlForecast";
            this.pnlForecast.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnlForecast.Size = new System.Drawing.Size(740, 608);
            this.pnlForecast.TabIndex = 0;
            // 
            // pnlForecastCard
            // 
            this.pnlForecastCard.BackColor = System.Drawing.Color.White;
            this.pnlForecastCard.Controls.Add(this.pnlChartHost);
            this.pnlForecastCard.Controls.Add(this.pnlMetrics);
            this.pnlForecastCard.Controls.Add(this.pnlForecastInfo);
            this.pnlForecastCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForecastCard.Location = new System.Drawing.Point(0, 0);
            this.pnlForecastCard.Name = "pnlForecastCard";
            this.pnlForecastCard.Size = new System.Drawing.Size(730, 608);
            this.pnlForecastCard.TabIndex = 0;
            // 
            // pnlChartHost
            // 
            this.pnlChartHost.BackColor = System.Drawing.Color.White;
            this.pnlChartHost.Controls.Add(this.chartPredictions);
            this.pnlChartHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChartHost.Location = new System.Drawing.Point(0, 196);
            this.pnlChartHost.Name = "pnlChartHost";
            this.pnlChartHost.Padding = new System.Windows.Forms.Padding(8, 0, 12, 4);
            this.pnlChartHost.Size = new System.Drawing.Size(730, 356);
            this.pnlChartHost.TabIndex = 0;
            // 
            // chartPredictions
            // 
            this.chartPredictions.BackColor = System.Drawing.Color.White;
            this.chartPredictions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPredictions.Location = new System.Drawing.Point(8, 0);
            this.chartPredictions.Name = "chartPredictions";
            this.chartPredictions.Size = new System.Drawing.Size(710, 352);
            this.chartPredictions.TabIndex = 0;
            this.chartPredictions.Text = "chartPredictions";
            // 
            // pnlMetrics
            // 
            this.pnlMetrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlMetrics.Controls.Add(this.lblMetrics);
            this.pnlMetrics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlMetrics.Location = new System.Drawing.Point(0, 552);
            this.pnlMetrics.Name = "pnlMetrics";
            this.pnlMetrics.Padding = new System.Windows.Forms.Padding(20, 8, 16, 8);
            this.pnlMetrics.Size = new System.Drawing.Size(730, 72);
            this.pnlMetrics.TabIndex = 1;
            // 
            // lblMetrics
            // 
            this.lblMetrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMetrics.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMetrics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(108)))), ((int)(((byte)(120)))));
            this.lblMetrics.Location = new System.Drawing.Point(20, 8);
            this.lblMetrics.Name = "lblMetrics";
            this.lblMetrics.Size = new System.Drawing.Size(694, 40);
            this.lblMetrics.TabIndex = 0;
            this.lblMetrics.Text = "Seri yükleniyor…";
            this.lblMetrics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlForecastInfo
            // 
            this.pnlForecastInfo.BackColor = System.Drawing.Color.White;
            this.pnlForecastInfo.Controls.Add(this.lblRangeNote);
            this.pnlForecastInfo.Controls.Add(this.lblResult);
            this.pnlForecastInfo.Controls.Add(this.lblPriceValue);
            this.pnlForecastInfo.Controls.Add(this.btnPredict);
            this.pnlForecastInfo.Controls.Add(this.cmbProduct);
            this.pnlForecastInfo.Controls.Add(this.dtpDate);
            this.pnlForecastInfo.Controls.Add(this.lblForecastTitle);
            this.pnlForecastInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlForecastInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlForecastInfo.Name = "pnlForecastInfo";
            this.pnlForecastInfo.Size = new System.Drawing.Size(730, 196);
            this.pnlForecastInfo.TabIndex = 2;
            // 
            // lblForecastTitle
            // 
            this.lblForecastTitle.AutoSize = true;
            this.lblForecastTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblForecastTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(42)))), ((int)(((byte)(51)))));
            this.lblForecastTitle.Location = new System.Drawing.Point(20, 16);
            this.lblForecastTitle.Name = "lblForecastTitle";
            this.lblForecastTitle.Size = new System.Drawing.Size(62, 21);
            this.lblForecastTitle.TabIndex = 0;
            this.lblForecastTitle.Text = "Tahmin";
            // 
            // dtpDate
            // 
            this.dtpDate.BorderRadius = 4;
            this.dtpDate.Checked = true;
            this.dtpDate.FillColor = System.Drawing.Color.White;
            this.dtpDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(42)))), ((int)(((byte)(51)))));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDate.Location = new System.Drawing.Point(20, 50);
            this.dtpDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(250, 36);
            this.dtpDate.TabIndex = 1;
            this.dtpDate.Value = new System.DateTime(2026, 10, 15, 0, 0, 0, 0);
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProduct.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Items.AddRange(new object[] {
            "Benzin",
            "Dizel"});
            this.cmbProduct.Location = new System.Drawing.Point(450, 50);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(140, 25);
            this.cmbProduct.TabIndex = 6;
            // 
            // btnPredict
            // 
            this.btnPredict.BorderRadius = 4;
            this.btnPredict.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPredict.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPredict.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPredict.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(188)))), ((int)(((byte)(196)))));
            this.btnPredict.DisabledState.ForeColor = System.Drawing.Color.White;
            this.btnPredict.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(122)))));
            this.btnPredict.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPredict.ForeColor = System.Drawing.Color.White;
            this.btnPredict.Location = new System.Drawing.Point(282, 50);
            this.btnPredict.Name = "btnPredict";
            this.btnPredict.Size = new System.Drawing.Size(156, 36);
            this.btnPredict.TabIndex = 2;
            this.btnPredict.Text = "Tahmini göster";
            this.btnPredict.Click += new System.EventHandler(this.btnPredict_Click);
            // 
            // lblPriceValue
            // 
            this.lblPriceValue.AutoSize = true;
            this.lblPriceValue.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblPriceValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(42)))), ((int)(((byte)(51)))));
            this.lblPriceValue.Location = new System.Drawing.Point(16, 98);
            this.lblPriceValue.Name = "lblPriceValue";
            this.lblPriceValue.Size = new System.Drawing.Size(36, 40);
            this.lblPriceValue.TabIndex = 3;
            this.lblPriceValue.Text = "—";
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(42)))), ((int)(((byte)(51)))));
            this.lblResult.Location = new System.Drawing.Point(20, 146);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(690, 22);
            this.lblResult.TabIndex = 4;
            this.lblResult.Text = "Bir tarih seçin. Grafik o yılın her gününü çizer.";
            // 
            // lblRangeNote
            // 
            this.lblRangeNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRangeNote.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblRangeNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(108)))), ((int)(((byte)(120)))));
            this.lblRangeNote.Location = new System.Drawing.Point(20, 168);
            this.lblRangeNote.Name = "lblRangeNote";
            this.lblRangeNote.Size = new System.Drawing.Size(690, 20);
            this.lblRangeNote.TabIndex = 5;
            this.lblRangeNote.Text = "Çizgi modelin fiyatı, turuncu nokta seçilen gün.";
            // 
            // pnlLive
            // 
            this.pnlLive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.pnlLive.Controls.Add(this.pnlLiveCard);
            this.pnlLive.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLive.Location = new System.Drawing.Point(760, 0);
            this.pnlLive.Name = "pnlLive";
            this.pnlLive.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlLive.Size = new System.Drawing.Size(420, 608);
            this.pnlLive.TabIndex = 1;
            // 
            // pnlLiveCard
            // 
            this.pnlLiveCard.BackColor = System.Drawing.Color.White;
            this.pnlLiveCard.Controls.Add(this.dataGridView1);
            this.pnlLiveCard.Controls.Add(this.lblLiveEmpty);
            this.pnlLiveCard.Controls.Add(this.pnlLiveTop);
            this.pnlLiveCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLiveCard.Location = new System.Drawing.Point(10, 0);
            this.pnlLiveCard.Name = "pnlLiveCard";
            this.pnlLiveCard.Size = new System.Drawing.Size(330, 608);
            this.pnlLiveCard.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersHeight = 36;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(236)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 112);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(330, 496);
            this.dataGridView1.TabIndex = 0;
            // 
            // lblLiveEmpty
            // 
            this.lblLiveEmpty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLiveEmpty.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLiveEmpty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(108)))), ((int)(((byte)(120)))));
            this.lblLiveEmpty.Location = new System.Drawing.Point(0, 112);
            this.lblLiveEmpty.Name = "lblLiveEmpty";
            this.lblLiveEmpty.Padding = new System.Windows.Forms.Padding(24);
            this.lblLiveEmpty.Size = new System.Drawing.Size(330, 496);
            this.lblLiveEmpty.TabIndex = 1;
            this.lblLiveEmpty.Text = "Henüz liste yok.\r\nFiyatları getir, Kadıköy benzin ve dizel fiyatlarını yükler.";
            this.lblLiveEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLiveTop
            // 
            this.pnlLiveTop.BackColor = System.Drawing.Color.White;
            this.pnlLiveTop.Controls.Add(this.btnGetir);
            this.pnlLiveTop.Controls.Add(this.comboBoxCities);
            this.pnlLiveTop.Controls.Add(this.lblLiveHint);
            this.pnlLiveTop.Controls.Add(this.lblLiveTitle);
            this.pnlLiveTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLiveTop.Location = new System.Drawing.Point(0, 0);
            this.pnlLiveTop.Name = "pnlLiveTop";
            this.pnlLiveTop.Size = new System.Drawing.Size(410, 124);
            this.pnlLiveTop.TabIndex = 2;
            // 
            // lblLiveTitle
            // 
            this.lblLiveTitle.AutoSize = true;
            this.lblLiveTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLiveTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(42)))), ((int)(((byte)(51)))));
            this.lblLiveTitle.Location = new System.Drawing.Point(16, 16);
            this.lblLiveTitle.Name = "lblLiveTitle";
            this.lblLiveTitle.Size = new System.Drawing.Size(99, 21);
            this.lblLiveTitle.TabIndex = 0;
            this.lblLiveTitle.Text = "Güncel pompa";
            // 
            // lblLiveHint
            // 
            this.lblLiveHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblLiveHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(108)))), ((int)(((byte)(120)))));
            this.lblLiveHint.Location = new System.Drawing.Point(16, 40);
            this.lblLiveHint.Name = "lblLiveHint";
            this.lblLiveHint.Size = new System.Drawing.Size(380, 32);
            this.lblLiveHint.TabIndex = 1;
            this.lblLiveHint.Text = "Kadıköy benzin ve dizel, TL / litre";
            // 
            // comboBoxCities
            // 
            this.comboBoxCities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxCities.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.comboBoxCities.FormattingEnabled = true;
            this.comboBoxCities.Items.AddRange(new object[] {
            "İstanbul"});
            this.comboBoxCities.Location = new System.Drawing.Point(16, 80);
            this.comboBoxCities.Name = "comboBoxCities";
            this.comboBoxCities.Size = new System.Drawing.Size(140, 25);
            this.comboBoxCities.TabIndex = 2;
            // 
            // btnGetir
            // 
            this.btnGetir.BorderRadius = 4;
            this.btnGetir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetir.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGetir.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGetir.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(188)))), ((int)(((byte)(196)))));
            this.btnGetir.DisabledState.ForeColor = System.Drawing.Color.White;
            this.btnGetir.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(122)))));
            this.btnGetir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGetir.ForeColor = System.Drawing.Color.White;
            this.btnGetir.Location = new System.Drawing.Point(168, 76);
            this.btnGetir.Name = "btnGetir";
            this.btnGetir.Size = new System.Drawing.Size(142, 32);
            this.btnGetir.TabIndex = 3;
            this.btnGetir.Text = "Fiyatları getir";
            this.btnGetir.Click += new System.EventHandler(this.btnGetir_Click);
            // 
            // frmAnaSayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1120, 700);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(980, 640);
            this.Name = "frmAnaSayfa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Akaryakıt fiyatı";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlForecast.ResumeLayout(false);
            this.pnlForecastCard.ResumeLayout(false);
            this.pnlChartHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPredictions)).EndInit();
            this.pnlMetrics.ResumeLayout(false);
            this.pnlForecastInfo.ResumeLayout(false);
            this.pnlForecastInfo.PerformLayout();
            this.pnlLive.ResumeLayout(false);
            this.pnlLiveCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlLiveTop.ResumeLayout(false);
            this.pnlLiveTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Panel pnlForecast;
        private System.Windows.Forms.Panel pnlForecastCard;
        private System.Windows.Forms.Panel pnlChartHost;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPredictions;
        private System.Windows.Forms.Panel pnlMetrics;
        private System.Windows.Forms.Label lblMetrics;
        private System.Windows.Forms.Panel pnlForecastInfo;
        private System.Windows.Forms.Label lblForecastTitle;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDate;
        private Guna.UI2.WinForms.Guna2Button btnPredict;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblPriceValue;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblRangeNote;
        private System.Windows.Forms.Panel pnlLive;
        private System.Windows.Forms.Panel pnlLiveCard;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblLiveEmpty;
        private System.Windows.Forms.Panel pnlLiveTop;
        private System.Windows.Forms.Label lblLiveTitle;
        private System.Windows.Forms.Label lblLiveHint;
        private System.Windows.Forms.ComboBox comboBoxCities;
        private Guna.UI2.WinForms.Guna2Button btnGetir;
    }
}
