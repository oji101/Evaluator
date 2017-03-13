using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormRezEval : Form
	{
		private delegate void PrintInBackgroundDelegate();

		private string[] files;

		private string rootDirectory;

		private TextPrintDocument printDoc = new TextPrintDocument();

		private bool reportsDeleted;

		private IContainer components;

		private GroupBox groupBox1;

		private ListBox listBox1;

		private RichTextBox richTextBox1;

		private GroupBox groupBox2;

		private Button button1;

		private PrintDialog dlgPrint;

		private Button button2;

		private Button btnEraseEvalReports;

		private CheckBox checkBoxEraseEvalReports;

		private ToolTip toolTipPrint;

		private ToolTip toolTipErase;

		private ToolTip toolTipAll;

		private Button btnClasament;

		public bool ReportsDeleted
		{
			get
			{
				return this.reportsDeleted;
			}
		}

		public FormRezEval(Eval f)
		{
			this.InitializeComponent();
			this.rootDirectory = f.rootDIR;
		}

		private void FormRezEval_Load(object sender, EventArgs e)
		{
			Directory.SetCurrentDirectory(this.rootDirectory);
			try
			{
				this.button1.Enabled = false;
				if (!Directory.Exists("..\\..\\rezultate\\evaluare\\"))
				{
					Directory.CreateDirectory("..\\..\\rezultate\\evaluare\\");
				}
				this.btnClasament.Enabled = File.Exists("..\\..\\rezultate\\evaluare\\Clasament.xls");
				this.files = Directory.GetFiles("..\\..\\rezultate\\evaluare\\");
				string[] array = this.files;
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					if (!(Path.GetFileName(text) == "All_Results-res.txt") && text.Substring(text.Length - 8) == "-res.txt")
					{
						string text2 = Path.GetFileName(text);
						text2 = text2.Substring(0, text2.Length - 8);
						if (!(text2 == "All_Results"))
						{
							this.listBox1.Items.Add(text2);
						}
					}
				}
				if (File.Exists("..\\..\\rezultate\\evaluare\\All_Results-res.txt"))
				{
					this.listBox1.Items.Insert(0, "All_Results");
				}
				if (this.listBox1.Items.Count == 0)
				{
					this.checkBoxEraseEvalReports.Enabled = false;
					this.btnEraseEvalReports.Enabled = false;
				}
				else
				{
					this.listBox1.SelectedIndex = 0;
				}
			}
			catch (Exception)
			{
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = this.listBox1.SelectedIndex;
			string str = null;
			if (selectedIndex != -1)
			{
				str = this.listBox1.SelectedItem.ToString();
				this.button1.Enabled = true;
			}
			try
			{
				if (File.Exists("..\\..\\rezultate\\evaluare\\" + str + "-res.txt"))
				{
					this.richTextBox1.LoadFile("..\\..\\rezultate\\evaluare\\" + str + "-res.txt", RichTextBoxStreamType.PlainText);
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Raportul '..\\..\\rezultate\\evaluare\\" + str + "' a fost sters!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				this.printDoc.FileToPrint = "..\\..\\rezultate\\evaluare\\" + this.listBox1.SelectedItem.ToString() + "-res.txt";
				this.dlgPrint.Document = this.printDoc;
				if (this.dlgPrint.ShowDialog(this) == DialogResult.OK)
				{
					this.Refresh();
					PrintController printController = new StandardPrintController();
					this.printDoc.PrintController = printController;
					this.button1.Enabled = false;
					this.btnEraseEvalReports.Enabled = false;
					this.checkBoxEraseEvalReports.Enabled = false;
					FormRezEval.PrintInBackgroundDelegate printInBackgroundDelegate = new FormRezEval.PrintInBackgroundDelegate(this.PrintInBackground);
					printInBackgroundDelegate.BeginInvoke(new AsyncCallback(this.PrintInBackgroundComplete), null);
				}
			}
			catch (Exception)
			{
			}
		}

		private void PrintInBackground()
		{
			try
			{
				this.printDoc.Print();
			}
			catch (Exception)
			{
			}
		}

		private void PrintInBackgroundComplete(IAsyncResult r)
		{
			try
			{
				this.button1.Enabled = true;
				this.button1.Enabled = true;
				this.btnEraseEvalReports.Enabled = true;
			}
			catch (Exception)
			{
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void EraseEvalReports_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.listBox1.SelectedIndex;
			string text = null;
			if (selectedIndex != -1)
			{
				text = this.listBox1.SelectedItem.ToString();
				this.button1.Enabled = true;
			}
			try
			{
				this.listBox1.Items.Remove(text);
				this.richTextBox1.Text = "";
				string text2 = "..\\..\\rezultate\\evaluare\\" + text + "-res.txt";
				if (File.Exists(text2))
				{
					File.Delete(text2);
				}
				string[] array = Directory.GetFiles("..\\..\\rezultate\\evaluare\\tmp\\");
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string path = array2[i];
					text2 = Path.GetFileNameWithoutExtension(path);
					if (text2.Contains(text))
					{
						File.Delete(path);
					}
				}
			}
			catch (Exception)
			{
			}
			if (this.checkBoxEraseEvalReports.Checked)
			{
				try
				{
					this.reportsDeleted = true;
					for (int j = 0; j < this.listBox1.Items.Count; j++)
					{
						this.listBox1.SetSelected(j, true);
						text = this.listBox1.SelectedItem.ToString();
						string text3 = "..\\..\\rezultate\\evaluare\\" + text + "-res.txt";
						if (File.Exists(text3))
						{
							File.Delete(text3);
							string[] array3 = Directory.GetFiles("..\\..\\rezultate\\evaluare\\tmp\\");
							string[] array4 = array3;
							for (int k = 0; k < array4.Length; k++)
							{
								string path2 = array4[k];
								text3 = Path.GetFileNameWithoutExtension(path2);
								if (text3.Contains(text))
								{
									File.Delete(path2);
								}
							}
							j--;
							this.listBox1.Items.Remove(text);
							this.listBox1.Refresh();
						}
						if (File.Exists("..\\..\\rezultate\\evaluare\\Clasament.xls"))
						{
							File.Delete("..\\..\\rezultate\\evaluare\\Clasament.xls");
						}
					}
					this.btnEraseEvalReports.Enabled = false;
					this.checkBoxEraseEvalReports.Enabled = false;
					this.btnClasament.Enabled = false;
					this.button1.Enabled = false;
					this.richTextBox1.Text = "";
				}
				catch (Exception)
				{
				}
			}
		}

		private void checkBoxEraseEvalReports_CheckedChanged(object sender, EventArgs e)
		{
			this.button1.Enabled = !this.checkBoxEraseEvalReports.Checked;
			if (this.checkBoxEraseEvalReports.Checked)
			{
				this.listBox1.BackColor = Color.DarkGray;
				return;
			}
			this.listBox1.BackColor = Color.LightGray;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("..\\..\\rezultate\\evaluare\\Clasament.xls");
			}
			catch (Exception)
			{
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormRezEval));
			this.groupBox1 = new GroupBox();
			this.listBox1 = new ListBox();
			this.richTextBox1 = new RichTextBox();
			this.groupBox2 = new GroupBox();
			this.button1 = new Button();
			this.dlgPrint = new PrintDialog();
			this.button2 = new Button();
			this.btnEraseEvalReports = new Button();
			this.checkBoxEraseEvalReports = new CheckBox();
			this.toolTipPrint = new ToolTip(this.components);
			this.toolTipErase = new ToolTip(this.components);
			this.toolTipAll = new ToolTip(this.components);
			this.btnClasament = new Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.listBox1);
			this.groupBox1.Location = new Point(4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(117, 677);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Concurenti";
			this.listBox1.BackColor = Color.LightGray;
			this.listBox1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new Point(6, 17);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new Size(103, 654);
			this.listBox1.TabIndex = 0;
			this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
			this.richTextBox1.AcceptsTab = true;
			this.richTextBox1.BackColor = Color.Gainsboro;
			this.richTextBox1.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox1.ForeColor = Color.Blue;
			this.richTextBox1.Location = new Point(5, 17);
			this.richTextBox1.Margin = new Padding(10, 3, 3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new Size(629, 651);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			this.groupBox2.Controls.Add(this.richTextBox1);
			this.groupBox2.Location = new Point(127, 4);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(641, 677);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Rapoarte Evaluare";
			this.button1.Location = new Point(207, 687);
			this.button1.Name = "button1";
			this.button1.Size = new Size(85, 28);
			this.button1.TabIndex = 3;
			this.button1.Text = "Print";
			this.toolTipPrint.SetToolTip(this.button1, "Printeaza borderoul selectat");
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.dlgPrint.UseEXDialog = true;
			this.button2.Location = new Point(676, 687);
			this.button2.Name = "button2";
			this.button2.Size = new Size(85, 28);
			this.button2.TabIndex = 4;
			this.button2.Text = "Iesire";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.btnEraseEvalReports.Location = new Point(10, 687);
			this.btnEraseEvalReports.Name = "btnEraseEvalReports";
			this.btnEraseEvalReports.Size = new Size(101, 28);
			this.btnEraseEvalReports.TabIndex = 2;
			this.btnEraseEvalReports.Text = "Sterge";
			this.toolTipErase.SetToolTip(this.btnEraseEvalReports, "Sterge rapoartele selectate");
			this.btnEraseEvalReports.UseVisualStyleBackColor = true;
			this.btnEraseEvalReports.Click += new EventHandler(this.EraseEvalReports_Click);
			this.checkBoxEraseEvalReports.AutoSize = true;
			this.checkBoxEraseEvalReports.Location = new Point(117, 694);
			this.checkBoxEraseEvalReports.Name = "checkBoxEraseEvalReports";
			this.checkBoxEraseEvalReports.RightToLeft = RightToLeft.No;
			this.checkBoxEraseEvalReports.Size = new Size(54, 17);
			this.checkBoxEraseEvalReports.TabIndex = 5;
			this.checkBoxEraseEvalReports.Text = "Toate";
			this.toolTipAll.SetToolTip(this.checkBoxEraseEvalReports, "Selecteaza toate rapoartele pentru stergere");
			this.checkBoxEraseEvalReports.UseVisualStyleBackColor = true;
			this.checkBoxEraseEvalReports.CheckedChanged += new EventHandler(this.checkBoxEraseEvalReports_CheckedChanged);
			this.toolTipPrint.ShowAlways = true;
			this.toolTipAll.ShowAlways = true;
			this.btnClasament.Location = new Point(402, 687);
			this.btnClasament.Name = "btnClasament";
			this.btnClasament.Size = new Size(85, 28);
			this.btnClasament.TabIndex = 6;
			this.btnClasament.Text = "Clasament";
			this.toolTipAll.SetToolTip(this.btnClasament, "Deschide fisierul Clasament.xls  ");
			this.btnClasament.UseVisualStyleBackColor = true;
			this.btnClasament.Click += new EventHandler(this.button3_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Silver;
			base.ClientSize = new Size(773, 722);
			base.Controls.Add(this.btnClasament);
			base.Controls.Add(this.checkBoxEraseEvalReports);
			base.Controls.Add(this.btnEraseEvalReports);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormRezEval";
			this.Text = "Rapoarte de evaluare";
			base.Load += new EventHandler(this.FormRezEval_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
