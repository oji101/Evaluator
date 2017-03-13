using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormViewIN : Form
	{
		private string Problem;

		private IContainer components;

		private RichTextBox richTextBoxIN;

		public FormViewIN(Eval fEval)
		{
			this.InitializeComponent();
			this.Problem = fEval.Problem + ".in";
			this.Text = fEval.Problem + ".in";
			try
			{
				string path = Directory.GetCurrentDirectory() + "\\" + fEval.Problem + ".in";
				if (!File.Exists(path))
				{
					MessageBox.Show("Nu s-a putut citi fisierul '" + fEval.Problem + ".in'", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					this.richTextBoxIN.LoadFile(path, RichTextBoxStreamType.PlainText);
				}
			}
			catch (IOException)
			{
				MessageBox.Show("Nu s-a putut citi fisierul '" + fEval.Problem + ".in'", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (ArgumentException)
			{
				MessageBox.Show("Formatul fisierului nu este de tip text!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private void FormViewIN_Load(object sender, EventArgs e)
		{
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormViewIN));
			this.richTextBoxIN = new RichTextBox();
			base.SuspendLayout();
			this.richTextBoxIN.AcceptsTab = true;
			this.richTextBoxIN.BackColor = SystemColors.Window;
			this.richTextBoxIN.Dock = DockStyle.Fill;
			this.richTextBoxIN.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBoxIN.Location = new Point(0, 0);
			this.richTextBoxIN.Name = "richTextBoxIN";
			this.richTextBoxIN.ReadOnly = true;
			this.richTextBoxIN.Size = new Size(304, 275);
			this.richTextBoxIN.TabIndex = 0;
			this.richTextBoxIN.Text = "";
			this.richTextBoxIN.WordWrap = false;
			base.ClientSize = new Size(304, 275);
			base.Controls.Add(this.richTextBoxIN);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FormViewIN";
			base.ResumeLayout(false);
		}
	}
}
