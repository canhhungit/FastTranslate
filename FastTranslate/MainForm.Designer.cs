namespace FastTranslate
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ResourceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeafResource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnOpenReferenceFile = new System.Windows.Forms.Button();
            this.btnOpenFileToTranslate = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnTrans = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.percent = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ResourceName,
            this.LeafResource,
            this.Value1,
            this.Value2,
            this.Value3,
            this.Value4});
            this.dataGridView1.DataSource = this.bindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(13, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1062, 532);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // ResourceName
            // 
            this.ResourceName.DataPropertyName = "ResourceName";
            this.ResourceName.HeaderText = "Resource name";
            this.ResourceName.Name = "ResourceName";
            this.ResourceName.Width = 300;
            // 
            // LeafResource
            // 
            this.LeafResource.DataPropertyName = "LeafResource";
            this.LeafResource.HeaderText = "Leaf resource";
            this.LeafResource.Name = "LeafResource";
            this.LeafResource.ReadOnly = true;
            // 
            // Value1
            // 
            this.Value1.DataPropertyName = "Value1";
            this.Value1.HeaderText = "Language1";
            this.Value1.Name = "Value1";
            this.Value1.Width = 300;
            // 
            // Value2
            // 
            this.Value2.DataPropertyName = "Value2";
            this.Value2.HeaderText = "Language2";
            this.Value2.Name = "Value2";
            this.Value2.Width = 300;
            // 
            // Value3
            // 
            this.Value3.DataPropertyName = "Value3";
            this.Value3.HeaderText = "Language3";
            this.Value3.Name = "Value3";
            this.Value3.Visible = false;
            this.Value3.Width = 300;
            // 
            // Value4
            // 
            this.Value4.DataPropertyName = "Value4";
            this.Value4.HeaderText = "Language4";
            this.Value4.Name = "Value4";
            this.Value4.Visible = false;
            // 
            // btnOpenReferenceFile
            // 
            this.btnOpenReferenceFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenReferenceFile.Location = new System.Drawing.Point(13, 12);
            this.btnOpenReferenceFile.Name = "btnOpenReferenceFile";
            this.btnOpenReferenceFile.Size = new System.Drawing.Size(145, 23);
            this.btnOpenReferenceFile.TabIndex = 0;
            this.btnOpenReferenceFile.Text = "Open reference file";
            this.toolTip1.SetToolTip(this.btnOpenReferenceFile, "Open a resource file to serve as a reference when translating. Reference strings " +
        "will appear on the LEFT side.");
            this.btnOpenReferenceFile.UseVisualStyleBackColor = true;
            this.btnOpenReferenceFile.Click += new System.EventHandler(this.btnOpenReferenceFile_Click);
            // 
            // btnOpenFileToTranslate
            // 
            this.btnOpenFileToTranslate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenFileToTranslate.Location = new System.Drawing.Point(164, 12);
            this.btnOpenFileToTranslate.Name = "btnOpenFileToTranslate";
            this.btnOpenFileToTranslate.Size = new System.Drawing.Size(145, 23);
            this.btnOpenFileToTranslate.TabIndex = 1;
            this.btnOpenFileToTranslate.Text = "Open file to translate";
            this.toolTip1.SetToolTip(this.btnOpenFileToTranslate, "Open a resource file with any translation done so far, or from a previous version" +
        ". Translated resources will appear on the RIGHT side.");
            this.btnOpenFileToTranslate.UseVisualStyleBackColor = true;
            this.btnOpenFileToTranslate.Click += new System.EventHandler(this.btnOpenFileToTranslate_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML files|*.xml|All files|*.*";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(315, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save translation...";
            this.toolTip1.SetToolTip(this.btnSave, "Save the translated strings from the RIGHT side. Only non-empty strings will be s" +
        "aved.");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.Filter = "XML files|*.xml";
            // 
            // btnTrans
            // 
            this.btnTrans.Location = new System.Drawing.Point(768, 12);
            this.btnTrans.Name = "btnTrans";
            this.btnTrans.Size = new System.Drawing.Size(75, 23);
            this.btnTrans.TabIndex = 10;
            this.btnTrans.Text = "Translate";
            this.btnTrans.UseVisualStyleBackColor = true;
            this.btnTrans.Click += new System.EventHandler(this.btnTrans_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "en",
            "af",
            "sq",
            "am",
            "ar",
            "hy",
            "az",
            "eu",
            "be",
            "bn",
            "bs",
            "bg",
            "ca",
            "ceb",
            "ny",
            "co",
            "hr",
            "cs",
            "da",
            "nl",
            "eo",
            "et",
            "tl",
            "fi",
            "fr",
            "fy",
            "gl",
            "ka",
            "de",
            "el",
            "gu",
            "ht",
            "ha",
            "haw",
            "iw",
            "hi",
            "hmn",
            "hu",
            "is",
            "ig",
            "id",
            "ga",
            "it",
            "ja",
            "jw",
            "kn",
            "kk",
            "km",
            "ko",
            "ku",
            "ky",
            "lo",
            "la",
            "lv",
            "lt",
            "lb",
            "mk",
            "mg",
            "ms",
            "ml",
            "mt",
            "mi",
            "mr",
            "mn",
            "my",
            "ne",
            "no",
            "ps",
            "fa",
            "pl",
            "pt",
            "pa",
            "ro",
            "ru",
            "sm",
            "gd",
            "sr",
            "st",
            "sn",
            "sd",
            "si",
            "sk",
            "sl",
            "so",
            "es",
            "su",
            "sw",
            "sv",
            "tg",
            "ta",
            "te",
            "th",
            "tr",
            "uk",
            "ur",
            "uz",
            "vi",
            "cy",
            "xh",
            "yi",
            "yo",
            "zu"});
            this.comboBox1.Location = new System.Drawing.Point(462, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(147, 21);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.Text = "Select Language From";
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteCustomSource.AddRange(new string[] {
            "vi",
            "af",
            "sq",
            "am",
            "ar",
            "hy",
            "az",
            "eu",
            "be",
            "bn",
            "bs",
            "bg",
            "ca",
            "ceb",
            "ny",
            "co",
            "hr",
            "cs",
            "da",
            "nl",
            "en",
            "eo",
            "et",
            "tl",
            "fi",
            "fr",
            "fy",
            "gl",
            "ka",
            "de",
            "el",
            "gu",
            "ht",
            "ha",
            "haw",
            "iw",
            "hi",
            "hmn",
            "hu",
            "is",
            "ig",
            "id",
            "ga",
            "it",
            "ja",
            "jw",
            "kn",
            "kk",
            "km",
            "ko",
            "ku",
            "ky",
            "lo",
            "la",
            "lv",
            "lt",
            "lb",
            "mk",
            "mg",
            "ms",
            "ml",
            "mt",
            "mi",
            "mr",
            "mn",
            "my",
            "ne",
            "no",
            "ps",
            "fa",
            "pl",
            "pt",
            "pa",
            "ro",
            "ru",
            "sm",
            "gd",
            "sr",
            "st",
            "sn",
            "sd",
            "si",
            "sk",
            "sl",
            "so",
            "es",
            "su",
            "sw",
            "sv",
            "tg",
            "ta",
            "te",
            "th",
            "tr",
            "uk",
            "ur",
            "uz",
            "cy",
            "xh",
            "yi",
            "yo",
            "zu"});
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "vi",
            "af",
            "sq",
            "am",
            "ar",
            "hy",
            "az",
            "eu",
            "be",
            "bn",
            "bs",
            "bg",
            "ca",
            "ceb",
            "ny",
            "co",
            "hr",
            "cs",
            "da",
            "nl",
            "en",
            "eo",
            "et",
            "tl",
            "fi",
            "fr",
            "fy",
            "gl",
            "ka",
            "de",
            "el",
            "gu",
            "ht",
            "ha",
            "haw",
            "iw",
            "hi",
            "hmn",
            "hu",
            "is",
            "ig",
            "id",
            "ga",
            "it",
            "ja",
            "jw",
            "kn",
            "kk",
            "km",
            "ko",
            "ku",
            "ky",
            "lo",
            "la",
            "lv",
            "lt",
            "lb",
            "mk",
            "mg",
            "ms",
            "ml",
            "mt",
            "mi",
            "mr",
            "mn",
            "my",
            "ne",
            "no",
            "ps",
            "fa",
            "pl",
            "pt",
            "pa",
            "ro",
            "ru",
            "sm",
            "gd",
            "sr",
            "st",
            "sn",
            "sd",
            "si",
            "sk",
            "sl",
            "so",
            "es",
            "su",
            "sw",
            "sv",
            "tg",
            "ta",
            "te",
            "th",
            "tr",
            "uk",
            "ur",
            "uz",
            "cy",
            "xh",
            "yi",
            "yo",
            "zu"});
            this.comboBox2.Location = new System.Drawing.Point(615, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(147, 21);
            this.comboBox2.TabIndex = 12;
            this.comboBox2.Text = "Select Language To";
            // 
            // percent
            // 
            this.percent.AutoSize = true;
            this.percent.Location = new System.Drawing.Point(955, 17);
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(31, 13);
            this.percent.TabIndex = 13;
            this.percent.Text = "%%%";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1000, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(849, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 585);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.percent);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnTrans);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpenFileToTranslate);
            this.Controls.Add(this.btnOpenReferenceFile);
            this.Controls.Add(this.dataGridView1);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FastTranslate edit by CanhHungIT for any nopCommerce";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnOpenReferenceFile;
        private System.Windows.Forms.Button btnOpenFileToTranslate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResourceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeafResource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value4;
        private System.Windows.Forms.Button btnTrans;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label percent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

