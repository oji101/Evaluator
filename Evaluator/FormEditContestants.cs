using System;
using System.ComponentModel;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormEditContestants : Form
	{
		private string root;

		private bool canDel;

		private IContainer components;

		private Panel panel1;

		private DataGridView dGW;

		private DataGridViewTextBoxColumn iD;

		private DataGridViewTextBoxColumn lastName;

		private DataGridViewTextBoxColumn firstName;

		private DataGridViewTextBoxColumn school;

		private Button btnSave;

		private Button btnExit;

		private Button btnImport;

		private OpenFileDialog openFileDialog1;

		private ToolTip toolTip1;

		public bool canDelID
		{
			get
			{
				return this.canDel;
			}
			set
			{
				this.canDel = value;
			}
		}

		public FormEditContestants(Eval f)
		{
			this.InitializeComponent();
			this.root = f.rootDIR;
		}

		private void FormEditContestants_Load(object sender, EventArgs e)
		{
			string str = "..\\..\\";
			if (File.Exists(str + "contestants.txt"))
			{
				try
				{
					StreamReader streamReader = new StreamReader(str + "contestants.txt");
					char[] separator = new char[]
					{
						'\t'
					};
					streamReader.ReadLine();
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						string[] array = text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
						if (array.Length == 4)
						{
							this.dGW.Rows.Add(array);
						}
					}
					streamReader.Close();
					return;
				}
				catch (Exception)
				{
					return;
				}
			}
			try
			{
				StreamWriter streamWriter = new StreamWriter(str + "contestants.txt");
				streamWriter.WriteLine("ID\tNume\tPrenume\tScoala");
				streamWriter.Close();
			}
			catch (Exception)
			{
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			string currentDirectory = "";
			try
			{
				this.canDel = true;
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.root);
				string str = "..\\..\\";
				StreamWriter streamWriter = new StreamWriter(str + "contestants.txt");
				streamWriter.WriteLine("ID\tNume\tPrenume\tScoala");
				for (int i = 0; i < this.dGW.Rows.Count; i++)
				{
					for (int j = 0; j < this.dGW.Columns.Count; j++)
					{
						streamWriter.Write(this.dGW.Rows[i].Cells[j].Value + "\t");
					}
					streamWriter.WriteLine();
				}
				streamWriter.Close();
				MessageBox.Show("Datele au fost salvate!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception)
			{
				MessageBox.Show("Nu s-au putut salva datele!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			try
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
			}
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			try
			{
				this.openFileDialog1.Title = "Importa date concurenti";
				this.openFileDialog1.Filter = "Fisiere Excel (*.xls) |*.xls";
				this.openFileDialog1.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
				this.openFileDialog1.CheckFileExists = true;
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					string connectionString = "Driver={Microsoft Excel Driver (*.xls)}; DriverId=790;\r\n                                Dbq=" + this.openFileDialog1.FileName + ";DefaultDir=" + this.root;
					int num = 0;
					string[] array = new string[]
					{
						"select * from [Foaie1$]",
						"select * from [Sheet1$]"
					};
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string cmdText = array2[i];
						try
						{
							OdbcConnection odbcConnection2;
							OdbcConnection odbcConnection = odbcConnection2 = new OdbcConnection(connectionString);
							try
							{
								odbcConnection.Open();
								OdbcCommand odbcCommand = new OdbcCommand(cmdText, odbcConnection);
								OdbcDataReader odbcDataReader = odbcCommand.ExecuteReader();
								this.dGW.Rows.Clear();
								while (odbcDataReader.Read())
								{
									string[] array3 = new string[]
									{
										odbcDataReader[0].ToString(),
										odbcDataReader[1].ToString(),
										odbcDataReader[2].ToString(),
										odbcDataReader[3].ToString()
									};
									if (array3 != null)
									{
										this.dGW.Rows.Add(array3);
									}
								}
							}
							finally
							{
								if (odbcConnection2 != null)
								{
									((IDisposable)odbcConnection2).Dispose();
								}
							}
							break;
						}
						catch (Exception)
						{
							num++;
							if (num == 2)
							{
								MessageBox.Show("Datele nu au putut fi importate !\nAsigurati-va ca foaia de calcul se numeste Sheet1 sau Foaie1", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
					}
				}
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
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormEditContestants));
			this.panel1 = new Panel();
			this.dGW = new DataGridView();
			this.iD = new DataGridViewTextBoxColumn();
			this.lastName = new DataGridViewTextBoxColumn();
			this.firstName = new DataGridViewTextBoxColumn();
			this.school = new DataGridViewTextBoxColumn();
			this.btnSave = new Button();
			this.btnExit = new Button();
			this.btnImport = new Button();
			this.openFileDialog1 = new OpenFileDialog();
			this.toolTip1 = new ToolTip(this.components);
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.dGW).BeginInit();
			base.SuspendLayout();
			this.panel1.Controls.Add(this.dGW);
			this.panel1.Location = new Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(595, 551);
			this.panel1.TabIndex = 0;
			this.dGW.BackgroundColor = Color.LightGray;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.LightGray;
			dataGridViewCellStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dGW.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dGW.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dGW.Columns.AddRange(new DataGridViewColumn[]
			{
				this.iD,
				this.lastName,
				this.firstName,
				this.school
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = Color.LightGray;
			dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dGW.DefaultCellStyle = dataGridViewCellStyle2;
			this.dGW.Dock = DockStyle.Fill;
			this.dGW.Location = new Point(0, 0);
			this.dGW.Name = "dGW";
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Control;
			dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
			this.dGW.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.dGW.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dGW.Size = new Size(595, 551);
			this.dGW.TabIndex = 0;
			this.iD.HeaderText = "ID";
			this.iD.Name = "iD";
			this.lastName.HeaderText = "Nume";
			this.lastName.Name = "lastName";
			this.firstName.HeaderText = "Prenume";
			this.firstName.Name = "firstName";
			this.school.HeaderText = "Scoala";
			this.school.Name = "school";
			this.school.Width = 300;
			this.btnSave.BackColor = Color.LightGray;
			this.btnSave.Location = new Point(450, 569);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Salveaza";
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new EventHandler(this.btnSave_Click);
			this.btnExit.BackColor = Color.LightGray;
			this.btnExit.Location = new Point(531, 569);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new Size(75, 23);
			this.btnExit.TabIndex = 2;
			this.btnExit.Text = "Iesire";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			this.btnImport.BackColor = Color.LightGray;
			this.btnImport.Location = new Point(13, 570);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new Size(75, 23);
			this.btnImport.TabIndex = 3;
			this.btnImport.Text = "Importa";
			this.toolTip1.SetToolTip(this.btnImport, "Importa date dintr-un fisier .xls\r\n\r\nTrebuie sa existe cap de tabel. Capul de tabel nu se importa.\r\nFoaia de calcul trebuie sa se numeasca Sheet1 sau Foaie1");
			this.btnImport.UseVisualStyleBackColor = false;
			this.btnImport.Click += new EventHandler(this.btnImport_Click);
			this.openFileDialog1.FileName = "openFileDialog1";
			this.toolTip1.ShowAlways = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Silver;
			base.ClientSize = new Size(619, 600);
			base.Controls.Add(this.btnImport);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.btnSave);
			base.Controls.Add(this.panel1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FormEditContestants";
			this.Text = "Concurenti";
			base.Load += new EventHandler(this.FormEditContestants_Load);
			this.panel1.ResumeLayout(false);
			((ISupportInitialize)this.dGW).EndInit();
			base.ResumeLayout(false);
		}
	}
}
