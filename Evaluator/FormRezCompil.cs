using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormRezCompil : Form
	{
		private IContainer components;

		private GroupBox groupBox1;

		private ListBox listBox1;

		private GroupBox groupBox2;

		private RichTextBox richTextBox1;

		private Button btnErasecompilResults;

		private CheckBox checkBox1;

		private Button button2;

		private ToolTip toolTip1;

		private ToolTip toolTip2;

		private string[] files;

		private string rootDirectory;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormRezCompil));
			this.groupBox1 = new GroupBox();
			this.listBox1 = new ListBox();
			this.groupBox2 = new GroupBox();
			this.richTextBox1 = new RichTextBox();
			this.btnErasecompilResults = new Button();
			this.checkBox1 = new CheckBox();
			this.button2 = new Button();
			this.toolTip1 = new ToolTip(this.components);
			this.toolTip2 = new ToolTip(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.listBox1);
			this.groupBox1.Location = new Point(4, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(113, 681);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Concurenti";
			this.listBox1.BackColor = Color.LightGray;
			this.listBox1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.listBox1.ForeColor = Color.Black;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new Point(7, 18);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new Size(101, 654);
			this.listBox1.TabIndex = 0;
			this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
			this.groupBox2.Controls.Add(this.richTextBox1);
			this.groupBox2.Location = new Point(123, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(643, 681);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Rapoarte Compilare";
			this.richTextBox1.BackColor = Color.Gainsboro;
			this.richTextBox1.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox1.ForeColor = Color.Blue;
			this.richTextBox1.Location = new Point(6, 17);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new Size(631, 655);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			this.btnErasecompilResults.Location = new Point(11, 689);
			this.btnErasecompilResults.Name = "btnErasecompilResults";
			this.btnErasecompilResults.Size = new Size(101, 28);
			this.btnErasecompilResults.TabIndex = 2;
			this.btnErasecompilResults.Text = "Sterge";
			this.toolTip1.SetToolTip(this.btnErasecompilResults, "Sterge rapoartele selectate");
			this.btnErasecompilResults.UseVisualStyleBackColor = true;
			this.btnErasecompilResults.Click += new EventHandler(this.btnErasecompilResults_Click);
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new Point(118, 696);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new Size(54, 17);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "Toate";
			this.toolTip1.SetToolTip(this.checkBox1, "Selecteaza toate rapoartele pentru stergere");
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
			this.button2.Location = new Point(675, 689);
			this.button2.Name = "button2";
			this.button2.Size = new Size(85, 28);
			this.button2.TabIndex = 4;
			this.button2.Text = "Iesire";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.toolTip1.ShowAlways = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Silver;
			base.ClientSize = new Size(773, 724);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.checkBox1);
			base.Controls.Add(this.btnErasecompilResults);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormRezCompil";
			this.Text = "Rapoarte de compilare";
			base.Load += new EventHandler(this.FormRezCompil_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public FormRezCompil(Eval f)
		{
			this.InitializeComponent();
			this.rootDirectory = f.rootDIR;
		}

		private void FormRezCompil_Load(object sender, EventArgs e)
		{
			Directory.SetCurrentDirectory(this.rootDirectory);
			try
			{
				if (!Directory.Exists("..\\..\\rezultate\\compilare\\"))
				{
					Directory.CreateDirectory("..\\..\\rezultate\\compilare\\");
				}
				this.files = Directory.GetFiles("..\\..\\rezultate\\compilare\\");
				string[] array = this.files;
				for (int i = 0; i < array.Length; i++)
				{
					string path = array[i];
					string text = Path.GetFileName(path);
					text = text.Substring(0, text.Length - 11);
					this.listBox1.Items.Add(text);
				}
				if (this.listBox1.Items.Count == 0)
				{
					this.checkBox1.Enabled = false;
					this.btnErasecompilResults.Enabled = false;
				}
				else
				{
					this.listBox1.SelectedIndex = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = this.listBox1.SelectedIndex;
			string str = null;
			if (selectedIndex != -1)
			{
				str = this.listBox1.SelectedItem.ToString();
			}
			try
			{
				if (File.Exists("..\\..\\rezultate\\compilare\\" + str + "-compil.txt"))
				{
					this.richTextBox1.LoadFile("..\\..\\rezultate\\compilare\\" + str + "-compil.txt", RichTextBoxStreamType.PlainText);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnErasecompilResults_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.listBox1.SelectedIndex;
			string text = null;
			if (selectedIndex != -1)
			{
				text = this.listBox1.SelectedItem.ToString();
				this.button2.Enabled = true;
			}
			try
			{
				this.listBox1.Items.Remove(text);
				this.richTextBox1.Text = "";
				string path = "..\\..\\rezultate\\compilare\\" + text + "-compil.txt";
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			catch (Exception)
			{
			}
			if (this.checkBox1.Checked)
			{
				try
				{
					for (int i = 0; i < this.listBox1.Items.Count; i++)
					{
						this.listBox1.SetSelected(i, true);
						text = this.listBox1.SelectedItem.ToString();
						string path2 = "..\\..\\rezultate\\compilare\\" + text + "-compil.txt";
						if (File.Exists(path2))
						{
							File.Delete(path2);
							i--;
							this.listBox1.Items.Remove(text);
							this.listBox1.Refresh();
						}
					}
					this.btnErasecompilResults.Enabled = false;
					this.checkBox1.Enabled = false;
					this.button2.Enabled = true;
					this.richTextBox1.Text = "";
				}
				catch (Exception)
				{
				}
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBox1.Checked)
			{
				this.listBox1.BackColor = Color.DarkGray;
				return;
			}
			this.listBox1.BackColor = Color.LightGray;
		}
	}
}
