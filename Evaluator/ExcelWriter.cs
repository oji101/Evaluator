using System;
using System.IO;
using System.Text;

namespace Evaluator
{
	internal class ExcelWriter
	{
		private Stream stream;

		private BinaryWriter writer;

		private ushort[] clBegin = new ushort[]
		{
			2057,
			8,
			0,
			16,
			0,
			0
		};

		private ushort[] clEnd;

		private void WriteUshortArray(ushort[] value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				try
				{
					this.writer.Write(value[i]);
				}
				catch (Exception)
				{
				}
			}
		}

		public ExcelWriter(Stream stream)
		{
			ushort[] array = new ushort[2];
			array[0] = 10;
			this.clEnd = array;
			base..ctor();
			this.stream = stream;
			try
			{
				this.writer = new BinaryWriter(stream);
			}
			catch (Exception)
			{
			}
		}

		public void WriteCell(int row, int col, string value)
		{
			ushort[] array = new ushort[6];
			array[0] = 516;
			ushort[] array2 = array;
			int length = value.Length;
			try
			{
				byte[] bytes = Encoding.ASCII.GetBytes(value);
				array2[1] = (ushort)(8 + length);
				array2[2] = (ushort)row;
				array2[3] = (ushort)col;
				array2[5] = (ushort)length;
				this.WriteUshortArray(array2);
				this.writer.Write(bytes);
			}
			catch (Exception)
			{
			}
		}

		public void WriteCell(int row, int col, int value)
		{
			ushort[] array = new ushort[5];
			array[0] = 638;
			array[1] = 10;
			ushort[] array2 = array;
			array2[2] = (ushort)row;
			array2[3] = (ushort)col;
			this.WriteUshortArray(array2);
			int value2 = value << 2 | 2;
			try
			{
				this.writer.Write(value2);
			}
			catch (Exception)
			{
			}
		}

		public void WriteCell(int row, int col, double value)
		{
			ushort[] array = new ushort[5];
			array[0] = 515;
			array[1] = 14;
			ushort[] array2 = array;
			array2[2] = (ushort)row;
			array2[3] = (ushort)col;
			this.WriteUshortArray(array2);
			try
			{
				this.writer.Write(value);
			}
			catch (Exception)
			{
			}
		}

		public void WriteCell(int row, int col)
		{
			ushort[] array = new ushort[]
			{
				513,
				6,
				0,
				0,
				23
			};
			array[2] = (ushort)row;
			array[3] = (ushort)col;
			this.WriteUshortArray(array);
		}

		public void BeginWrite()
		{
			this.WriteUshortArray(this.clBegin);
		}

		public void EndWrite()
		{
			this.WriteUshortArray(this.clEnd);
			this.writer.Flush();
		}
	}
}
