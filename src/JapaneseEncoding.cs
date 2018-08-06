/*
 *  Copyright © 2013-2018, Amy Nagle.
 *  All rights reserved.
 *
 *  This file is part of ZoraSharp.
 *
 *  ZoraSharp is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as
 *  published by the Free Software Foundation, either version 3 of the
 *  License, or (at your option) any later version.
 *
 *  ZoraSharp is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with ZoraSharp. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Text;

namespace Zyrenth.Zora
{
	/// <summary>
	/// Encodes bytes to characters, or characters to bytes, for japanese text.
	/// Only used for Link and Child names; not for secret encoding.
	/// </summary>
	public class JapaneseEncoding : Encoding
	{
		private char[] characters =
		{
			'\0','\0','\0','\0','♥','↑','↓','←','→','\0','\0','「','」','\0','\0','。',
			' ','!','"','#','$','%','&','\'','(',')','*','+',',','-','.','/',
			'0','1','2','3','4','5','6','7','8','9',':',';','<','=','>','?',
			'@','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O',
			'P','Q','R','S','T','U','V','W','X','Y','Z','[','~',']','^','\0',
			'あ','い','う','え','お','か','き','く','け','こ','さ','し','す','せ','そ','た',
			'ち','つ','て','と','な','に','ぬ','ね','の','は','ひ','ふ','へ','ほ','ま','み',
			'む','め','も','や','ゆ','よ','ら','り','る','れ','ろ','を','わ','ん','ぁ','ぃ',
			'ぅ','ぇ','ぉ','っ','ゃ','ゅ','ょ','が','ぎ','ぐ','げ','ご','ざ','じ','ず','ぜ',
			'ぞ','だ','ぢ','づ','で','ど','ば','び','ぶ','べ','ぼ','ぱ','ぴ','ぷ','ぺ','ぽ',
			'ア','イ','ウ','エ','オ','カ','キ','ク','ケ','コ','サ','シ','ス','セ','ソ','タ',
			'チ','ツ','テ','ト','ナ','ニ','ヌ','ネ','ノ','ハ','ヒ','フ','ヘ','ホ','マ','ミ',
			'ム','メ','モ','ヤ','ユ','ヨ','ラ','リ','ル','レ','ロ','ワ','ヲ','ン','ァ','ィ',
			'ゥ','ェ','ォ','ッ','ャ','ュ','ョ','ガ','ギ','グ','ゲ','ゴ','ザ','ジ','ズ','ゼ',
			'ゾ','ダ','ヂ','ヅ','デ','ド','バ','ビ','ブ','ベ','ボ','パ','ピ','プ','ペ','ポ',
		};

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Zyrenth.Zora.JapaneseEncoding"/> class.
		/// </summary>
		public JapaneseEncoding()
		{
		}

		/// <summary>
		/// Calculates the number of bytes produced by encoding a set of characters from the specified character array.
		/// </summary>
		/// <returns>The number of bytes produced by encoding the specified characters.</returns>
		/// <param name="chars">The character array containing the set of characters to encode.</param>
		/// <param name="index">The index of the first character to encode.</param>
		/// <param name="count">The number of characters to encode.</param>
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		/// <summary>
		/// Encodes a set of characters from the specified character array into the specified byte array.
		/// </summary>
		/// <returns>The actual number of bytes written into bytes.</returns>
		/// <param name="chars">The character array containing the set of characters to encode.</param>
		/// <param name="charIndex">The index of the first character to encode.</param>
		/// <param name="charCount">The number of characters to encode.</param>
		/// <param name="bytes">The byte array to contain the resulting sequence of bytes.</param>
		/// <param name="byteIndex">The index at which to start writing the resulting sequence of bytes.</param>
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = charIndex; i < charCount; i++)
			{
				byte b = 0;
				char c = chars[i];
				if (c >= 'a' && c <= 'z')
				{
					c -= 'a';
					c += 'A';
				}

				if (c == 0)
					b = 0;
				else
				{
					for (int j = 0; j < characters.Length; j++)
					{
						if (characters[j] == c)
						{
							b = (byte)( j + 0x10 );
							break;
						}
					}
				}

				bytes[i] = b;
			}

			return charCount;
		}

		/// <summary>
		/// Calculates the number of characters produced by decoding a sequence of bytes from the specified byte array.
		/// </summary>
		/// <returns>The number of characters produced by decoding the specified sequence of bytes.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="index">The index of the first byte to decode.</param>
		/// <param name="count">The number of bytes to decode.</param>
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		/// <summary>
		/// Decodes a sequence of bytes from the specified byte array into the specified character array.
		/// </summary>
		/// <returns>The actual number of characters written into chars.</returns>
		/// <param name="bytes">The byte array containing the sequence of bytes to decode.</param>
		/// <param name="byteIndex">The index of the first byte to decode.</param>
		/// <param name="byteCount">The number of bytes to decode.</param>
		/// <param name="chars">The character array to contain the resulting set of characters.</param>
		/// <param name="charIndex">The index at which to start writing the resulting set of characters.</param>
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = byteIndex; i < byteCount; i++)
			{
				int b = bytes[i] - 0x10;
				if (b < 0 || b >= characters.Length)
					chars[i] = '\0';
				else
					chars[i] = characters[b];
			}

			return byteCount;
		}

		/// <summary>
		/// Calculates the maximum number of bytes produced by encoding the
		/// specified number of characters.
		/// </summary>
		/// <returns>The maximum number of bytes produced by encoding the specified number of characters.</returns>
		/// <param name="charCount">The number of characters to encode.</param>
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		/// <summary>
		/// Calculates the maximum number of characters produced by decoding the specified number of bytes.
		/// </summary>
		/// <returns>The number of bytes to decode.</returns>
		/// <param name="byteCount">The maximum number of characters produced by decoding the specified number of bytes.</param>
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}
	}
}
