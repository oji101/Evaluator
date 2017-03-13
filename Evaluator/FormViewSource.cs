using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormViewSource : Form
	{
		private string root;

		private string dir;

		private string file;

		private string[] keywordsC = new string[]
		{
			"void",
			"for",
			"if",
			"while",
			"do",
			"#include",
			"switch",
			"return",
			"begin",
			"bool",
			"break",
			"case",
			"char",
			"class",
			"struct",
			"const",
			"continue",
			"default",
			"delete",
			"new",
			"double",
			"else",
			"false",
			"float",
			"inline",
			"int",
			"long",
			"namespace",
			"operator",
			"private",
			"protected",
			"public",
			"short",
			"signed",
			"sizeof",
			"static",
			"template",
			"true",
			"typedef",
			"typename",
			"unsigned",
			"using"
		};

		private string[] keywordsPas = new string[]
		{
			"all",
			"and",
			"as",
			"array",
			"begin",
			"case",
			"const",
			"div",
			"do",
			"downto",
			"else",
			"end",
			"end.",
			"end;",
			"file",
			"for",
			"function",
			"goto",
			"in",
			"if",
			"is",
			"mod",
			"nil",
			"not",
			"of",
			"or",
			"pow",
			"private",
			"procedure",
			"program",
			"public",
			"switch",
			"repeat",
			"set",
			"shl",
			"shr",
			"then",
			"to",
			"type",
			"uses",
			"value",
			"var",
			"while",
			"with",
			"xor"
		};

		private HashSet<string> kC = new HashSet<string>();

		private HashSet<string> kP = new HashSet<string>();

		private HashSet<string> kW;

		private IContainer components;

		private RichTextBox m_rtb;

		public FormViewSource(Eval fEval)
		{
			this.InitializeComponent();
			string currentDirectory = null;
			this.root = fEval.rootDIR;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.root);
				this.dir = "..\\..\\probleme\\" + fEval.Info2 + "\\surse_concurenti\\";
				this.file = fEval.Information;
				if (!File.Exists(this.dir + this.file))
				{
					MessageBox.Show("Fisierul '" + this.file + "' nu exista!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					string[] array = this.keywordsC;
					for (int i = 0; i < array.Length; i++)
					{
						string item = array[i];
						this.kC.Add(item);
					}
					string[] array2 = this.keywordsPas;
					for (int j = 0; j < array2.Length; j++)
					{
						string item2 = array2[j];
						this.kP.Add(item2);
					}
					this.Parse();
				}
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		private void Parse()
		{
			try
			{
				this.Text = this.file;
				string text = this.dir + this.file;
				string language = null;
				int length = text.Length;
				if (text.Substring(length - 3, 3).ToLower() == "cpp" || text.Substring(length - 1, 1).ToLower() == "c")
				{
					language = "C";
				}
				if (text.Substring(length - 3, 3).ToLower() == "pas")
				{
					language = "Pascal";
				}
				StreamReader streamReader = new StreamReader(text);
				string text2;
				while ((text2 = streamReader.ReadLine()) != null)
				{
					if (!(text2 == ""))
					{
						this.ParseLine(text2, language);
					}
				}
				streamReader.Close();
			}
			catch (Exception)
			{
			}
		}

		private void ParseLine(string line, string language)
		{
			try
			{
				Regex regex;
				if (language == "C")
				{
					regex = new Regex("([ \\t{}();])", RegexOptions.IgnorePatternWhitespace);
				}
				else
				{
					regex = new Regex("([ \\t{}();])", RegexOptions.IgnorePatternWhitespace);
				}
				string[] array = regex.Split(line);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					if (!(text == ""))
					{
						this.m_rtb.SelectionColor = Color.Black;
						this.m_rtb.SelectionFont = new Font("Courier New", 10f, FontStyle.Regular);
						if ((language == "C" && text == "//") || text.StartsWith("//"))
						{
							int num = line.IndexOf("//");
							string selectedText = line.Substring(num, line.Length - num);
							this.m_rtb.SelectionColor = Color.Gray;
							this.m_rtb.SelectionFont = new Font("Courier New", 10f, FontStyle.Regular);
							this.m_rtb.SelectedText = selectedText;
							break;
						}
						if ((language == "Pascal" && text == "{") || text.StartsWith("{"))
						{
							int num2 = line.IndexOf("{");
							string selectedText2 = line.Substring(num2, line.Length - num2);
							this.m_rtb.SelectionColor = Color.Gray;
							this.m_rtb.SelectionFont = new Font("Courier New", 10f, FontStyle.Regular);
							this.m_rtb.SelectedText = selectedText2;
							break;
						}
						if (language == "C")
						{
							this.kW = this.kC;
						}
						else
						{
							this.kW = this.kP;
						}
						if (this.kW.Contains(text.ToLower()))
						{
							this.m_rtb.SelectionColor = Color.Blue;
							this.m_rtb.SelectionFont = new Font("Courier New", 10f, FontStyle.Bold);
						}
						this.m_rtb.SelectedText = text;
					}
				}
				this.m_rtb.SelectedText = "\n";
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormViewSource));
			this.m_rtb = new RichTextBox();
			base.SuspendLayout();
			this.m_rtb.AcceptsTab = true;
			this.m_rtb.BackColor = SystemColors.Window;
			this.m_rtb.Dock = DockStyle.Fill;
			this.m_rtb.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.m_rtb.Location = new Point(0, 0);
			this.m_rtb.Name = "m_rtb";
			this.m_rtb.ReadOnly = true;
			this.m_rtb.Size = new Size(435, 398);
			this.m_rtb.TabIndex = 0;
			this.m_rtb.Text = "";
			this.m_rtb.WordWrap = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(435, 398);
			base.Controls.Add(this.m_rtb);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FormViewSource";
			base.ResumeLayout(false);
		}
	}
}
