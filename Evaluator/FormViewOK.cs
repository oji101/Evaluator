using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormViewOK : Form
	{
		private IContainer components;

		private RichTextBox richTextBox1;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormViewOK));
			this.richTextBox1 = new RichTextBox();
			base.SuspendLayout();
			this.richTextBox1.AcceptsTab = true;
			this.richTextBox1.BackColor = SystemColors.Window;
			this.richTextBox1.Dock = DockStyle.Fill;
			this.richTextBox1.Font = new Font("Courier New", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.richTextBox1.Location = new Point(0, 0);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new Size(304, 274);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(304, 274);
			base.Controls.Add(this.richTextBox1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FormViewOK";
			base.ResumeLayout(false);
		}

		public FormViewOK(Eval fEval)
		{
			this.InitializeComponent();
			this.Text = fEval.Problem + ".ok";
			try
			{
				string path = Directory.GetCurrentDirectory() + "\\" + fEval.Problem + ".ok";
				if (!File.Exists(path))
				{
					MessageBox.Show("Nu s-a putut citi fisierul " + fEval.Problem + ".ok", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					this.richTextBox1.LoadFile(path, RichTextBoxStreamType.PlainText);
				}
			}
			catch (IOException)
			{
				MessageBox.Show("Nu s-a putut citi fisierul " + fEval.Problem + ".ok", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (ArgumentException)
			{
				MessageBox.Show("Formatul fisierului este incorect", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}
