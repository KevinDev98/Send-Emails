using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendGridAzureEmail.Class
{
	public class SecurityClass
	{
		static string[,] ChangeCharacters = new string[22, 2] { { "%1=!", "A" }, { "%2=!", "B" }, { "%3=!", "C" }, { "%4=!", "D" }, { "%5=!", "E" }, { "%6=!", "F" }, { "%7=!", "G" }, { "%8=!", "H" }, { "%9=!", "I" },
	{ "%0=!", "J" }, { "}1-", "K" }, { "}2-", "L" }, { "}3-", "L" }, { "}4-", "N" }, { "}5-", "O" }, { "}6-", "}P" }, { "}7-", "Q" }, { "}8-", "R" }, { "}9-", "S" },
	{ "}0-", "T" },{ "#", "." },{ "[", ";" }  };
		static int ArrLength = ChangeCharacters.Length / 2;
		public string Encriptar(string _cadenaAencriptar)
		{
			string result = string.Empty;
			byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
			result = Convert.ToBase64String(encryted);
			return result;
		}
		/// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
		public string DesEncriptar(string _cadenaAdesencriptar)
		{
			string result = string.Empty;
			byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
			//result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
			result = System.Text.Encoding.Unicode.GetString(decryted);
			return result;
		}
		public string semiencrypt(string text)
		{
			for (int Fila = 0; Fila < ArrLength; Fila++)
			{
				string search = ChangeCharacters[Fila, 1];
				//Console.WriteLine(search);
				string Replace = ChangeCharacters[Fila, 0];
				//Console.WriteLine(Replace);
				text = text.ToUpper();
				if (text.Contains(search))
				{
					//Console.WriteLine("Searched Value "+ search + " repleace vlue " + Replace +" for " + text);
					text = text.Replace(search, Replace);
				}
			}
			//Console.WriteLine("New Value " + text);
			return text;
		}
		public string desencrypt2(string text)
		{
			for (int Fila = 0; Fila < ArrLength; Fila++)
			{
				string search = ChangeCharacters[Fila, 0];
				//Console.WriteLine(search);
				string Replace = ChangeCharacters[Fila, 1];
				//Console.WriteLine(Replace);
				text = text.ToUpper();
				if (text.Contains(search))
				{
					text = text.Replace(search, Replace);
				}
			}
			return text;
		}
		public string Fullencrypt(string encryptText)
		{
			encryptText = Encriptar(semiencrypt(encryptText));

			return encryptText;
		}
		public string Fulldesencrypt(string encryptText)
		{
			encryptText = desencrypt2(DesEncriptar(encryptText));

			return encryptText;
		}
	}
}