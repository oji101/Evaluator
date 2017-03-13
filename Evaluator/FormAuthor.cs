using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormAuthor : Form
	{
		private IContainer components;

		private Panel panel1;

		private Label label1;

		private Label label2;

		private Label label3;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormAuthor));
			this.panel1 = new Panel();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.panel1.BorderStyle = BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new Point(13, 13);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(227, 88);
			this.panel1.TabIndex = 0;
			this.label3.AutoSize = true;
			this.label3.Font = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label3.Location = new Point(74, 29);
			this.label3.Name = "label3";
			this.label3.Size = new Size(40, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "v. 2.2";
			this.label2.AutoSize = true;
			this.label2.Font = new Font("Microsoft Sans Serif", 9.5f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label2.Location = new Point(64, 9);
			this.label2.Name = "label2";
			this.label2.Size = new Size(74, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Evaluator";
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Microsoft Sans Serif", 9.27f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(12, 51);
			this.label1.Name = "label1";
			this.label1.Size = new Size(191, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "© 2012  Constantin Galatan";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Silver;
			base.ClientSize = new Size(254, 115);
			base.Controls.Add(this.panel1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormAuthor";
			this.Text = "Autor";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		public FormAuthor()
		{
			this.InitializeComponent();
		}
	}
}
