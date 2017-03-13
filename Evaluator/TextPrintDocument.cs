using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace Evaluator
{
	internal class TextPrintDocument : PrintDocument
	{
		private Font printFont;

		private TextReader printStream;

		private string fileToPrint;

		public bool Watermark;

		public string FileToPrint
		{
			get
			{
				return this.fileToPrint;
			}
			set
			{
				if (File.Exists(value))
				{
					this.fileToPrint = value;
					base.DocumentName = value;
					return;
				}
				throw new Exception("Nu s-a gasit fisierul!.");
			}
		}

		public Font Font
		{
			get
			{
				return this.printFont;
			}
			set
			{
				this.printFont = value;
			}
		}

		public TextPrintDocument()
		{
		}

		public TextPrintDocument(string fileName) : this()
		{
			this.FileToPrint = fileName;
		}

		protected override void OnBeginPrint(PrintEventArgs e)
		{
			base.OnBeginPrint(e);
			this.printFont = new Font("Verdana", 10f);
			this.printStream = new StreamReader(this.fileToPrint);
		}

		protected override void OnEndPrint(PrintEventArgs e)
		{
			base.OnEndPrint(e);
			this.printFont.Dispose();
			this.printStream.Close();
		}

		protected override void OnPrintPage(PrintPageEventArgs e)
		{
			base.OnPrintPage(e);
			Graphics graphics = e.Graphics;
			float x = (float)e.MarginBounds.Left;
			float num = (float)e.MarginBounds.Top;
			float height = this.printFont.GetHeight(graphics);
			float num2 = (float)e.MarginBounds.Height / height;
			int num3 = 0;
			string text = null;
			while ((float)num3 < num2 && (text = this.printStream.ReadLine()) != null)
			{
				graphics.DrawString(text, this.printFont, Brushes.Black, x, num + (float)num3++ * height);
			}
			if (text != null)
			{
				e.HasMorePages = true;
				return;
			}
			e.HasMorePages = false;
		}
	}
}
