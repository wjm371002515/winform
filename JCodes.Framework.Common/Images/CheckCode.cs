using System;
using System.IO;
using System.Drawing;
using System.IO.Compression;
using System.Collections.Generic;

namespace JCodes.Framework.Common.Images
{
    /// <summary>
    /// 读取验证码的图片处理辅助类。
    /// 本辅助类来源于读取铁道部12306网站的验证码的抢票助手。
    /// </summary>
	public class CheckCode
	{
		private class CharInfo
		{
			public char Char
			{
				get;
				private set;
			}
			public bool[,] Table
			{
				get;
				private set;
			}
			public CharInfo(char ch, bool[,] table)
			{
				this.Char = ch;
				this.Table = table;
			}
		}
		private class MatchedChar
		{
			public int X
			{
				get;
				set;
			}
			public int Y
			{
				get;
				set;
			}
			public char Char
			{
				get;
				set;
			}
			public double Rate
			{
				get;
				set;
			}
		}
		private List<CheckCode.CharInfo> words_ = new List<CheckCode.CharInfo>();
		private static CheckCode _checkCode;
		private CheckCode()
		{
			byte[] buffer = new byte[]
			{
				31,
				139,
				8,
				0,
				0,
				0,
				0,
				0,
				4,
				0,
				197,
				88,
				217,
				146,
				19,
				49,
				12,
				148,
				158,
				147,
				12,
				97,
				151,
				47,
				225,
				88,
				224,
				145,
				155,
				130,
				98,
				11,
				88,
				238,
				255,
				255,
				16,
				216,
				204,
				200,
				234,
				150,
				108,
				143,
				19,
				72,
				225,
				170,
				77,
				70,
				150,
				109,
				181,
				142,
				150,
				103,
				115,
				127,
				59,
				9,
				14,
				37,
				65,
				73,
				163,
				174,
				215,
				91,
				169,
				168,
				213,
				180,
				118,
				2,
				106,
				92,
				82,
				148,
				84,
				237,
				24,
				90,
				127,
				24,
				0,
				0,
				132,
				7,
				27,
				128,
				74,
				154,
				8,
				53,
				184,
				129,
				80,
				231,
				173,
				190,
				196,
				142,
				177,
				79,
				45,
				95,
				186,
				128,
				187,
				253,
				154,
				173,
				25,
				54,
				229,
				173,
				135,
				241,
				16,
				192,
				141,
				198,
				80,
				64,
				82,
				248,
				179,
				152,
				44,
				214,
				236,
				89,
				231,
				13,
				62,
				15,
				147,
				62,
				29,
				2,
				122,
				24,
				143,
				182,
				199,
				70,
				78,
				1,
				163,
				150,
				220,
				58,
				32,
				119,
				191,
				44,
				36,
				228,
				128,
				169,
				32,
				20,
				229,
				45,
				181,
				104,
				201,
				85,
				137,
				35,
				150,
				130,
				170,
				186,
				88,
				166,
				3,
				56,
				113,
				75,
				41,
				210,
				71,
				128,
				227,
				132,
				145,
				244,
				120,
				67,
				100,
				65,
				123,
				115,
				153,
				128,
				66,
				72,
				0,
				222,
				0,
				18,
				136,
				128,
				219,
				81,
				74,
				73,
				132,
				67,
				246,
				81,
				144,
				39,
				33,
				201,
				248,
				172,
				0,
				77,
				205,
				70,
				9,
				157,
				21,
				120,
				224,
				0,
				30,
				68,
				42,
				81,
				140,
				188,
				211,
				163,
				104,
				138,
				213,
				58,
				32,
				121,
				186,
				77,
				113,
				76,
				11,
				145,
				152,
				144,
				123,
				42,
				66,
				197,
				120,
				122,
				252,
				213,
				27,
				75,
				9,
				167,
				39,
				153,
				56,
				5,
				1,
				194,
				128,
				57,
				156,
				103,
				187,
				78,
				127,
				108,
				51,
				221,
				237,
				135,
				85,
				218,
				93,
				181,
				86,
				51,
				198,
				249,
				234,
				96,
				100,
				207,
				167,
				65,
				224,
				92,
				28,
				196,
				178,
				37,
				163,
				137,
				136,
				141,
				22,
				0,
				181,
				237,
				165,
				34,
				157,
				82,
				65,
				83,
				141,
				146,
				127,
				49,
				81,
				63,
				168,
				0,
				133,
				138,
				113,
				16,
				146,
				120,
				196,
				89,
				8,
				57,
				105,
				169,
				56,
				65,
				72,
				247,
				64,
				90,
				3,
				213,
				58,
				245,
				229,
				157,
				51,
				102,
				195,
				215,
				31,
				239,
				148,
				160,
				83,
				234,
				244,
				21,
				178,
				28,
				64,
				45,
				207,
				175,
				206,
				233,
				212,
				122,
				137,
				9,
				230,
				221,
				219,
				14,
				184,
				88,
				167,
				96,
				55,
				253,
				242,
				250,
				44,
				78,
				81,
				135,
				13,
				252,
				22,
				114,
				42,
				95,
				192,
				128,
				240,
				84,
				167,
				222,
				252,
				21,
				139,
				154,
				54,
				58,
				44,
				98,
				252,
				212,
				140,
				49,
				183,
				234,
				215,
				38,
				196,
				175,
				117,
				234,
				219,
				139,
				255,
				155,
				155,
				80,
				126,
				254,
				21,
				171,
				23,
				47,
				150,
				150,
				189,
				170,
				135,
				221,
				119,
				163,
				119,
				211,
				133,
				240,
				224,
				88,
				213,
				246,
				140,
				205,
				196,
				99,
				82,
				18,
				72,
				70,
				15,
				147,
				90,
				227,
				234,
				36,
				103,
				115,
				99,
				160,
				223,
				223,
				61,
				103,
				246,
				169,
				252,
				237,
				8,
				227,
				130,
				87,
				8,
				53,
				71,
				104,
				156,
				1,
				64,
				135,
				139,
				189,
				12,
				179,
				244,
				225,
				114,
				215,
				84,
				98,
				253,
				64,
				237,
				153,
				166,
				126,
				43,
				228,
				180,
				196,
				98,
				13,
				121,
				174,
				27,
				215,
				244,
				9,
				183,
				225,
				124,
				68,
				9,
				154,
				218,
				255,
				82,
				106,
				60,
				225,
				200,
				215,
				189,
				187,
				190,
				55,
				252,
				214,
				213,
				78,
				60,
				64,
				42,
				75,
				57,
				26,
				189,
				42,
				205,
				193,
				24,
				89,
				64,
				98,
				120,
				236,
				99,
				25,
				114,
				240,
				207,
				248,
				56,
				250,
				66,
				58,
				200,
				2,
				236,
				91,
				235,
				141,
				174,
				241,
				69,
				221,
				50,
				152,
				53,
				60,
				159,
				166,
				61,
				206,
				19,
				206,
				148,
				56,
				135,
				0,
				141,
				133,
				196,
				112,
				23,
				38,
				14,
				166,
				30,
				22,
				203,
				191,
				82,
				223,
				41,
				99,
				196,
				246,
				140,
				53,
				186,
				242,
				249,
				31,
				191,
				115,
				31,
				145,
				27,
				158,
				36,
				94,
				99,
				34,
				130,
				35,
				5,
				25,
				185,
				113,
				115,
				220,
				207,
				5,
				136,
				148,
				113,
				219,
				221,
				72,
				16,
				213,
				85,
				179,
				82,
				195,
				27,
				1,
				148,
				19,
				116,
				148,
				58,
				128,
				47,
				57,
				226,
				117,
				14,
				242,
				198,
				24,
				220,
				70,
				252,
				243,
				234,
				20,
				128,
				193,
				206,
				36,
				238,
				114,
				237,
				148,
				175,
				251,
				169,
				170,
				74,
				224,
				212,
				34,
				198,
				240,
				87,
				29,
				142,
				210,
				144,
				198,
				12,
				211,
				154,
				83,
				251,
				214,
				183,
				221,
				20,
				212,
				189,
				65,
				167,
				128,
				123,
				35,
				254,
				52,
				86,
				13,
				150,
				70,
				2,
				254,
				253,
				178,
				0,
				95,
				1,
				156,
				160,
				50,
				57,
				215,
				144,
				194,
				108,
				199,
				78,
				104,
				136,
				125,
				159,
				155,
				207,
				167,
				190,
				160,
				252,
				24,
				125,
				7,
				91,
				169,
				190,
				86,
				31,
				103,
				26,
				74,
				145,
				156,
				4,
				56,
				83,
				107,
				112,
				104,
				143,
				234,
				244,
				52,
				135,
				127,
				110,
				130,
				195,
				193,
				171,
				64,
				196,
				80,
				19,
				14,
				51,
				93,
				103,
				125,
				1,
				31,
				219,
				192,
				127,
				237,
				135,
				127,
				188,
				15,
				117,
				224,
				165,
				186,
				192,
				132,
				61,
				36,
				4,
				224,
				241,
				22,
				65,
				59,
				116,
				210,
				82,
				197,
				248,
				124,
				18,
				251,
				228,
				55,
				91,
				251,
				87,
				17,
				161,
				24,
				0,
				0
			};
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
				{
					using (BinaryReader binaryReader = new BinaryReader(gZipStream))
					{
						while (true)
						{
							char c = binaryReader.ReadChar();
							if (c == '\0')
							{
								break;
							}
							int num = (int)binaryReader.ReadByte();
							int num2 = (int)binaryReader.ReadByte();
							bool[,] array = new bool[num, num2];
							for (int i = 0; i < num; i++)
							{
								for (int j = 0; j < num2; j++)
								{
									array[i, j] = binaryReader.ReadBoolean();
								}
							}
							this.words_.Add(new CheckCode.CharInfo(c, array));
						}
					}
				}
			}
		}

        /// <summary>
        /// 读取Bitmap图片，获取里面的验证码字符串
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
		public static string Read(Bitmap bmp)
		{
			if (CheckCode._checkCode == null)
			{
				CheckCode._checkCode = new CheckCode();
			}
			return CheckCode._checkCode.ReadImg(bmp);
		}

		private string ReadImg(Bitmap bmp)
		{
			string text = string.Empty;
			int width = bmp.Width;
			int arg_13_0 = bmp.Height;
			bool[,] array = this.ToTable(bmp);
			int i = this.SearchNext(array, -1);
			while (i < width - 7)
			{
				CheckCode.MatchedChar matchedChar = this.Match(array, i);
				if (matchedChar.Rate > 0.6)
				{
					text += matchedChar.Char;
					i = matchedChar.X + 10;
				}
				else
				{
					i++;
				}
			}
			return text;
		}
		private bool[,] ToTable(Bitmap bmp)
		{
			bool[,] array = new bool[bmp.Width, bmp.Height];
			for (int i = 0; i < bmp.Width; i++)
			{
				for (int j = 0; j < bmp.Height; j++)
				{
					Color pixel = bmp.GetPixel(i, j);
					array[i, j] = ((int)(pixel.R + pixel.G + pixel.B) < 500);
				}
			}
			return array;
		}
		private int SearchNext(bool[,] table, int start)
		{
			int length = table.GetLength(0);
			int length2 = table.GetLength(1);
			for (start++; start < length; start++)
			{
				for (int i = 0; i < length2; i++)
				{
					if (table[start, i])
					{
						return start;
					}
				}
			}
			return start;
		}
		private double FixedMatch(bool[,] source, bool[,] target, int x0, int y0)
		{
			double num = 0.0;
			double num2 = 0.0;
			int length = target.GetLength(0);
			int length2 = target.GetLength(1);
			int length3 = source.GetLength(0);
			int length4 = source.GetLength(1);
			for (int i = 0; i < length; i++)
			{
				int num3 = i + x0;
				if (num3 >= 0 && num3 < length3)
				{
					for (int j = 0; j < length2; j++)
					{
						int num4 = j + y0;
						if (num4 >= 0 && num4 < length4)
						{
							if (target[i, j])
							{
								num += 1.0;
								if (source[num3, num4])
								{
									num2 += 1.0;
								}
								else
								{
									num2 -= 1.0;
								}
							}
							else
							{
								if (source[num3, num4])
								{
									num2 -= 0.55;
								}
							}
						}
					}
				}
			}
			return num2 / num;
		}
		private CheckCode.MatchedChar ScopeMatch(bool[,] source, bool[,] target, int start)
		{
			target.GetLength(0);
			int length = target.GetLength(1);
			source.GetLength(0);
			int length2 = source.GetLength(1);
			double num = 0.0;
			CheckCode.MatchedChar matchedChar = new CheckCode.MatchedChar();
			for (int i = -2; i < 6; i++)
			{
				for (int j = -3; j < length2 - length + 5; j++)
				{
					double num2 = this.FixedMatch(source, target, i + start, j);
					if (num2 > num)
					{
						num = num2;
						matchedChar.X = i + start;
						matchedChar.Y = j;
						matchedChar.Rate = num2;
					}
				}
			}
			return matchedChar;
		}
		private CheckCode.MatchedChar Match(bool[,] source, int start)
		{
			CheckCode.MatchedChar matchedChar = null;
			foreach (CheckCode.CharInfo current in this.words_)
			{
				CheckCode.MatchedChar matchedChar2 = this.ScopeMatch(source, current.Table, start);
				matchedChar2.Char = current.Char;
				if (matchedChar == null || matchedChar.Rate < matchedChar2.Rate)
				{
					matchedChar = matchedChar2;
				}
			}
			return matchedChar;
		}
	}
}
