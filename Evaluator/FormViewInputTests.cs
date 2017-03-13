using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormViewInputTests : Form
	{
		private IContainer components;

		private RichTextBox richTextBox1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem copiazaToolStripMenuItem;

		private ToolStripMenuItem taieToolStripMenuItem;

		private ToolStripMenuItem lipesteToolStripMenuItem;

		private string testIn;

		private string path;

		private string root;

		private string file;

		public RichTextBox RichIn
		{
			get
			{
				return this.richTextBox1;
			}
			set
			{
				this.richTextBox1 = value;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormViewInputTests));
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
			this.richTextBox1.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox1.Location = new Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new Size(292, 266);
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
			base.ClientSize = new Size(292, 266);
			base.Controls.Add(this.richTextBox1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FormViewInputTests";
			base.FormClosing += new FormClosingEventHandler(this.FormViewInputTests_FormClosing);
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public FormViewInputTests(Eval fEval)
		{
			this.InitializeComponent();
			try
			{
				this.root = fEval.rootDIR;
				this.testIn = fEval.Information;
				this.path = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(fEval.rootDIR);
				this.file = fEval.Info2 + this.testIn;
				this.Text = this.testIn;
				if (!File.Exists(this.file))
				{
					MessageBox.Show("Nu s-a putut citi fisierul '" + this.testIn + "' !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					this.richTextBox1.LoadFile(this.file, RichTextBoxStreamType.PlainText);
				}
				Directory.SetCurrentDirectory(this.path);
			}
			catch (IOException)
			{
				Directory.SetCurrentDirectory(this.path);
				MessageBox.Show("Nu s-a putut citi fisierul '" + this.testIn + "' !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (ArgumentException)
			{
				Directory.SetCurrentDirectory(this.path);
				MessageBox.Show("Formatul fisierului nu este de tip text!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void FormViewInputTests_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				Directory.SetCurrentDirectory(this.root);
				this.richTextBox1.SaveFile(this.file, RichTextBoxStreamType.PlainText);
			}
			catch (Exception)
			{
			}
		}

		private void copiazaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.richTextBox1.Copy();
			}
			catch (Exception)
			{
			}
		}

		private void taieToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.richTextBox1.Cut();
			}
			catch (Exception)
			{
			}
		}

		private void lipesteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.richTextBox1.Paste();
			}
			catch (Exception)
			{
			}
		}
	}
}
