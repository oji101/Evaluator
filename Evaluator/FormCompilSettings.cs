using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Evaluator
{
	public class FormCompilSettings : Form
	{
		private static RegistryPermission readPerm1 = new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE");

		private IContainer components;

		private TextBox textBoxGPP;

		private TextBox textBoxGCC;

		private TextBox textBoxPAS;

		private Label label3;

		private Label label2;

		private Label label1;

		private Button btnBrowsePas;

		private Button btnbrowseCPP;

		private Button btnBrowseC;

		private Button btnOK;

		private Button btnCancel;

		private GroupBox groupBox1;

		private OpenFileDialog openFileDialog1;

		private Button btnDetect;

		private ToolTip toolTip1;

		public string GCC
		{
			get
			{
				return this.textBoxGCC.Text;
			}
		}

		public string GPP
		{
			get
			{
				return this.textBoxGPP.Text;
			}
		}

		public string PAS
		{
			get
			{
				return this.textBoxPAS.Text;
			}
		}

		public FormCompilSettings(Eval f)
		{
			this.InitializeComponent();
			this.textBoxGCC.Text = f.GCC;
			this.textBoxGPP.Text = f.GPP;
			this.textBoxPAS.Text = f.PAS;
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
				text = text + " " + ((string.IsNullOrEmpty(text2) || string.Compare(text2, 0, "x86", 0, 3, true) == 0) ? 32 : 64).ToString();
			}
			return text;
		}

		private string GetInstalledSoftware()
		{
			string text = null;
			string oSInfo = this.GetOSInfo();
			string name = null;
			if (oSInfo.Substring(oSInfo.Length - 2, 2) == "64")
			{
				name = "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
			}
			else
			{
				name = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				string name2 = "FreePascal_is1";
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(name2))
				{
					try
					{
						if (registryKey2.GetValue("DisplayName") != null)
						{
							if (registryKey2.GetValue("InstallLocation") == null)
							{
								this.textBoxPAS.Text = "";
							}
							else
							{
								text = registryKey2.GetValue("InstallLocation") + "bin\\i386-win32\\";
								this.textBoxPAS.Text = text + "fpc.exe";
							}
						}
					}
					catch (Exception)
					{
						this.textBoxPAS.Text = "";
					}
				}
			}
			text = "";
			using (RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey(name))
			{
				string name3 = "MinGW Developer Studio";
				using (RegistryKey registryKey4 = registryKey3.OpenSubKey(name3))
				{
					try
					{
						text = registryKey4.GetValue("UninstallString").ToString();
						text = text.Substring(0, text.Length - 13) + "MinGW\\bin\\";
						this.textBoxGPP.Text = text + "g++.exe";
						this.textBoxGCC.Text = text + "gcc.exe";
					}
					catch (Exception)
					{
						this.textBoxGPP.Text = "";
						this.textBoxGCC.Text = "";
					}
				}
			}
			return text;
		}

		private void FormCompilSettings_Load(object sender, EventArgs e)
		{
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnOK.DialogResult = DialogResult.OK;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnBrowseC_Click(object sender, EventArgs e)
		{
			try
			{
				this.openFileDialog1.Title = "Selecteaza compilator C";
				this.openFileDialog1.Filter = "Fisiere EXE (*.exe) |*.exe";
				this.openFileDialog1.InitialDirectory = "C:";
				this.openFileDialog1.CheckFileExists = true;
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					this.textBoxGCC.Text = this.openFileDialog1.FileName;
				}
			}
			catch (Exception)
			{
			}
		}

		private void btnbrowseCPP_Click(object sender, EventArgs e)
		{
			try
			{
				this.openFileDialog1.Title = "Selecteaza compilator C++";
				this.openFileDialog1.Filter = "Fisiere EXE (*.exe) |*.exe";
				this.openFileDialog1.InitialDirectory = "C:";
				this.openFileDialog1.CheckFileExists = true;
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					this.textBoxGPP.Text = this.openFileDialog1.FileName;
				}
			}
			catch (Exception)
			{
			}
		}

		private void btnBrowsePas_Click(object sender, EventArgs e)
		{
			try
			{
				this.openFileDialog1.Title = "Selecteaza compilator Free Pascal";
				this.openFileDialog1.Filter = "Fisiere EXE (*.exe) |*.exe";
				this.openFileDialog1.InitialDirectory = "C:";
				this.openFileDialog1.CheckFileExists = true;
				this.openFileDialog1.FileName = "";
				if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					this.textBoxPAS.Text = this.openFileDialog1.FileName;
				}
			}
			catch (Exception)
			{
			}
		}

		private void btnDetect_Click(object sender, EventArgs e)
		{
			this.GetInstalledSoftware();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormCompilSettings));
			this.btnBrowsePas = new Button();
			this.btnbrowseCPP = new Button();
			this.btnBrowseC = new Button();
			this.label3 = new Label();
			this.label2 = new Label();
			this.label1 = new Label();
			this.textBoxPAS = new TextBox();
			this.textBoxGPP = new TextBox();
			this.textBoxGCC = new TextBox();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.groupBox1 = new GroupBox();
			this.btnDetect = new Button();
			this.openFileDialog1 = new OpenFileDialog();
			this.toolTip1 = new ToolTip(this.components);
			this.groupBox1.SuspendLayout();
			base.SuspendLayout();
			this.btnBrowsePas.BackColor = Color.LightGray;
			this.btnBrowsePas.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btnBrowsePas.Location = new Point(278, 71);
			this.btnBrowsePas.Name = "btnBrowsePas";
			this.btnBrowsePas.Size = new Size(37, 23);
			this.btnBrowsePas.TabIndex = 8;
			this.btnBrowsePas.Text = "...";
			this.toolTip1.SetToolTip(this.btnBrowsePas, "Alege alt compilator fpc");
			this.btnBrowsePas.UseVisualStyleBackColor = false;
			this.btnBrowsePas.Click += new EventHandler(this.btnBrowsePas_Click);
			this.btnbrowseCPP.BackColor = Color.LightGray;
			this.btnbrowseCPP.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btnbrowseCPP.Location = new Point(278, 44);
			this.btnbrowseCPP.Name = "btnbrowseCPP";
			this.btnbrowseCPP.Size = new Size(37, 23);
			this.btnbrowseCPP.TabIndex = 7;
			this.btnbrowseCPP.Text = "...";
			this.toolTip1.SetToolTip(this.btnbrowseCPP, "Alege alt compilator g++");
			this.btnbrowseCPP.UseVisualStyleBackColor = false;
			this.btnbrowseCPP.Click += new EventHandler(this.btnbrowseCPP_Click);
			this.btnBrowseC.BackColor = Color.LightGray;
			this.btnBrowseC.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btnBrowseC.Location = new Point(278, 19);
			this.btnBrowseC.Name = "btnBrowseC";
			this.btnBrowseC.Size = new Size(37, 23);
			this.btnBrowseC.TabIndex = 6;
			this.btnBrowseC.Text = "...";
			this.btnBrowseC.TextAlign = ContentAlignment.TopCenter;
			this.toolTip1.SetToolTip(this.btnBrowseC, "Alege alt compilator gcc");
			this.btnBrowseC.UseVisualStyleBackColor = false;
			this.btnBrowseC.Click += new EventHandler(this.btnBrowseC_Click);
			this.label3.AutoSize = true;
			this.label3.Location = new Point(6, 76);
			this.label3.Name = "label3";
			this.label3.Size = new Size(39, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Pascal";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(6, 46);
			this.label2.Name = "label2";
			this.label2.Size = new Size(26, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "C++";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(6, 23);
			this.label1.Name = "label1";
			this.label1.Size = new Size(14, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "C";
			this.textBoxPAS.BackColor = Color.Gainsboro;
			this.textBoxPAS.Location = new Point(47, 73);
			this.textBoxPAS.Name = "textBoxPAS";
			this.textBoxPAS.Size = new Size(226, 20);
			this.textBoxPAS.TabIndex = 2;
			this.textBoxGPP.BackColor = Color.Gainsboro;
			this.textBoxGPP.Location = new Point(47, 46);
			this.textBoxGPP.Name = "textBoxGPP";
			this.textBoxGPP.Size = new Size(225, 20);
			this.textBoxGPP.TabIndex = 1;
			this.textBoxGCC.BackColor = Color.Gainsboro;
			this.textBoxGCC.Location = new Point(47, 20);
			this.textBoxGCC.Name = "textBoxGCC";
			this.textBoxGCC.Size = new Size(225, 20);
			this.textBoxGCC.TabIndex = 0;
			this.btnOK.BackColor = Color.LightGray;
			this.btnOK.Location = new Point(240, 100);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.BackColor = Color.LightGray;
			this.btnCancel.Location = new Point(159, 100);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Renunta";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.groupBox1.Controls.Add(this.btnDetect);
			this.groupBox1.Controls.Add(this.btnCancel);
			this.groupBox1.Controls.Add(this.btnBrowsePas);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.btnBrowseC);
			this.groupBox1.Controls.Add(this.btnOK);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.btnbrowseCPP);
			this.groupBox1.Controls.Add(this.textBoxGCC);
			this.groupBox1.Controls.Add(this.textBoxPAS);
			this.groupBox1.Controls.Add(this.textBoxGPP);
			this.groupBox1.FlatStyle = FlatStyle.Popup;
			this.groupBox1.Location = new Point(10, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new Size(326, 132);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.btnDetect.BackColor = Color.LightGray;
			this.btnDetect.Location = new Point(47, 100);
			this.btnDetect.Name = "btnDetect";
			this.btnDetect.Size = new Size(75, 23);
			this.btnDetect.TabIndex = 9;
			this.btnDetect.Text = "Detect";
			this.toolTip1.SetToolTip(this.btnDetect, "Detecteaza caile spre compilatoarele\r\n MinGW Developer Studio si Free Pascal");
			this.btnDetect.UseVisualStyleBackColor = false;
			this.btnDetect.Click += new EventHandler(this.btnDetect_Click);
			this.openFileDialog1.FileName = "openFileDialog1";
			this.toolTip1.ShowAlways = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.Silver;
			base.ClientSize = new Size(347, 145);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormCompilSettings";
			this.Text = "Compilatoare";
			base.Load += new EventHandler(this.FormCompilSettings_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
