using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormTests : Form
	{
		private IContainer components;

		private RichTextBox richTextBox1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem copiazaToolStripMenuItem;

		private ToolStripMenuItem taieToolStripMenuItem;

		private ToolStripMenuItem lipesteToolStripMenuItem;

		private string path;

		private string root;

		private string file;

		private bool save;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormTests));
			this.richTextBox1 = new RichTextBox();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.copiazaToolStripMenuItem = new ToolStripMenuItem();
			this.taieToolStripMenuItem = new ToolStripMenuItem();
			this.lipesteToolStripMenuItem = new ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.richTextBox1.AcceptsTab = true;
			this.richTextBox1.ContextMenuStrip = this.contextMenuStrip1;
			this.richTextBox1.Dock = DockStyle.Fill;
			this.richTextBox1.Font = new Font("Courier New", 9.75f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox1.Location = new Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new Size(265, 243);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.copiazaToolStripMenuItem,
				this.taieToolStripMenuItem,
				this.lipesteToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(124, 70);
			this.copiazaToolStripMenuItem.Name = "copiazaToolStripMenuItem";
			this.copiazaToolStripMenuItem.Size = new Size(123, 22);
			this.copiazaToolStripMenuItem.Text = "&Copiaza";
			this.copiazaToolStripMenuItem.Click += new EventHandler(this.copiazaToolStripMenuItem_Click);
			this.taieToolStripMenuItem.Name = "taieToolStripMenuItem";
			this.taieToolStripMenuItem.Size = new Size(123, 22);
			this.taieToolStripMenuItem.Text = "&Taie";
			this.taieToolStripMenuItem.Click += new EventHandler(this.taieToolStripMenuItem_Click);
			this.lipesteToolStripMenuItem.Name = "lipesteToolStripMenuItem";
			this.lipesteToolStripMenuItem.Size = new Size(123, 22);
			this.lipesteToolStripMenuItem.Text = "&Lipeste";
			this.lipesteToolStripMenuItem.Click += new EventHandler(this.lipesteToolStripMenuItem_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(265, 243);
			base.Controls.Add(this.richTextBox1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "FormTests";
			base.Load += new EventHandler(this.FormTests_Load);
			base.FormClosing += new FormClosingEventHandler(this.FormTests_FormClosing);
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public FormTests(FormNewProblem f)
		{
			this.InitializeComponent();
			try
			{
				this.save = f.NewOrModif;
				this.root = f.RootDir;
				this.file = f.ProblemDirectory + "teste.txt";
				this.Text = "teste.txt";
				if (!File.Exists(this.file))
				{
					bool flag = false;
					while (!flag)
					{
						try
						{
							flag = true;
							StreamWriter streamWriter = new StreamWriter(this.file);
							streamWriter.WriteLine("0 10");
							streamWriter.WriteLine("1 10");
							streamWriter.WriteLine("2 10");
							streamWriter.WriteLine("3 10");
							streamWriter.WriteLine("4 10");
							streamWriter.WriteLine("5 10");
							streamWriter.WriteLine("6 10");
							streamWriter.WriteLine("7 10");
							streamWriter.WriteLine("8 10");
							streamWriter.WriteLine("9 10");
							streamWriter.Flush();
							streamWriter.Close();
						}
						catch (Exception)
						{
							flag = false;
						}
					}
				}
			}
			catch (ArgumentException)
			{
				MessageBox.Show("Nu s-a putut deschide fisierul 'teste.txt'!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void FormTests_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (this.save)
				{
					this.richTextBox1.SaveFile(this.file, RichTextBoxStreamType.PlainText);
				}
			}
			catch (Exception)
			{
			}
		}

		private void FormTests_Load(object sender, EventArgs e)
		{
			bool flag = false;
			while (!flag)
			{
				flag = true;
				try
				{
					this.richTextBox1.LoadFile(this.file, RichTextBoxStreamType.PlainText);
					if (this.richTextBox1.Text == "")
					{
						this.richTextBox1.Text = "0 10\n1 10\n2 10\n2 10\n4 10\n5 10\n6 10\n7 10\n8 10\n9 10\n";
					}
				}
				catch (Exception)
				{
					flag = false;
				}
			}
			if (!this.save)
			{
				this.richTextBox1.ReadOnly = true;
				this.richTextBox1.Cursor = null;
				this.contextMenuStrip1.Enabled = false;
			}
		}

		private void copiazaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.richTextBox1.Copy();
		}

		private void taieToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.richTextBox1.Cut();
		}

		private void lipesteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.richTextBox1.Paste();
		}
	}
}
