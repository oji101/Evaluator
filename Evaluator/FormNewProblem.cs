using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Evaluator
{
	public class FormNewProblem : Form
	{
		private bool newOrModif;

		private bool changes;

		private XmlDocument doc;

		private XmlNodeList pr;

		private string rootDirectory;

		private string prob;

		private int time;

		private double mem;

		private string problemDirectory;

		private Dictionary<string, bool> deletedProblems;

		private Dictionary<string, bool> createdProblems;

		private IContainer components;

		private TextBox textBoxProblemName;

		private TextBox textBoxMemory;

		private TextBox textBoxTimeLimit;

		private Label label1;

		private Label label2;

		private Label label3;

		private Button button2;

		private GroupBox groupBox1;

		private ListBox listBoxProblems;

		private Button buttonModifProb;

		private Button buttonAddProblem;

		private Button buttonDeleteProblem;

		private GroupBox groupBox2;

		private TextBox textBoxStack;

		private Label label5;

		private Label label4;

		private TextBox textBoxMaxSourse;

		private Label label6;

		private Button btnTests;

		public bool NewOrModif
		{
			get
			{
				return this.newOrModif;
			}
		}

		public string RootDir
		{
			get
			{
				return this.rootDirectory;
			}
		}

		public string ProblemDirectory
		{
			get
			{
				return this.problemDirectory;
			}
			set
			{
				this.problemDirectory = value;
			}
		}

		public Dictionary<string, bool> DeletedProblems
		{
			get
			{
				return this.deletedProblems;
			}
		}

		public Dictionary<string, bool> CreatedProblems
		{
			get
			{
				return this.createdProblems;
			}
		}

		public string PROB
		{
			get
			{
				return this.prob;
			}
		}

		public int TIME
		{
			get
			{
				return this.time;
			}
		}

		public double MEM
		{
			get
			{
				return this.mem;
			}
		}

		public bool Changes
		{
			get
			{
				return this.changes;
			}
		}

		public FormNewProblem(Eval f)
		{
			this.InitializeComponent();
			this.rootDirectory = f.rootDIR;
		}

		private void textBoxMemory_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '.' || e.KeyChar == '\b')
			{
				return;
			}
			if (e.KeyChar < '0' || e.KeyChar > '9')
			{
				e.Handled = true;
			}
		}

		private void textBoxTimeLimit_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\b')
			{
				return;
			}
			if (e.KeyChar < '0' || e.KeyChar > '9')
			{
				e.Handled = true;
			}
		}

		private void FormNewProblem_Load(object sender, EventArgs e)
		{
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				this.createdProblems = new Dictionary<string, bool>();
				this.deletedProblems = new Dictionary<string, bool>();
				this.listBoxProblems.Items.Clear();
				this.doc = new XmlDocument();
				this.doc.Load("../../rounds.xml");
				this.pr = this.doc.SelectNodes("oji/problema");
				for (int i = 0; i < this.pr.Count; i++)
				{
					XmlNode xmlNode = this.pr.Item(i).SelectSingleNode("nume");
					this.listBoxProblems.Items.Add(xmlNode.InnerText);
				}
				if (this.listBoxProblems.Items.Count != 0)
				{
					this.listBoxProblems.SelectedIndex = 0;
				}
			}
			catch (Exception)
			{
			}
		}

		private void listBoxProblems_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				int selectedIndex = this.listBoxProblems.SelectedIndex;
				this.doc = new XmlDocument();
				this.doc.Load("../../rounds.xml");
				this.pr = this.doc.SelectNodes("oji/problema");
				if (selectedIndex != -1)
				{
					XmlNode xmlNode = this.pr.Item(selectedIndex).SelectSingleNode("nume");
					this.textBoxProblemName.Text = xmlNode.InnerText;
					XmlNode xmlNode2 = this.pr.Item(selectedIndex).SelectSingleNode("limita_timp");
					this.textBoxTimeLimit.Text = xmlNode2.InnerText;
					XmlNode xmlNode3 = this.pr.Item(selectedIndex).SelectSingleNode("limita_memorie");
					this.textBoxMemory.Text = xmlNode3.InnerText;
					XmlNode xmlNode4 = this.pr.Item(selectedIndex).SelectSingleNode("limita_stiva");
					this.textBoxStack.Text = xmlNode4.InnerText;
					XmlNode xmlNode5 = this.pr.Item(selectedIndex).SelectSingleNode("limita_dimens_sursa");
					this.textBoxMaxSourse.Text = xmlNode5.InnerText;
				}
			}
			catch (Exception)
			{
			}
		}

		private void buttonAddProblem_Click(object sender, EventArgs e)
		{
			if (this.buttonAddProblem.Text == "Adauga problema")
			{
				this.newOrModif = true;
				this.textBoxProblemName.ReadOnly = false;
				this.textBoxProblemName.Focus();
				this.textBoxTimeLimit.ReadOnly = false;
				this.textBoxMemory.ReadOnly = false;
				this.textBoxMaxSourse.ReadOnly = false;
				this.textBoxStack.ReadOnly = false;
				this.textBoxStack.Text = "2";
				this.textBoxMaxSourse.Text = "5";
				this.textBoxTimeLimit.ForeColor = Color.Red;
				this.textBoxMemory.ForeColor = Color.Red;
				this.textBoxMaxSourse.ForeColor = Color.Red;
				this.textBoxProblemName.ForeColor = Color.Red;
				this.textBoxStack.ForeColor = Color.Red;
				this.buttonModifProb.Enabled = false;
				this.buttonDeleteProblem.Enabled = false;
				this.textBoxTimeLimit.Text = "";
				this.textBoxProblemName.Text = "";
				this.textBoxMemory.Text = "";
				this.buttonAddProblem.Text = "Valideaza";
				this.btnTests.Text = "Editeaza";
				this.buttonAddProblem.ForeColor = Color.Red;
				this.button2.Enabled = false;
				return;
			}
			this.newOrModif = false;
			this.textBoxProblemName.ReadOnly = true;
			this.textBoxTimeLimit.ReadOnly = true;
			this.textBoxMemory.ReadOnly = true;
			this.textBoxMemory.ReadOnly = true;
			this.textBoxMaxSourse.ReadOnly = true;
			this.textBoxTimeLimit.ForeColor = Color.Black;
			this.textBoxMemory.ForeColor = Color.Black;
			this.textBoxMaxSourse.ForeColor = Color.Black;
			this.textBoxProblemName.ForeColor = Color.Black;
			this.textBoxStack.ForeColor = Color.Black;
			this.buttonModifProb.Enabled = true;
			this.buttonDeleteProblem.Enabled = true;
			this.buttonAddProblem.Text = "Adauga problema";
			this.btnTests.Text = "Vezi";
			this.buttonAddProblem.ForeColor = Color.Black;
			this.button2.Enabled = true;
			try
			{
				if (this.textBoxProblemName.Text == "" || this.textBoxTimeLimit.Text == "" || this.textBoxMemory.Text == "")
				{
					MessageBox.Show("Nu ati completat toate campurile!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (this.listBoxProblems.Items.IndexOf(this.textBoxProblemName.Text) != -1)
				{
					MessageBox.Show("Problema '" + this.textBoxProblemName.Text + "' exista !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			catch (Exception)
			{
			}
			try
			{
				double num = double.Parse(this.textBoxMemory.Text);
				double num2 = double.Parse(this.textBoxStack.Text);
				if (num2 > num)
				{
					MessageBox.Show("Limita stivei e mai mare dacat limita memoriei totale!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					this.textBoxMemory.Text = "";
					this.textBoxProblemName.Text = "";
					this.textBoxTimeLimit.Text = "";
					return;
				}
			}
			catch (Exception)
			{
			}
			try
			{
				this.changes = true;
				Directory.SetCurrentDirectory(this.rootDirectory);
				string text = "..\\..\\probleme\\" + this.textBoxProblemName.Text + "\\teste\\";
				string str = "teste.txt";
				try
				{
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
						this.problemDirectory = text;
					}
				}
				catch (Exception)
				{
				}
				bool flag = false;
				if (File.Exists(text + str))
				{
					flag = true;
				}
				while (!flag)
				{
					try
					{
						flag = true;
						StreamWriter streamWriter = new StreamWriter(text + str);
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
				this.doc = new XmlDocument();
				this.doc.Load("..\\..\\rounds.xml");
				this.prob = this.textBoxProblemName.Text;
				this.time = int.Parse(this.textBoxTimeLimit.Text);
				this.mem = double.Parse(this.textBoxMemory.Text);
				XmlElement xmlElement = this.doc.CreateElement("problema");
				XmlElement xmlElement2 = this.doc.CreateElement("nume");
				xmlElement2.InnerText = this.prob;
				xmlElement.AppendChild(xmlElement2);
				this.listBoxProblems.Items.Add(this.prob);
				XmlElement xmlElement3 = this.doc.CreateElement("limita_timp");
				xmlElement3.InnerText = this.textBoxTimeLimit.Text;
				xmlElement.AppendChild(xmlElement3);
				XmlElement xmlElement4 = this.doc.CreateElement("limita_memorie");
				xmlElement4.InnerText = this.textBoxMemory.Text;
				xmlElement.AppendChild(xmlElement4);
				XmlElement xmlElement5 = this.doc.CreateElement("limita_stiva");
				xmlElement5.InnerText = this.textBoxStack.Text;
				xmlElement.AppendChild(xmlElement5);
				XmlElement xmlElement6 = this.doc.CreateElement("limita_dimens_sursa");
				xmlElement6.InnerText = this.textBoxMaxSourse.Text;
				xmlElement.AppendChild(xmlElement6);
				this.doc.DocumentElement.AppendChild(xmlElement);
				this.doc.PreserveWhitespace = false;
				this.doc.Save("..\\..\\rounds.xml");
				this.createdProblems.Add(this.prob, true);
			}
			catch (Exception)
			{
			}
		}

		private void buttonDeleteProblem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.listBoxProblems.Items.Count == 0)
				{
					MessageBox.Show("Nu puteti sterge !\nNu s-a definit nicio problema !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				if (this.listBoxProblems.SelectedIndex == -1)
				{
					MessageBox.Show("Selectati in lista problema pe care doriti sa o stergeti", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				DialogResult dialogResult = MessageBox.Show("Sunteti sigur ca vreti sa stergeti problema '" + this.listBoxProblems.SelectedItem.ToString() + "' ?\nSe vor sterge folderele asociate problemei\nimpreuna cu tot continutul lor !", "Evaluator", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.No)
				{
					return;
				}
			}
			catch (Exception)
			{
			}
			string text = null;
			try
			{
				this.changes = true;
				Directory.SetCurrentDirectory(this.rootDirectory);
				this.textBoxStack.Text = "";
				this.textBoxMaxSourse.Text = "";
				int selectedIndex = this.listBoxProblems.SelectedIndex;
				text = this.listBoxProblems.SelectedItem.ToString();
				if (selectedIndex != -1)
				{
					this.doc = new XmlDocument();
					this.doc.Load("../../rounds.xml");
					this.doc.DocumentElement.RemoveChild(this.doc.DocumentElement.SelectSingleNode("descendant::nume[text()=\"" + text + "\"]").ParentNode);
					this.doc.PreserveWhitespace = false;
					this.doc.Save("..\\..\\rounds.xml");
					this.listBoxProblems.Items.RemoveAt(selectedIndex);
					this.textBoxMemory.Text = "";
					this.textBoxTimeLimit.Text = "";
					this.textBoxProblemName.Text = "";
				}
				if (text != null)
				{
					this.deletedProblems.Add(text, true);
					if (this.createdProblems.ContainsKey(text))
					{
						this.createdProblems[text] = false;
					}
				}
			}
			catch (Exception)
			{
			}
			try
			{
				if (Directory.Exists("..\\..\\probleme\\" + text))
				{
					Directory.Delete("..\\..\\probleme\\" + text, true);
				}
			}
			catch (Exception)
			{
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				base.Close();
			}
			catch (Exception)
			{
			}
		}

		private void buttonModifProb_Click(object sender, EventArgs e)
		{
			if (this.textBoxMemory.Text == "" || this.textBoxProblemName.Text == "" || this.textBoxTimeLimit.Text == "")
			{
				MessageBox.Show("Selectati o problema din lista!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (this.buttonModifProb.Text == "Modifica problema")
			{
				this.newOrModif = true;
				this.textBoxProblemName.ReadOnly = true;
				this.textBoxTimeLimit.ReadOnly = false;
				this.textBoxMemory.ReadOnly = false;
				this.textBoxMaxSourse.ReadOnly = false;
				this.textBoxStack.ReadOnly = false;
				this.textBoxTimeLimit.ForeColor = Color.Red;
				this.textBoxMemory.ForeColor = Color.Red;
				this.textBoxStack.ForeColor = Color.Red;
				this.textBoxMaxSourse.ForeColor = Color.Red;
				this.buttonAddProblem.Enabled = false;
				this.buttonDeleteProblem.Enabled = false;
				this.buttonModifProb.Text = "Valideaza";
				this.btnTests.Text = "Editeaza";
				this.buttonModifProb.ForeColor = Color.Red;
				this.button2.Enabled = false;
				return;
			}
			this.newOrModif = false;
			this.textBoxProblemName.ReadOnly = true;
			this.textBoxTimeLimit.ReadOnly = true;
			this.textBoxMemory.ReadOnly = true;
			this.textBoxMaxSourse.ReadOnly = true;
			this.textBoxStack.ReadOnly = true;
			this.textBoxTimeLimit.ForeColor = Color.Black;
			this.textBoxMemory.ForeColor = Color.Black;
			this.textBoxStack.ForeColor = Color.Black;
			this.textBoxMaxSourse.ForeColor = Color.Black;
			this.buttonAddProblem.Enabled = true;
			this.buttonDeleteProblem.Enabled = true;
			this.buttonModifProb.Text = "Modifica problema";
			this.btnTests.Text = "Vezi";
			this.buttonModifProb.ForeColor = Color.Black;
			this.button2.Enabled = true;
			double num = 0.0;
			double num2 = 0.0;
			try
			{
				num = double.Parse(this.textBoxMemory.Text);
				num2 = double.Parse(this.textBoxStack.Text);
			}
			catch (Exception)
			{
			}
			if (num2 > num)
			{
				MessageBox.Show("Limita stivei e mai mare dacat limita memoriei totale!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				this.textBoxMemory.Text = "";
				this.textBoxProblemName.Text = "";
				this.textBoxTimeLimit.Text = "";
				return;
			}
			this.doc = new XmlDocument();
			try
			{
				this.changes = true;
				this.doc.Load("../../rounds.xml");
				string text = this.doc.DocumentElement.Name.ToString();
				string text2 = this.textBoxProblemName.Text;
				string xpath = string.Concat(new string[]
				{
					"/",
					text,
					"/problema[nume='",
					text2,
					"']"
				});
				XmlNode xmlNode = this.doc.DocumentElement.SelectSingleNode(xpath);
				xmlNode.FirstChild.NextSibling.InnerText = this.textBoxTimeLimit.Text;
				xmlNode.FirstChild.NextSibling.NextSibling.InnerText = this.textBoxMemory.Text;
				xmlNode.FirstChild.NextSibling.NextSibling.NextSibling.InnerText = this.textBoxStack.Text;
				xmlNode.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.InnerText = this.textBoxMaxSourse.Text;
				XmlNode oldChild = this.doc.DocumentElement.SelectSingleNode(xpath);
				this.doc.DocumentElement.ReplaceChild(xmlNode, oldChild);
				this.doc.PreserveWhitespace = false;
				this.doc.Save("..\\..\\rounds.xml");
			}
			catch (Exception)
			{
			}
		}

		private void textBoxStack_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '.' || e.KeyChar == '\b')
			{
				return;
			}
			if (e.KeyChar < '0' || e.KeyChar > '9')
			{
				e.Handled = true;
			}
		}

		private void textBoxMaxSourse_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\b')
			{
				return;
			}
			if (e.KeyChar < '0' || e.KeyChar > '9')
			{
				e.Handled = true;
			}
		}

		private void btnTests_Click(object sender, EventArgs e)
		{
			string currentDirectory = null;
			try
			{
				if (this.textBoxProblemName.Text == "" || this.textBoxTimeLimit.Text == "" || this.textBoxStack.Text == "" || this.textBoxMemory.Text == "" || this.textBoxMaxSourse.Text == "")
				{
					MessageBox.Show("Completati mai intai celelalte campuri !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					double num = double.Parse(this.textBoxMemory.Text);
					double num2 = double.Parse(this.textBoxStack.Text);
					if (num2 > num)
					{
						MessageBox.Show("Limita stivei e mai mare dacat limita memoriei totale!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.textBoxMemory.Text = "";
						this.textBoxProblemName.Text = "";
						this.textBoxTimeLimit.Text = "";
					}
					else
					{
						string text = "..\\..\\probleme\\" + this.textBoxProblemName.Text + "\\teste\\";
						string str = "teste.txt";
						this.problemDirectory = text;
						try
						{
							currentDirectory = Directory.GetCurrentDirectory();
							Directory.SetCurrentDirectory(this.rootDirectory);
							if (!Directory.Exists(text))
							{
								Directory.CreateDirectory(text);
							}
						}
						catch (Exception)
						{
						}
						if (!File.Exists(text + str))
						{
							bool flag = false;
							if (!this.newOrModif)
							{
								while (!flag)
								{
									try
									{
										flag = true;
										StreamWriter streamWriter = new StreamWriter(text + str);
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
						FormTests formTests = new FormTests(this);
						formTests.ShowDialog();
						Directory.SetCurrentDirectory(currentDirectory);
					}
				}
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormNewProblem));
			this.textBoxProblemName = new TextBox();
			this.textBoxMemory = new TextBox();
			this.textBoxTimeLimit = new TextBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.label3 = new Label();
			this.button2 = new Button();
			this.groupBox1 = new GroupBox();
			this.btnTests = new Button();
			this.label6 = new Label();
			this.textBoxStack = new TextBox();
			this.label5 = new Label();
			this.label4 = new Label();
			this.textBoxMaxSourse = new TextBox();
			this.buttonDeleteProblem = new Button();
			this.buttonAddProblem = new Button();
			this.buttonModifProb = new Button();
			this.listBoxProblems = new ListBox();
			this.groupBox2 = new GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			base.SuspendLayout();
			this.textBoxProblemName.BackColor = Color.LightGray;
			this.textBoxProblemName.Location = new Point(8, 37);
			this.textBoxProblemName.Name = "textBoxProblemName";
			this.textBoxProblemName.ReadOnly = true;
			this.textBoxProblemName.Size = new Size(85, 20);
			this.textBoxProblemName.TabIndex = 0;
			this.textBoxMemory.BackColor = Color.LightGray;
			this.textBoxMemory.Location = new Point(168, 37);
			this.textBoxMemory.Name = "textBoxMemory";
			this.textBoxMemory.ReadOnly = true;
			this.textBoxMemory.Size = new Size(69, 20);
			this.textBoxMemory.TabIndex = 2;
			this.textBoxMemory.KeyPress += new KeyPressEventHandler(this.textBoxMemory_KeyPress);
			this.textBoxTimeLimit.BackColor = Color.LightGray;
			this.textBoxTimeLimit.Location = new Point(99, 37);
			this.textBoxTimeLimit.Name = "textBoxTimeLimit";
			this.textBoxTimeLimit.ReadOnly = true;
			this.textBoxTimeLimit.Size = new Size(63, 20);
			this.textBoxTimeLimit.TabIndex = 1;
			this.textBoxTimeLimit.KeyPress += new KeyPressEventHandler(this.textBoxTimeLimit_KeyPress);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(8, 17);
			this.label1.Name = "label1";
			this.label1.Size = new Size(35, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Nume";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(165, 17);
			this.label2.Name = "label2";
			this.label2.Size = new Size(72, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Memorie (MB)";
			this.label3.AutoSize = true;
			this.label3.Location = new Point(96, 17);
			this.label3.Name = "label3";
			this.label3.Size = new Size(52, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Timp (ms)";
			this.button2.BackColor = Color.LightGray;
			this.button2.Location = new Point(158, 227);
			this.button2.Name = "button2";
			this.button2.Size = new Size(79, 28);
			this.button2.TabIndex = 8;
			this.button2.Text = "Iesire";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.groupBox1.Controls.Add(this.btnTests);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textBoxStack);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textBoxMaxSourse);
			this.groupBox1.Controls.Add(this.buttonDeleteProblem);
			this.groupBox1.Controls.Add(this.buttonAddProblem);
			this.groupBox1.Controls.Add(this.buttonModifProb);
			this.groupBox1.Controls.Add(this.textBoxProblemName);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textBoxMemory);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textBoxTimeLimit);
			this.groupBox1.Location = new Point(149, 11);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(246, 265);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Operatii";
			this.btnTests.BackColor = Color.LightGray;
			this.btnTests.Location = new Point(167, 116);
			this.btnTests.Name = "btnTests";
			this.btnTests.Size = new Size(70, 22);
			this.btnTests.TabIndex = 11;
			this.btnTests.Text = "Vezi";
			this.btnTests.UseVisualStyleBackColor = false;
			this.btnTests.Click += new EventHandler(this.btnTests_Click);
			this.label6.AutoSize = true;
			this.label6.Location = new Point(7, 120);
			this.label6.Name = "label6";
			this.label6.Size = new Size(136, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Testele si punctajul pe test:";
			this.textBoxStack.BackColor = Color.LightGray;
			this.textBoxStack.Location = new Point(168, 63);
			this.textBoxStack.Name = "textBoxStack";
			this.textBoxStack.ReadOnly = true;
			this.textBoxStack.Size = new Size(69, 20);
			this.textBoxStack.TabIndex = 3;
			this.textBoxStack.Text = "2";
			this.textBoxStack.KeyPress += new KeyPressEventHandler(this.textBoxStack_KeyPress);
			this.label5.AutoSize = true;
			this.label5.Location = new Point(7, 66);
			this.label5.Name = "label5";
			this.label5.Size = new Size(144, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Dimens. maxima a stivei (MB)";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(7, 93);
			this.label4.Name = "label4";
			this.label4.Size = new Size(145, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Dimens. maxima a sursei (KB)";
			this.textBoxMaxSourse.BackColor = Color.LightGray;
			this.textBoxMaxSourse.Location = new Point(168, 90);
			this.textBoxMaxSourse.Name = "textBoxMaxSourse";
			this.textBoxMaxSourse.ReadOnly = true;
			this.textBoxMaxSourse.Size = new Size(69, 20);
			this.textBoxMaxSourse.TabIndex = 4;
			this.textBoxMaxSourse.Text = "5";
			this.textBoxMaxSourse.KeyPress += new KeyPressEventHandler(this.textBoxMaxSourse_KeyPress);
			this.buttonDeleteProblem.BackColor = Color.LightGray;
			this.buttonDeleteProblem.Location = new Point(8, 222);
			this.buttonDeleteProblem.Name = "buttonDeleteProblem";
			this.buttonDeleteProblem.Size = new Size(122, 33);
			this.buttonDeleteProblem.TabIndex = 7;
			this.buttonDeleteProblem.Text = "Sterge problema";
			this.buttonDeleteProblem.UseVisualStyleBackColor = false;
			this.buttonDeleteProblem.Click += new EventHandler(this.buttonDeleteProblem_Click);
			this.buttonAddProblem.BackColor = Color.LightGray;
			this.buttonAddProblem.Location = new Point(8, 149);
			this.buttonAddProblem.Name = "buttonAddProblem";
			this.buttonAddProblem.Size = new Size(122, 33);
			this.buttonAddProblem.TabIndex = 5;
			this.buttonAddProblem.Text = "Adauga problema";
			this.buttonAddProblem.UseVisualStyleBackColor = false;
			this.buttonAddProblem.Click += new EventHandler(this.buttonAddProblem_Click);
			this.buttonModifProb.BackColor = Color.LightGray;
			this.buttonModifProb.Location = new Point(8, 186);
			this.buttonModifProb.Name = "buttonModifProb";
			this.buttonModifProb.Size = new Size(122, 33);
			this.buttonModifProb.TabIndex = 6;
			this.buttonModifProb.Text = "Modifica problema";
			this.buttonModifProb.UseVisualStyleBackColor = false;
			this.buttonModifProb.Click += new EventHandler(this.buttonModifProb_Click);
			this.listBoxProblems.BackColor = Color.LightGray;
			this.listBoxProblems.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.listBoxProblems.FormattingEnabled = true;
			this.listBoxProblems.Location = new Point(9, 17);
			this.listBoxProblems.Name = "listBoxProblems";
			this.listBoxProblems.Size = new Size(115, 238);
			this.listBoxProblems.TabIndex = 0;
			this.listBoxProblems.SelectedIndexChanged += new EventHandler(this.listBoxProblems_SelectedIndexChanged);
			this.groupBox2.Controls.Add(this.listBoxProblems);
			this.groupBox2.Location = new Point(8, 11);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(132, 265);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Probleme";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Silver;
			base.ClientSize = new Size(405, 287);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormNewProblem";
			this.Text = "Probleme";
			base.Load += new EventHandler(this.FormNewProblem_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
