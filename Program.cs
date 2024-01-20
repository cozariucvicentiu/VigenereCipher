using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] tableOfLetters = new char[26, 26];
            tableOfLetters = LetterTable(tableOfLetters);
            Console.WriteLine("Introduceti textul pe care doriti sa il criptati:");
            string plainText = Console.ReadLine();
            Console.WriteLine("Introduceti cheia de criptare ca un cuvant latin:");
            string key=Console.ReadLine();
            string cipherText = VigenereEncrypt(plainText,key,tableOfLetters);
            Console.WriteLine($"Encrypted:{cipherText}.");
            string decryptedText=VigenereDecrypt(cipherText,key);
            Console.WriteLine($"Decrypted:{decryptedText}");
            Console.ReadKey();

        }

        private static char[,] LetterTable(char[,] tableOfLetters)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] alphabetLetters = alphabet.ToCharArray();
            char[,] alphabetTable = new char[alphabetLetters.Length, alphabetLetters.Length];
            char[] shiftfAlphabet = new char[alphabetLetters.Length];
            shiftfAlphabet = alphabetLetters;
            for (int i = 0; i < alphabetLetters.Length; i++)
            {

                for (int j = 0; j < alphabetLetters.Length; j++)
                {
                    alphabetTable[i, j] = shiftfAlphabet[j];
                }
                char copy = shiftfAlphabet[0];
                for (int z = 0; z < 25; z++)
                {
                    shiftfAlphabet[z] = shiftfAlphabet[z + 1];
                }
                shiftfAlphabet[25] = copy;

            }
            return alphabetTable;   
        }

        private static string VigenereDecrypt(string cipherText, string encryptionKey)
        {
            string decryptedText = "";
            while (cipherText.Length > encryptionKey.Length)
            {
                encryptionKey += encryptionKey;
            }
            int i = 0;
            foreach (char c in cipherText)
            {
                if (char.IsLetter(c))
                {
                    char upperKey = char.ToUpper(encryptionKey[i]);
                    char upperC = char.ToUpper(c);
                    int min=upperC-upperKey+'A';
                    if (min < 65)
                    {
                        upperC = (char)(upperC - upperKey + 'A'+26);
                    }
                    else
                    {
                        upperC = (char)(upperC - upperKey + 'A');
                    }
                    decryptedText += upperC;    
                }
                else
                {
                    decryptedText += c;
                }
                i++;
            }
            return decryptedText;
        }

        private static string VigenereEncrypt(string plainText, string encryptionKey, char[,] tableOfLetters)
        {
            string cipherText = "";
            while(plainText.Length > encryptionKey.Length)
            {
                encryptionKey += encryptionKey;
            }
            int i = 0;
            foreach(char c in plainText)
            {
                if (char.IsLetter(c))
                {
                    char upperKey = char.ToUpper(encryptionKey[i]);
                    char upperC = char.ToUpper(c);
                    upperC = tableOfLetters[upperC - 'A', upperKey-'A'];
                    cipherText += upperC;
                }
                else
                {
                    cipherText += c;
                }
                i++;
            }
            return cipherText;
        }
    }
}
