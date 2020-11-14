using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public class MorphologyAnalyze
    {
        public string[] MachineCodes { get; set; }
        public List<Token> Tokens { get; set; }
        public List<Symbol> Symbols { get; set; }
        public List<Error> Errors { get; set; }

        private readonly string input;
        private int i = 0;
        private int rowNum = 1;

        public MorphologyAnalyze(string s)  //Get input as a string
        {
            MachineCodes = new string[38];
            Tokens = new List<Token>();
            Symbols = new List<Symbol>();
            Errors = new List<Error>();
            input = s + " ";
            NewKeyWord();   //Define key word in input
            Dispose();      //Main logic of morphology analyze
        }

        #region Define Key Wrods
        private void NewKeyWord()
        {
            MachineCodes[0] = "";
            MachineCodes[1] = "and";
            MachineCodes[2] = "begin";
            MachineCodes[3] = "bool";
            MachineCodes[4] = "do";
            MachineCodes[5] = "else";
            MachineCodes[6] = "end";
            MachineCodes[7] = "false";
            MachineCodes[8] = "if";
            MachineCodes[9] = "integer";
            MachineCodes[10] = "not";
            MachineCodes[11] = "or";
            MachineCodes[12] = "program";
            MachineCodes[13] = "real";
            MachineCodes[14] = "then";
            MachineCodes[15] = "true";
            MachineCodes[16] = "var";
            MachineCodes[17] = "while";
            MachineCodes[18] = "Identifier";
            MachineCodes[19] = "IntegerInput";
            MachineCodes[20] = "RealInput";
            MachineCodes[21] = "(";
            MachineCodes[22] = ")";
            MachineCodes[23] = "+";
            MachineCodes[24] = "-";
            MachineCodes[25] = "*";
            MachineCodes[26] = "/";
            MachineCodes[27] = ".";
            MachineCodes[28] = ",";
            MachineCodes[29] = ":";
            MachineCodes[30] = ";";
            MachineCodes[31] = ":=";
            MachineCodes[32] = "=";
            MachineCodes[33] = "<=";
            MachineCodes[34] = "<";
            MachineCodes[35] = "<>";
            MachineCodes[36] = ">";
            MachineCodes[37] = ">=";
        }
        #endregion

        #region Morphology Analyze of Input String
        public void Dispose()
        {
            while (i < input.Length)    //Continue analyzing until meeting the last character of input string
            {
                if (IsAlpha(input[i]))
                {
                    // Recognize if this is a key word
                    RecogId();
                }
                else if (IsDight(input[i]))
                {
                    // Recognize if this word is a number
                    RecogCons();
                }
                else if (input[i] == '/')
                {
                    // Recognize / or //
                    i++;
                    if (input[i] == '/')
                    {
                        while (input[i] != '\r' && i < input.Length)
                        {
                            i++;
                        }
                    }
                    else
                    {
                        i--;
                    }
                }
                else if (input[i] == '\r' && input[i + 1] == '\n')
                {
                    // Recognize enter
                    rowNum++;
                    i++;
                    i++;
                }
                else if (IsDelimiter(input[i]))
                {
                    // Recognize if this is a symbol
                    RecogSym();
                }
                else if (input[i] == ' ')
                {
                    // Recoginze space
                    i++;
                }
                else
                {
                    Error e = new Error(rowNum, input[i].ToString(), "Illegal Character");
                    Errors.Add(e);
                    i++;
                }
            }
        }
        #endregion

        #region Recognize if this is a key word
        private void RecogId()
        {
            string str = "";
            int code;
            do
            {
                str += input[i];
                i++;
            } while (IsAlpha(input[i]) || IsDight(input[i]));   //Only if it's alpha or digit, add it into current word
            code = Reserve(str);
            Token t = new Token
            {
                TokenCount = Tokens.Count,
                Name = str
            };  //Store into token list
            if (code == 0)
            {
                t.Code = 18;
                t.IdentifierCount = Symbols.Count;
                Symbol s = new Symbol
                {
                    IdentifierCount = t.IdentifierCount,
                    Name = str,
                    Code = 18
                };    //Store into symbol list
                Symbols.Add(s);
            }
            else
            {
                t.Code = code;
                t.IdentifierCount = -1;
            }
            Tokens.Add(t);
        }
        #endregion

        #region Recognize if this word is a number
        private void RecogCons()
        {
            string str = input[i].ToString();
            bool flag = true;
            bool point = true;
            while (flag)
            {
                i++;
                if (IsDight(input[i]))
                {
                    str += input[i];
                }
                else if (input[i] == '.')
                {
                    if (point)
                    {
                        str += input[i];
                        point = false;
                    }
                    else
                    {
                        Error e = new Error(rowNum, str, "Have the second '.' for this number!");
                        Errors.Add(e);
                        flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
            }
            if (point)
            {
                Token t = new Token
                {
                    TokenCount = Tokens.Count,
                    Name = str,
                    Code = 19,
                    IdentifierCount = Symbols.Count
                };
                Symbol s = new Symbol
                {
                    IdentifierCount = t.IdentifierCount,
                    Name = str,
                    Code = 19
                };
                Symbols.Add(s);
                Tokens.Add(t);
            }
            else
            {
                Token t = new Token
                {
                    TokenCount = Tokens.Count,
                    Name = str,
                    Code = 20,
                    IdentifierCount = Symbols.Count
                };
                Symbol s = new Symbol
                {
                    IdentifierCount = t.IdentifierCount,
                    Name = str,
                    Code = 20
                };
                Symbols.Add(s);
                Tokens.Add(t);
            }
        }
        #endregion

        #region Recognize if this is a symbol
        private void RecogSym()
        {
            string str = "" + input[i];
            if (str == ":" || str == "<" || str == ">")
            {
                i++;
                if (input[i] == '=')
                {
                    str += input[i];
                }
                else if (input[i] == '>' && str == "<")
                {
                    str += input[i];
                }
                else
                {
                    i--;
                }
            }
            for (int j = 21; j <= 37; j++)
            {
                if (str == MachineCodes[j])
                {
                    Token t = new Token
                    {
                        TokenCount = Tokens.Count,
                        Name = str,
                        Code = j,
                        IdentifierCount = -1
                    };
                    Tokens.Add(t);
                    i++;
                }
            }
        }
        #endregion

        #region Check if this char is an alpha
        private bool IsAlpha(char c)
        {
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Check if this char is a digit
        private bool IsDight(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Check if this char is a delimiter
        private bool IsDelimiter(char c)
        {
            switch (c)
            {
                case '(': return true;
                case ')': return true;
                case '+': return true;
                case '-': return true;
                case '*': return true;
                case '.': return true;
                case ',': return true;
                case ':': return true;
                case ';': return true;
                case '=': return true;
                case '<': return true;
                case '>': return true;
                default: return false;
            }
        }
        #endregion

        #region Match key word, and return its code
        private int Reserve(string str)
        {
            for (int i = 1; i <= 17; i++)
            {

                if (str == MachineCodes[i])
                {
                    return i;   //return the code of the key word
                }
            }
            return 0;   //return 0 as the identifier
        }
        #endregion
    }
}
