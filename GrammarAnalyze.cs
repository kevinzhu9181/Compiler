using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public class GrammarAnalyze
    {
        public List<Token> Tokens { get; set; }
        public List<Symbol> Symbols { get; set; }

        public string error = "";
        int i = 0;

        public GrammarAnalyze(MorphologyAnalyze m)
        {
            Tokens = m.Tokens;
            Symbols = m.Symbols;
            Dispose();
        }
        private void Next()
        {
            if (i < Tokens.Count - 1)
            {
                i++;
            }
        }
        private void Before()
        {
            if (i > 0)
            {
                i--;
            }
        }

        #region Main Function
        private void Dispose()
        {
            if (Tokens[i].Code == 12)       // Check Program
            {
                Next();
                if (Tokens[i].Code == 18)   // Check Identifier
                {
                    Next();
                    if (Tokens[i].Code == 30)                                           // Check Semi Colon
                    {
                        Next();
                        // Execute Processing Function
                        ProBody();
                    }
                    else
                    {
                        error = "Semi Colon Is Missing In Program Header!";
                    }
                }
                else
                {
                    error = "Program Name Is Missing!";
                }
            }
            else
            {
                error = "Program Keyword Is Missing!";
            }
        }
        #endregion

        #region Processing Function
        private void ProBody()
        {
            if (Tokens[i].Code == 16)       // Check Var
            {
                Next();
                VarDef();
            }
            else if (Tokens[i].Code == 2)   // Check Begin
            {
                Next();
                ComSent();
            }
            else
            {
                error = "Var Keyword Or Begin Keyword Is Missing!";
            }
        }
        #endregion

        #region Check Var Definition
        private void VarDef()
        {
            // Check Identifier List
            if (IsIdlist())
            {
                Next();
                if (Tokens[i].Code == 29)                                                   // Check Colon
                {
                    Next();
                    if (Tokens[i].Code == 9 || Tokens[i].Code == 3 || Tokens[i].Code == 13) // Check Integer, Bool, Real
                    {
                        int j = i;
                        j -= 2;
                        Symbols[Tokens[j].IdentifierCount].Code = Tokens[i].Code;
                        j--;
                        while (Tokens[j].Code == 28)                                        // Check Comma
                        {
                            j--;
                            Symbols[Tokens[j].IdentifierCount].Code = Tokens[i].Code;
                        }
                        Next();
                        if (Tokens[i].Code == 30)                                           // Check Semi Colon
                        {
                            Next();
                            if (Tokens[i].Code == 2)                                        // Check Begin
                            {
                                Next();
                                // Code Block Ending Check
                                ComSent();
                            }
                            else
                            {
                                // Recursive Call
                                VarDef();
                            }
                        }
                        else
                        {
                            error = "Semi Colon Is Missing In Var Definition!";
                        }
                    }
                    else
                    {
                        error = "Variable Type Is Missing Or Wrong Variable Type!";
                        return;
                    }
                }
                else
                {
                    error = "Colon Is Missing In Var Definition!";
                }
            }
            else
            {
                error = "Wrong Identifier Format In Var Definition!";
            }
        }
        #endregion

        #region Check Format of Identifier List
        private bool IsIdlist()
        {
            if (Tokens[i].Code == 18)       // Check Identifier
            {
                Next();
                if (Tokens[i].Code == 28)   // Check Comma
                {
                    Next();
                    // Recursive Call
                    return IsIdlist();
                }
                else
                {
                    Before();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Check Code Block Ending
        private void ComSent()
        {
            // Check Sentences
            SentList();

            if (error == "")
            {
                if (Tokens[i].Code == 6)        // Check End
                {
                    return;
                }
                else
                {
                    error = "End Is Missing For Code Block!";
                }
            }
        }
        #endregion

        #region Check Sentences
        private void SentList()
        {
            // Check Execution Sentences
            ExecSent();

            if (error == "")
            {
                Next();
                if (Tokens[i].Code == 30)       // Check Semi Colon
                {
                    Next();
                    // Recursive Call
                    SentList();
                }
            }
        }
        #endregion

        #region Check Execution Sentences
        private void ExecSent()
        {
            if (Tokens[i].Code == 18)                                                       // Check Identifier
            {
                Next();
                // Check Assignment Sentences
                AssiSent();
            }
            else if (Tokens[i].Code == 2 || Tokens[i].Code == 8 || Tokens[i].Code == 17)    // Check Begin/ If/ While
            {
                // Check Structure Sentences
                StructSent();
            }
            else
            {
                Before();
            }
        }
        #endregion

        #region Check Assignment Sentences
        private void AssiSent()
        {
            if (Tokens[i].Code == 31)           // Check Assignment Sign
            {
                Next();
                // Check Expressions
                Expression();
            }
            else
            {
                error = "Assignment Sign Is Missing!";
            }
        }
        #endregion

        #region Check Expressions
        private void Expression()
        {
            // Check If It Is Boolean Expression
            if (Tokens[i].Code == 7 || Tokens[i].Code == 15 || (Tokens[i].IdentifierCount != -1 && Symbols[Tokens[i].IdentifierCount].Code == 3))
            {
                // Check Boolean Expression
                BoolExp();
            }
            else
            {
                // Check Arithmetic Expression
                AritExp();
            }
        }
        #endregion

        #region Check Boolean Expression
        private void BoolExp()
        {
            // Check Other Boolean Item First
            BoolItem();

            if (error == "")
            {
                Next();
                if (Tokens[i].Code == 11)   // Check Or Latest
                {
                    Next();
                    // Recursive Call
                    BoolExp();
                }
                else
                {
                    Before();
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Check Boolean Item
        private void BoolItem()
        {
            // Check Other Boolean Factor First
            BoolFactor();

            if (error == "")
            {
                Next();
                if (Tokens[i].Code == 1)    // Check And Before Or
                {
                    Next();
                    // Recursive Call
                    BoolItem();
                }
                else
                {
                    Before();
                }
            }
        }
        #endregion

        #region Check Boolean Factor
        private void BoolFactor()
        {
            if (Tokens[i].Code == 10)   // Check Not First
            {
                Next();
                // Recursive Call
                BoolFactor();
            }
            else
            {
                // Check Boolean Value
                BoolValue();
            }
        }
        #endregion

        #region Check Boolean Value
        private void BoolValue()
        {
            if (Tokens[i].Code == 15 || Tokens[i].Code == 7)    // Check True/ False
            {
                return;
            }
            else if (Tokens[i].Code == 18)                      // Check Identifier
            {
                Next();
                // Check </ <=/ =/ >=/ >/ <>
                if (Tokens[i].Code == 34 || Tokens[i].Code == 33 || Tokens[i].Code == 32 || Tokens[i].Code == 37 || Tokens[i].Code == 36 || Tokens[i].Code == 35)
                {
                    Next();
                    // Identifier/ Integer Input/ Real Input
                    if (Tokens[i].Code != 18 && Tokens[i].Code != 19 && Tokens[i].Code != 20)
                    {
                        error = "Identifier/ Integer Input/ Real Input Is Missing After Comparison Sign!";
                    }
                }
                else
                {
                    Before();
                }
            }
            else if (Tokens[i].Code == 21)                      // Check Left Parenthesis
            {
                Next();
                // Check Boolean Expression From The Beginning
                BoolExp();

                Next();
                if (Tokens[i].Code == 22)                       // Check Right Parenthesis
                {
                    return;
                }
                else
                {
                    error = "Right Parenthesis Is Missing In Boolean Expression!";
                }
            }
            else
            {
                error = "Boolean Value Is Wrong!";
            }
        }
        #endregion

        #region Check Arithmetic Expression
        private void AritExp()
        {
            // Check Arithmetic Item
            Item();

            if (error == "")
            {
                Next();
                if (Tokens[i].Code == 23 || Tokens[i].Code == 24)   // Check Plus/ Minus Sign Later
                {
                    Next();
                    // Recursive Call
                    AritExp();
                }
                else
                {
                    Before();
                    return;
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Check Arithmetic Item
        private void Item()
        {
            // Check Arithmetic Factor
            Factor();
            
            if (error == "")
            {
                Next();
                if (Tokens[i].Code == 25 || Tokens[i].Code == 26)   // Check Product/ Division Sign Before Plus/ Minus Sign
                {
                    Next();
                    // Recursive Call
                    Item();
                }
                else
                {
                    Before();
                    return;
                }
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Check Arithmetic Factor
        private void Factor()
        {
            if (Tokens[i].Code == 21)       // Check Left Parenthesis First
            {
                Next();
                // Check Arithmetic Expression From The Beginning
                AritExp();

                Next();
                if (Tokens[i].Code == 22)   // Check Right Parenthesis
                {
                    return;
                }
                else
                {
                    error = "Right Parenthesis Is Missing In Arithmetic Expression!";
                }
            }
            else
            {
                // Check Arithmetic Value
                CalQua();
            }
        }
        #endregion

        #region Check Arithmetic Value
        private void CalQua()
        {
            if (Tokens[i].Code == 18 || Tokens[i].Code == 19 || Tokens[i].Code == 20)   // Check If Arithmetic Value Is Identifier/ Integer/ Real
            {
                return;
            }
            else
            {
                error = "Arithmetic Value Is Wrong!";
            }
        }
        #endregion

        #region Check Structure Sentences
        private void StructSent()
        {
            if (Tokens[i].Code == 2)            // Check Begin
            {
                Next();
                // Check Code Block Ending
                ComSent();
            }
            else if (Tokens[i].Code == 8)       // Check If
            {
                Next();
                // Check If Statements
                IfSent();
            }
            else if (Tokens[i].Code == 17)      // Check While
            {
                Next();
                // Check While Statements
                WhileSent();
            }
        }
        #endregion

        #region Check If Statements
        private void IfSent()
        {
            // Check Boolean Expression
            BoolExp();

            if (error == "")
            {
                Next();
                if (Tokens[i].Code == 14)       // Check Then
                {
                    Next();
                    // Check Execution Sentences
                    ExecSent();

                    Next();
                    if (Tokens[i].Code == 5)    // Check Else
                    {
                        Next();
                        // Check Execution Sentences
                        ExecSent();
                    }
                    else
                    {
                        Before();
                        return;
                    }
                }
                else
                {
                    error = "Then Keyword Is Missing In If Statement!";
                }
            }
            else
            {
                error = "Boolean Expression Is Wrong In If Statement!";
            }
        }
        #endregion

        #region Check While Statements
        private void WhileSent()
        {
            // Check Boolean Expression
            BoolExp();

            if (error == "")
            {
                Next();
                if (Tokens[i].Code == 4)        // Check Do
                {
                    Next();
                    // Check Execution Sentences
                    ExecSent();
                }
                else
                {
                    error = "Do Keyword Is Missing In While Statement!";
                }
            }
        }
        #endregion
    }
}