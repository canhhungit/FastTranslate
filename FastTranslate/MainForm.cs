/*
 * FastTranslate - This software is useful when translating language XML files, providing reference language and searchability.
 * Copyright (C) 2012 Markus Kvist, StjärnDistribution AB
 * Contact: markus.kvist@sdist.se
 * 
 * This program is free software; you can redistribute it and/or modify it 
 * under the terms of the GNU General Public License as published by the Free Software Foundation; 
 * either version 2 of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this program; 
 * if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace FastTranslate
{
    public partial class MainForm : Form
    {
        #region Private variables

        private static readonly string[] InvalidLeafResources = new string[] {
            "Hint", "Text", "Title", "ErrorMessage", "Required" };
        private DataTable _table;
        private Dictionary<string, DataRow> _rows;
        private string _searchText;

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            InitializeVariables();
        }

        #endregion

        #region Event handlers

        private void btnOpenReferenceFile_Click(object sender, EventArgs e)
        {
            OpenFile(1);
        }

        private void btnOpenFileToTranslate_Click(object sender, EventArgs e)
        {
            OpenFile(2);
        }

        private void btnOpenFile3_Click(object sender, EventArgs e)
        {
            Value3.Visible = true;
            OpenFile(3);
        }

        private void btnOpenFile4_Click(object sender, EventArgs e)
        {
            Value4.Visible = true;
            OpenFile(4);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DoSave(true);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            DoFind();
        }

        private void btnRemoveOrphanLines_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Do you really want to delete all orphan lines?", "Delete",
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int count = 0;
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    if (Convert.ToString(row.Cells[Value1.Name].Value) == string.Empty)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        count++;
                    }
                }
                MessageBox.Show(string.Format(
                    "{0} rows were removed.", count), "Remove orphan lines");
            }
        }

        private void btnFillBlanks_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Do you really want to fill all blanks?", "Fill blanks",
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int count = 0;
                foreach (DataRow row in _table.Rows)
                {
                    if (Convert.ToString(row[3]) == string.Empty)
                    {
                        row[3] = Convert.ToString(row[2]);
                        //targetCell.Style.BackColor = Color.Moccasin;
                        count++;
                    }
                }
                MessageBox.Show(string.Format(
                    "{0} strings were copied.", count), "Fill blanks");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.BeginEdit(true);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.X)
                {
                    // Cut
                    if (!dataGridView1.IsCurrentCellInEditMode)
                    {
                        // Get selected cells to clipboard
                        Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                        // Clear selected cells
                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            if (!cell.ReadOnly)
                                cell.Value = string.Empty;
                        }
                        e.Handled = true;
                    }
                }
                else if (e.KeyCode == Keys.V)
                {
                    // Paste
                    if (!dataGridView1.IsCurrentCellInEditMode)
                    {
                        // TODO: A better method would be a combination of below and this:
                        // http://www.codeproject.com/KB/office/DataGridViewCopyPaste.aspx
                        foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                        {
                            if (!cell.ReadOnly)
                                cell.Value = Clipboard.GetText();
                        }
                        e.Handled = true;
                    }
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if ((e.Modifiers & Keys.Control) == Keys.Control)
                {
                    if (!dataGridView1.IsCurrentCellInEditMode ||
                        dataGridView1.EndEdit())
                    {
                        bool findPrevious = (e.Modifiers & Keys.Shift) == Keys.Shift;
                        FindNextToTranslate(findPrevious);
                        e.Handled = true;
                    }
                }
            }
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    // Save
                    DoSave(false);
                    e.Handled = true;
                }
            }
            if (e.Modifiers == Keys.Shift)
            {
                if (e.KeyCode == Keys.F3)
                {
                    // Find next
                    FindNext(true);
                    e.Handled = true;
                }
            }
            if (e.Modifiers == Keys.None)
            {
                if (e.KeyCode == Keys.F3)
                {
                    // Find next
                    FindNext(false);
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    // Clear selected cells
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (!cell.IsInEditMode && !cell.ReadOnly)
                        {
                            cell.Value = string.Empty;
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.F)
                {
                    DoFind();
                    e.Handled = true;
                }
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 3)// Value 2
            {
                if (Convert.ToString(e.Value) == string.Empty)
                    e.CellStyle.BackColor = Color.Beige;
                else
                    e.CellStyle.BackColor = Color.White;
            }
        }

        #endregion

        #region Private methods

        private void InitializeVariables()
        {
            _rows = new Dictionary<string, DataRow>();
            _table = new DataTable();
            _table.Columns.Add("ResourceName", typeof(string));
            _table.Columns.Add("LeafResource", typeof(string));
            _table.Columns.Add("Value1", typeof(string));
            _table.Columns.Add("Value2", typeof(string));
            _table.Columns.Add("Value3", typeof(string));
            _table.Columns.Add("Value4", typeof(string));
            bindingSource1.DataSource = _table;
        }

        private void OpenFile(int columnNo)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                ReadFile(fileName, columnNo);
            }
        }

        private void ReadFile(string fileName, int columnNumber)
        {
            //<Language Name="English">
            //  <LocaleResource Name="AboutUs">
            //    <Value>About us</Value>
            //  </LocaleResource>

            try
            {
                using (XmlReader reader = XmlReader.Create(fileName))
                {
                    _table.BeginLoadData();
                    reader.MoveToContent();
                    XElement languageElement = XElement.ReadFrom(reader) as XElement;
                    string languageName = languageElement.Attribute("Name").Value;
                    string valueColumn = string.Format("Value{0}", columnNumber);
                    dataGridView1.Columns[valueColumn].HeaderText = languageName;
                    ReadLocaleResourceElementsRecursive(languageElement, valueColumn, string.Empty);
                    _table.EndLoadData();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "There is no Unicode byte order mark. Cannot switch to Unicode.")
                {
                    MessageBox.Show("Change encoding utf-16 to utf-8 in file");
                    return;
                }
            }
        }

        /// <summary>
        /// Reads a LocaleResource element, both old version with fully qualified names,
        /// and new version with Children sub elements.
        /// </summary>
        /// <param name="node">The LocaleResource node</param>
        /// <param name="valueColumn">The value column in the grid to insert the value into.</param>
        /// <param name="currentPath">The path, if any, of the parent element.</param>
        private void ReadLocaleResourceElementsRecursive(XElement node, string valueColumn, string currentPath)
        {
            foreach (XElement localeResource in node.Elements("LocaleResource"))
            {
                string name = localeResource.Attribute("Name").Value;
                string qualifiedName;
                if (!string.IsNullOrEmpty(currentPath))
                    qualifiedName = string.Format("{0}.{1}", currentPath, name);
                else
                    qualifiedName = name;
                XElement valueElement = localeResource.Element("Value");
                if (valueElement != null)
                {
                    string value = valueElement.Value.Trim();
                    AddValue(qualifiedName, valueColumn, value);
                }
                XElement children = localeResource.Element("Children");
                if (children != null)
                    ReadLocaleResourceElementsRecursive(children, valueColumn, qualifiedName);
            }
        }

        private void DoSave(bool saveAs)
        {
            if (saveAs || string.IsNullOrEmpty(saveFileDialog1.FileName))
            {
                if (string.IsNullOrEmpty(saveFileDialog1.InitialDirectory))
                    saveFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                DialogResult dialogResult = saveFileDialog1.ShowDialog();
                if (dialogResult != DialogResult.OK)
                    return;
            }
            string fileName = saveFileDialog1.FileName;
            SaveFile(fileName, 2);
        }

        private void SaveFile(string fileName, int columnNumber)
        {
            // This is what we are reading:
            //<Language Name="English">
            //  <LocaleResource Name="AboutUs">
            //    <Value>About us</Value>
            //  </LocaleResource>

            string valueColumn = string.Format("Value{0}", columnNumber);
            string languageName = dataGridView1.Columns[valueColumn].HeaderText;
            XElement languageElement = new XElement("Language", new XAttribute("Name", languageName));
            foreach (DataRow currentRow in _table.Rows)
            {
                string name = (string)currentRow["ResourceName"];
                string value = Convert.ToString(currentRow[valueColumn]);
                if (!string.IsNullOrEmpty(value))
                    languageElement.Add(
                        new XElement("LocaleResource",
                            new XAttribute("Name", name),
                            new XElement("Value", value)));
            }
            XDocument document = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                languageElement);
            document.Save(fileName, SaveOptions.None);
        }

        private void AddValue(string resourceName, string valueColumn, string value)
        {
            DataRow row = null;
            // Find any existing resource name
            bool exists = _rows.TryGetValue(resourceName, out row);
            if (!exists)
            {
                // Create row if it does not exist
                row = _table.NewRow();
                row["ResourceName"] = resourceName;
                row["LeafResource"] = GetLeafResource(resourceName);
            }
            // Insert value
            row[valueColumn] = value;
            if (!exists)
            {
                // Insert row if it did not exist
                _rows.Add(resourceName, row);
                _table.Rows.Add(row);
            }
        }

        private static string GetLeafResource(string resourceName)
        {
            string[] resourceNameSplit = resourceName.Split('.');
            string leaf = resourceNameSplit.Last();
            if (InvalidLeafResources.Contains(leaf) && resourceNameSplit.Length > 1)
                leaf = string.Format("{0}.{1}", resourceNameSplit[resourceNameSplit.Length - 2], leaf);
            return leaf;
        }

        private void DoFind()
        {
            FindDialog findDialog = new FindDialog() { SearchText = _searchText };
            DialogResult dialogResult = findDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string searchText = findDialog.SearchText;
                _searchText = searchText;
                FindNext(false);
            }
        }

        private void FindNext(bool reverse)
        {
            if (!string.IsNullOrEmpty(_searchText))
            {
                int columnIndex = dataGridView1.CurrentCell.ColumnIndex;
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int rowCount = dataGridView1.Rows.Count;
                bool success = false;
                int direction = reverse ? -1 : 1;
                //for (int i = rowIndex + 1; i < rowCount; i++)
                int i = rowIndex + direction;
                while (true)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    DataGridViewCell cell = row.Cells[columnIndex];
                    string value = Convert.ToString(cell.Value);
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (value.IndexOf(_searchText, StringComparison.CurrentCultureIgnoreCase) >= 0)
                        {
                            dataGridView1.CurrentCell = cell;
                            success = true;
                            break;
                        }
                    }
                    i += direction;
                    if (i >= rowCount)
                        i = 0;
                    else if (i < 0)
                        i = rowCount - 1;
                    if (i == rowIndex)
                        break; // Not found
                }
                if (!success)
                {
                    MessageBox.Show("The program has searched all rows in the current column, and no further instances were found.", "No more results");
                }
            }
        }

        private void FindNextToTranslate(bool findPrevious)
        {
            bool success = false;
            int rowCount = dataGridView1.Rows.Count;
            if (rowCount > 1)
            {
                const int columnIndex = 3;
                int rowIndex = dataGridView1.CurrentCell != null ? dataGridView1.CurrentCell.RowIndex : 0;
                for (int i = rowIndex + (findPrevious ? -1 : 1); i != rowIndex;)
                {
                    if (i >= rowCount)
                        i = 0; // Wrap around to beginning
                    else if (i < 0)
                        i = rowCount - 1;
                    DataGridViewRow row = dataGridView1.Rows[i];
                    DataGridViewCell cell = row.Cells[columnIndex];
                    string value = Convert.ToString(cell.Value);
                    if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(Convert.ToString(row.Cells[2].Value)))
                    {
                        dataGridView1.CurrentCell = cell;
                        success = true;
                        break;
                    }
                    if (findPrevious) i--; else i++;
                }
            }
            if (!success)
            {
                MessageBox.Show("The program has searched through all records and no further fields to translate were found.", "No more results");
            }
        }

        BackgroundWorker backgroundWorker1;
        private void MainForm_Load(object sender, EventArgs e)
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            //string[] dirs = Directory.GetDirectories(txtFolderPath.Text);
            //float length = dirs.Length;
            int all = dataGridView1.Rows.Count;
            progressBar1.Invoke((Action)(() => progressBar1.Maximum = all));
            int i = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                backgroundWorker1.ReportProgress((int)(i / all * 100));
                DataGridViewRow rowCopy = row;


                try
                {
                    string orgin = rowCopy.Cells["Value1"].Value.ToString();
                    string al = TranslateGoogle(orgin, "en", "vi");
                    byte[] utf8Bytess = System.Text.Encoding.UTF8.GetBytes(al);

                    // Convert utf-8 bytes to a string.
                    string s_unicode2w = System.Text.Encoding.UTF8.GetString(utf8Bytess);

                    rowCopy.Cells["Value2"].Value = s_unicode2w;
                }
                catch
                {
                }
                i++;

            }

            //for (int i = 0; i < all; i++)
            //{
            //    backgroundWorker1.ReportProgress((int)(i / all * 100));
            //    //ScanDirectory(dirs[i], txtSearch.Text);
            //}

            backgroundWorker1.ReportProgress(100);

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
            {
                percent.Text = e.ProgressPercentage + "%";
                progressBar1.PerformStep();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //lblProgress.Text = String.Format("{0} files found", listView1.Items.Count);
            if (progressBar1.Value < progressBar1.Maximum)
            {
                //lblProgress.Text = "Searching cancelled. " + lblProgress.Text;
            }
            //btnSearch.Text = "Search";
        }


        /// <summary>
        /// Translates a string into another language using Google's translate API JSON calls.
        /// <seealso>Class TranslationServices</seealso>
        /// </summary>
        /// <param name="Text">Text to translate. Should be a single word or sentence.</param>
        /// <param name="FromCulture">
        /// Two letter culture (en of en-us, fr of fr-ca, de of de-ch)
        /// </param>
        /// <param name="ToCulture">
        /// Two letter culture (as for FromCulture)
        /// </param>
        public string TranslateGoogle(string text, string fromCulture, string toCulture)
        {
            fromCulture = fromCulture.ToLower();
            toCulture = toCulture.ToLower();

            WebClient webClient = new WebClient();
            try
            {
                text = "Added a new vendor (ID = {0})";
                string a = "Đã thêm nhà cung cấp mới (ID \x3d {0})";




                string url = String.Format("http://www.google.com/translate_t?hl={0}&ie=UTF8&text={1}&langpair={2}", toCulture, text, fromCulture);
                string result = webClient.DownloadString(url);
                int bas = result.IndexOf("TRANSLATED_TEXT='") + "TRANSLATED_TEXT='".Length;
                int bit = result.Substring(bas).IndexOf("';var");
                result = result.Substring(bas, bit);


                var data = HttpUtility.HtmlDecode(result);



                return data;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void btnTrans_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                progressBar1.Value = progressBar1.Minimum;
                //btnSearch.Text = "Stop";
                //listView1.Items.Clear();
                backgroundWorker1.RunWorkerAsync();
            }
            return;
            try
            {
                int all = dataGridView1.Rows.Count;
                if (all == 0)
                {
                    MessageBox.Show("Please choose file");
                    return;
                }
                int i = 0;
                string from = comboBox1.Text;
                string to = comboBox2.Text;
                if (from.Length > 2 || to.Length > 2)
                {
                    MessageBox.Show("Please choose language");
                    return;
                }
                percent.Text = "Starting translate...";

                try
                {

                    List<Task> tasks = new List<Task>();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        i++;
                        DataGridViewRow rowCopy = row;
                        Task newTask = new Task(() =>
                        {

                            try
                            {
                                string orgin = rowCopy.Cells["Value1"].Value.ToString();
                                string al = TranslateGoogle(orgin, from, to);
                                rowCopy.Cells["Value2"].Value = al;
                            }
                            catch
                            {
                            }
                        });

                        tasks.Add(newTask);
                        newTask.Start();

                        if (i % 100 == 0)
                        {
                            double j = i;
                            string ss = Math.Round((j / all * 100), 2).ToString();
                            percent.Text = ss + "% complete. Please wait translate";
                        }
                    }
                    Task.WaitAll(tasks.ToArray());
                }
                catch (Exception)
                {
                }
            }
            catch (Exception)
            {
                throw;
            }
            MessageBox.Show("Done");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_info frm = new frm_info();
            frm.ShowDialog();
        }
    }

    #endregion
}
