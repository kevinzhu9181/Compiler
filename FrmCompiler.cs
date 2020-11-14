using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Compiler
{
    public partial class FrmCompiler : Form
    {
        private MorphologyAnalyze morphologyAnalyze;
        private GrammarAnalyze grammarAnalyze;
        private readonly string inputString;
        private string formattedOutputString;
        public FrmCompiler()
        {
            InitializeComponent();
            inputString = File.ReadAllText(@"C:\Users\Kevin\Desktop\CPT\Courses\CISC603 Theory of Computation\Compiler\Pascal Codes.txt");
            formattedOutputString = "";
        }

        private void BtnReadSource_Click(object sender, EventArgs e)
        {
            TxtResult.Text = inputString;
            BtnMorphology.Visible = true;
        }

        private void BtnMorphology_Click(object sender, EventArgs e)
        {
            morphologyAnalyze = new MorphologyAnalyze(inputString);
            if (morphologyAnalyze.Errors != null && morphologyAnalyze.Errors.Count() > 0)
            {
                formattedOutputString += "Error!\r\n";
                for (int index = 0; index < morphologyAnalyze.Errors.Count(); index ++)
                {
                    formattedOutputString += "(" + (index+1).ToString() + ") " +
                        "(Row Number: " + morphologyAnalyze.Errors.ElementAt(index).RowNumber + 
                        ",  Character: \"" + morphologyAnalyze.Errors.ElementAt(index).ErrorChar + 
                        "\",  Message: \"" + morphologyAnalyze.Errors.ElementAt(index).ErrorNote + "\")\r\n";
                }
            }
            else
            {
                formattedOutputString += "Token Output\r\n";
                foreach (var token in morphologyAnalyze.Tokens)
                {
                    formattedOutputString += "(" + token.TokenCount + ") " + "(" + token.Code + ",  \"" + token.Name + "\",  " + token.IdentifierCount + ")\r\n";
                }
                BtnGrammar.Visible = true;
            }

            TxtResult.Text = formattedOutputString;
        }

        private void BtnGrammar_Click(object sender, EventArgs e)
        {
            grammarAnalyze = new GrammarAnalyze(morphologyAnalyze);
            if (grammarAnalyze.error != null && grammarAnalyze.error.Length > 0)
            {
                TxtResult.Text = grammarAnalyze.error;
            }
            else
            {
                TxtResult.Text = "Correct!";
                BtnSemantic.Visible = true;
            }
        }
    }
}
