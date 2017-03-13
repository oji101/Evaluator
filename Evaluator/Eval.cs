using JobManagement;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Evaluator
{
	public class Eval : Form
	{
		private static RegistryPermission readPerm1 = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE");

		private string rootDirectory;

		private bool calibratedMemory;

		private string[] files;

		private string[] sources;

		private string[] in_tests;

		private string[] out_tests;

		private int nrProblems;

		private string[] extensii = new string[]
		{
			".pas",
			".PAS",
			".pp",
			".PP",
			".cpp",
			".CPP",
			".cc",
			".CC",
			".c",
			".C"
		};

		private string compiler;

		private string cmd_line;

		private string error;

		private string the_exe;

		private string[] compilTxtFiles;

		private int nrSurse;

		private bool compilSingleProblem = true;

		private CheckedListBox.CheckedIndexCollection col;

		private string information;

		private string info2;

		private string problem = "";

		private string[] exe_s;

		private int nrTests;

		private int globalTest;

		private bool allProbs;

		private XmlDocument xml_doc;

		private XmlNodeList xml_probs;

		private string std_out;

		private int Total;

		private string dirProb;

		private string workDirectory;

		private double memoryLimit;

		private int timeLimit;

		private long zeroMemoryC;

		private long zeroMemoryCPP;

		private long zeroMemoryPAS;

		private string probName;

		private double stackLimit = 1048576.0;

		private string gpp;

		private string gcc;

		private string fpc;

		private long sourceDimension;

		private Dictionary<string, string> D;

		private SortedDictionary<string, KeyValuePair<int, double>> probLimits;

		private SortedDictionary<string, KeyValuePair<double, long>> probLimits2;

		private Dictionary<string, bool> canWriteCompil = new Dictionary<string, bool>();

		private Dictionary<string, string> assoc;

		private Dictionary<string, Contestant> contestantsID = new Dictionary<string, Contestant>();

		private List<KeyValuePair<int, string>> cont;

		private Dictionary<KeyValuePair<string, string>, bool> dEval = new Dictionary<KeyValuePair<string, string>, bool>();

		private Dictionary<string, ZeroMem> zMem;

		private string testNumber2;

		private int nrProbleme;

		private string testN = "";

		private bool loadExesTests = true;

		private bool allTests;

		private int selIndex;

		private bool evaluationRunning;

		private CheckedListBox.CheckedItemCollection col2;

		private static EventWaitHandle _wait1 = new AutoResetEvent(false);

		private static bool lovit = false;

		private bool undeterminedMemory;

		private static EventWaitHandle _waitHandle = new AutoResetEvent(false);

		private IContainer components;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem problemeToolStripMenuItem;

		private ToolStripMenuItem nouaToolStripMenuItem;

		private ToolStripMenuItem iesireToolStripMenuItem;

		private TabControl tabControl1;

		private TabPage tabPageCompil;

		private TabPage tabPageEval;

		private GroupBox groupBoxCompilare;

		private Button buttonCompil;

		private GroupBox groupBox1;

		private GroupBox groupBoxCompilProb;

		private CheckBox checkBoxCompilAll;

		private ComboBox comboBoxEvalProblem;

		private CheckBox checkBoxCompileaza;

		private TextBox textBoxCompilerMessage;

		private GroupBox groupBoxSurse;

		private GroupBox groupBox2;

		private GroupBox groupBoxExecutabile;

		private GroupBox groupBox3;

		private ListBox listBoxInputTests;

		private CheckedListBox listBoxExe;

		private CheckedListBox checkedListBoxSurse;

		private ListBox listBoxOutputTests;

		private GroupBox groupBoxSelect;

		private CheckBox checkBoxAllProblems;

		private CheckedListBox checkedListBoxExe;

		private CheckBox checkBoxAllIDs;

		private ComboBox comboBoxProblem;

		private Label label1;

		private Button buttonEval;

		private Button buttonOK;

		private Button buttonContestant;

		private Label label2;

		private ComboBox comboBoxTest;

		private CheckBox checkBoxAllTests;

		private CheckBox checkBoxStepByStep;

		private GroupBox groupBox4;

		private Label labelListView;

		private ListView listViewEval;

		private ColumnHeader Test;

		private ColumnHeader Punctaj;

		private ColumnHeader Timp;

		private ColumnHeader Memorie;

		private ColumnHeader Mesaj;

		private Button buttonIN;

		private ContextMenuStrip contextMenuStripDeleteExe;

		private ToolStripMenuItem stergeToolStripMenuItem1;

		private ToolTip toolTip1;

		private Label labelCompil;

		private Label labelInfoTime;

		private Label labelInfoProb;

		private Label labelMemLimit;

		private Label labelInfoTests;

		private GroupBox groupBox5;

		private Label labelMem;

		private Label labelTime;

		private Label labelProb;

		private ToolStripMenuItem rapoarteToolStripMenuItem;

		private ToolStripMenuItem compilareToolStripMenuItem;

		private ToolStripMenuItem evaluareToolStripMenuItem;

		private FolderBrowserDialog foldCompil;

		private ToolStripMenuItem despreToolStripMenuItem;

		private ToolStripMenuItem utilizareToolStripMenuItem;

		private ToolStripMenuItem produsToolStripMenuItem;

		private Button buttonDeleteExe;

		private CheckBox checkBoxAllExes;

		private ToolTip toolTipEvalAll;

		private ToolStripMenuItem compilareToolStripMenuItem1;

		private ToolTip toolTip2;

		private Label labelStack;

		private Label labelMaxSource;

		private Label labelSource;

		private Label labelStackLimit;

		private BackgroundWorker backgroundWorker1;

		private ProgressBar progressBar1;

		private ProgressBar progressBar2;

		private BackgroundWorker backgroundWorker2;

		private Button button1;

		private ContextMenuStrip contextMenuStripViewInputTest;

		private ToolStripMenuItem editeazaToolStripMenuItem;

		private Button button2;

		private ToolStripMenuItem stergeToolStripMenuItem;

		private Button button4;

		private Button button3;

		private ContextMenuStrip contextMenuStripOutputTest;

		private ToolStripMenuItem editeazaToolStripMenuItem1;

		private ToolStripMenuItem stergeToolStripMenuItem2;

		private Button button5;

		private ToolStripMenuItem concurentiToolStripMenuItem;

		public string rootDIR
		{
			get
			{
				return this.rootDirectory;
			}
		}

		public string GCC
		{
			get
			{
				return this.gcc;
			}
		}

		public string GPP
		{
			get
			{
				return this.gpp;
			}
		}

		public string PAS
		{
			get
			{
				return this.fpc;
			}
		}

		public CheckedListBox CheckedListBoxSurse
		{
			get
			{
				return this.checkedListBoxSurse;
			}
		}

		public string Info2
		{
			get
			{
				return this.info2;
			}
			set
			{
				this.info2 = value;
			}
		}

		public string Information
		{
			get
			{
				return this.information;
			}
			set
			{
				this.information = value;
			}
		}

		public string Problem
		{
			get
			{
				return this.problem;
			}
		}

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern int SetErrorMode(int wMode);

		[DllImport("ws2_32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int WSAGetLastError();

		public Eval()
		{
			this.InitializeComponent();
			this.rootDirectory = Directory.GetCurrentDirectory();
		}

		private string GetOSInfo()
		{
			OperatingSystem operatingSystem = null;
			try
			{
				operatingSystem = Environment.OSVersion;
			}
			catch (Exception)
			{
			}
			Version version = operatingSystem.Version;
			string text = "";
			if (operatingSystem.Platform == PlatformID.Win32Windows)
			{
				int minor = version.Minor;
				if (minor != 0)
				{
					if (minor != 10)
					{
						if (minor == 90)
						{
							text = "Me";
						}
					}
					else if (version.Revision.ToString() == "2222A")
					{
						text = "98SE";
					}
					else
					{
						text = "98";
					}
				}
				else
				{
					text = "95";
				}
			}
			else if (operatingSystem.Platform == PlatformID.Win32NT)
			{
				switch (version.Major)
				{
				case 3:
					text = "NT 3.51";
					break;
				case 4:
					text = "NT 4.0";
					break;
				case 5:
					if (version.Minor == 0)
					{
						text = "2000";
					}
					else
					{
						text = "XP";
					}
					break;
				case 6:
					if (version.Minor == 0)
					{
						text = "Vista";
					}
					else
					{
						text = "7";
					}
					break;
				}
			}
			if (text != "")
			{
				text = "Windows " + text;
				if (operatingSystem.ServicePack != "")
				{
					text = text + " " + operatingSystem.ServicePack;
				}
				string text2 = null;
				try
				{
					text2 = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
				}
				catch (Exception)
				{
				}
				try
				{
					text = text + " " + ((string.IsNullOrEmpty(text2) || string.Compare(text2, 0, "x86", 0, 3, true) == 0) ? 32 : 64).ToString();
				}
				catch (Exception)
				{
				}
			}
			return text;
		}

		private string GetInstalledSoftware()
		{
			string text = null;
			string oSInfo = this.GetOSInfo();
			string name = null;
			try
			{
				if (oSInfo.Substring(oSInfo.Length - 2, 2) == "64")
				{
					name = "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
				}
				else
				{
					name = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
				}
			}
			catch (Exception)
			{
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				try
				{
					string name2 = "FreePascal_is1";
					try
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(name2))
						{
							try
							{
								if (registryKey2.GetValue("DisplayName") != null)
								{
									if (registryKey2.GetValue("InstallLocation") == null)
									{
										this.fpc = "";
									}
									else
									{
										text = registryKey2.GetValue("InstallLocation") + "bin\\i386-win32\\";
										this.fpc = text + "fpc.exe";
									}
								}
							}
							catch (Exception)
							{
								this.fpc = "";
							}
						}
					}
					catch (Exception)
					{
						this.fpc = "";
					}
				}
				catch (Exception)
				{
				}
			}
			text = "";
			try
			{
				using (RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey(name))
				{
					string name3 = "MinGW Developer Studio";
					using (RegistryKey registryKey4 = registryKey3.OpenSubKey(name3))
					{
						try
						{
							text = registryKey4.GetValue("UninstallString").ToString();
							text = text.Substring(0, text.Length - 13) + "MinGW\\bin\\";
							this.gpp = text + "g++.exe";
							this.gcc = text + "gcc.exe";
						}
						catch (Exception)
						{
							this.gpp = "";
							this.gcc = "";
						}
					}
				}
			}
			catch (Exception)
			{
				this.gpp = "";
				this.gcc = "";
			}
			text = "";
			try
			{
				using (RegistryKey registryKey5 = Registry.CurrentUser.OpenSubKey("Software"))
				{
					string name4 = "CodeBlocks";
					using (RegistryKey registryKey6 = registryKey5.OpenSubKey(name4))
					{
						try
						{
							text = registryKey6.GetValue("Path").ToString();
							text += "\\MinGW\\bin\\";
							this.gpp = text + "g++.exe";
							this.gcc = text + "gcc.exe";
						}
						catch (Exception)
						{
							this.gpp = "";
							this.gcc = "";
						}
					}
				}
			}
			catch (Exception)
			{
				this.gpp = "";
				this.gcc = "";
			}
			return text;
		}

		private void LoadSourcesExesTests(bool auto)
		{
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				if (!auto)
				{
					this.problem = (string)this.comboBoxEvalProblem.SelectedItem;
				}
				string text = "..\\..\\probleme\\";
				if (!Directory.Exists(text + this.problem))
				{
					MessageBox.Show("Folderul " + this.problem + " nu exista!");
					Directory.SetCurrentDirectory(this.rootDirectory);
				}
				else
				{
					this.checkedListBoxSurse.Items.Clear();
					this.listBoxExe.Items.Clear();
					text = "..\\..\\probleme\\" + this.problem + "\\surse_concurenti\\";
					Directory.SetCurrentDirectory(text);
					this.files = Directory.GetFiles(Directory.GetCurrentDirectory());
					Array.Sort<string>(this.files);
					for (int i = 0; i < this.files.Length; i++)
					{
						bool flag = false;
						int num = 0;
						while (num < this.extensii.Length && !flag)
						{
							if (Path.GetExtension(this.files[i]) == this.extensii[num])
							{
								flag = true;
								string fileName = Path.GetFileName(this.files[i]);
								this.checkedListBoxSurse.Items.Add(fileName);
							}
							num++;
						}
						if (!flag)
						{
							File.Delete(this.files[i]);
						}
					}
					this.checkedListBoxSurse.Refresh();
					this.files = Directory.GetFiles(Directory.GetCurrentDirectory());
					Array.Sort<string>(this.files);
					text = "..\\exe_concurenti\\";
					Directory.SetCurrentDirectory(text);
					this.exe_s = Directory.GetFiles(Directory.GetCurrentDirectory());
					Array.Sort<string>(this.exe_s);
					for (int j = 0; j < this.exe_s.Length; j++)
					{
						string extension = Path.GetExtension(this.exe_s[j]);
						if (extension == ".exe" || extension == ".EXE")
						{
							string fileName2 = Path.GetFileName(this.exe_s[j]);
							this.listBoxExe.Items.Add(fileName2);
						}
						else
						{
							File.Delete(this.exe_s[j]);
						}
					}
					this.listBoxExe.Refresh();
					Directory.SetCurrentDirectory(this.rootDirectory);
					if (!auto)
					{
						this.problem = (string)this.comboBoxEvalProblem.SelectedItem;
					}
					text = "..\\..\\probleme\\";
					if (!Directory.Exists(text + this.problem))
					{
						MessageBox.Show("Folderul " + this.problem + " nu exista!");
						Directory.SetCurrentDirectory(this.rootDirectory);
					}
					else
					{
						this.listBoxInputTests.Items.Clear();
						this.listBoxOutputTests.Items.Clear();
						text = "..\\..\\probleme\\" + this.problem + "\\teste\\";
						Directory.SetCurrentDirectory(text);
						this.files = Directory.GetFiles(Directory.GetCurrentDirectory());
						Array.Sort<string>(this.files);
						for (int k = 0; k < this.files.Length; k++)
						{
							string extension2 = Path.GetExtension(this.files[k]);
							string text2 = Path.GetFileName(this.files[k]);
							if (extension2 == ".in" || extension2 == ".IN")
							{
								this.listBoxInputTests.Items.Add(text2);
							}
							else if (extension2 == ".ok" || extension2 == ".OK")
							{
								this.listBoxOutputTests.Items.Add(text2);
							}
							else
							{
								text2 = text2.ToLower();
								if (!(text2 == "verif.cpp") && !(text2 == "verif.pas") && !(text2 == "verif.c") && !(text2 == "verif.exe") && !(text2 == "teste.txt") && !(text2 == "Teste.txt") && !(text2 == "TESTE.TXT"))
								{
									File.Delete(this.files[k]);
								}
							}
						}
						this.listBoxInputTests.Refresh();
						this.listBoxOutputTests.Refresh();
						this.in_tests = new string[this.listBoxInputTests.Items.Count];
						this.out_tests = new string[this.listBoxOutputTests.Items.Count];
						this.listBoxInputTests.Items.CopyTo(this.in_tests, 0);
						this.listBoxOutputTests.Items.CopyTo(this.out_tests, 0);
						Directory.SetCurrentDirectory(this.rootDirectory);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void ResultCompil(string source, string the_exe, string prob, bool mustOverwrite)
		{
			string currentDirectory = null;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				string text = "..\\..\\rezultate\\compilare\\";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				string text2 = the_exe.Substring(0, the_exe.Length - 1);
				text2 = text2.ToUpper();
				string path = text + text2 + "-compil.txt";
				StreamWriter streamWriter;
				if (!File.Exists(path))
				{
					streamWriter = new StreamWriter(path, true);
					streamWriter.WriteLine("\t\t\t\t BORDEROU DE COMPILARE");
					streamWriter.WriteLine();
					string value = string.Format("{0,-11}{1,-15} {2, -20} {3, -30}", new object[]
					{
						"ID",
						"Nume",
						"Prenume",
						"Scoala"
					});
					streamWriter.WriteLine(value);
					streamWriter.Write("{0, -11}", text2);
					if (this.contestantsID.ContainsKey(text2))
					{
						value = string.Format("{0,-15} {1, -20} {2, -30}", this.contestantsID[text2].LastName, this.contestantsID[text2].FirstName, this.contestantsID[text2].Location);
						streamWriter.WriteLine(value);
					}
					else
					{
						streamWriter.WriteLine();
					}
					streamWriter.WriteLine();
				}
				else if (mustOverwrite)
				{
					streamWriter = new StreamWriter(path, false);
					streamWriter.WriteLine("\t\t\t\t BORDEROU DE COMPILARE");
					streamWriter.WriteLine();
					string value = string.Format("{0,-11}{1,-15} {2, -20} {3, -30}", new object[]
					{
						"ID",
						"Nume",
						"Prenume",
						"Scoala"
					});
					streamWriter.WriteLine(value);
					streamWriter.Write("{0, -11}", text2);
					if (this.contestantsID.ContainsKey(text2))
					{
						value = string.Format("{0,-15} {1, -20} {2, -30}", this.contestantsID[text2].LastName, this.contestantsID[text2].FirstName, this.contestantsID[text2].Location);
						streamWriter.WriteLine(value);
					}
					else
					{
						streamWriter.WriteLine();
					}
					streamWriter.WriteLine();
				}
				else
				{
					streamWriter = new StreamWriter(path, true);
				}
				streamWriter.WriteLine("Sursa:     " + source);
				streamWriter.WriteLine("Problema:  " + prob);
				streamWriter.WriteLine();
				streamWriter.WriteLine("Mesaj compilator:");
				streamWriter.WriteLine(this.error);
				streamWriter.WriteLine();
				streamWriter.WriteLine();
				streamWriter.Flush();
				streamWriter.Close();
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		private void CleanMove()
		{
		}

		private void SourceCompile(int index, bool mustOverride, BackgroundWorker bw, DoWorkEventArgs e)
		{
			if (bw.CancellationPending)
			{
				e.Cancel = true;
				return;
			}
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
			}
			catch (Exception)
			{
			}
			string text = "";
			string text2 = "";
			string currentDirectory = null;
			try
			{
				currentDirectory = "..\\..\\probleme\\" + this.problem + "\\surse_concurenti";
				Directory.SetCurrentDirectory(currentDirectory);
				this.sources = Directory.GetFiles(Directory.GetCurrentDirectory());
				Array.Sort<string>(this.sources);
				string path = this.sources[index];
				text = Path.GetExtension(path);
				text2 = Path.GetFileName(path);
				this.the_exe = Path.GetFileNameWithoutExtension(path);
			}
			catch (Exception)
			{
				try
				{
					Directory.SetCurrentDirectory(currentDirectory);
				}
				catch (Exception)
				{
				}
			}
			try
			{
				if (text == ".cpp" || text == ".CPP" || text == ".cc" || text == ".CC")
				{
					this.compiler = this.gpp;
					this.cmd_line = " -Wl,--stack=" + (this.probLimits2[this.problem].Key * 1048576.0).ToString() + " -O2 -Wall -o ";
					this.cmd_line += this.the_exe;
				}
				if (text == ".c" || text == ".C")
				{
					this.compiler = this.gcc;
					this.cmd_line = " -Wl,--stack=" + (this.probLimits2[this.problem].Key * 1048576.0).ToString() + " -O2 -Wall -o ";
					this.cmd_line += this.the_exe;
				}
				if (text == ".pas" || text == ".PAS" || text == ".pp" || text == ".PP")
				{
					this.compiler = this.fpc;
					this.cmd_line = " -O2 -Cs" + (this.probLimits2[this.problem].Key * 1048576.0).ToString();
				}
			}
			catch (Exception)
			{
				try
				{
					Directory.SetCurrentDirectory(currentDirectory);
				}
				catch (Exception)
				{
				}
			}
			this.cmd_line += " ";
			this.cmd_line += text2;
			try
			{
				Process process = new Process();
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.FileName = this.compiler;
				process.StartInfo.Arguments = this.cmd_line;
				UserState userState = new UserState("", "\"" + this.the_exe + text + "\"  ....  ", this.problem);
				int percentProgress = index * 100 / this.nrSurse + 1;
				bw.ReportProgress(percentProgress, userState);
				Eval._wait1.WaitOne();
				process.Start();
				string text3 = process.StandardError.ReadToEnd();
				string a = process.StandardOutput.ReadToEnd();
				if (!process.HasExited)
				{
					process.WaitForExit();
				}
				this.error = text3;
				if (this.error == "" && a != "")
				{
					this.error = a;
				}
				userState = new UserState(this.error, "", this.problem);
				bw.ReportProgress(percentProgress, userState);
				Eval._wait1.WaitOne();
				if (File.Exists(this.the_exe + ".exe"))
				{
					userState = new UserState(this.error, " OK !", this.problem);
					bw.ReportProgress(percentProgress, userState);
					Eval._wait1.WaitOne();
					try
					{
						if (File.Exists(this.the_exe + ".o"))
						{
							File.Delete(this.the_exe + ".o");
						}
						if (File.Exists("..\\exe_concurenti\\" + this.the_exe + ".exe"))
						{
							File.Delete("..\\exe_concurenti\\" + this.the_exe + ".exe");
						}
						userState = new UserState("", "", "");
						bw.ReportProgress(percentProgress, userState);
						Eval._wait1.WaitOne();
						File.Move(this.the_exe + ".exe", "..\\exe_concurenti\\" + this.the_exe + ".exe");
						goto IL_4DB;
					}
					catch (Exception)
					{
						goto IL_4DB;
					}
				}
				userState = new UserState(this.error, " Eroare !", this.problem);
				bw.ReportProgress(percentProgress, userState);
				Eval._wait1.WaitOne();
				IL_4DB:
				this.ResultCompil(text2, this.the_exe, this.problem, mustOverride);
			}
			catch (Exception)
			{
			}
		}

		private void checkBoxAllIDs_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.checkBoxAllProblems.Checked)
			{
				for (int i = 0; i < this.checkedListBoxExe.Items.Count; i++)
				{
					try
					{
						if (this.checkBoxAllIDs.Checked)
						{
							this.checkedListBoxExe.SetItemChecked(i, true);
						}
						else
						{
							this.checkedListBoxExe.SetItemChecked(i, false);
						}
					}
					catch (Exception)
					{
					}
				}
			}
			this.labelListView.TextAlign = ContentAlignment.MiddleCenter;
			this.labelListView.Text = "Panou de evaluare ";
			this.labelListView.Refresh();
			this.listViewEval.Items.Clear();
		}

		private void comboBoxEvalProblem_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ReadProbLimits(false, false);
			this.textBoxCompilerMessage.Text = "";
			this.textBoxCompilerMessage.Refresh();
			this.checkBoxCompileaza.Checked = false;
			this.checkBoxAllExes.Checked = false;
			this.problem = this.comboBoxEvalProblem.SelectedItem.ToString();
			try
			{
				this.labelInfoProb.Text = string.Format("Problema{0,15}", this.problem);
				this.labelInfoProb.Refresh();
				this.labelInfoTime.Text = string.Format("Limita timp: {0,8} ms", this.probLimits[this.problem].Key);
				this.labelInfoTime.Refresh();
				this.labelMemLimit.Text = string.Format("Memorie totala:{0,4} MB", this.probLimits[this.problem].Value);
				this.labelMemLimit.Refresh();
				this.labelMaxSource.Text = string.Format("Dimens. sursa:{0,5} KB", this.probLimits2[this.problem].Value);
				this.labelMaxSource.Refresh();
				this.labelStack.Text = string.Format("Limita stiva:{0,9} MB", this.probLimits2[this.problem].Key);
				this.labelStack.Refresh();
				this.labelCompil.Text = "[" + this.problem + "]    ";
				this.labelCompil.Refresh();
				this.LoadSourcesExesTests(false);
				this.labelInfoTests.Text = string.Format("Numarul de teste: {0,5}  ", this.listBoxInputTests.Items.Count);
				this.labelInfoTests.Refresh();
			}
			catch (Exception)
			{
			}
		}

		private void buttonCompil_Click(object sender, EventArgs e)
		{
			if (this.comboBoxEvalProblem.Items.Count == 0)
			{
				MessageBox.Show("Nu s-a definit nicio problema!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (!this.calibratedMemory)
			{
				this.zMem = new Dictionary<string, ZeroMem>();
				this.labelCompil.Text = "Asteptati... Se calibreaza memoria ";
				try
				{
					this.buttonCompil.Enabled = false;
					this.checkBoxAllExes.Enabled = false;
					this.checkBoxCompilAll.Enabled = false;
					this.checkBoxCompileaza.Enabled = false;
					this.buttonCompil.Enabled = false;
					this.buttonDeleteExe.Enabled = false;
					this.button1.Enabled = false;
					this.button2.Enabled = false;
					this.button3.Enabled = false;
					this.button4.Enabled = false;
					this.button5.Enabled = false;
					this.menuStrip1.Enabled = false;
					this.tabPageEval.Enabled = false;
					Directory.SetCurrentDirectory(this.rootDirectory);
					if (!Directory.Exists("..\\..\\work\\"))
					{
						DirectoryInfo directoryInfo = Directory.CreateDirectory("..\\..\\work\\");
						directoryInfo.Attributes = (FileAttributes.Hidden | FileAttributes.Directory);
					}
					string str = "zerocpp.cpp";
					StreamWriter streamWriter = new StreamWriter("..\\..\\work\\" + str);
					streamWriter.WriteLine("#include <fstream>");
					streamWriter.WriteLine("using namespace std;");
					streamWriter.WriteLine("int main()");
					streamWriter.WriteLine("{");
					streamWriter.WriteLine("return 0;");
					streamWriter.WriteLine("}");
					streamWriter.Close();
					str = "zeroc.c";
					streamWriter = new StreamWriter("..\\..\\work\\" + str);
					streamWriter.WriteLine("#include <stdio.h>");
					streamWriter.WriteLine("int main()");
					streamWriter.WriteLine("{");
					streamWriter.WriteLine("return 0;");
					streamWriter.WriteLine("}");
					streamWriter.Close();
					str = "zeropas.pas";
					streamWriter = new StreamWriter("..\\..\\work\\" + str);
					streamWriter.WriteLine("program zero;");
					streamWriter.WriteLine("begin");
					streamWriter.WriteLine("end.");
					streamWriter.Close();
				}
				catch (Exception)
				{
				}
				foreach (string current in this.probLimits.Keys)
				{
					this.CalibrateMemory(current);
					this.labelCompil.Text = "Asteptati... Se calibreaza memoria ";
					this.labelCompil.Refresh();
				}
				this.calibratedMemory = true;
				this.buttonCompil.Enabled = true;
				this.checkBoxAllExes.Enabled = true;
				this.checkBoxCompilAll.Enabled = true;
				this.checkBoxCompileaza.Enabled = true;
				this.buttonCompil.Enabled = true;
				this.buttonDeleteExe.Enabled = true;
				this.button1.Enabled = true;
				this.button2.Enabled = true;
				this.button3.Enabled = true;
				this.button4.Enabled = true;
				this.button5.Enabled = true;
				this.menuStrip1.Enabled = true;
				this.tabPageEval.Enabled = true;
				try
				{
					if (File.Exists("..\\..\\work\\zerocpp.cpp"))
					{
						File.Delete("..\\..\\work\\zerocpp.cpp");
					}
					if (File.Exists("..\\..\\work\\zeroc.c"))
					{
						File.Delete("..\\..\\work\\zeroc.c");
					}
					if (File.Exists("..\\..\\work\\zeropas.pas"))
					{
						File.Delete("..\\..\\work\\zeropas.pas");
					}
					if (File.Exists("..\\..\\work\\zerocpp.exe"))
					{
						File.Delete("..\\..\\work\\zerocpp.exe");
					}
					if (File.Exists("..\\..\\work\\zeroc.exe"))
					{
						File.Delete("..\\..\\work\\zeroc.exe");
					}
					if (File.Exists("..\\..\\work\\zeropas.exe"))
					{
						File.Delete("..\\..\\work\\zeropas.exe");
					}
					if (File.Exists("..\\..\\work\\zeropas.exe"))
					{
						File.Delete("..\\..\\work\\zeropas.o");
					}
				}
				catch (Exception)
				{
				}
			}
			if (this.checkBoxCompilAll.Checked)
			{
				bool flag = false;
				do
				{
					try
					{
						Directory.SetCurrentDirectory(this.rootDirectory);
						if (!Directory.Exists("..\\..\\rezultate\\compilare"))
						{
							Directory.CreateDirectory("..\\..\\rezultate\\compilare");
						}
						else
						{
							this.compilTxtFiles = Directory.GetFiles("..\\..\\rezultate\\compilare");
							Array.Sort<string>(this.compilTxtFiles);
						}
					}
					catch (Exception)
					{
					}
				}
				while (flag);
				if (this.compilTxtFiles != null)
				{
					string[] array = this.compilTxtFiles;
					for (int i = 0; i < array.Length; i++)
					{
						string path = array[i];
						try
						{
							File.Delete(path);
						}
						catch (Exception)
						{
						}
					}
				}
				try
				{
					if (!this.backgroundWorker1.IsBusy)
					{
						this.checkBoxAllExes.Enabled = false;
						this.checkBoxCompileaza.Enabled = false;
						this.checkBoxCompilAll.Enabled = false;
						this.buttonDeleteExe.Enabled = false;
						this.backgroundWorker1.RunWorkerAsync();
						this.buttonCompil.Text = "Stop";
						this.button1.Enabled = false;
						this.button2.Enabled = false;
						this.button3.Enabled = false;
						this.button4.Enabled = false;
						this.button5.Enabled = false;
						this.menuStrip1.Enabled = false;
						this.tabPageEval.Enabled = false;
						this.comboBoxEvalProblem.UseWaitCursor = true;
					}
					else
					{
						this.backgroundWorker1.CancelAsync();
					}
					return;
				}
				catch (Exception)
				{
					return;
				}
			}
			if (this.checkedListBoxSurse.Items.Count == 0)
			{
				this.labelCompil.Text = "Terminat!";
				this.labelCompil.Refresh();
			}
			else
			{
				try
				{
					this.compilSingleProblem = true;
					this.problem = this.comboBoxEvalProblem.SelectedItem.ToString();
					this.labelInfoProb.Text = string.Format("Problema{0,15}", this.problem);
					this.labelInfoProb.Refresh();
					this.labelInfoTime.Text = string.Format("Limita timp: {0,8} ms", this.probLimits[this.problem].Key);
					this.labelInfoTime.Refresh();
					this.labelMemLimit.Text = string.Format("Memorie totala:{0,4} MB", this.probLimits[this.problem].Value);
					this.labelMemLimit.Refresh();
					this.labelMaxSource.Text = string.Format("Dimens. sursa:{0,5} KB", this.probLimits2[this.problem].Value);
					this.labelMaxSource.Refresh();
					this.labelStack.Text = string.Format("Limita stiva:{0,9} MB", this.probLimits2[this.problem].Key);
					this.labelStack.Refresh();
					this.labelCompil.Text = "[" + this.problem + "]    ";
					this.labelCompil.Refresh();
					this.labelInfoTests.Text = string.Format("Numarul de teste: {0,5}  ", this.listBoxInputTests.Items.Count);
					this.labelInfoTests.Refresh();
				}
				catch (Exception)
				{
				}
				this.col = this.checkedListBoxSurse.CheckedIndices;
				try
				{
					if (!this.backgroundWorker1.IsBusy)
					{
						this.checkBoxAllExes.Enabled = false;
						this.checkBoxCompileaza.Enabled = false;
						this.checkBoxCompilAll.Enabled = false;
						this.buttonDeleteExe.Enabled = false;
						this.button1.Enabled = false;
						this.button2.Enabled = false;
						this.button3.Enabled = false;
						this.button4.Enabled = false;
						this.button5.Enabled = false;
						this.menuStrip1.Enabled = false;
						this.tabPageEval.Enabled = false;
						this.comboBoxEvalProblem.UseWaitCursor = true;
						this.backgroundWorker1.RunWorkerAsync();
						this.buttonCompil.Text = "Stop";
					}
					else
					{
						this.backgroundWorker1.CancelAsync();
					}
				}
				catch (Exception)
				{
				}
			}
		}

		private void checkedListBoxSurse_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.textBoxCompilerMessage.Text = "";
			this.textBoxCompilerMessage.Refresh();
			this.progressBar1.Value = 0;
			this.progressBar1.Refresh();
			this.labelCompil.Text = "";
			this.labelCompil.Text = "";
			this.labelCompil.Refresh();
		}

		private void CalibrateMemory(string prob)
		{
			string[] array = new string[]
			{
				"zerocpp",
				"zeroc",
				"zeropas"
			};
			try
			{
				int i = 0;
				while (i < 3)
				{
					Label expr_35 = this.labelListView;
					expr_35.Text += ". ";
					this.labelListView.Refresh();
					Label expr_5B = this.labelCompil;
					expr_5B.Text += ". ";
					this.labelCompil.Refresh();
					string currentDirectory = "..\\..\\work\\";
					Directory.SetCurrentDirectory(currentDirectory);
					if (i == 0)
					{
						this.compiler = this.gpp;
						this.cmd_line = " -Wl,--stack=" + (this.probLimits2[prob].Key * 1048576.0).ToString() + " -O2 -Wall  -o zerocpp zerocpp.cpp";
					}
					if (i == 1)
					{
						this.compiler = this.gcc;
						this.cmd_line = " -Wl,--stack=" + (this.probLimits2[prob].Key * 1048576.0).ToString() + " -O2 -Wall-o zeroc zeroc.c";
					}
					if (i == 2)
					{
						this.compiler = this.fpc;
						this.cmd_line = " -O2 -Cs" + (this.probLimits2[prob].Key * 1048576.0).ToString() + " zeropas.pas";
					}
					Process process = new Process();
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardError = true;
					process.StartInfo.CreateNoWindow = true;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.FileName = this.compiler;
					process.StartInfo.Arguments = this.cmd_line;
					process.Start();
					process.WaitForExit();
					using (JobObject jobObject = new JobObject("Compilare"))
					{
						ProcessStartInfo processStartInfo = new ProcessStartInfo(array[i]);
						this.timeLimit = this.probLimits[prob].Key;
						processStartInfo.UseShellExecute = false;
						processStartInfo.CreateNoWindow = true;
						int errorMode = Eval.SetErrorMode(3);
						long num = 0L;
						Process process2 = jobObject.CreateProcessMayBreakAway(processStartInfo);
						try
						{
							process2.WaitForExit();
						}
						catch (Exception)
						{
						}
						try
						{
							num = long.Parse(jobObject.PeakJobMemoryUsed.ToString());
							process2.Close();
						}
						catch (Exception)
						{
							i--;
							Directory.SetCurrentDirectory(this.rootDirectory);
							goto IL_2B6;
						}
						finally
						{
							Eval.SetErrorMode(errorMode);
						}
						if (i == 0)
						{
							this.zeroMemoryCPP = num;
						}
						if (i == 1)
						{
							this.zeroMemoryC = num;
						}
						if (i == 2)
						{
							this.zeroMemoryPAS = num;
						}
					}
					goto IL_2AB;
					IL_2B6:
					i++;
					continue;
					IL_2AB:
					Directory.SetCurrentDirectory(this.rootDirectory);
					goto IL_2B6;
				}
				if (!this.zMem.ContainsKey(prob))
				{
					this.zMem.Add(prob, new ZeroMem(this.zeroMemoryCPP, this.zeroMemoryC, this.zeroMemoryPAS));
				}
			}
			catch (Exception)
			{
				if (this.zeroMemoryCPP == 0L)
				{
					this.zeroMemoryCPP = 201000L;
				}
				if (this.zeroMemoryC == 0L)
				{
					this.zeroMemoryC = 201000L;
				}
				if (this.zeroMemoryPAS == 0L)
				{
					this.zeroMemoryPAS = 470000L;
				}
				if (!this.zMem.ContainsKey(prob))
				{
					this.zMem.Add(prob, new ZeroMem(this.zeroMemoryCPP, this.zeroMemoryC, this.zeroMemoryPAS));
				}
				Directory.SetCurrentDirectory(this.rootDirectory);
			}
		}

		private void ReadContestantsData(bool canDeleteID)
		{
			string str = null;
			string currentDirectory = null;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				str = "..\\..\\";
			}
			catch (Exception)
			{
			}
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
					string text = null;
					string text2 = null;
					string text3 = null;
					string text4 = null;
					string text5;
					while ((text5 = streamReader.ReadLine()) != null)
					{
						string[] array = text5.Split(separator, StringSplitOptions.RemoveEmptyEntries);
						for (int i = 0; i < array.Length; i++)
						{
							if (i == 0)
							{
								text = array[i];
							}
							if (i == 1)
							{
								text2 = array[i];
							}
							if (i == 2)
							{
								text3 = array[i];
							}
							if (i == 3)
							{
								text4 = array[i];
							}
						}
						text = text.ToUpper();
						if (!this.contestantsID.ContainsKey(text))
						{
							this.contestantsID.Add(text, new Contestant(text, text2, text3, text4));
						}
						else
						{
							this.contestantsID[text].LastName = text2;
							this.contestantsID[text].FirstName = text3;
							this.contestantsID[text].Location = text4;
						}
					}
					streamReader.Close();
					Directory.SetCurrentDirectory(currentDirectory);
				}
				catch (Exception)
				{
					Directory.SetCurrentDirectory(currentDirectory);
				}
			}
		}

		private void ReadProbLimits(bool canAdd, bool canDeleteID)
		{
			this.xml_doc = new XmlDocument();
			this.probLimits = new SortedDictionary<string, KeyValuePair<int, double>>();
			this.probLimits2 = new SortedDictionary<string, KeyValuePair<double, long>>();
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
				string path = "..\\..\\work";
				if (!Directory.Exists(path))
				{
					DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
					directoryInfo.Attributes = (FileAttributes.Hidden | FileAttributes.Directory);
				}
				path = "..\\..\\rezultate\\compilare";
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				path = "..\\..\\rezultate\\evaluare";
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				string str = "..\\..\\";
				this.canWriteCompil.Clear();
				if (File.Exists(str + "rounds.xml"))
				{
					this.xml_doc.Load(str + "rounds.xml");
					this.xml_probs = this.xml_doc.SelectNodes("oji/problema");
					if (this.xml_probs.Count == 0)
					{
						this.checkedListBoxSurse.Items.Clear();
						this.listBoxExe.Items.Clear();
						this.listBoxInputTests.Items.Clear();
						this.listBoxOutputTests.Items.Clear();
						this.nrProblems = 0;
						return;
					}
					for (int i = 0; i < this.xml_probs.Count; i++)
					{
						XmlNode xmlNode = this.xml_probs.Item(i).SelectSingleNode("nume");
						if (canAdd && !this.comboBoxEvalProblem.Items.Contains(xmlNode.InnerText.ToString()))
						{
							this.comboBoxEvalProblem.Items.Add(xmlNode.InnerText);
						}
						this.probName = xmlNode.InnerText;
						try
						{
							path = "..\\..\\probleme\\" + this.probName + "\\surse_concurenti";
							if (!Directory.Exists(path))
							{
								Directory.CreateDirectory(path);
							}
							path = "..\\..\\probleme\\" + this.probName + "\\exe_concurenti";
							if (!Directory.Exists(path))
							{
								Directory.CreateDirectory(path);
							}
							path = "..\\..\\probleme\\" + this.probName + "\\teste";
							if (!Directory.Exists(path))
							{
								Directory.CreateDirectory(path);
							}
						}
						catch (Exception)
						{
						}
						if (!this.canWriteCompil.ContainsKey(xmlNode.InnerText))
						{
							this.canWriteCompil.Add(xmlNode.InnerText, true);
						}
						xmlNode = this.xml_probs.Item(i).SelectSingleNode("limita_timp");
						this.timeLimit = int.Parse(xmlNode.InnerText);
						xmlNode = this.xml_probs.Item(i).SelectSingleNode("limita_memorie");
						this.memoryLimit = double.Parse(xmlNode.InnerText);
						this.probLimits.Add(this.probName, new KeyValuePair<int, double>(this.timeLimit, this.memoryLimit));
						xmlNode = this.xml_probs.Item(i).SelectSingleNode("limita_stiva");
						this.stackLimit = double.Parse(xmlNode.InnerText);
						xmlNode = this.xml_probs.Item(i).SelectSingleNode("limita_dimens_sursa");
						this.sourceDimension = long.Parse(xmlNode.InnerText);
						this.probLimits2.Add(this.probName, new KeyValuePair<double, long>(this.stackLimit, this.sourceDimension));
					}
					this.nrProblems = this.xml_probs.Count;
				}
				else
				{
					MessageBox.Show("nu exista fisierul \"rounds.xml\"", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			catch (Exception)
			{
			}
			this.ReadContestantsData(canDeleteID);
		}

		private void tabPageCompil_Enter(object sender, EventArgs e)
		{
			if (this.evaluationRunning)
			{
				return;
			}
			this.ReadProbLimits(true, false);
			try
			{
				string path = "..\\..\\work";
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
			}
			catch (Exception)
			{
			}
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				string text = "..\\..\\work";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				Directory.SetCurrentDirectory(text);
				if (this.nrProblems != 0 && this.comboBoxProblem.Text == "")
				{
					this.comboBoxEvalProblem.SelectedIndex = 0;
				}
				else
				{
					this.comboBoxProblem.SelectedItem = this.problem;
				}
			}
			catch (Exception)
			{
			}
		}

		private void CreateProblemFolders(string problem)
		{
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				if (this.comboBoxEvalProblem.Items.Contains(problem))
				{
					MessageBox.Show("Problema " + problem + " exista deja !");
				}
				else
				{
					string path = "..\\..\\probleme\\" + problem + "\\surse_concurenti";
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					path = "..\\..\\probleme\\" + problem + "\\exe_concurenti";
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					path = "..\\..\\probleme\\" + problem + "\\teste";
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void nouaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormNewProblem formNewProblem = new FormNewProblem(this);
			formNewProblem.ShowDialog();
			if (formNewProblem.Changes)
			{
				this.calibratedMemory = false;
				this.contestantsID.Clear();
				this.dEval.Clear();
				this.ReadContestantsData(false);
				string currentDirectory = Directory.GetCurrentDirectory();
				try
				{
					Directory.SetCurrentDirectory(this.rootDirectory);
					string path = "..\\..\\rezultate\\evaluare\\";
					string[] array = Directory.GetFiles(path);
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string path2 = array2[i];
						File.Delete(path2);
					}
					Directory.SetCurrentDirectory(currentDirectory);
				}
				catch (Exception)
				{
					Directory.SetCurrentDirectory(currentDirectory);
				}
			}
			this.comboBoxEvalProblem.Items.Clear();
			this.comboBoxEvalProblem.Refresh();
			if (formNewProblem.Changes)
			{
				this.ReadProbLimits(true, true);
			}
			else
			{
				this.ReadProbLimits(true, false);
			}
			try
			{
				string text = this.comboBoxEvalProblem.Text;
				if (this.probLimits2.Count == 0 || !this.probLimits2.ContainsKey(text))
				{
					this.comboBoxEvalProblem.Text = "";
					this.textBoxCompilerMessage.Text = "";
					this.textBoxCompilerMessage.Refresh();
					this.checkBoxCompileaza.Checked = false;
					this.checkBoxAllExes.Checked = false;
					this.labelInfoProb.Text = string.Format("Problema:", new object[0]);
					this.labelInfoProb.Refresh();
					this.labelInfoTime.Text = string.Format("Limita timp:", new object[0]);
					this.labelInfoTime.Refresh();
					this.labelMemLimit.Text = string.Format("Memorie totala:", new object[0]);
					this.labelMemLimit.Refresh();
					this.labelMaxSource.Text = string.Format("Dimens. sursa:", new object[0]);
					this.labelMaxSource.Refresh();
					this.labelStack.Text = string.Format("Limita stiva:", new object[0]);
					this.labelStack.Refresh();
					this.labelInfoTests.Text = string.Format("Numarul de teste:", new object[0]);
					this.labelInfoTests.Refresh();
				}
				text = this.comboBoxProblem.Text;
				if (this.probLimits2.Count == 0 || !this.probLimits2.ContainsKey(text))
				{
					this.comboBoxProblem.Text = "";
					this.checkBoxAllIDs.Checked = false;
					this.checkedListBoxExe.Items.Clear();
					this.checkBoxAllTests.Checked = false;
					this.labelProb.Text = "Problema:";
					this.labelProb.Refresh();
					this.labelMem.Text = "Memorie:";
					this.labelMem.Refresh();
					this.labelSource.Text = "Dim. sursa:";
					this.labelSource.Refresh();
					this.labelTime.Text = "Timp:";
					this.labelTime.Refresh();
					this.labelStackLimit.Text = "Stiva:";
					this.labelStackLimit.Refresh();
				}
			}
			catch (Exception)
			{
			}
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				if (!Directory.Exists("..\\..\\work"))
				{
					DirectoryInfo directoryInfo = Directory.CreateDirectory("..\\..\\work");
					directoryInfo.Attributes = (FileAttributes.Hidden | FileAttributes.Directory);
				}
				Directory.SetCurrentDirectory("..\\..\\work");
				this.workDirectory = Directory.GetCurrentDirectory();
			}
			catch (Exception)
			{
			}
			this.ReadRoundsXml();
		}

		private void buttonDeleteExe_Click(object sender, EventArgs e)
		{
			string currentDirectory = null;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				string str = "..\\..\\probleme\\" + this.comboBoxEvalProblem.Text + "\\exe_concurenti\\";
				string text = this.listBoxExe.SelectedItem.ToString();
				if (!File.Exists(str + text))
				{
					MessageBox.Show("Fisierul '" + text + "' nu exista!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					File.Delete(str + text);
				}
				this.listBoxExe.Items.Remove(text);
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				string str2 = "..\\..\\probleme\\" + this.comboBoxEvalProblem.Text + "\\exe_concurenti\\";
				for (int i = 0; i < this.listBoxExe.Items.Count; i++)
				{
					if (this.listBoxExe.GetItemChecked(i))
					{
						this.listBoxExe.SetSelected(i, true);
						string text2 = this.listBoxExe.SelectedItem.ToString();
						if (!File.Exists(str2 + text2))
						{
							MessageBox.Show("Fisierul '" + text2 + "' nu exista!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
						else
						{
							this.listBoxExe.SetItemChecked(i, false);
							File.Delete(str2 + text2);
							i--;
							this.listBoxExe.Items.Remove(text2);
						}
					}
				}
				this.listBoxExe.Refresh();
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		private bool ReadNrTestsAndPoints()
		{
			string text = this.dirProb + this.problem + "\\teste\\";
			if (!Directory.Exists(text))
			{
				MessageBox.Show("Folderul " + text + " nu exista!");
				return false;
			}
			if (!File.Exists(text + "teste.txt"))
			{
				MessageBox.Show("Nu exista fisierul" + text + "teste.txt\nEvaluare oprita !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.checkBoxAllProblems.Enabled = true;
				this.checkBoxAllProblems.Checked = false;
				return false;
			}
			string path = text + "teste.txt";
			try
			{
				StreamReader streamReader = new StreamReader(path);
				this.D = new Dictionary<string, string>();
				char[] separator = new char[]
				{
					' '
				};
				string text2;
				while ((text2 = streamReader.ReadLine()) != null)
				{
					string[] array = text2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
					if (array.Length == 1)
					{
						this.D.Add(array[0], "10");
					}
					else if (array.Length == 2)
					{
						this.D.Add(array[0], array[1]);
					}
				}
				streamReader.Close();
				this.nrTests = this.D.Count;
			}
			catch (Exception)
			{
				MessageBox.Show("Nu s-a putut citi fisierul 'teste.txt'");
				return false;
			}
			return true;
		}

		private bool LoadExesTests(bool load)
		{
			if (!load)
			{
				return true;
			}
			try
			{
				if (!Directory.Exists(this.dirProb + this.problem))
				{
					MessageBox.Show("Folderul " + this.problem + " nu exista!\nEvaluare oprita !");
					this.checkBoxAllProblems.Checked = false;
					bool result = false;
					return result;
				}
				if (!this.ReadNrTestsAndPoints())
				{
					this.checkedListBoxExe.Items.Clear();
					bool result = false;
					return result;
				}
				this.comboBoxTest.Items.Clear();
				if (this.nrTests != 0)
				{
					foreach (KeyValuePair<string, string> current in this.D)
					{
						this.comboBoxTest.Items.Add(current.Key);
					}
				}
				this.checkedListBoxExe.Items.Clear();
				string text = this.dirProb + this.problem + "\\exe_concurenti\\";
				this.exe_s = Directory.GetFiles(text);
				Array.Sort<string>(this.exe_s);
				for (int i = 0; i < this.exe_s.Length; i++)
				{
					string extension = Path.GetExtension(this.exe_s[i]);
					if (extension == ".exe" || extension == ".EXE")
					{
						string fileName = Path.GetFileName(this.exe_s[i]);
						this.checkedListBoxExe.Items.Add(fileName);
					}
					else
					{
						File.Delete(text + this.exe_s[i]);
					}
				}
				if (this.exe_s.Length == 0 && this.nrTests != 0)
				{
					MessageBox.Show("Nu exista fisiere .exe pentru aceasta problema !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.checkBoxAllProblems.Checked = false;
					bool result = false;
					return result;
				}
				if (this.nrTests == 0 && this.exe_s.Length != 0)
				{
					MessageBox.Show("Nu exista teste pentru aceasta problema !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.checkBoxAllProblems.Checked = false;
					bool result = false;
					return result;
				}
				if (this.nrTests == 0 && this.exe_s.Length == 0)
				{
					MessageBox.Show("Nu exista surse si teste pentru aceasta problema !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					this.checkBoxAllProblems.Checked = false;
					bool result = false;
					return result;
				}
				if (this.checkBoxAllProblems.Checked)
				{
					this.checkBoxAllIDs.Checked = true;
					for (int j = 0; j < this.checkedListBoxExe.Items.Count; j++)
					{
						if (this.checkBoxAllIDs.Checked)
						{
							this.checkedListBoxExe.SetItemChecked(j, true);
						}
					}
				}
				this.checkedListBoxExe.Refresh();
			}
			catch (Exception)
			{
				bool result = false;
				return result;
			}
			return true;
		}

		private bool ReadRoundsXml()
		{
			this.xml_doc = new XmlDocument();
			this.probLimits = new SortedDictionary<string, KeyValuePair<int, double>>();
			this.probLimits2 = new SortedDictionary<string, KeyValuePair<double, long>>();
			this.comboBoxProblem.Items.Clear();
			try
			{
				string str = "..\\";
				if (!File.Exists(str + "rounds.xml"))
				{
					MessageBox.Show("Nu exista fisierul \"rounds.xml\"! Va fi creat un altul.", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					File.CreateText(str + "rounds.xml");
					bool result = false;
					return result;
				}
				this.xml_doc.Load(str + "rounds.xml");
				this.xml_probs = this.xml_doc.SelectNodes("oji/problema");
				for (int i = 0; i < this.xml_probs.Count; i++)
				{
					XmlNode xmlNode = this.xml_probs.Item(i).SelectSingleNode("nume");
					this.probName = xmlNode.InnerText;
					this.comboBoxProblem.Items.Add(this.probName);
					xmlNode = this.xml_probs.Item(i).SelectSingleNode("limita_timp");
					this.timeLimit = int.Parse(xmlNode.InnerText);
					xmlNode = this.xml_probs.Item(i).SelectSingleNode("limita_memorie");
					this.memoryLimit = double.Parse(xmlNode.InnerText);
					this.probLimits.Add(this.probName, new KeyValuePair<int, double>(this.timeLimit, this.memoryLimit));
					xmlNode = this.xml_probs.Item(i).SelectSingleNode("limita_stiva");
					this.stackLimit = double.Parse(xmlNode.InnerText);
					xmlNode = this.xml_probs.Item(i).SelectSingleNode("limita_dimens_sursa");
					this.sourceDimension = long.Parse(xmlNode.InnerText);
					this.probLimits2.Add(this.probName, new KeyValuePair<double, long>(this.stackLimit, this.sourceDimension));
				}
			}
			catch (Exception)
			{
				bool result = false;
				return result;
			}
			return true;
		}

		private void tabControl1_Selected(object sender, TabControlEventArgs e)
		{
		}

		private void comboBoxProblem_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				string text = "..\\..\\work";
				if (!Directory.Exists(text))
				{
					DirectoryInfo directoryInfo = Directory.CreateDirectory(text);
					directoryInfo.Attributes = (FileAttributes.Hidden | FileAttributes.Directory);
				}
				Directory.SetCurrentDirectory(text);
			}
			catch (Exception)
			{
			}
			this.problem = this.comboBoxProblem.SelectedItem.ToString();
			this.dirProb = "..\\probleme\\";
			this.checkBoxAllIDs.Enabled = true;
			this.checkBoxAllIDs.Checked = true;
			this.LoadExesTests(true);
			try
			{
				this.labelProb.Text = string.Format("Problema:{0,10}", this.problem);
				this.labelProb.Refresh();
				this.labelTime.Text = string.Format("Timp: {0, 12} ms", this.probLimits[this.problem].Key);
				this.labelTime.Refresh();
				this.labelMem.Text = string.Format("Memorie: {0, 7} MB", this.probLimits[this.problem].Value);
				this.labelMem.Refresh();
				this.labelSource.Text = string.Format("Dim. sursa:{0,6} KB", this.probLimits2[this.problem].Value);
				this.labelSource.Refresh();
				this.labelStackLimit.Text = string.Format("Stiva:{0,13} MB", this.probLimits2[this.problem].Key);
				this.labelStackLimit.Refresh();
			}
			catch (Exception)
			{
			}
			for (int i = 0; i < this.checkedListBoxExe.Items.Count; i++)
			{
				if (this.checkBoxAllIDs.Checked)
				{
					this.checkedListBoxExe.SetItemChecked(i, true);
				}
			}
		}

		private void InsertListView(string testNumber, string points, string time, string memory, string msg)
		{
			if (testNumber != "Total:")
			{
				this.Total += int.Parse(points);
			}
			if (testNumber != "Total:" && int.Parse(testNumber) == 0)
			{
				this.listViewEval.Items.Clear();
			}
			ListViewItem listViewItem = new ListViewItem();
			ListViewItem listViewItem2 = new ListViewItem();
			if (testNumber == "Total:")
			{
				listViewItem2.ForeColor = Color.Red;
				listViewItem2.UseItemStyleForSubItems = true;
				listViewItem2.Text = "";
				ListViewItem listViewItem3 = new ListViewItem();
				ListViewItem listViewItem4 = new ListViewItem();
				ListViewItem listViewItem5 = new ListViewItem();
				ListViewItem listViewItem6 = new ListViewItem();
				listViewItem3.Text = "";
				listViewItem4.Text = "";
				listViewItem5.Text = "";
				listViewItem6.Text = "";
				listViewItem2.SubItems.Add(listViewItem3.Text);
				listViewItem2.SubItems.Add(listViewItem4.Text);
				listViewItem2.SubItems.Add(listViewItem5.Text);
				listViewItem2.SubItems.Add(listViewItem6.Text);
				this.listViewEval.Items.Add(listViewItem2);
			}
			listViewItem.Text = testNumber;
			listViewItem.SubItems.Add(points);
			listViewItem.SubItems.Add(time);
			listViewItem.SubItems.Add(memory);
			listViewItem.SubItems.Add(msg);
			this.listViewEval.Items.Add(listViewItem);
			this.listViewEval.Update();
		}

		private void DefaultVerif(string testNumber)
		{
			try
			{
				string path = this.problem + ".ok";
				string path2 = this.problem + ".out";
				if (!File.Exists(path2))
				{
					this.std_out = "0 Nu exista fisierul " + this.problem + ".out";
				}
				else if (!File.Exists(path))
				{
					MessageBox.Show(string.Concat(new string[]
					{
						"Nu s-a putut copia fisierul ",
						testNumber,
						"-",
						this.problem,
						".ok !"
					}), "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					StreamReader streamReader = new StreamReader(this.problem + ".ok");
					StreamReader streamReader2 = new StreamReader(this.problem + ".out");
					char[] trimChars = new char[]
					{
						' '
					};
					string text;
					string text2;
					while ((text = streamReader.ReadLine()) != null)
					{
						text = text.TrimEnd(trimChars);
						text2 = streamReader2.ReadLine();
						if (!(text == ""))
						{
							if (text2 == null)
							{
								this.std_out = "0 Incorect!";
								streamReader.Close();
								streamReader2.Close();
								return;
							}
							text2 = text2.TrimEnd(trimChars);
							if (text2 != text)
							{
								this.std_out = "0 Incorect!";
								streamReader.Close();
								streamReader2.Close();
								return;
							}
						}
					}
					while ((text2 = streamReader2.ReadLine()) != null)
					{
						text2 = text2.TrimEnd(trimChars);
						if (text2 != "")
						{
							this.std_out = "0 Incorect!";
							streamReader.Close();
							streamReader2.Close();
							return;
						}
					}
					this.std_out = this.D[testNumber] + " Corect!";
					streamReader.Close();
					streamReader2.Close();
				}
			}
			catch (Exception)
			{
			}
		}

		private void LaunchVerif(string testNumber)
		{
			string text = "verif.exe";
			try
			{
				if (!File.Exists(text) && !File.Exists(text.ToUpper()))
				{
					this.DefaultVerif(testNumber);
				}
				else
				{
					Process process = new Process();
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.CreateNoWindow = true;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.FileName = text;
					process.StartInfo.Arguments = testNumber + " " + this.D[testNumber];
					process.Start();
					this.std_out = process.StandardOutput.ReadToEnd();
					if (!process.HasExited)
					{
						process.WaitForExit();
					}
					process.Close();
				}
			}
			catch (Exception)
			{
			}
		}

		private void Debug(string s)
		{
			StreamWriter streamWriter = new StreamWriter("debug.txt", true);
			streamWriter.WriteLine(s);
			streamWriter.Close();
		}

		private string ElapsedTime(double d)
		{
			string str = null;
			string str2 = null;
			try
			{
				string text = (d / 1000000000.0).ToString();
				str = text.Substring(0, text.IndexOf(','));
				str2 = text.Substring(text.IndexOf(',') + 1, 3);
			}
			catch (Exception)
			{
			}
			return str + "." + str2;
		}

		private void Events_OnProcessMemoryLimit(object sender, ProcessMemoryLimitEventArgs args)
		{
			Eval.lovit = true;
		}

		private long SourceSize(string s)
		{
			s = s.ToLower();
			long result = 0L;
			string path = this.dirProb + this.problem + "\\surse_concurenti\\";
			try
			{
				string[] array = Directory.GetFiles(path);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					string text2 = Path.GetFileName(text);
					text2 = text2.ToLower();
					if (text2 == s)
					{
						FileInfo fileInfo = new FileInfo(text);
						return fileInfo.Length / 1024L;
					}
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		private void Evaluate(string the_exe, string testNumber)
		{
			this.testNumber2 = testNumber;
			Eval.lovit = false;
			string source = the_exe.Substring(0, the_exe.Length - 4);
			this.undeterminedMemory = false;
			int errorMode = 0;
			Stopwatch stopwatch = new Stopwatch();
			TimeSpan timeSpan = default(TimeSpan);
			string time = "";
			int num = 0;
			int num2 = 0;
			string text;
			if (this.assoc.ContainsKey(the_exe))
			{
				text = this.assoc[the_exe].ToLower();
			}
			else
			{
				text = ".cpp";
			}
			string str = the_exe.Substring(0, the_exe.Length - 4);
			long num3 = this.SourceSize(str + text);
			if (num3 > this.probLimits2[this.problem].Value)
			{
				this.InsertListView(testNumber, "0", "-", "-", string.Concat(new string[]
				{
					"Source size [",
					num3.ToString(),
					"] exceeds limit [",
					this.probLimits2[this.problem].Value.ToString(),
					"]"
				}));
				this.WriteTempRezults(source, testNumber, "0", "-", "-", string.Concat(new string[]
				{
					"Source size [",
					num3.ToString(),
					"] exceeds limit [",
					this.probLimits2[this.problem].Value.ToString(),
					"]"
				}), this.problem);
				return;
			}
			if (text == ".cpp")
			{
				num = (int)this.zMem[this.problem].zCPP;
			}
			if (text == ".c")
			{
				num = (int)this.zMem[this.problem].zC;
			}
			if (text == ".pas")
			{
				num = (int)this.zMem[this.problem].zPAS;
			}
			num2 = 0;
			try
			{
				using (JobObject jobObject = new JobObject("Executie"))
				{
					jobObject.Events.OnProcessMemoryLimit += new jobEventHandler<ProcessMemoryLimitEventArgs>(this.Events_OnProcessMemoryLimit);
					this.timeLimit = this.probLimits[this.problem].Key;
					string text2 = (1048576.0 * this.probLimits[this.problem].Value).ToString();
					int num4 = text2.IndexOf('.');
					if (num4 == -1)
					{
						text2 = text2.Substring(0, text2.Length);
					}
					else
					{
						text2 = text2.Substring(0, num4);
					}
					int num5 = int.Parse(text2);
					num5 += num;
					jobObject.Limits.ProcessMemoryLimit = new IntPtr?(new IntPtr(num5));
					ProcessStartInfo processStartInfo = new ProcessStartInfo(the_exe);
					processStartInfo.UseShellExecute = false;
					processStartInfo.CreateNoWindow = true;
					int num6 = 0;
					errorMode = Eval.SetErrorMode(3);
					Process process = jobObject.CreateProcessMayBreakAway(processStartInfo);
					stopwatch.Start();
					if (!process.HasExited)
					{
						try
						{
							process.WaitForExit(this.timeLimit);
							stopwatch.Stop();
							if (!process.HasExited)
							{
								try
								{
									num2 = (num2 = ((int)jobObject.PeakJobMemoryUsed - num) / 1024);
									if (num2 < 0)
									{
										num2 = 0;
									}
								}
								catch (Exception)
								{
									this.undeterminedMemory = true;
								}
								try
								{
									jobObject.TerminateAllProcesses(8u);
								}
								catch (Exception)
								{
								}
								this.InsertListView(testNumber, "0", "-", num2.ToString() + " KB", "Time limit exceeded!");
								this.WriteTempRezults(source, testNumber, "0", "-", num2.ToString(), "Time limit exceeded!", this.problem);
								return;
							}
							num6 = process.ExitCode;
						}
						catch (Exception)
						{
						}
						timeSpan = stopwatch.Elapsed;
						try
						{
							if (timeSpan.Seconds * 1000 + timeSpan.Milliseconds > this.timeLimit)
							{
								time = string.Format("{0}.{1:000}", this.timeLimit / 1000, this.timeLimit % 1000);
							}
							else
							{
								time = string.Format("{0}.{1:000}", timeSpan.Seconds, timeSpan.Milliseconds);
							}
							num2 = ((int)jobObject.PeakJobMemoryUsed - num) / 1024;
							if (num2 < 0)
							{
								num2 = 0;
							}
						}
						catch (Exception)
						{
							this.undeterminedMemory = true;
						}
						Thread.Sleep(50);
						if (Eval.lovit)
						{
							try
							{
								num2 = (num2 = ((int)jobObject.PeakJobMemoryUsed - num) / 1024);
								if (num2 < 0)
								{
									num2 = 0;
								}
							}
							catch (Exception)
							{
								this.undeterminedMemory = true;
							}
							try
							{
								jobObject.TerminateAllProcesses(8u);
							}
							catch (Exception)
							{
							}
							this.InsertListView(this.testNumber2, "0", time, "-", "Memory limit exceeded!");
							this.WriteTempRezults(source, this.testNumber2, "0", time, "-", "Memory limit exceeded!", this.problem);
						}
						else if (!process.HasExited)
						{
							try
							{
								jobObject.TerminateAllProcesses(8u);
							}
							catch (Exception)
							{
								Eval.SetErrorMode(errorMode);
							}
							if (this.undeterminedMemory)
							{
								this.InsertListView(testNumber, "0", "-", "nedeterminata", "Time limit exceeded!");
								this.WriteTempRezults(source, testNumber, "0", "-", "nedeterminata", "Time limit exceeded!", this.problem);
							}
							else
							{
								this.InsertListView(testNumber, "0", "-", num2.ToString() + " KB", "Time limit exceeded!");
								this.WriteTempRezults(source, testNumber, "0", "-", num2.ToString(), "Time limit exceeded!", this.problem);
							}
						}
						else if (num6 != 0)
						{
							if (Eval.lovit)
							{
								this.InsertListView(this.testNumber2, "0", time, "-", "Memory limit exceeded!");
								this.WriteTempRezults(source, testNumber, "0", time, "-", "Memory limit exceeded!", this.problem);
							}
							else
							{
								this.InsertListView(testNumber, "0", time, num2.ToString() + " KB", "Runtime Error!");
								this.WriteTempRezults(source, testNumber, "0", time, num2.ToString(), "Runtime Error!", this.problem);
							}
						}
						else
						{
							this.CopyTestsToWork(testNumber, true);
							this.LaunchVerif(testNumber);
							char[] separator = new char[]
							{
								' ',
								'\n'
							};
							string[] array = this.std_out.Split(separator);
							if (array.Length < 2)
							{
								MessageBox.Show("verif-ul nu afiseaza in formatul 'punctaj mesaj' !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								string points = array[0];
								string text3 = null;
								for (int i = 1; i < array.Length; i++)
								{
									text3 += array[i];
									text3 += " ";
								}
								if (this.undeterminedMemory)
								{
									this.InsertListView(testNumber, points, time, "nedeterminata", text3);
									this.WriteTempRezults(source, testNumber, points, time, "nedeterminata", text3, this.problem);
								}
								else
								{
									this.InsertListView(testNumber, points, time, num2.ToString() + " KB", text3);
									this.WriteTempRezults(source, testNumber, points, time, num2.ToString(), text3, this.problem);
								}
							}
						}
					}
					else
					{
						try
						{
							stopwatch.Stop();
							num6 = process.ExitCode;
						}
						catch (Exception)
						{
						}
						timeSpan = stopwatch.Elapsed;
						try
						{
							time = string.Format("{0}.{1:000}", timeSpan.Seconds, timeSpan.Milliseconds);
							num2 = ((int)jobObject.PeakJobMemoryUsed - num) / 1024;
							if (num2 < 0)
							{
								num2 = 0;
							}
						}
						catch (Exception)
						{
							this.undeterminedMemory = true;
						}
						if (Eval.lovit)
						{
							try
							{
								jobObject.TerminateAllProcesses(8u);
							}
							catch (Exception)
							{
							}
							this.InsertListView(this.testNumber2, "0", time, "-", "Memory limit exceeded!");
							this.WriteTempRezults(source, testNumber, "0", time, "-", "Memory limit exceeded!", this.problem);
						}
						else if (num6 != 0)
						{
							this.InsertListView(testNumber, "0", time, num2.ToString() + " KB", "Runtime Error!");
							this.WriteTempRezults(source, testNumber, "0", time, num2.ToString(), "Runtime Error!", this.problem);
						}
						else
						{
							this.CopyTestsToWork(testNumber, true);
							this.LaunchVerif(testNumber);
							char[] separator2 = new char[]
							{
								' ',
								'\n'
							};
							string[] array2 = this.std_out.Split(separator2);
							if (array2.Length < 2)
							{
								MessageBox.Show("verif-ul nu afiseaza in formatul 'punctaj mesaj' !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								string points2 = array2[0];
								string text4 = null;
								for (int j = 1; j < array2.Length; j++)
								{
									text4 += array2[j];
									text4 += " ";
								}
								if (this.undeterminedMemory)
								{
									this.InsertListView(testNumber, points2, time, "nedeterminata", text4);
									this.WriteTempRezults(source, testNumber, points2, time, "nedeterminata", text4, this.problem);
								}
								else
								{
									this.InsertListView(testNumber, points2, time, num2.ToString() + " KB", text4);
									this.WriteTempRezults(source, testNumber, points2, time, num2.ToString(), text4, this.problem);
								}
							}
						}
					}
				}
			}
			catch (Win32Exception)
			{
			}
			catch (Exception)
			{
			}
			finally
			{
				Eval.SetErrorMode(errorMode);
				stopwatch.Reset();
			}
		}

		private void Evaluate(string the_exe, string testNumber, int proc, BackgroundWorker bw, DoWorkEventArgs e)
		{
			if (bw.CancellationPending)
			{
				e.Cancel = true;
				return;
			}
			this.testNumber2 = testNumber;
			Eval.lovit = false;
			string source = the_exe.Substring(0, the_exe.Length - 4);
			this.undeterminedMemory = false;
			int errorMode = 0;
			Stopwatch stopwatch = new Stopwatch();
			string text = null;
			int num = 0;
			int num2 = 0;
			string text2;
			if (this.assoc.ContainsKey(the_exe))
			{
				text2 = this.assoc[the_exe].ToLower();
			}
			else
			{
				text2 = ".cpp";
			}
			string str = the_exe.Substring(0, the_exe.Length - 4);
			long num3 = this.SourceSize(str + text2);
			if (num3 > this.probLimits2[this.problem].Value)
			{
				UserState2 userState = new UserState2(testNumber, "0", "-", "-", string.Concat(new string[]
				{
					"Source size [",
					num3.ToString(),
					"] exceeds limit [",
					this.probLimits2[this.problem].Value.ToString(),
					"]"
				}));
				userState.caz = 2;
				Eval._waitHandle.WaitOne();
				bw.ReportProgress(proc, userState);
				this.WriteTempRezults(source, testNumber, "0", "-", "-", string.Concat(new string[]
				{
					"Source size [",
					num3.ToString(),
					"] exceeds limit [",
					this.probLimits2[this.problem].Value.ToString(),
					"]"
				}), this.problem);
				return;
			}
			if (text2 == ".cpp")
			{
				num = (int)this.zMem[this.problem].zCPP;
			}
			if (text2 == ".c")
			{
				num = (int)this.zMem[this.problem].zC;
			}
			if (text2 == ".pas")
			{
				num = (int)this.zMem[this.problem].zPAS;
			}
			num2 = 0;
			try
			{
				using (JobObject jobObject = new JobObject("Executie"))
				{
					jobObject.Events.OnProcessMemoryLimit += new jobEventHandler<ProcessMemoryLimitEventArgs>(this.Events_OnProcessMemoryLimit);
					this.timeLimit = this.probLimits[this.problem].Key;
					string text3 = (1048576.0 * this.probLimits[this.problem].Value).ToString();
					int num4 = text3.IndexOf('.');
					if (num4 == -1)
					{
						text3 = text3.Substring(0, text3.Length);
					}
					else
					{
						text3 = text3.Substring(0, num4);
					}
					int num5 = int.Parse(text3);
					num5 += num;
					jobObject.Limits.ProcessMemoryLimit = new IntPtr?(new IntPtr(num5));
					ProcessStartInfo processStartInfo = new ProcessStartInfo(the_exe);
					processStartInfo.UseShellExecute = false;
					processStartInfo.CreateNoWindow = true;
					int num6 = 0;
					errorMode = Eval.SetErrorMode(3);
					Process process = jobObject.CreateProcessMayBreakAway(processStartInfo);
					stopwatch.Start();
					if (!process.HasExited)
					{
						try
						{
							process.WaitForExit(this.timeLimit);
							stopwatch.Stop();
							if (!process.HasExited)
							{
								try
								{
									num2 = (num2 = ((int)jobObject.PeakJobMemoryUsed - num) / 1024);
									if (num2 < 0)
									{
										num2 = 0;
									}
								}
								catch (Exception)
								{
									this.undeterminedMemory = true;
								}
								try
								{
									jobObject.TerminateAllProcesses(8u);
								}
								catch (Exception)
								{
								}
								bw.ReportProgress(proc, new UserState2(testNumber, "0", "-", num2.ToString() + " KB", "Time limit exceeded!")
								{
									caz = 9
								});
								Eval._waitHandle.WaitOne();
								this.WriteTempRezults(source, testNumber, "0", "-", num2.ToString(), "Time limit exceeded!", this.problem);
								return;
							}
							num6 = process.ExitCode;
						}
						catch (Exception)
						{
						}
						TimeSpan elapsed = stopwatch.Elapsed;
						try
						{
							if (elapsed.Seconds * 1000 + elapsed.Milliseconds > this.timeLimit)
							{
								text = string.Format("{0}.{1:000}", this.timeLimit / 1000, this.timeLimit % 1000);
							}
							else
							{
								text = string.Format("{0}.{1:000}", elapsed.Seconds, elapsed.Milliseconds);
							}
							num2 = ((int)jobObject.PeakJobMemoryUsed - num) / 1024;
							if (num2 < 0)
							{
								num2 = 0;
							}
						}
						catch (Exception)
						{
							this.undeterminedMemory = true;
						}
						Thread.Sleep(50);
						if (Eval.lovit)
						{
							try
							{
								num2 = (num2 = ((int)jobObject.PeakJobMemoryUsed - num) / 1024);
								if (num2 < 0)
								{
									num2 = 0;
								}
							}
							catch (Exception)
							{
								this.undeterminedMemory = true;
							}
							try
							{
								jobObject.TerminateAllProcesses(8u);
							}
							catch (Exception)
							{
							}
							bw.ReportProgress(proc, new UserState2(this.testNumber2, "0", text, "-", "Memory limit exceeded!")
							{
								caz = 7
							});
							Eval._waitHandle.WaitOne();
							this.WriteTempRezults(source, testNumber, "0", text, "-", "Memory limit exceeded!", this.problem);
						}
						else if (!process.HasExited)
						{
							try
							{
								jobObject.TerminateAllProcesses(8u);
							}
							catch (Exception)
							{
								Eval.SetErrorMode(errorMode);
							}
							if (this.undeterminedMemory)
							{
								bw.ReportProgress(proc, new UserState2(testNumber, "0", "-", "nedeterminata", "Time limit exceeded!")
								{
									caz = 8
								});
								Eval._waitHandle.WaitOne();
								this.WriteTempRezults(source, testNumber, "0", "-", "nedeterminata", "Time limit exceeded!", this.problem);
							}
							else
							{
								bw.ReportProgress(proc, new UserState2(testNumber, "0", "-", num2.ToString() + " KB", "Time limit exceeded!")
								{
									caz = 9
								});
								Eval._waitHandle.WaitOne();
								this.WriteTempRezults(source, testNumber, "0", "-", num2.ToString(), "Time limit exceeded!", this.problem);
							}
						}
						else if (num6 != 0)
						{
							if (Eval.lovit)
							{
								bw.ReportProgress(proc, new UserState2(testNumber, "0", text, "-", "Memory limit exceeded!")
								{
									caz = 10
								});
								Eval._waitHandle.WaitOne();
								this.WriteTempRezults(source, testNumber, "0", text, "-", "Memory limit exceeded!", this.problem);
							}
							else
							{
								bw.ReportProgress(proc, new UserState2(testNumber, "0", text, num2.ToString() + " KB", "Runtime Error!")
								{
									caz = 11
								});
								Eval._waitHandle.WaitOne();
								this.WriteTempRezults(source, testNumber, "0", text, num2.ToString(), "Runtime Error!", this.problem);
							}
						}
						else
						{
							this.CopyTestsToWork(testNumber, true);
							this.LaunchVerif(testNumber);
							char[] separator = new char[]
							{
								' ',
								'\n'
							};
							string[] array = this.std_out.Split(separator);
							if (array.Length < 2)
							{
								MessageBox.Show("verif-ul nu afiseaza in formatul 'punctaj mesaj' !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								string text4 = array[0];
								string text5 = null;
								for (int i = 1; i < array.Length; i++)
								{
									text5 += array[i];
									text5 += " ";
								}
								if (this.undeterminedMemory)
								{
									bw.ReportProgress(proc, new UserState2(testNumber, text4, text, "nedeterminata", text5)
									{
										caz = 12
									});
									Eval._waitHandle.WaitOne();
									this.WriteTempRezults(source, testNumber, text4, text, "nedeterminata", text5, this.problem);
								}
								else
								{
									bw.ReportProgress(proc, new UserState2(testNumber, text4, text, num2.ToString() + " KB", text5)
									{
										caz = 1
									});
									Eval._waitHandle.WaitOne();
									this.WriteTempRezults(source, testNumber, text4, text, num2.ToString(), text5, this.problem);
								}
							}
						}
					}
					else
					{
						try
						{
							stopwatch.Stop();
							num6 = process.ExitCode;
						}
						catch (Exception)
						{
						}
						TimeSpan elapsed = stopwatch.Elapsed;
						try
						{
							text = string.Format("{0}.{1:000}", elapsed.Seconds, elapsed.Milliseconds);
							num2 = ((int)jobObject.PeakJobMemoryUsed - num) / 1024;
							if (num2 < 0)
							{
								num2 = 0;
							}
						}
						catch (Exception)
						{
							this.undeterminedMemory = true;
						}
						if (Eval.lovit)
						{
							try
							{
								jobObject.TerminateAllProcesses(8u);
							}
							catch (Exception)
							{
							}
							bw.ReportProgress(proc, new UserState2(this.testNumber2, "0", text, "-", "Memory limit exceeded!")
							{
								caz = 3
							});
							Eval._waitHandle.WaitOne();
							this.WriteTempRezults(source, this.testNumber2, "0", text, "-", "Memory limit exceeded!", this.problem);
						}
						else if (num6 != 0)
						{
							bw.ReportProgress(proc, new UserState2(testNumber, "0", text, num2.ToString() + " KB", "Runtime Error!")
							{
								caz = 4
							});
							Eval._waitHandle.WaitOne();
							this.WriteTempRezults(source, testNumber, "0", text, num2.ToString(), "Runtime Error!", this.problem);
						}
						else
						{
							this.CopyTestsToWork(testNumber, true);
							this.LaunchVerif(testNumber);
							char[] separator2 = new char[]
							{
								' ',
								'\n'
							};
							string[] array2 = this.std_out.Split(separator2);
							if (array2.Length < 2)
							{
								MessageBox.Show("verif-ul nu afiseaza in formatul 'punctaj mesaj' !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								string text6 = array2[0];
								string text7 = null;
								for (int j = 1; j < array2.Length; j++)
								{
									text7 += array2[j];
									text7 += " ";
								}
								if (this.undeterminedMemory)
								{
									bw.ReportProgress(proc, new UserState2(testNumber, text6, text, "nedeterminata", text7)
									{
										caz = 5
									});
									Thread.Sleep(50);
									this.WriteTempRezults(source, testNumber, text6, text, "nedeterminata", text7, this.problem);
								}
								else
								{
									bw.ReportProgress(proc, new UserState2(testNumber, text6, text, num2.ToString() + " KB", text7)
									{
										caz = 6
									});
									Eval._waitHandle.WaitOne();
									this.WriteTempRezults(source, testNumber, text6, text, num2.ToString(), text7, this.problem);
								}
							}
						}
					}
				}
			}
			catch (Win32Exception)
			{
			}
			catch (Exception)
			{
			}
			finally
			{
				Eval.SetErrorMode(errorMode);
				stopwatch.Reset();
			}
		}

		private void WriteTempRezults(string source, string test, string points, string time, string memory, string msg, string prob)
		{
			try
			{
				string text = "..\\rezultate\\evaluare\\tmp\\";
				if (!Directory.Exists(text))
				{
					DirectoryInfo directoryInfo = Directory.CreateDirectory(text);
					directoryInfo.Attributes = (FileAttributes.Hidden | FileAttributes.Directory);
				}
				string path = text + source + "." + prob;
				if (this.D.ElementAt(0).Key == test && File.Exists(path))
				{
					File.Delete(path);
				}
				StreamWriter streamWriter = new StreamWriter(path, true);
				streamWriter.WriteLine(string.Concat(new string[]
				{
					test,
					"\t",
					points,
					"\t",
					time,
					"\t",
					memory,
					"\t",
					msg
				}));
				streamWriter.Flush();
				streamWriter.Close();
			}
			catch (Exception)
			{
			}
		}

		private void ResultEval(string source, string the_exe, string prob)
		{
			string text = null;
			StreamWriter streamWriter = null;
			string text2 = "..\\rezultate\\evaluare\\";
			string path = text2 + "tmp\\";
			text = the_exe.Substring(0, the_exe.Length - 5);
			string path2 = text2 + text + "-res.txt";
			try
			{
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				streamWriter = new StreamWriter(path2, false);
				streamWriter.WriteLine("\t\t\t\tBORDEROU DE EVALUARE");
				streamWriter.WriteLine();
				string value = string.Format("{0,-11}{1,-15} {2, -20} {3, -30}", new object[]
				{
					"ID",
					"Nume",
					"Prenume",
					"Scoala"
				});
				streamWriter.WriteLine(value);
				streamWriter.Write("{0, -11}", text);
				if (this.contestantsID.ContainsKey(text))
				{
					value = string.Format("{0,-15} {1, -20} {2, -30}", this.contestantsID[text].LastName, this.contestantsID[text].FirstName, this.contestantsID[text].Location);
					streamWriter.WriteLine(value);
				}
				else
				{
					streamWriter.WriteLine();
				}
				string[] array = Directory.GetFiles(path);
				string text3 = null;
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string path3 = array2[i];
					string extension = Path.GetExtension(path3);
					text3 = Path.GetFileNameWithoutExtension(path3);
					string a = text3.Substring(0, text3.Length - 1);
					if (a == text)
					{
						if (!this.contestantsID.ContainsKey(text))
						{
							streamWriter.WriteLine();
						}
						streamWriter.WriteLine("Problema: " + extension.Substring(1));
						try
						{
							if (this.assoc.ContainsKey(source + ".exe"))
							{
								streamWriter.Write("Sursa:    " + text3);
								streamWriter.WriteLine(this.assoc[source + ".exe"]);
							}
							else
							{
								streamWriter.WriteLine("Sursa:    " + source);
							}
						}
						catch (Exception)
						{
							streamWriter.WriteLine("Sursa:  " + text3);
						}
						streamWriter.WriteLine();
						streamWriter.WriteLine("Test   Punctaj  Timp     Memorie  Mesaj:");
						streamWriter.WriteLine("                (ms)     (KB)");
						KeyValuePair<string, string> key = new KeyValuePair<string, string>(text, prob);
						if (this.dEval.ContainsKey(key))
						{
							this.dEval[key] = true;
						}
						StreamReader streamReader = new StreamReader(path3);
						char[] separator = new char[]
						{
							'\t'
						};
						int num = 0;
						string text4;
						while ((text4 = streamReader.ReadLine()) != null)
						{
							string[] array3 = text4.Split(separator, StringSplitOptions.RemoveEmptyEntries);
							if (array3.Length == 5)
							{
								value = string.Format("{0, -7}{1, -8} {2, -8} {3, -9}{4, -25}", new object[]
								{
									array3[0],
									array3[1],
									array3[2],
									array3[3],
									array3[4]
								});
								num += int.Parse(array3[1]);
								streamWriter.WriteLine(value);
							}
						}
						streamWriter.WriteLine("TOTAL: " + num + " puncte");
						streamReader.Close();
						streamWriter.WriteLine("=======================================================");
						streamReader.Close();
					}
				}
				streamWriter.Flush();
				streamWriter.Close();
			}
			catch (Exception)
			{
			}
		}

		private void WriteExcel()
		{
			string str = "..\\rezultate\\evaluare\\";
			try
			{
				this.cont = new List<KeyValuePair<int, string>>();
				foreach (string current in this.contestantsID.Keys)
				{
					this.cont.Add(new KeyValuePair<int, string>(this.contestantsID[current].Total, current));
				}
				for (int i = 0; i < this.cont.Count - 1; i++)
				{
					for (int j = i + 1; j < this.cont.Count; j++)
					{
						if (this.cont[i].Key < this.cont[j].Key)
						{
							KeyValuePair<int, string> value = this.cont[i];
							this.cont[i] = this.cont[j];
							this.cont[j] = value;
						}
					}
				}
				FileStream fileStream = new FileStream(str + "Clasament.xls", FileMode.OpenOrCreate);
				ExcelWriter excelWriter = new ExcelWriter(fileStream);
				excelWriter.BeginWrite();
				excelWriter.WriteCell(0, 5, "CLASAMENT");
				excelWriter.WriteCell(2, 0, "TOTAL");
				int num = 1;
				foreach (string current2 in this.probLimits.Keys)
				{
					excelWriter.WriteCell(2, num, current2);
					num++;
				}
				excelWriter.WriteCell(2, num, "ID");
				excelWriter.WriteCell(2, num + 1, "Nume");
				excelWriter.WriteCell(2, num + 2, "Prenume");
				excelWriter.WriteCell(2, num + 3, "Scoala");
				int num2 = 3;
				for (int i = 0; i < this.cont.Count; i++)
				{
					string text = this.cont[i].Value.ToString();
					if (!this.contestantsID[text].AlreadyEvaluated)
					{
						excelWriter.WriteCell(num2, 0, "-");
					}
					else
					{
						excelWriter.WriteCell(num2, 0, this.cont[i].Key);
					}
					num = 1;
					for (int k = 0; k < this.probLimits.Count; k++)
					{
						string key = this.probLimits.Keys.ElementAt(k);
						if (this.contestantsID[text].PB.ContainsKey(key))
						{
							excelWriter.WriteCell(num2, num, int.Parse(this.contestantsID[text].PB[key]));
						}
						else
						{
							excelWriter.WriteCell(num2, num, "-");
						}
						num++;
					}
					excelWriter.WriteCell(num2, num, text);
					excelWriter.WriteCell(num2, num + 1, this.contestantsID[text].LastName);
					excelWriter.WriteCell(num2, num + 2, this.contestantsID[text].FirstName);
					excelWriter.WriteCell(num2, num + 3, this.contestantsID[text].Location);
					num2++;
				}
				excelWriter.EndWrite();
				fileStream.Close();
			}
			catch (Exception)
			{
			}
		}

		private void CopyExeToWork(string exe)
		{
			try
			{
				string str = this.dirProb + this.problem + "\\exe_concurenti\\";
				File.Copy(str + exe, exe, true);
			}
			catch (Exception)
			{
			}
		}

		private bool TryCopy(bool cpOK, string testIn1, string testIn2, string testOk1, string testOk2)
		{
			try
			{
				try
				{
					if ((File.GetAttributes(testIn1) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
					{
						File.SetAttributes(testIn1, FileAttributes.Normal);
					}
					if ((File.GetAttributes(testOk1) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
					{
						File.SetAttributes(testOk1, FileAttributes.Normal);
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Fisierele .in sau fisierele .ok sunt read-only!", "Eroare de copiere!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					bool result = false;
					return result;
				}
				File.Copy(testIn1, testIn2, true);
				if (cpOK)
				{
					File.Copy(testOk1, testOk2, true);
				}
			}
			catch (Exception)
			{
				bool result = false;
				return result;
			}
			return true;
		}

		private bool CopyTestsToWork(string test, bool copyOK)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			try
			{
				text = this.dirProb + this.problem + "\\teste\\";
				text2 = test + "-" + this.problem + ".in";
				text3 = test + "-" + this.problem + ".ok";
				if (!File.Exists(text + text2))
				{
					MessageBox.Show("Nu s-a gasit fisierul \"" + text + text2 + "\" !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					bool result = false;
					return result;
				}
				if (!File.Exists(text + text3))
				{
					MessageBox.Show("Nu s-a gasit fisierul\"" + text + text3 + "\" !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					bool result = false;
					return result;
				}
			}
			catch (Exception)
			{
			}
			try
			{
				int num = 0;
				do
				{
					num++;
				}
				while (num < 2 && !this.TryCopy(copyOK, text + text2, this.problem + ".in", text + text3, this.problem + ".ok"));
			}
			catch (Exception)
			{
			}
			return true;
		}

		private void CopyVerifToWork()
		{
			string str = this.dirProb + this.problem + "\\teste\\";
			if (!File.Exists(str + "verif.exe"))
			{
				return;
			}
			try
			{
				File.Copy(str + "verif.exe", "verif.exe", true);
			}
			catch (Exception)
			{
			}
		}

		private void DeleteExe(string exe)
		{
			while (File.Exists(exe))
			{
				try
				{
					File.Delete(exe);
				}
				catch (Exception)
				{
				}
			}
		}

		private void DeleteTests()
		{
			try
			{
				if (File.Exists(this.problem + ".in"))
				{
					File.Delete(this.problem + ".in");
				}
				if (File.Exists(this.problem + ".ok"))
				{
					File.Delete(this.problem + ".ok");
				}
				if (File.Exists(this.problem + ".out"))
				{
					File.Delete(this.problem + ".out");
				}
			}
			catch (Exception)
			{
			}
		}

		private void DeleteVerif()
		{
			try
			{
				if (File.Exists("verif.exe"))
				{
					File.Delete("verif.exe");
				}
			}
			catch (Exception)
			{
			}
		}

		private void EvalProblem(BackgroundWorker bw, DoWorkEventArgs e)
		{
			if (bw.CancellationPending)
			{
				e.Cancel = true;
				return;
			}
			string text = null;
			string str = null;
			int percentProgress = 0;
			try
			{
				for (int i = 0; i < this.col2.Count; i++)
				{
					if (bw.CancellationPending)
					{
						e.Cancel = true;
						return;
					}
					text = this.col2[i].ToString();
					this.CopyExeToWork(text);
					UserState2 userState = new UserState2("text", text, "", "", "");
					percentProgress = i * 100 / this.col2.Count;
					bw.ReportProgress(percentProgress, userState);
					Eval._waitHandle.WaitOne();
					if (!this.allTests)
					{
						if (this.selIndex == -1)
						{
							MessageBox.Show("Selectati unul sau toate testele sau bifati \"Test cu test\"", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							return;
						}
						if (!this.CopyTestsToWork(this.testN, false))
						{
							e.Cancel = true;
							return;
						}
						this.Evaluate(text, this.testN, i * 100 / this.col2.Count, bw, e);
						if (this.assoc.ContainsKey(text))
						{
							str = this.assoc[text].ToLower();
						}
						this.DeleteTests();
					}
					else
					{
						foreach (KeyValuePair<string, string> current in this.D)
						{
							if (!this.CopyTestsToWork(current.Key, false))
							{
								e.Cancel = true;
								return;
							}
							this.Evaluate(text, current.Key, i * 100 / this.col2.Count, bw, e);
							this.DeleteTests();
						}
						string text2 = text.Substring(0, text.Length - 5);
						text2 = text2.ToUpper();
						if (!this.contestantsID.ContainsKey(text2))
						{
							this.contestantsID.Add(text2, new Contestant(text2, "-", "-", "-"));
						}
						KeyValuePair<string, string> key = new KeyValuePair<string, string>(text2, this.problem);
						if (this.dEval.ContainsKey(key))
						{
							this.contestantsID[text2].SetProb(this.problem, this.Total.ToString(), true);
						}
						else
						{
							this.contestantsID[text2].SetProb(this.problem, this.Total.ToString(), false);
							this.dEval.Add(key, true);
						}
						userState = new UserState2("Total", this.Total.ToString(), " puncte", "", "");
						bw.ReportProgress(percentProgress, userState);
						Eval._waitHandle.WaitOne();
					}
					this.DeleteExe(text);
					this.ResultEval(text.Substring(0, text.Length - 4) + str, text, this.problem);
				}
				this.DeleteVerif();
			}
			catch (Exception)
			{
			}
		}

		private void WriteDownZeroMemory()
		{
			StreamWriter streamWriter = new StreamWriter("zeromemory.txt");
			streamWriter.WriteLine("zeroMemoryC = " + this.zeroMemoryC);
			streamWriter.WriteLine("zeroMemoryCPP = " + this.zeroMemoryCPP);
			streamWriter.WriteLine("zeroMemoryPAS = " + this.zeroMemoryPAS);
			streamWriter.Flush();
			streamWriter.Close();
		}

		private void EnableDisableControls(bool e)
		{
			this.checkBoxStepByStep.Enabled = e;
			this.checkBoxAllProblems.Enabled = e;
			this.checkBoxAllTests.Enabled = e;
			this.checkBoxAllIDs.Enabled = e;
			this.menuStrip1.Enabled = e;
			this.tabPageCompil.Enabled = e;
			this.buttonEval.Enabled = e;
		}

		private void buttonEval_Click(object sender, EventArgs e)
		{
			if (this.comboBoxProblem.Items.Count == 0)
			{
				MessageBox.Show("Nu s-a definit nicio problema!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string currentDirectory = "";
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				currentDirectory = "..\\..\\work";
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
			}
			if (this.checkBoxAllProblems.Checked)
			{
				this.checkBoxStepByStep.Checked = false;
			}
			try
			{
				if (this.comboBoxProblem.Text != "")
				{
					this.comboBoxProblem.SelectedItem = this.comboBoxProblem.Text;
				}
				if (this.comboBoxProblem.SelectedItem == null && !this.checkBoxAllProblems.Checked && !this.backgroundWorker2.IsBusy)
				{
					MessageBox.Show("Selectati o problema!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (this.checkBoxStepByStep.Checked && this.checkedListBoxExe.CheckedItems.Count != 1 && !this.checkBoxAllProblems.Checked)
				{
					MessageBox.Show("Selectati o singura sursa din lista !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (this.checkedListBoxExe.CheckedItems.Count == 0 && !this.checkBoxAllProblems.Checked)
				{
					MessageBox.Show("Selectati unul sau mai multe executabile !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (!this.checkBoxStepByStep.Checked && this.checkedListBoxExe.CheckedItems.Count != 0 && !this.checkBoxAllProblems.Checked && !this.checkBoxAllTests.Checked && this.comboBoxTest.SelectedIndex == -1)
				{
					MessageBox.Show("Selectati unul sau toate testele !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			catch (Exception)
			{
			}
			try
			{
				this.labelListView.TextAlign = ContentAlignment.MiddleLeft;
			}
			catch (Exception)
			{
			}
			if (this.checkBoxAllProblems.Checked)
			{
				this.allProbs = true;
				string[] array = null;
				try
				{
					foreach (string current in this.probLimits2.Keys)
					{
						if (Directory.Exists("..\\probleme\\" + current + "\\exe_concurenti"))
						{
							array = Directory.GetFiles("..\\probleme\\" + current + "\\exe_concurenti");
							if (array.Length == 0)
							{
								MessageBox.Show("Nu exista fisiere executabile pentru problema '" + current + "' !\nEvaluare oprita !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
						if (Directory.Exists("..\\probleme\\" + current + "\\teste"))
						{
							array = Directory.GetFiles("..\\probleme\\" + current + "\\teste");
							if (array.Length == 0)
							{
								MessageBox.Show("Nu exista teste pentru problema '" + current + "' !\nEvaluare oprita !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								return;
							}
						}
					}
				}
				catch (Exception)
				{
				}
				try
				{
					if (Directory.Exists("..\\rezultate\\evaluare"))
					{
						array = Directory.GetFiles("..\\rezultate\\evaluare");
						if (array != null)
						{
							Array.Sort<string>(array);
						}
					}
					else
					{
						Directory.CreateDirectory("..\\rezultate\\evaluare");
					}
				}
				catch (Exception)
				{
				}
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string path = array2[i];
					try
					{
						File.Delete(path);
					}
					catch (Exception)
					{
					}
				}
				try
				{
					if (Directory.Exists("..\\rezultate\\evaluare\\tmp\\"))
					{
						array = Directory.GetFiles("..\\rezultate\\evaluare\\tmp\\");
					}
					string[] array3 = array;
					for (int j = 0; j < array3.Length; j++)
					{
						string path2 = array3[j];
						File.Delete(path2);
					}
				}
				catch (Exception)
				{
				}
			}
			if (!this.checkBoxAllProblems.Checked)
			{
				this.CopyVerifToWork();
			}
			if (!this.calibratedMemory)
			{
				this.EnableDisableControls(false);
				this.zMem = new Dictionary<string, ZeroMem>();
				this.labelListView.Text = "Asteptati... Se calibreaza memoria ";
				this.labelListView.Refresh();
				try
				{
					Directory.SetCurrentDirectory(this.rootDirectory);
					string str = "zerocpp.cpp";
					StreamWriter streamWriter = new StreamWriter("..\\..\\work\\" + str);
					streamWriter.WriteLine("#include <fstream>");
					streamWriter.WriteLine("using namespace std;");
					streamWriter.WriteLine("int main()");
					streamWriter.WriteLine("{");
					streamWriter.WriteLine("return 0;");
					streamWriter.WriteLine("}");
					streamWriter.Close();
					str = "zeroc.c";
					streamWriter = new StreamWriter("..\\..\\work\\" + str);
					streamWriter.WriteLine("#include <stdio.h>");
					streamWriter.WriteLine("int main()");
					streamWriter.WriteLine("{");
					streamWriter.WriteLine("return 0;");
					streamWriter.WriteLine("}");
					streamWriter.Close();
					str = "zeropas.pas";
					streamWriter = new StreamWriter("..\\..\\work\\" + str);
					streamWriter.WriteLine("program zero;");
					streamWriter.WriteLine("begin");
					streamWriter.WriteLine("end.");
					streamWriter.Close();
				}
				catch (Exception)
				{
				}
				try
				{
					foreach (string current2 in this.probLimits.Keys)
					{
						this.CalibrateMemory(current2);
						this.labelListView.Text = "Asteptati... Se calibreaza memoria ";
						this.labelListView.Refresh();
					}
					this.EnableDisableControls(true);
				}
				catch (Exception)
				{
				}
				this.calibratedMemory = true;
				try
				{
					if (File.Exists("..\\..\\work\\zerocpp.cpp"))
					{
						File.Delete("..\\..\\work\\zerocpp.cpp");
					}
					if (File.Exists("..\\..\\work\\zeroc.c"))
					{
						File.Delete("..\\..\\work\\zeroc.c");
					}
					if (File.Exists("..\\..\\work\\zeropas.pas"))
					{
						File.Delete("..\\..\\work\\zeropas.pas");
					}
					if (File.Exists("..\\..\\work\\zerocpp.exe"))
					{
						File.Delete("..\\..\\work\\zerocpp.exe");
					}
					if (File.Exists("..\\..\\work\\zeroc.exe"))
					{
						File.Delete("..\\..\\work\\zeroc.exe");
					}
					if (File.Exists("..\\..\\work\\zeropas.exe"))
					{
						File.Delete("..\\..\\work\\zeropas.exe");
					}
					if (File.Exists("..\\..\\work\\zeropas.exe"))
					{
						File.Delete("..\\..\\work\\zeropas.o");
					}
				}
				catch (Exception)
				{
				}
				try
				{
					Directory.SetCurrentDirectory(currentDirectory);
				}
				catch (Exception)
				{
				}
			}
			if (this.checkBoxStepByStep.Checked)
			{
				string text = null;
				try
				{
					foreach (string text2 in this.checkedListBoxExe.CheckedItems)
					{
						text = text2;
					}
					string arg = "[" + this.problem + "]";
					this.labelListView.Text = string.Format("{0,-12}", arg);
					arg = "Evaluare '" + text + "'";
					Label expr_677 = this.labelListView;
					expr_677.Text += string.Format("{0,50} ... ", arg);
					this.labelListView.Refresh();
					if (this.globalTest == 0)
					{
						this.listViewEval.Items.Clear();
						this.DeleteExe(text);
						this.DeleteTests();
					}
					this.CopyExeToWork(text);
				}
				catch (Exception)
				{
				}
				try
				{
					KeyValuePair<string, string> keyValuePair = this.D.ElementAt(this.globalTest);
					this.DeleteTests();
					if (!this.CopyTestsToWork(keyValuePair.Key, false))
					{
						this.labelListView.TextAlign = ContentAlignment.MiddleCenter;
						this.labelListView.Text = "Evaluare oprita!";
						return;
					}
					this.Evaluate(text, keyValuePair.Key);
				}
				catch (Exception)
				{
				}
				this.DeleteVerif();
				try
				{
					if (this.globalTest == this.nrTests - 1)
					{
						this.InsertListView("Total:", this.Total.ToString(), " puncte", "", "");
						Label expr_774 = this.labelListView;
						expr_774.Text += " terminat !";
						this.labelListView.Refresh();
						string str2 = "";
						if (this.assoc.ContainsKey(text))
						{
							str2 = this.assoc[text].ToLower();
						}
						string text3 = text.Substring(0, text.Length - 5);
						text3 = text3.ToUpper();
						if (!this.contestantsID.ContainsKey(text3))
						{
							this.contestantsID.Add(text3, new Contestant(text3, "-", "-", "-"));
						}
						KeyValuePair<string, string> key = new KeyValuePair<string, string>(text3, this.problem);
						if (this.dEval.ContainsKey(key))
						{
							this.contestantsID[text3].SetProb(this.problem, this.Total.ToString(), true);
						}
						else
						{
							this.contestantsID[text3].SetProb(this.problem, this.Total.ToString(), false);
							this.dEval.Add(key, true);
						}
						this.ResultEval(text.Substring(0, text.Length - 4) + str2, text, this.problem);
						this.WriteExcel();
						this.Total = 0;
						this.globalTest = 0;
					}
					else
					{
						this.globalTest++;
					}
				}
				catch (Exception)
				{
				}
			}
			else
			{
				try
				{
					this.dirProb = "..\\probleme\\";
					this.nrProbleme = this.comboBoxProblem.Items.Count;
					this.allProbs = this.checkBoxAllProblems.Checked;
					this.allTests = this.checkBoxAllTests.Checked;
					this.testN = "";
					if (!this.checkBoxAllTests.Checked && !this.allProbs)
					{
						this.testN = this.comboBoxTest.SelectedItem.ToString();
						this.selIndex = this.comboBoxTest.SelectedIndex;
					}
				}
				catch (Exception)
				{
				}
				try
				{
					if (!this.backgroundWorker2.IsBusy)
					{
						this.comboBoxProblem.UseWaitCursor = true;
						this.comboBoxTest.UseWaitCursor = true;
						this.checkBoxAllIDs.Enabled = false;
						this.checkBoxAllTests.Enabled = false;
						this.checkBoxAllProblems.Enabled = false;
						this.checkBoxStepByStep.Enabled = false;
						this.menuStrip1.Enabled = false;
						this.tabPageCompil.Enabled = false;
						this.evaluationRunning = true;
						this.backgroundWorker2.RunWorkerAsync();
						this.buttonEval.Text = "Stop";
					}
					else
					{
						this.backgroundWorker2.CancelAsync();
					}
				}
				catch (Exception)
				{
				}
			}
		}

		private void checkBoxStepByStep_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxStepByStep.Checked)
			{
				this.globalTest = 0;
				this.Total = 0;
				this.checkBoxAllTests.Checked = false;
				this.comboBoxTest.SelectedItem = null;
				this.checkBoxAllProblems.Checked = false;
				this.checkBoxAllIDs.Checked = false;
			}
			else
			{
				this.Total = 0;
				this.globalTest = 0;
			}
			try
			{
				this.labelListView.TextAlign = ContentAlignment.MiddleCenter;
			}
			catch (Exception)
			{
			}
			this.labelListView.Text = "Panou de evaluare ";
			this.labelListView.Refresh();
			this.listViewEval.Items.Clear();
			this.buttonOK.Enabled = this.checkBoxStepByStep.Checked;
			this.buttonIN.Enabled = this.checkBoxStepByStep.Checked;
			this.buttonContestant.Enabled = this.checkBoxStepByStep.Checked;
		}

		private void checkBoxAllTests_CheckedChanged(object sender, EventArgs e)
		{
			this.Total = 0;
			this.globalTest = 0;
			if (this.checkBoxAllTests.Checked)
			{
				this.comboBoxTest.SelectedItem = null;
				this.comboBoxTest.Text = "";
				this.checkBoxStepByStep.Checked = false;
			}
			this.labelListView.TextAlign = ContentAlignment.MiddleCenter;
			this.labelListView.Text = "Panou de evaluare ";
			this.labelListView.Refresh();
			this.listViewEval.Items.Clear();
		}

		private void comboBoxTest_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.comboBoxTest.SelectedItem != null)
			{
				this.checkBoxStepByStep.Checked = false;
				this.checkBoxAllTests.Checked = false;
			}
			this.labelListView.TextAlign = ContentAlignment.MiddleCenter;
			this.labelListView.Text = "Panou de evaluare ";
			this.labelListView.Refresh();
			this.listViewEval.Items.Clear();
		}

		private void checkedListBoxExe_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (this.globalTest != 0)
			{
				this.checkBoxStepByStep.Checked = false;
				this.globalTest = 0;
			}
			this.listViewEval.Items.Clear();
			try
			{
				this.labelListView.TextAlign = ContentAlignment.MiddleCenter;
			}
			catch (Exception)
			{
			}
			this.labelListView.Text = "Panou de evaluare ";
			this.progressBar2.Value = 0;
			this.progressBar2.Refresh();
			this.labelListView.Refresh();
			this.listViewEval.Items.Clear();
		}

		private void listViewEval_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			try
			{
				if ((e.State & ListViewItemStates.Selected) != (ListViewItemStates)0)
				{
					e.Graphics.FillRectangle(Brushes.BlueViolet, e.Bounds);
					e.DrawFocusRectangle();
				}
				else
				{
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.Bounds, Color.LightGray, Color.Violet, LinearGradientMode.Horizontal))
					{
						e.Graphics.FillRectangle(linearGradientBrush, e.Bounds);
					}
				}
				if (this.listViewEval.View != View.Details)
				{
					e.DrawText();
				}
			}
			catch (Exception)
			{
			}
		}

		private void listViewEval_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			try
			{
				using (StringFormat stringFormat = new StringFormat())
				{
					switch (e.Header.TextAlign)
					{
					case HorizontalAlignment.Right:
						stringFormat.Alignment = StringAlignment.Far;
						break;
					case HorizontalAlignment.Center:
						stringFormat.Alignment = StringAlignment.Center;
						break;
					}
					e.DrawBackground();
					using (Font font = new Font("Helvetica", 8f, FontStyle.Bold))
					{
						e.Graphics.DrawString(e.Header.Text, font, Brushes.Black, e.Bounds, stringFormat);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void listViewEval_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				ListViewItem itemAt = this.listViewEval.GetItemAt(e.X, e.Y);
				if (itemAt != null && itemAt.Tag == null)
				{
					this.listViewEval.Invalidate(itemAt.Bounds);
					itemAt.Tag = "tagged";
				}
			}
			catch (Exception)
			{
			}
		}

		private void listViewEval_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				ListViewItem itemAt = this.listViewEval.GetItemAt(5, e.Y);
				if (itemAt != null)
				{
					itemAt.Selected = true;
					itemAt.Focused = true;
				}
			}
			catch (Exception)
			{
			}
		}

		private void listViewEval_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			try
			{
				TextFormatFlags flags = TextFormatFlags.Default;
				using (StringFormat stringFormat = new StringFormat())
				{
					switch (e.Header.TextAlign)
					{
					case HorizontalAlignment.Left:
						stringFormat.Alignment = StringAlignment.Near;
						flags = TextFormatFlags.Default;
						break;
					case HorizontalAlignment.Right:
						stringFormat.Alignment = StringAlignment.Far;
						flags = TextFormatFlags.Right;
						break;
					case HorizontalAlignment.Center:
						if (e.Header.Text == "Mesaj")
						{
							stringFormat.Alignment = StringAlignment.Near;
							flags = TextFormatFlags.Default;
						}
						else
						{
							stringFormat.Alignment = StringAlignment.Center;
							flags = TextFormatFlags.HorizontalCenter;
						}
						break;
					}
					double num;
					if (e.ColumnIndex > 0 && double.TryParse(e.SubItem.Text, NumberStyles.Currency, NumberFormatInfo.CurrentInfo, out num) && num < 0.0)
					{
						if ((e.ItemState & ListViewItemStates.Selected) == (ListViewItemStates)0)
						{
							e.DrawBackground();
						}
						e.Graphics.DrawString(e.SubItem.Text, this.listViewEval.Font, Brushes.Red, e.Bounds, stringFormat);
					}
					else
					{
						e.DrawText(flags);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private void listViewEval_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
		{
			this.listViewEval.Invalidate();
		}

		private void listViewEval_Invalidated(object sender, InvalidateEventArgs e)
		{
			foreach (ListViewItem listViewItem in this.listViewEval.Items)
			{
				if (listViewItem == null)
				{
					break;
				}
				listViewItem.Tag = null;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			FormViewOK formViewOK = new FormViewOK(this);
			formViewOK.Show();
		}

		private void buttonContestant_Click(object sender, EventArgs e)
		{
			FormViewOUT formViewOUT = new FormViewOUT(this);
			formViewOUT.Show();
		}

		private void buttonIN_Click(object sender, EventArgs e)
		{
			FormViewIN formViewIN = new FormViewIN(this);
			formViewIN.Show();
		}

		private void stergeToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			string currentDirectory = null;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				string str = "..\\..\\probleme\\" + this.problem + "\\exe_concurenti\\";
				string text = this.listBoxExe.SelectedItem.ToString();
				if (!File.Exists(str + text))
				{
					MessageBox.Show("Fisierul '" + text + "' nu exista!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					File.Delete(str + text);
				}
				this.listBoxExe.Items.Remove(text);
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
		{
		}

		private void checkBoxCompileaza_CheckedChanged(object sender, EventArgs e)
		{
			this.progressBar1.Value = 0;
			this.progressBar1.Refresh();
			for (int i = 0; i < this.checkedListBoxSurse.Items.Count; i++)
			{
				if (this.checkBoxCompileaza.Checked)
				{
					this.checkedListBoxSurse.SetItemChecked(i, true);
				}
				else
				{
					this.checkedListBoxSurse.SetItemChecked(i, false);
				}
			}
			if (this.checkBoxCompileaza.Checked)
			{
				this.checkBoxCompilAll.Checked = false;
			}
			this.textBoxCompilerMessage.Text = "";
			this.textBoxCompilerMessage.Refresh();
			this.labelCompil.Text = "";
			this.labelCompil.Refresh();
		}

		private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void checkBoxCompilAll_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxCompilAll.Checked)
			{
				this.checkBoxCompileaza.Checked = false;
				this.textBoxCompilerMessage.Text = "";
				this.textBoxCompilerMessage.Refresh();
				this.labelCompil.Text = "";
				this.labelCompil.Refresh();
				this.progressBar1.Value = 0;
				this.progressBar1.Refresh();
				this.compilSingleProblem = false;
				return;
			}
			this.textBoxCompilerMessage.Text = "";
			this.textBoxCompilerMessage.Refresh();
			this.textBoxCompilerMessage.Text = "";
			this.labelCompil.Text = "";
			this.labelCompil.Refresh();
			this.compilSingleProblem = true;
			this.progressBar1.Value = 0;
			this.progressBar1.Refresh();
		}

		private void LoadTabPageEval()
		{
		}

		private void tabPageEval_Enter(object sender, EventArgs e)
		{
			try
			{
				Directory.SetCurrentDirectory(this.rootDirectory);
				if (!Directory.Exists("..\\..\\work"))
				{
					DirectoryInfo directoryInfo = Directory.CreateDirectory("..\\..\\work");
					directoryInfo.Attributes = (FileAttributes.Hidden | FileAttributes.Directory);
				}
				Directory.SetCurrentDirectory("..\\..\\work");
				this.workDirectory = Directory.GetCurrentDirectory();
			}
			catch (Exception)
			{
			}
			this.ReadRoundsXml();
			this.assoc = new Dictionary<string, string>();
			foreach (string current in this.probLimits.Keys)
			{
				string text = "..\\probleme\\" + current + "\\surse_concurenti";
				string text2 = "..\\probleme\\" + current + "\\exe_concurenti";
				string[] array = null;
				try
				{
					array = Directory.GetFiles(text);
					Array.Sort<string>(array);
				}
				catch (Exception)
				{
					MessageBox.Show("Nu exista folderul " + text, "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				try
				{
					string[] source = Directory.GetFiles(text2);
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string path = array2[i];
						string extension = Path.GetExtension(path);
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
						if (source.Contains(text2 + "\\" + fileNameWithoutExtension + ".exe") && !this.assoc.ContainsKey(fileNameWithoutExtension + ".exe"))
						{
							this.assoc.Add(fileNameWithoutExtension + ".exe", extension.ToLower());
						}
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Nu exista folderul " + text2, "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
			}
			if (this.comboBoxProblem.Text != "")
			{
				this.comboBoxProblem.SelectedItem = this.comboBoxProblem.Text;
			}
			this.checkBoxAllTests.Checked = true;
			this.buttonOK.Enabled = this.checkBoxStepByStep.Checked;
			this.buttonIN.Enabled = this.checkBoxStepByStep.Checked;
			this.buttonContestant.Enabled = this.checkBoxStepByStep.Checked;
		}

		private void compilareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormRezCompil formRezCompil = new FormRezCompil(this);
			formRezCompil.ShowDialog();
		}

		private void evaluareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormRezEval formRezEval = new FormRezEval(this);
			formRezEval.ShowDialog();
			if (formRezEval.ReportsDeleted)
			{
				this.contestantsID.Clear();
				this.dEval.Clear();
				this.ReadContestantsData(false);
			}
		}

		private void Eval_Load(object sender, EventArgs e)
		{
			this.GetInstalledSoftware();
		}

		private void produsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormAuthor formAuthor = new FormAuthor();
			formAuthor.Show();
		}

		private void checkBoxAllProblems_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxAllProblems.Checked)
			{
				this.checkBoxAllTests.Checked = true;
				this.checkBoxStepByStep.Checked = false;
			}
		}

		private void checkBoxAllExes_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < this.listBoxExe.Items.Count; i++)
			{
				if (this.checkBoxAllExes.Checked)
				{
					this.listBoxExe.SetItemChecked(i, true);
				}
				else
				{
					this.listBoxExe.SetItemChecked(i, false);
				}
			}
		}

		private void compilareToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			try
			{
				FormCompilSettings formCompilSettings = new FormCompilSettings(this);
				DialogResult dialogResult = formCompilSettings.ShowDialog();
				if (dialogResult == DialogResult.OK)
				{
					this.gcc = formCompilSettings.GCC;
					this.gpp = formCompilSettings.GPP;
					this.fpc = formCompilSettings.PAS;
				}
				else if (dialogResult == DialogResult.Cancel)
				{
					this.GetInstalledSoftware();
				}
			}
			catch (Exception)
			{
			}
		}

		private void labelListView_Click(object sender, EventArgs e)
		{
		}

		private void utilizareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string currentDirectory = null;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				Process.Start("EvaluatorHelp.chm");
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			if (this.compilSingleProblem)
			{
				UserState userState = new UserState("", "", this.problem);
				try
				{
					backgroundWorker.ReportProgress(0, userState);
					Eval._wait1.WaitOne();
				}
				catch (Exception)
				{
				}
				using (IEnumerator enumerator = this.col.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int index = (int)enumerator.Current;
						if (backgroundWorker.CancellationPending)
						{
							e.Cancel = true;
							break;
						}
						this.SourceCompile(index, true, backgroundWorker, e);
					}
					return;
				}
			}
			foreach (string current in this.probLimits.Keys)
			{
				this.problem = current;
				UserState userState2 = new UserState("", "", current);
				try
				{
					backgroundWorker.ReportProgress(0, userState2);
					Eval._wait1.WaitOne();
				}
				catch (Exception)
				{
				}
				for (int i = 0; i < this.nrSurse; i++)
				{
					if (backgroundWorker.CancellationPending)
					{
						e.Cancel = true;
						return;
					}
					this.SourceCompile(i, false, backgroundWorker, e);
				}
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			UserState userState = (UserState)e.UserState;
			try
			{
				if (userState.err != "")
				{
					this.textBoxCompilerMessage.Text = userState.err;
					this.textBoxCompilerMessage.Refresh();
				}
				this.progressBar1.Value = e.ProgressPercentage;
				if (e.ProgressPercentage == 0 && userState.msg == "" && userState.err == "" && userState.probl != "")
				{
					Convert.ToString(e.UserState);
					this.labelInfoProb.Text = string.Format("Problema{0,15}", userState.probl);
					this.labelInfoProb.Refresh();
					this.labelInfoTime.Text = string.Format("Limita timp: {0,8} ms", this.probLimits[userState.probl].Key);
					this.labelInfoTime.Refresh();
					this.labelMemLimit.Text = string.Format("Memorie totala:{0,4} MB", this.probLimits[userState.probl].Value);
					this.labelMemLimit.Refresh();
					this.labelMaxSource.Text = string.Format("Dimens. sursa:{0,5} KB", this.probLimits2[userState.probl].Value);
					this.labelMaxSource.Refresh();
					this.labelStack.Text = string.Format("Limita stiva:{0,9} MB", this.probLimits2[userState.probl].Key);
					this.labelStack.Refresh();
					this.labelCompil.Text = "[" + userState.probl + "]    ";
					this.labelCompil.Refresh();
					this.comboBoxEvalProblem.SelectedItem = userState.probl;
					this.comboBoxEvalProblem.Refresh();
					if (!this.compilSingleProblem)
					{
						this.LoadSourcesExesTests(false);
					}
					this.labelInfoTests.Text = string.Format("Numarul de teste: {0,5}  ", this.listBoxInputTests.Items.Count);
					this.labelInfoTests.Refresh();
					this.groupBox1.Refresh();
					this.nrSurse = this.checkedListBoxSurse.Items.Count;
				}
				else
				{
					this.progressBar1.Value = e.ProgressPercentage;
					if (userState.err == "" && userState.msg != "" && userState.msg != " OK !")
					{
						this.labelCompil.Text = "[" + userState.probl + "]     Se compileaza " + userState.msg;
						this.labelCompil.Refresh();
					}
					else if (userState.err != "" && userState.msg == "" && userState.probl != "")
					{
						this.textBoxCompilerMessage.Text = userState.err;
						this.textBoxCompilerMessage.Refresh();
					}
					else
					{
						if (userState.msg == " Eroare !")
						{
							Label expr_35D = this.labelCompil;
							expr_35D.Text += " Eroare !";
							this.textBoxCompilerMessage.Text = userState.err;
							this.labelCompil.Refresh();
							this.textBoxCompilerMessage.Refresh();
							Eval._wait1.Set();
							return;
						}
						if (userState.err == "" && userState.msg == "" && userState.probl == "" && !this.listBoxExe.Items.Contains(this.the_exe + ".exe"))
						{
							this.listBoxExe.Items.Add(this.the_exe + ".exe");
							this.listBoxExe.Refresh();
						}
						if (userState.msg == " OK !")
						{
							Label expr_445 = this.labelCompil;
							expr_445.Text += " OK !";
							this.labelCompil.Refresh();
							this.textBoxCompilerMessage.Text = userState.err;
							this.textBoxCompilerMessage.Refresh();
						}
					}
				}
			}
			catch (Exception)
			{
				Eval._wait1.Set();
			}
			Eval._wait1.Set();
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Cancelled)
			{
				this.labelCompil.Text = "Compilare oprita !";
			}
			else if (e.Error != null)
			{
				this.labelCompil.Text = "Eroare: " + e.Error.Message;
			}
			else
			{
				this.labelCompil.Text = "Terminat !";
				this.progressBar1.Value = 100;
			}
			this.buttonCompil.Text = "Compileaza";
			this.checkBoxAllExes.Enabled = true;
			this.checkBoxCompilAll.Enabled = true;
			this.checkBoxCompileaza.Enabled = true;
			this.buttonDeleteExe.Enabled = true;
			this.button1.Enabled = true;
			this.button2.Enabled = true;
			this.button3.Enabled = true;
			this.button4.Enabled = true;
			this.button5.Enabled = true;
			this.menuStrip1.Enabled = true;
			this.tabPageEval.Enabled = true;
			this.comboBoxEvalProblem.UseWaitCursor = false;
			this.labelCompil.Refresh();
		}

		private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker backgroundWorker = sender as BackgroundWorker;
			if (this.allProbs)
			{
				for (int i = 0; i < this.nrProbleme; i++)
				{
					if (backgroundWorker.CancellationPending)
					{
						e.Cancel = true;
						return;
					}
					try
					{
						this.problem = this.probLimits.Keys.ElementAt(i);
						this.CopyVerifToWork();
						UserState2 userState = new UserState2("", "", "", "", "");
						backgroundWorker.ReportProgress(0, userState);
						Eval._waitHandle.WaitOne();
					}
					catch (Exception)
					{
					}
					string text = this.dirProb + this.problem + "\\exe_concurenti\\";
					try
					{
						if (!Directory.Exists(text))
						{
							MessageBox.Show(string.Concat(new string[]
							{
								"Problema '",
								this.problem,
								": Directorul ",
								text,
								" nu exista!"
							}), "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						this.loadExesTests = true;
						UserState2 userState = new UserState2("Load", "", "", "", "");
						backgroundWorker.ReportProgress(0, userState);
						Eval._waitHandle.WaitOne();
						if (!this.loadExesTests)
						{
							e.Cancel = true;
							return;
						}
						this.col2 = this.checkedListBoxExe.CheckedItems;
						this.EvalProblem(backgroundWorker, e);
					}
					catch (Exception)
					{
					}
				}
				this.WriteExcel();
			}
			else
			{
				try
				{
					if (backgroundWorker.CancellationPending)
					{
						e.Cancel = true;
						return;
					}
					if (!this.LoadExesTests(false))
					{
						MessageBox.Show("Nu s-au putut incarca testele !", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						e.Cancel = true;
						return;
					}
					this.col2 = this.checkedListBoxExe.CheckedItems;
					this.EvalProblem(backgroundWorker, e);
					this.WriteExcel();
				}
				catch (Exception)
				{
				}
			}
			try
			{
				if (this.allProbs)
				{
					this.allProbs = false;
					string searchPattern = "*.txt";
					string[] array = Directory.GetFiles("..\\rezultate\\evaluare\\", searchPattern);
					Array.Sort<string>(array);
					StringBuilder stringBuilder = new StringBuilder();
					string[] array2 = array;
					for (int j = 0; j < array2.Length; j++)
					{
						string path = array2[j];
						stringBuilder.AppendLine();
						using (TextReader textReader = new StreamReader(path))
						{
							stringBuilder.Append(textReader.ReadToEnd());
							textReader.Close();
						}
						stringBuilder.AppendLine();
					}
					using (TextWriter textWriter = new StreamWriter("..\\rezultate\\evaluare\\All_Results-res.txt"))
					{
						textWriter.WriteLine(stringBuilder.ToString());
						textWriter.Close();
					}
					this.WriteExcel();
				}
			}
			catch (Exception)
			{
			}
		}

		private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			UserState2 userState = (UserState2)e.UserState;
			try
			{
				this.progressBar2.Value = e.ProgressPercentage;
				if (e.ProgressPercentage == 0 && userState.tst == "" && userState.pct == "" && userState.msg == "" && userState.tmp == "" && userState.mem == "")
				{
					this.comboBoxProblem.SelectedItem = this.problem;
					this.comboBoxProblem.Refresh();
					this.checkBoxAllIDs.Checked = true;
					Eval._waitHandle.Set();
				}
				else
				{
					if (userState.tst == "Load")
					{
						if (!this.LoadExesTests(true))
						{
							MessageBox.Show("Nu s-au putut incarca testele! ", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
							this.loadExesTests = false;
							Eval._waitHandle.Set();
							return;
						}
						Eval._waitHandle.Set();
					}
					if (userState.tst == "Total")
					{
						this.InsertListView("Total:", this.Total.ToString(), " puncte", "", "");
						Label expr_14A = this.labelListView;
						expr_14A.Text += " terminat !";
						Thread.Sleep(200);
						this.labelListView.Refresh();
						Eval._waitHandle.Set();
					}
					else if (userState.tst == "text" && userState.pct != "" && userState.tmp == "" && userState.mem == "" && userState.msg == "")
					{
						this.labelListView.TextAlign = ContentAlignment.MiddleLeft;
						this.Total = 0;
						string arg = "[" + this.problem + "]";
						this.labelListView.Text = string.Format("{0,-12}", arg);
						arg = "Evaluare '" + userState.pct + "'";
						Label expr_249 = this.labelListView;
						expr_249.Text += string.Format("{0,50} ... ", arg);
						this.labelListView.Refresh();
						this.listViewEval.Items.Clear();
						Eval._waitHandle.Set();
					}
					else if (userState.tst == "progres")
					{
						this.progressBar2.Value = e.ProgressPercentage;
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 2)
					{
						this.InsertListView(userState.tst, "0", "-", "-", userState.msg);
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 7)
					{
						this.InsertListView(userState.tst, "0", userState.tmp, "-", "Memory limit exceeded!");
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 8)
					{
						this.InsertListView(userState.tst, "0", "-", "nedeterminata", "Time limit exceeded!");
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 9)
					{
						this.InsertListView(userState.tst, "0", "-", userState.mem, "Time limit exceeded!");
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 10)
					{
						this.InsertListView(userState.tst, "0", userState.tmp, "-", "Memory limit exceeded!");
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 11)
					{
						this.InsertListView(userState.tst, "0", userState.tmp, userState.mem, "Runtime Error!");
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 12)
					{
						this.InsertListView(userState.tst, userState.pct, userState.tmp, "nedeterminata", userState.msg);
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 1)
					{
						this.InsertListView(userState.tst, userState.pct, userState.tmp, userState.mem, userState.msg);
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 3)
					{
						this.InsertListView(userState.tst, "0", userState.tmp, "-", "Memory limit exceeded!");
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 4)
					{
						this.InsertListView(userState.tst, "0", userState.tmp, userState.mem, "Runtime Error!");
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 5)
					{
						this.InsertListView(userState.tst, userState.pct, userState.tmp, "nedeterminata", userState.msg);
						Eval._waitHandle.Set();
					}
					else if (userState.caz == 6)
					{
						this.InsertListView(userState.tst, userState.pct, userState.tmp, userState.mem, userState.msg);
						Eval._waitHandle.Set();
					}
					else
					{
						Eval._waitHandle.Set();
					}
				}
			}
			catch (Exception)
			{
				Eval._waitHandle.Set();
			}
		}

		private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				Eval._waitHandle.Set();
				if (e.Cancelled)
				{
					this.labelListView.TextAlign = ContentAlignment.MiddleCenter;
					this.labelListView.Text = "Evaluare oprita !";
				}
				else if (e.Error != null)
				{
					this.labelListView.Text = "Eroare: " + e.Error.Message;
				}
				else
				{
					this.progressBar2.Value = 100;
				}
				this.comboBoxProblem.UseWaitCursor = false;
				this.comboBoxTest.UseWaitCursor = false;
				this.checkBoxAllIDs.Enabled = true;
				this.checkBoxAllTests.Enabled = true;
				this.checkBoxAllProblems.Enabled = true;
				this.checkBoxStepByStep.Enabled = true;
				this.menuStrip1.Enabled = true;
				this.tabPageCompil.Enabled = true;
				this.buttonEval.Text = "Evalueaza";
				this.evaluationRunning = false;
				if (!this.checkBoxAllTests.Checked && !e.Cancelled)
				{
					Label expr_FB = this.labelListView;
					expr_FB.Text += " Terminat !";
				}
				this.labelListView.Refresh();
			}
			catch (Exception)
			{
			}
		}

		private void checkedListBoxExe_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void DeleteInputTest()
		{
			string currentDirectory = null;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				string str = "..\\..\\probleme\\" + this.comboBoxEvalProblem.Text + "\\teste\\";
				string text = this.listBoxInputTests.SelectedItem.ToString();
				if (!File.Exists(str + text))
				{
					MessageBox.Show("Fisierul '" + text + "' nu exista!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					DialogResult dialogResult = MessageBox.Show("Sunteti sigur ca vreti sa stergeti fisierul '" + text + "' ?", "Evaluator", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
					if (dialogResult == DialogResult.OK)
					{
						File.Delete(str + text);
						this.listBoxInputTests.Items.Remove(text);
					}
				}
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		private void EditInputTest()
		{
			string currentDirectory = null;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				string str = "..\\..\\probleme\\" + this.comboBoxEvalProblem.Text + "\\teste\\";
				this.info2 = str;
				string str2 = this.listBoxInputTests.SelectedItem.ToString();
				if (!File.Exists(str + str2))
				{
					MessageBox.Show("Fisierul '" + str2 + "' nu exista!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					this.information = str2;
					FormViewInputTests formViewInputTests = new FormViewInputTests(this);
					formViewInputTests.ShowDialog();
				}
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.EditInputTest();
		}

		private void contextMenuStripViewInputTest_Opening(object sender, CancelEventArgs e)
		{
		}

		private void editeazaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.EditInputTest();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.DeleteInputTest();
		}

		private void listBoxInputTests_DoubleClick(object sender, EventArgs e)
		{
			this.EditInputTest();
		}

		private void stergeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.DeleteInputTest();
		}

		private void EditOutputTest()
		{
			string currentDirectory = null;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				string str = "..\\..\\probleme\\" + this.comboBoxEvalProblem.Text + "\\teste\\";
				this.info2 = str;
				string str2 = this.listBoxOutputTests.SelectedItem.ToString();
				if (!File.Exists(str + str2))
				{
					MessageBox.Show("Fisierul '" + str2 + "' nu exista!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					this.information = str2;
					FormViewInputTests formViewInputTests = new FormViewInputTests(this);
					formViewInputTests.ShowDialog();
				}
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		private void DeleteOutputTest()
		{
			string currentDirectory = null;
			try
			{
				currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				string str = "..\\..\\probleme\\" + this.comboBoxEvalProblem.Text + "\\teste\\";
				string text = this.listBoxOutputTests.SelectedItem.ToString();
				if (!File.Exists(str + text))
				{
					MessageBox.Show("Fisierul '" + text + "' nu exista!", "Evaluator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					DialogResult dialogResult = MessageBox.Show("Sunteti sigur ca vreti sa stergeti fisierul '" + text + "' ?", "Evaluator", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
					if (dialogResult == DialogResult.OK)
					{
						File.Delete(str + text);
						this.listBoxOutputTests.Items.Remove(text);
					}
				}
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
				Directory.SetCurrentDirectory(currentDirectory);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.EditOutputTest();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			this.DeleteOutputTest();
		}

		private void listBoxOutputTests_DoubleClick(object sender, EventArgs e)
		{
			this.EditOutputTest();
		}

		private void editeazaToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.EditOutputTest();
		}

		private void stergeToolStripMenuItem2_Click(object sender, EventArgs e)
		{
			this.DeleteOutputTest();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.checkedListBoxSurse.SelectedItem != null)
				{
					this.information = this.checkedListBoxSurse.SelectedItem.ToString();
					this.info2 = this.comboBoxEvalProblem.Text;
					FormViewSource formViewSource = new FormViewSource(this);
					formViewSource.ShowDialog();
				}
			}
			catch (Exception)
			{
			}
		}

		private void checkedListBoxSurse_DoubleClick(object sender, EventArgs e)
		{
			if (this.checkedListBoxSurse.SelectedItem != null)
			{
				this.information = this.checkedListBoxSurse.SelectedItem.ToString();
				this.info2 = this.comboBoxEvalProblem.Text;
				FormViewSource formViewSource = new FormViewSource(this);
				formViewSource.ShowDialog();
			}
		}

		private void concurentiToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string currentDirectory = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(this.rootDirectory);
				FormEditContestants formEditContestants = new FormEditContestants(this);
				formEditContestants.ShowDialog();
				if (formEditContestants.canDelID)
				{
					this.contestantsID.Clear();
					this.dEval.Clear();
					this.ReadContestantsData(false);
				}
				Directory.SetCurrentDirectory(currentDirectory);
			}
			catch (Exception)
			{
			}
		}

		private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
		{
			if (this.backgroundWorker2.IsBusy)
			{
				e.Cancel = true;
			}
			if (this.backgroundWorker1.IsBusy)
			{
				e.Cancel = true;
			}
		}

		private void groupBoxSelect_Enter(object sender, EventArgs e)
		{
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Eval));
			this.menuStrip1 = new MenuStrip();
			this.problemeToolStripMenuItem = new ToolStripMenuItem();
			this.nouaToolStripMenuItem = new ToolStripMenuItem();
			this.concurentiToolStripMenuItem = new ToolStripMenuItem();
			this.compilareToolStripMenuItem1 = new ToolStripMenuItem();
			this.iesireToolStripMenuItem = new ToolStripMenuItem();
			this.rapoarteToolStripMenuItem = new ToolStripMenuItem();
			this.compilareToolStripMenuItem = new ToolStripMenuItem();
			this.evaluareToolStripMenuItem = new ToolStripMenuItem();
			this.despreToolStripMenuItem = new ToolStripMenuItem();
			this.utilizareToolStripMenuItem = new ToolStripMenuItem();
			this.produsToolStripMenuItem = new ToolStripMenuItem();
			this.tabControl1 = new TabControl();
			this.tabPageCompil = new TabPage();
			this.progressBar1 = new ProgressBar();
			this.labelCompil = new Label();
			this.groupBox3 = new GroupBox();
			this.button4 = new Button();
			this.button3 = new Button();
			this.listBoxOutputTests = new ListBox();
			this.contextMenuStripOutputTest = new ContextMenuStrip(this.components);
			this.editeazaToolStripMenuItem1 = new ToolStripMenuItem();
			this.stergeToolStripMenuItem2 = new ToolStripMenuItem();
			this.groupBox2 = new GroupBox();
			this.button2 = new Button();
			this.button1 = new Button();
			this.listBoxInputTests = new ListBox();
			this.contextMenuStripViewInputTest = new ContextMenuStrip(this.components);
			this.editeazaToolStripMenuItem = new ToolStripMenuItem();
			this.stergeToolStripMenuItem = new ToolStripMenuItem();
			this.groupBoxExecutabile = new GroupBox();
			this.buttonDeleteExe = new Button();
			this.checkBoxAllExes = new CheckBox();
			this.listBoxExe = new CheckedListBox();
			this.contextMenuStripDeleteExe = new ContextMenuStrip(this.components);
			this.stergeToolStripMenuItem1 = new ToolStripMenuItem();
			this.groupBoxSurse = new GroupBox();
			this.button5 = new Button();
			this.checkBoxCompileaza = new CheckBox();
			this.checkedListBoxSurse = new CheckedListBox();
			this.textBoxCompilerMessage = new TextBox();
			this.groupBox1 = new GroupBox();
			this.labelMaxSource = new Label();
			this.labelStack = new Label();
			this.labelInfoTests = new Label();
			this.labelMemLimit = new Label();
			this.labelInfoTime = new Label();
			this.labelInfoProb = new Label();
			this.groupBoxCompilProb = new GroupBox();
			this.comboBoxEvalProblem = new ComboBox();
			this.groupBoxCompilare = new GroupBox();
			this.checkBoxCompilAll = new CheckBox();
			this.buttonCompil = new Button();
			this.tabPageEval = new TabPage();
			this.groupBox4 = new GroupBox();
			this.progressBar2 = new ProgressBar();
			this.listViewEval = new ListView();
			this.Test = new ColumnHeader();
			this.Punctaj = new ColumnHeader();
			this.Timp = new ColumnHeader();
			this.Memorie = new ColumnHeader();
			this.Mesaj = new ColumnHeader();
			this.labelListView = new Label();
			this.groupBoxSelect = new GroupBox();
			this.groupBox5 = new GroupBox();
			this.labelSource = new Label();
			this.labelStackLimit = new Label();
			this.labelMem = new Label();
			this.labelTime = new Label();
			this.labelProb = new Label();
			this.buttonIN = new Button();
			this.checkedListBoxExe = new CheckedListBox();
			this.checkBoxStepByStep = new CheckBox();
			this.buttonEval = new Button();
			this.buttonOK = new Button();
			this.buttonContestant = new Button();
			this.label2 = new Label();
			this.comboBoxTest = new ComboBox();
			this.checkBoxAllTests = new CheckBox();
			this.checkBoxAllIDs = new CheckBox();
			this.comboBoxProblem = new ComboBox();
			this.label1 = new Label();
			this.checkBoxAllProblems = new CheckBox();
			this.toolTip1 = new ToolTip(this.components);
			this.foldCompil = new FolderBrowserDialog();
			this.toolTipEvalAll = new ToolTip(this.components);
			this.toolTip2 = new ToolTip(this.components);
			this.backgroundWorker1 = new BackgroundWorker();
			this.backgroundWorker2 = new BackgroundWorker();
			this.menuStrip1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageCompil.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.contextMenuStripOutputTest.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.contextMenuStripViewInputTest.SuspendLayout();
			this.groupBoxExecutabile.SuspendLayout();
			this.contextMenuStripDeleteExe.SuspendLayout();
			this.groupBoxSurse.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBoxCompilProb.SuspendLayout();
			this.groupBoxCompilare.SuspendLayout();
			this.tabPageEval.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBoxSelect.SuspendLayout();
			this.groupBox5.SuspendLayout();
			base.SuspendLayout();
			this.menuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.problemeToolStripMenuItem,
				this.rapoarteToolStripMenuItem,
				this.despreToolStripMenuItem
			});
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new Size(809, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.problemeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.nouaToolStripMenuItem,
				this.concurentiToolStripMenuItem,
				this.compilareToolStripMenuItem1,
				this.iesireToolStripMenuItem
			});
			this.problemeToolStripMenuItem.Name = "problemeToolStripMenuItem";
			this.problemeToolStripMenuItem.Size = new Size(47, 20);
			this.problemeToolStripMenuItem.Text = "&Setari";
			this.nouaToolStripMenuItem.Name = "nouaToolStripMenuItem";
			this.nouaToolStripMenuItem.Size = new Size(148, 22);
			this.nouaToolStripMenuItem.Text = "&Probleme";
			this.nouaToolStripMenuItem.Click += new EventHandler(this.nouaToolStripMenuItem_Click);
			this.concurentiToolStripMenuItem.Name = "concurentiToolStripMenuItem";
			this.concurentiToolStripMenuItem.Size = new Size(148, 22);
			this.concurentiToolStripMenuItem.Text = "C&oncurenti";
			this.concurentiToolStripMenuItem.Click += new EventHandler(this.concurentiToolStripMenuItem_Click);
			this.compilareToolStripMenuItem1.Name = "compilareToolStripMenuItem1";
			this.compilareToolStripMenuItem1.Size = new Size(148, 22);
			this.compilareToolStripMenuItem1.Text = "&Compilatoare";
			this.compilareToolStripMenuItem1.Click += new EventHandler(this.compilareToolStripMenuItem1_Click);
			this.iesireToolStripMenuItem.Name = "iesireToolStripMenuItem";
			this.iesireToolStripMenuItem.Size = new Size(148, 22);
			this.iesireToolStripMenuItem.Text = "&Iesire";
			this.iesireToolStripMenuItem.Click += new EventHandler(this.iesireToolStripMenuItem_Click);
			this.rapoarteToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.compilareToolStripMenuItem,
				this.evaluareToolStripMenuItem
			});
			this.rapoarteToolStripMenuItem.Name = "rapoarteToolStripMenuItem";
			this.rapoarteToolStripMenuItem.Size = new Size(64, 20);
			this.rapoarteToolStripMenuItem.Text = "&Rapoarte";
			this.compilareToolStripMenuItem.Name = "compilareToolStripMenuItem";
			this.compilareToolStripMenuItem.Size = new Size(132, 22);
			this.compilareToolStripMenuItem.Text = "&Compilare";
			this.compilareToolStripMenuItem.Click += new EventHandler(this.compilareToolStripMenuItem_Click);
			this.evaluareToolStripMenuItem.Name = "evaluareToolStripMenuItem";
			this.evaluareToolStripMenuItem.Size = new Size(132, 22);
			this.evaluareToolStripMenuItem.Text = "&Evaluare";
			this.evaluareToolStripMenuItem.Click += new EventHandler(this.evaluareToolStripMenuItem_Click);
			this.despreToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.utilizareToolStripMenuItem,
				this.produsToolStripMenuItem
			});
			this.despreToolStripMenuItem.Name = "despreToolStripMenuItem";
			this.despreToolStripMenuItem.Size = new Size(49, 20);
			this.despreToolStripMenuItem.Text = "&Ajutor";
			this.utilizareToolStripMenuItem.Name = "utilizareToolStripMenuItem";
			this.utilizareToolStripMenuItem.Size = new Size(123, 22);
			this.utilizareToolStripMenuItem.Text = "Utilizare";
			this.utilizareToolStripMenuItem.Click += new EventHandler(this.utilizareToolStripMenuItem_Click);
			this.produsToolStripMenuItem.Name = "produsToolStripMenuItem";
			this.produsToolStripMenuItem.Size = new Size(123, 22);
			this.produsToolStripMenuItem.Text = "Produs";
			this.produsToolStripMenuItem.Click += new EventHandler(this.produsToolStripMenuItem_Click);
			this.tabControl1.Controls.Add(this.tabPageCompil);
			this.tabControl1.Controls.Add(this.tabPageEval);
			this.tabControl1.Dock = DockStyle.Fill;
			this.tabControl1.Location = new Point(0, 24);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new Size(809, 465);
			this.tabControl1.SizeMode = TabSizeMode.Fixed;
			this.tabControl1.TabIndex = 2;
			this.tabControl1.Selecting += new TabControlCancelEventHandler(this.tabControl1_Selecting);
			this.tabControl1.Selected += new TabControlEventHandler(this.tabControl1_Selected);
			this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
			this.tabPageCompil.BackColor = Color.Silver;
			this.tabPageCompil.Controls.Add(this.progressBar1);
			this.tabPageCompil.Controls.Add(this.labelCompil);
			this.tabPageCompil.Controls.Add(this.groupBox3);
			this.tabPageCompil.Controls.Add(this.groupBox2);
			this.tabPageCompil.Controls.Add(this.groupBoxExecutabile);
			this.tabPageCompil.Controls.Add(this.groupBoxSurse);
			this.tabPageCompil.Controls.Add(this.textBoxCompilerMessage);
			this.tabPageCompil.Controls.Add(this.groupBox1);
			this.tabPageCompil.Controls.Add(this.groupBoxCompilProb);
			this.tabPageCompil.Controls.Add(this.groupBoxCompilare);
			this.tabPageCompil.Location = new Point(4, 22);
			this.tabPageCompil.Name = "tabPageCompil";
			this.tabPageCompil.Padding = new Padding(3);
			this.tabPageCompil.Size = new Size(801, 439);
			this.tabPageCompil.TabIndex = 0;
			this.tabPageCompil.Text = "Compilare";
			this.tabPageCompil.Enter += new EventHandler(this.tabPageCompil_Enter);
			this.progressBar1.BackColor = Color.LightGray;
			this.progressBar1.Location = new Point(8, 344);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new Size(786, 11);
			this.progressBar1.TabIndex = 10;
			this.labelCompil.BorderStyle = BorderStyle.Fixed3D;
			this.labelCompil.FlatStyle = FlatStyle.Flat;
			this.labelCompil.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelCompil.ForeColor = Color.MediumVioletRed;
			this.labelCompil.Location = new Point(8, 358);
			this.labelCompil.Name = "labelCompil";
			this.labelCompil.Size = new Size(786, 20);
			this.labelCompil.TabIndex = 9;
			this.labelCompil.TextAlign = ContentAlignment.MiddleLeft;
			this.groupBox3.Controls.Add(this.button4);
			this.groupBox3.Controls.Add(this.button3);
			this.groupBox3.Controls.Add(this.listBoxOutputTests);
			this.groupBox3.Location = new Point(653, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new Size(141, 334);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Teste Iesire";
			this.button4.BackColor = Color.LightGray;
			this.button4.Location = new Point(70, 312);
			this.button4.Name = "button4";
			this.button4.Size = new Size(60, 20);
			this.button4.TabIndex = 2;
			this.button4.Text = "Sterge";
			this.button4.UseVisualStyleBackColor = false;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.button3.BackColor = Color.LightGray;
			this.button3.Location = new Point(12, 312);
			this.button3.Name = "button3";
			this.button3.Size = new Size(60, 20);
			this.button3.TabIndex = 1;
			this.button3.Text = "Editeaza";
			this.toolTip1.SetToolTip(this.button3, "Vizualizati si editati testul selectat");
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.listBoxOutputTests.BackColor = Color.LightGray;
			this.listBoxOutputTests.ContextMenuStrip = this.contextMenuStripOutputTest;
			this.listBoxOutputTests.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.listBoxOutputTests.FormattingEnabled = true;
			this.listBoxOutputTests.Location = new Point(12, 21);
			this.listBoxOutputTests.Name = "listBoxOutputTests";
			this.listBoxOutputTests.Size = new Size(118, 290);
			this.listBoxOutputTests.TabIndex = 0;
			this.listBoxOutputTests.DoubleClick += new EventHandler(this.listBoxOutputTests_DoubleClick);
			this.contextMenuStripOutputTest.Items.AddRange(new ToolStripItem[]
			{
				this.editeazaToolStripMenuItem1,
				this.stergeToolStripMenuItem2
			});
			this.contextMenuStripOutputTest.Name = "contextMenuStripOutputTest";
			this.contextMenuStripOutputTest.Size = new Size(127, 48);
			this.editeazaToolStripMenuItem1.Name = "editeazaToolStripMenuItem1";
			this.editeazaToolStripMenuItem1.Size = new Size(126, 22);
			this.editeazaToolStripMenuItem1.Text = "&Editeaza";
			this.editeazaToolStripMenuItem1.Click += new EventHandler(this.editeazaToolStripMenuItem1_Click);
			this.stergeToolStripMenuItem2.Name = "stergeToolStripMenuItem2";
			this.stergeToolStripMenuItem2.Size = new Size(126, 22);
			this.stergeToolStripMenuItem2.Text = "&Sterge";
			this.stergeToolStripMenuItem2.Click += new EventHandler(this.stergeToolStripMenuItem2_Click);
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Controls.Add(this.listBoxInputTests);
			this.groupBox2.Location = new Point(501, 6);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new Size(141, 334);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Teste Intrare";
			this.button2.BackColor = Color.LightGray;
			this.button2.Location = new Point(70, 312);
			this.button2.Name = "button2";
			this.button2.Size = new Size(60, 20);
			this.button2.TabIndex = 2;
			this.button2.Text = "Sterge";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.button1.BackColor = Color.LightGray;
			this.button1.Location = new Point(12, 312);
			this.button1.Name = "button1";
			this.button1.Size = new Size(60, 20);
			this.button1.TabIndex = 1;
			this.button1.Text = "Editeaza";
			this.toolTip1.SetToolTip(this.button1, "Vizualizati si editati testul selectat");
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.listBoxInputTests.BackColor = Color.LightGray;
			this.listBoxInputTests.ContextMenuStrip = this.contextMenuStripViewInputTest;
			this.listBoxInputTests.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.listBoxInputTests.FormattingEnabled = true;
			this.listBoxInputTests.Location = new Point(12, 21);
			this.listBoxInputTests.Name = "listBoxInputTests";
			this.listBoxInputTests.Size = new Size(118, 290);
			this.listBoxInputTests.TabIndex = 0;
			this.listBoxInputTests.DoubleClick += new EventHandler(this.listBoxInputTests_DoubleClick);
			this.contextMenuStripViewInputTest.Items.AddRange(new ToolStripItem[]
			{
				this.editeazaToolStripMenuItem,
				this.stergeToolStripMenuItem
			});
			this.contextMenuStripViewInputTest.Name = "contextMenuStripViewInputTest";
			this.contextMenuStripViewInputTest.Size = new Size(127, 48);
			this.contextMenuStripViewInputTest.Opening += new CancelEventHandler(this.contextMenuStripViewInputTest_Opening);
			this.editeazaToolStripMenuItem.Name = "editeazaToolStripMenuItem";
			this.editeazaToolStripMenuItem.Size = new Size(126, 22);
			this.editeazaToolStripMenuItem.Text = "&Editeaza";
			this.editeazaToolStripMenuItem.Click += new EventHandler(this.editeazaToolStripMenuItem_Click);
			this.stergeToolStripMenuItem.Name = "stergeToolStripMenuItem";
			this.stergeToolStripMenuItem.Size = new Size(126, 22);
			this.stergeToolStripMenuItem.Text = "&Sterge";
			this.stergeToolStripMenuItem.Click += new EventHandler(this.stergeToolStripMenuItem_Click);
			this.groupBoxExecutabile.Controls.Add(this.buttonDeleteExe);
			this.groupBoxExecutabile.Controls.Add(this.checkBoxAllExes);
			this.groupBoxExecutabile.Controls.Add(this.listBoxExe);
			this.groupBoxExecutabile.Location = new Point(341, 6);
			this.groupBoxExecutabile.Name = "groupBoxExecutabile";
			this.groupBoxExecutabile.Size = new Size(149, 334);
			this.groupBoxExecutabile.TabIndex = 6;
			this.groupBoxExecutabile.TabStop = false;
			this.groupBoxExecutabile.Text = "Executabile";
			this.buttonDeleteExe.BackColor = Color.LightGray;
			this.buttonDeleteExe.Location = new Point(78, 311);
			this.buttonDeleteExe.Name = "buttonDeleteExe";
			this.buttonDeleteExe.Size = new Size(60, 20);
			this.buttonDeleteExe.TabIndex = 2;
			this.buttonDeleteExe.Text = "Sterge";
			this.buttonDeleteExe.UseVisualStyleBackColor = false;
			this.buttonDeleteExe.Click += new EventHandler(this.buttonDeleteExe_Click);
			this.checkBoxAllExes.AutoSize = true;
			this.checkBoxAllExes.Location = new Point(13, 313);
			this.checkBoxAllExes.Name = "checkBoxAllExes";
			this.checkBoxAllExes.RightToLeft = RightToLeft.No;
			this.checkBoxAllExes.Size = new Size(54, 17);
			this.checkBoxAllExes.TabIndex = 1;
			this.checkBoxAllExes.Text = "Toate";
			this.toolTip1.SetToolTip(this.checkBoxAllExes, "Selectati pentru stergere \r\ntoate executabilele problemei");
			this.checkBoxAllExes.UseVisualStyleBackColor = true;
			this.checkBoxAllExes.CheckedChanged += new EventHandler(this.checkBoxAllExes_CheckedChanged);
			this.listBoxExe.BackColor = Color.LightGray;
			this.listBoxExe.CheckOnClick = true;
			this.listBoxExe.ContextMenuStrip = this.contextMenuStripDeleteExe;
			this.listBoxExe.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.listBoxExe.ForeColor = Color.MediumVioletRed;
			this.listBoxExe.FormattingEnabled = true;
			this.listBoxExe.Location = new Point(12, 20);
			this.listBoxExe.Name = "listBoxExe";
			this.listBoxExe.Size = new Size(126, 289);
			this.listBoxExe.TabIndex = 0;
			this.contextMenuStripDeleteExe.Items.AddRange(new ToolStripItem[]
			{
				this.stergeToolStripMenuItem1
			});
			this.contextMenuStripDeleteExe.Name = "contextMenuStripDeleteExe";
			this.contextMenuStripDeleteExe.Size = new Size(118, 26);
			this.stergeToolStripMenuItem1.Name = "stergeToolStripMenuItem1";
			this.stergeToolStripMenuItem1.Size = new Size(117, 22);
			this.stergeToolStripMenuItem1.Text = "Sterge";
			this.stergeToolStripMenuItem1.Click += new EventHandler(this.stergeToolStripMenuItem1_Click);
			this.groupBoxSurse.Controls.Add(this.button5);
			this.groupBoxSurse.Controls.Add(this.checkBoxCompileaza);
			this.groupBoxSurse.Controls.Add(this.checkedListBoxSurse);
			this.groupBoxSurse.Location = new Point(182, 6);
			this.groupBoxSurse.Name = "groupBoxSurse";
			this.groupBoxSurse.Size = new Size(149, 334);
			this.groupBoxSurse.TabIndex = 5;
			this.groupBoxSurse.TabStop = false;
			this.groupBoxSurse.Text = "Surse";
			this.button5.BackColor = Color.LightGray;
			this.button5.Location = new Point(77, 310);
			this.button5.Name = "button5";
			this.button5.Size = new Size(60, 20);
			this.button5.TabIndex = 2;
			this.button5.Text = "Vezi";
			this.toolTip1.SetToolTip(this.button5, "Vizualizati sursa selectata");
			this.button5.UseVisualStyleBackColor = false;
			this.button5.Click += new EventHandler(this.button5_Click);
			this.checkBoxCompileaza.AutoSize = true;
			this.checkBoxCompileaza.Location = new Point(12, 312);
			this.checkBoxCompileaza.Name = "checkBoxCompileaza";
			this.checkBoxCompileaza.RightToLeft = RightToLeft.No;
			this.checkBoxCompileaza.Size = new Size(54, 17);
			this.checkBoxCompileaza.TabIndex = 1;
			this.checkBoxCompileaza.TabStop = false;
			this.checkBoxCompileaza.Text = "Toate";
			this.toolTip1.SetToolTip(this.checkBoxCompileaza, "Selectati compilarea tuturor \r\nsurselor problemei");
			this.checkBoxCompileaza.UseVisualStyleBackColor = true;
			this.checkBoxCompileaza.CheckedChanged += new EventHandler(this.checkBoxCompileaza_CheckedChanged);
			this.checkedListBoxSurse.BackColor = Color.LightGray;
			this.checkedListBoxSurse.CheckOnClick = true;
			this.checkedListBoxSurse.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.checkedListBoxSurse.ForeColor = Color.Blue;
			this.checkedListBoxSurse.FormattingEnabled = true;
			this.checkedListBoxSurse.Location = new Point(12, 19);
			this.checkedListBoxSurse.Name = "checkedListBoxSurse";
			this.checkedListBoxSurse.Size = new Size(126, 289);
			this.checkedListBoxSurse.TabIndex = 0;
			this.checkedListBoxSurse.SelectedIndexChanged += new EventHandler(this.checkedListBoxSurse_SelectedIndexChanged);
			this.checkedListBoxSurse.DoubleClick += new EventHandler(this.checkedListBoxSurse_DoubleClick);
			this.textBoxCompilerMessage.BackColor = Color.LightGray;
			this.textBoxCompilerMessage.Location = new Point(8, 381);
			this.textBoxCompilerMessage.Multiline = true;
			this.textBoxCompilerMessage.Name = "textBoxCompilerMessage";
			this.textBoxCompilerMessage.ReadOnly = true;
			this.textBoxCompilerMessage.ScrollBars = ScrollBars.Vertical;
			this.textBoxCompilerMessage.Size = new Size(786, 53);
			this.textBoxCompilerMessage.TabIndex = 4;
			this.groupBox1.Controls.Add(this.labelMaxSource);
			this.groupBox1.Controls.Add(this.labelStack);
			this.groupBox1.Controls.Add(this.labelInfoTests);
			this.groupBox1.Controls.Add(this.labelMemLimit);
			this.groupBox1.Controls.Add(this.labelInfoTime);
			this.groupBox1.Controls.Add(this.labelInfoProb);
			this.groupBox1.FlatStyle = FlatStyle.Flat;
			this.groupBox1.ImeMode = ImeMode.NoControl;
			this.groupBox1.Location = new Point(8, 80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(163, 157);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Informatii";
			this.labelMaxSource.BorderStyle = BorderStyle.Fixed3D;
			this.labelMaxSource.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelMaxSource.Location = new Point(9, 107);
			this.labelMaxSource.Name = "labelMaxSource";
			this.labelMaxSource.Size = new Size(145, 17);
			this.labelMaxSource.TabIndex = 5;
			this.labelMaxSource.Text = "Dimens. sursa:";
			this.labelStack.BorderStyle = BorderStyle.Fixed3D;
			this.labelStack.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelStack.Location = new Point(8, 86);
			this.labelStack.Name = "labelStack";
			this.labelStack.Size = new Size(145, 17);
			this.labelStack.TabIndex = 4;
			this.labelStack.Text = "Limita stiva:";
			this.labelInfoTests.BorderStyle = BorderStyle.Fixed3D;
			this.labelInfoTests.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelInfoTests.Location = new Point(9, 128);
			this.labelInfoTests.Name = "labelInfoTests";
			this.labelInfoTests.Size = new Size(145, 17);
			this.labelInfoTests.TabIndex = 3;
			this.labelInfoTests.Text = "Numarul de teste:";
			this.labelMemLimit.BorderStyle = BorderStyle.Fixed3D;
			this.labelMemLimit.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelMemLimit.Location = new Point(10, 65);
			this.labelMemLimit.Name = "labelMemLimit";
			this.labelMemLimit.Size = new Size(143, 17);
			this.labelMemLimit.TabIndex = 2;
			this.labelMemLimit.Text = "Memorie totala: ";
			this.labelInfoTime.BorderStyle = BorderStyle.Fixed3D;
			this.labelInfoTime.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelInfoTime.Location = new Point(8, 44);
			this.labelInfoTime.Name = "labelInfoTime";
			this.labelInfoTime.Size = new Size(145, 17);
			this.labelInfoTime.TabIndex = 1;
			this.labelInfoTime.Text = "Limita timp: ";
			this.labelInfoProb.BorderStyle = BorderStyle.Fixed3D;
			this.labelInfoProb.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelInfoProb.Location = new Point(8, 23);
			this.labelInfoProb.Name = "labelInfoProb";
			this.labelInfoProb.Size = new Size(145, 17);
			this.labelInfoProb.TabIndex = 0;
			this.labelInfoProb.Text = "Problema: ";
			this.groupBoxCompilProb.Controls.Add(this.comboBoxEvalProblem);
			this.groupBoxCompilProb.Location = new Point(8, 6);
			this.groupBoxCompilProb.Name = "groupBoxCompilProb";
			this.groupBoxCompilProb.Size = new Size(163, 64);
			this.groupBoxCompilProb.TabIndex = 2;
			this.groupBoxCompilProb.TabStop = false;
			this.groupBoxCompilProb.Text = "Problema";
			this.comboBoxEvalProblem.BackColor = Color.Gainsboro;
			this.comboBoxEvalProblem.FormattingEnabled = true;
			this.comboBoxEvalProblem.Location = new Point(8, 21);
			this.comboBoxEvalProblem.Name = "comboBoxEvalProblem";
			this.comboBoxEvalProblem.Size = new Size(144, 21);
			this.comboBoxEvalProblem.TabIndex = 0;
			this.toolTip1.SetToolTip(this.comboBoxEvalProblem, "Selectati problema pentru care \r\ndoriti sa compilati sursele");
			this.comboBoxEvalProblem.SelectedIndexChanged += new EventHandler(this.comboBoxEvalProblem_SelectedIndexChanged);
			this.groupBoxCompilare.Controls.Add(this.checkBoxCompilAll);
			this.groupBoxCompilare.Controls.Add(this.buttonCompil);
			this.groupBoxCompilare.Location = new Point(8, 243);
			this.groupBoxCompilare.Name = "groupBoxCompilare";
			this.groupBoxCompilare.Size = new Size(163, 97);
			this.groupBoxCompilare.TabIndex = 1;
			this.groupBoxCompilare.TabStop = false;
			this.groupBoxCompilare.Text = "Compilare";
			this.checkBoxCompilAll.AutoSize = true;
			this.checkBoxCompilAll.Location = new Point(42, 74);
			this.checkBoxCompilAll.Name = "checkBoxCompilAll";
			this.checkBoxCompilAll.RightToLeft = RightToLeft.Yes;
			this.checkBoxCompilAll.Size = new Size(108, 17);
			this.checkBoxCompilAll.TabIndex = 1;
			this.checkBoxCompilAll.Text = "Toate problemele";
			this.toolTip1.SetToolTip(this.checkBoxCompilAll, "Selectati compilarea tuturor surselor \r\npentru toate problemele");
			this.checkBoxCompilAll.UseVisualStyleBackColor = true;
			this.checkBoxCompilAll.CheckedChanged += new EventHandler(this.checkBoxCompilAll_CheckedChanged);
			this.buttonCompil.BackColor = Color.LightGray;
			this.buttonCompil.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.buttonCompil.ForeColor = Color.Red;
			this.buttonCompil.Location = new Point(8, 19);
			this.buttonCompil.Name = "buttonCompil";
			this.buttonCompil.Size = new Size(145, 48);
			this.buttonCompil.TabIndex = 0;
			this.buttonCompil.Text = "Compileaza";
			this.buttonCompil.UseVisualStyleBackColor = false;
			this.buttonCompil.Click += new EventHandler(this.buttonCompil_Click);
			this.tabPageEval.BackColor = Color.Silver;
			this.tabPageEval.Controls.Add(this.groupBox4);
			this.tabPageEval.Controls.Add(this.groupBoxSelect);
			this.tabPageEval.Location = new Point(4, 22);
			this.tabPageEval.Name = "tabPageEval";
			this.tabPageEval.Padding = new Padding(3);
			this.tabPageEval.Size = new Size(801, 439);
			this.tabPageEval.TabIndex = 1;
			this.tabPageEval.Text = "Evaluare";
			this.tabPageEval.Enter += new EventHandler(this.tabPageEval_Enter);
			this.groupBox4.Controls.Add(this.progressBar2);
			this.groupBox4.Controls.Add(this.listViewEval);
			this.groupBox4.Controls.Add(this.labelListView);
			this.groupBox4.Location = new Point(293, 7);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new Size(504, 427);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Evaluare";
			this.progressBar2.BackColor = Color.LightGray;
			this.progressBar2.Location = new Point(7, 408);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new Size(493, 13);
			this.progressBar2.TabIndex = 2;
			this.listViewEval.BackColor = Color.LightGray;
			this.listViewEval.Columns.AddRange(new ColumnHeader[]
			{
				this.Test,
				this.Punctaj,
				this.Timp,
				this.Memorie,
				this.Mesaj
			});
			this.listViewEval.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.listViewEval.FullRowSelect = true;
			this.listViewEval.GridLines = true;
			this.listViewEval.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.listViewEval.Location = new Point(7, 43);
			this.listViewEval.Name = "listViewEval";
			this.listViewEval.OwnerDraw = true;
			this.listViewEval.Size = new Size(493, 364);
			this.listViewEval.TabIndex = 1;
			this.listViewEval.UseCompatibleStateImageBehavior = false;
			this.listViewEval.View = View.Details;
			this.listViewEval.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(this.listViewEval_DrawColumnHeader);
			this.listViewEval.DrawItem += new DrawListViewItemEventHandler(this.listViewEval_DrawItem);
			this.listViewEval.ColumnWidthChanged += new ColumnWidthChangedEventHandler(this.listViewEval_ColumnWidthChanged);
			this.listViewEval.MouseUp += new MouseEventHandler(this.listViewEval_MouseUp);
			this.listViewEval.MouseMove += new MouseEventHandler(this.listViewEval_MouseMove);
			this.listViewEval.Invalidated += new InvalidateEventHandler(this.listViewEval_Invalidated);
			this.listViewEval.DrawSubItem += new DrawListViewSubItemEventHandler(this.listViewEval_DrawSubItem);
			this.Test.Text = "Test";
			this.Test.Width = 55;
			this.Punctaj.Text = "Punctaj";
			this.Punctaj.TextAlign = HorizontalAlignment.Center;
			this.Punctaj.Width = 59;
			this.Timp.Text = "Timp";
			this.Timp.TextAlign = HorizontalAlignment.Center;
			this.Timp.Width = 63;
			this.Memorie.Text = "Memorie";
			this.Memorie.TextAlign = HorizontalAlignment.Center;
			this.Memorie.Width = 81;
			this.Mesaj.Text = "Mesaj";
			this.Mesaj.TextAlign = HorizontalAlignment.Center;
			this.Mesaj.Width = 241;
			this.labelListView.BackColor = Color.LightGray;
			this.labelListView.BorderStyle = BorderStyle.Fixed3D;
			this.labelListView.FlatStyle = FlatStyle.Flat;
			this.labelListView.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelListView.ForeColor = Color.MediumVioletRed;
			this.labelListView.Location = new Point(7, 16);
			this.labelListView.Name = "labelListView";
			this.labelListView.Size = new Size(492, 26);
			this.labelListView.TabIndex = 0;
			this.labelListView.Text = "Panou de evaluare";
			this.labelListView.TextAlign = ContentAlignment.MiddleCenter;
			this.labelListView.Click += new EventHandler(this.labelListView_Click);
			this.groupBoxSelect.BackColor = Color.Silver;
			this.groupBoxSelect.Controls.Add(this.groupBox5);
			this.groupBoxSelect.Controls.Add(this.buttonIN);
			this.groupBoxSelect.Controls.Add(this.checkedListBoxExe);
			this.groupBoxSelect.Controls.Add(this.checkBoxStepByStep);
			this.groupBoxSelect.Controls.Add(this.buttonEval);
			this.groupBoxSelect.Controls.Add(this.buttonOK);
			this.groupBoxSelect.Controls.Add(this.buttonContestant);
			this.groupBoxSelect.Controls.Add(this.label2);
			this.groupBoxSelect.Controls.Add(this.comboBoxTest);
			this.groupBoxSelect.Controls.Add(this.checkBoxAllTests);
			this.groupBoxSelect.Controls.Add(this.checkBoxAllIDs);
			this.groupBoxSelect.Controls.Add(this.comboBoxProblem);
			this.groupBoxSelect.Controls.Add(this.label1);
			this.groupBoxSelect.Controls.Add(this.checkBoxAllProblems);
			this.groupBoxSelect.Location = new Point(4, 7);
			this.groupBoxSelect.Name = "groupBoxSelect";
			this.groupBoxSelect.Size = new Size(284, 427);
			this.groupBoxSelect.TabIndex = 0;
			this.groupBoxSelect.TabStop = false;
			this.groupBoxSelect.Text = "Selectare";
			this.groupBoxSelect.Enter += new EventHandler(this.groupBoxSelect_Enter);
			this.groupBox5.Controls.Add(this.labelSource);
			this.groupBox5.Controls.Add(this.labelStackLimit);
			this.groupBox5.Controls.Add(this.labelMem);
			this.groupBox5.Controls.Add(this.labelTime);
			this.groupBox5.Controls.Add(this.labelProb);
			this.groupBox5.Location = new Point(6, 115);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new Size(137, 114);
			this.groupBox5.TabIndex = 15;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Informatii";
			this.labelSource.BorderStyle = BorderStyle.Fixed3D;
			this.labelSource.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelSource.Location = new Point(6, 89);
			this.labelSource.Name = "labelSource";
			this.labelSource.Size = new Size(125, 19);
			this.labelSource.TabIndex = 4;
			this.labelSource.Text = "Dim. sursa:";
			this.labelStackLimit.BorderStyle = BorderStyle.Fixed3D;
			this.labelStackLimit.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelStackLimit.Location = new Point(6, 70);
			this.labelStackLimit.Name = "labelStackLimit";
			this.labelStackLimit.Size = new Size(125, 18);
			this.labelStackLimit.TabIndex = 3;
			this.labelStackLimit.Text = "Stiva:";
			this.labelMem.BorderStyle = BorderStyle.Fixed3D;
			this.labelMem.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelMem.Location = new Point(6, 53);
			this.labelMem.Name = "labelMem";
			this.labelMem.Size = new Size(125, 16);
			this.labelMem.TabIndex = 2;
			this.labelMem.Text = "Memorie:";
			this.labelTime.BorderStyle = BorderStyle.Fixed3D;
			this.labelTime.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelTime.Location = new Point(6, 35);
			this.labelTime.Name = "labelTime";
			this.labelTime.Size = new Size(125, 17);
			this.labelTime.TabIndex = 1;
			this.labelTime.Text = "Timp:";
			this.labelProb.BorderStyle = BorderStyle.Fixed3D;
			this.labelProb.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelProb.Location = new Point(6, 15);
			this.labelProb.Name = "labelProb";
			this.labelProb.Size = new Size(125, 19);
			this.labelProb.TabIndex = 0;
			this.labelProb.Text = "Problema:";
			this.buttonIN.BackColor = Color.LightGray;
			this.buttonIN.ForeColor = Color.Black;
			this.buttonIN.Location = new Point(4, 234);
			this.buttonIN.Name = "buttonIN";
			this.buttonIN.Size = new Size(139, 32);
			this.buttonIN.TabIndex = 14;
			this.buttonIN.Text = "Fisier IN";
			this.toolTip1.SetToolTip(this.buttonIN, "Vizualizati fisierul de intrare\r\nla evaluarea \"Test cu Test\"");
			this.buttonIN.UseVisualStyleBackColor = false;
			this.buttonIN.Click += new EventHandler(this.buttonIN_Click);
			this.checkedListBoxExe.BackColor = Color.LightGray;
			this.checkedListBoxExe.CheckOnClick = true;
			this.checkedListBoxExe.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.checkedListBoxExe.ForeColor = Color.MediumVioletRed;
			this.checkedListBoxExe.FormattingEnabled = true;
			this.checkedListBoxExe.Location = new Point(152, 42);
			this.checkedListBoxExe.Name = "checkedListBoxExe";
			this.checkedListBoxExe.Size = new Size(126, 379);
			this.checkedListBoxExe.TabIndex = 4;
			this.checkedListBoxExe.SelectedIndexChanged += new EventHandler(this.checkedListBoxExe_SelectedIndexChanged);
			this.checkedListBoxExe.ItemCheck += new ItemCheckEventHandler(this.checkedListBoxExe_ItemCheck);
			this.checkBoxStepByStep.AutoSize = true;
			this.checkBoxStepByStep.Location = new Point(61, 391);
			this.checkBoxStepByStep.Name = "checkBoxStepByStep";
			this.checkBoxStepByStep.RightToLeft = RightToLeft.Yes;
			this.checkBoxStepByStep.Size = new Size(82, 17);
			this.checkBoxStepByStep.TabIndex = 11;
			this.checkBoxStepByStep.Text = "Test cu test";
			this.toolTip2.SetToolTip(this.checkBoxStepByStep, "Selectati evaluarea \"Test cu Test\"\r\na unei singure surse\r\n");
			this.checkBoxStepByStep.UseVisualStyleBackColor = true;
			this.checkBoxStepByStep.CheckedChanged += new EventHandler(this.checkBoxStepByStep_CheckedChanged);
			this.buttonEval.BackColor = Color.LightGray;
			this.buttonEval.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.buttonEval.ForeColor = Color.Red;
			this.buttonEval.Location = new Point(4, 339);
			this.buttonEval.Name = "buttonEval";
			this.buttonEval.Size = new Size(140, 48);
			this.buttonEval.TabIndex = 10;
			this.buttonEval.Text = "Evalueaza";
			this.buttonEval.UseVisualStyleBackColor = false;
			this.buttonEval.Click += new EventHandler(this.buttonEval_Click);
			this.buttonOK.BackColor = Color.LightGray;
			this.buttonOK.Location = new Point(4, 300);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new Size(139, 32);
			this.buttonOK.TabIndex = 9;
			this.buttonOK.Text = "Fisier OK";
			this.toolTip1.SetToolTip(this.buttonOK, "Vizualizati fisierul de iesire corect\r\nla evaluarea \"Test cu Test\"");
			this.buttonOK.UseVisualStyleBackColor = false;
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
			this.buttonContestant.BackColor = Color.LightGray;
			this.buttonContestant.Location = new Point(4, 267);
			this.buttonContestant.Name = "buttonContestant";
			this.buttonContestant.Size = new Size(139, 32);
			this.buttonContestant.TabIndex = 8;
			this.buttonContestant.Text = "Fisier OUT";
			this.toolTip1.SetToolTip(this.buttonContestant, "Vizualizati fisierul de iesire produs\r\nla evaluarea \"Test cu Test\"\r\n");
			this.buttonContestant.UseVisualStyleBackColor = false;
			this.buttonContestant.Click += new EventHandler(this.buttonContestant_Click);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(3, 71);
			this.label2.Name = "label2";
			this.label2.Size = new Size(36, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Testul";
			this.comboBoxTest.BackColor = Color.Gainsboro;
			this.comboBoxTest.FormattingEnabled = true;
			this.comboBoxTest.Location = new Point(6, 88);
			this.comboBoxTest.Name = "comboBoxTest";
			this.comboBoxTest.Size = new Size(137, 21);
			this.comboBoxTest.TabIndex = 6;
			this.toolTip1.SetToolTip(this.comboBoxTest, "Selectati un singur test \r\npentru evaluare");
			this.comboBoxTest.SelectedIndexChanged += new EventHandler(this.comboBoxTest_SelectedIndexChanged);
			this.checkBoxAllTests.AutoSize = true;
			this.checkBoxAllTests.Location = new Point(89, 69);
			this.checkBoxAllTests.Name = "checkBoxAllTests";
			this.checkBoxAllTests.RightToLeft = RightToLeft.Yes;
			this.checkBoxAllTests.Size = new Size(54, 17);
			this.checkBoxAllTests.TabIndex = 5;
			this.checkBoxAllTests.Text = "Toate";
			this.toolTip1.SetToolTip(this.checkBoxAllTests, "Selectati toate testele problemei");
			this.checkBoxAllTests.UseVisualStyleBackColor = true;
			this.checkBoxAllTests.CheckedChanged += new EventHandler(this.checkBoxAllTests_CheckedChanged);
			this.checkBoxAllIDs.AutoSize = true;
			this.checkBoxAllIDs.Location = new Point(224, 23);
			this.checkBoxAllIDs.Name = "checkBoxAllIDs";
			this.checkBoxAllIDs.RightToLeft = RightToLeft.Yes;
			this.checkBoxAllIDs.Size = new Size(54, 17);
			this.checkBoxAllIDs.TabIndex = 3;
			this.checkBoxAllIDs.Text = "Toate";
			this.toolTip1.SetToolTip(this.checkBoxAllIDs, "Selectati pentru evaluare toate \r\nexecutabilele problemei ");
			this.checkBoxAllIDs.UseVisualStyleBackColor = true;
			this.checkBoxAllIDs.CheckedChanged += new EventHandler(this.checkBoxAllIDs_CheckedChanged);
			this.comboBoxProblem.BackColor = Color.Gainsboro;
			this.comboBoxProblem.FormattingEnabled = true;
			this.comboBoxProblem.Location = new Point(6, 42);
			this.comboBoxProblem.Name = "comboBoxProblem";
			this.comboBoxProblem.Size = new Size(137, 21);
			this.comboBoxProblem.TabIndex = 2;
			this.toolTip1.SetToolTip(this.comboBoxProblem, "Selectati problema pentru care doriti\r\nsa evaluati fisierele executabile");
			this.comboBoxProblem.SelectedIndexChanged += new EventHandler(this.comboBoxProblem_SelectedIndexChanged);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 23);
			this.label1.Name = "label1";
			this.label1.Size = new Size(54, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Problema:";
			this.checkBoxAllProblems.AutoSize = true;
			this.checkBoxAllProblems.Location = new Point(35, 407);
			this.checkBoxAllProblems.Name = "checkBoxAllProblems";
			this.checkBoxAllProblems.RightToLeft = RightToLeft.Yes;
			this.checkBoxAllProblems.Size = new Size(108, 17);
			this.checkBoxAllProblems.TabIndex = 0;
			this.checkBoxAllProblems.Text = "Toate problemele";
			this.toolTipEvalAll.SetToolTip(this.checkBoxAllProblems, "Selectati evaluarea tuturor surselor\r\npentru toate problemele");
			this.checkBoxAllProblems.UseVisualStyleBackColor = true;
			this.checkBoxAllProblems.CheckedChanged += new EventHandler(this.checkBoxAllProblems_CheckedChanged);
			this.toolTip1.ShowAlways = true;
			this.foldCompil.RootFolder = Environment.SpecialFolder.MyComputer;
			this.foldCompil.SelectedPath = "F:\\Final\\Evaluator\\Evaluator\\rezultate\\compilare";
			this.toolTipEvalAll.ShowAlways = true;
			this.toolTip2.ShowAlways = true;
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			this.backgroundWorker2.WorkerReportsProgress = true;
			this.backgroundWorker2.WorkerSupportsCancellation = true;
			this.backgroundWorker2.DoWork += new DoWorkEventHandler(this.backgroundWorker2_DoWork);
			this.backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
			this.backgroundWorker2.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Silver;
			base.ClientSize = new Size(809, 489);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.menuStrip1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.MaximizeBox = false;
			base.Name = "Eval";
			this.Text = "Evaluator";
			base.Load += new EventHandler(this.Eval_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPageCompil.ResumeLayout(false);
			this.tabPageCompil.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.contextMenuStripOutputTest.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.contextMenuStripViewInputTest.ResumeLayout(false);
			this.groupBoxExecutabile.ResumeLayout(false);
			this.groupBoxExecutabile.PerformLayout();
			this.contextMenuStripDeleteExe.ResumeLayout(false);
			this.groupBoxSurse.ResumeLayout(false);
			this.groupBoxSurse.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBoxCompilProb.ResumeLayout(false);
			this.groupBoxCompilare.ResumeLayout(false);
			this.groupBoxCompilare.PerformLayout();
			this.tabPageEval.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBoxSelect.ResumeLayout(false);
			this.groupBoxSelect.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
