namespace Compiler
{
    partial class FrmCompiler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnReadSource = new System.Windows.Forms.Button();
            this.BtnMorphology = new System.Windows.Forms.Button();
            this.BtnGrammar = new System.Windows.Forms.Button();
            this.BtnSemantic = new System.Windows.Forms.Button();
            this.BtnCreateAssembly = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.TxtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtnReadSource
            // 
            this.BtnReadSource.Location = new System.Drawing.Point(29, 31);
            this.BtnReadSource.Name = "BtnReadSource";
            this.BtnReadSource.Size = new System.Drawing.Size(135, 35);
            this.BtnReadSource.TabIndex = 0;
            this.BtnReadSource.Text = "Read Source File";
            this.BtnReadSource.UseVisualStyleBackColor = true;
            this.BtnReadSource.Click += new System.EventHandler(this.BtnReadSource_Click);
            // 
            // BtnMorphology
            // 
            this.BtnMorphology.Location = new System.Drawing.Point(29, 98);
            this.BtnMorphology.Name = "BtnMorphology";
            this.BtnMorphology.Size = new System.Drawing.Size(135, 35);
            this.BtnMorphology.TabIndex = 1;
            this.BtnMorphology.Text = "Morphology Analyze";
            this.BtnMorphology.UseVisualStyleBackColor = true;
            this.BtnMorphology.Visible = false;
            this.BtnMorphology.Click += new System.EventHandler(this.BtnMorphology_Click);
            // 
            // BtnGrammar
            // 
            this.BtnGrammar.Location = new System.Drawing.Point(29, 165);
            this.BtnGrammar.Name = "BtnGrammar";
            this.BtnGrammar.Size = new System.Drawing.Size(135, 35);
            this.BtnGrammar.TabIndex = 2;
            this.BtnGrammar.Text = "Grammar Analyze";
            this.BtnGrammar.UseVisualStyleBackColor = true;
            this.BtnGrammar.Visible = false;
            this.BtnGrammar.Click += new System.EventHandler(this.BtnGrammar_Click);
            // 
            // BtnSemantic
            // 
            this.BtnSemantic.Location = new System.Drawing.Point(29, 232);
            this.BtnSemantic.Name = "BtnSemantic";
            this.BtnSemantic.Size = new System.Drawing.Size(135, 35);
            this.BtnSemantic.TabIndex = 3;
            this.BtnSemantic.Text = "Semantic Analyze";
            this.BtnSemantic.UseVisualStyleBackColor = true;
            this.BtnSemantic.Visible = false;
            // 
            // BtnCreateAssembly
            // 
            this.BtnCreateAssembly.Location = new System.Drawing.Point(29, 299);
            this.BtnCreateAssembly.Name = "BtnCreateAssembly";
            this.BtnCreateAssembly.Size = new System.Drawing.Size(135, 35);
            this.BtnCreateAssembly.TabIndex = 4;
            this.BtnCreateAssembly.Text = "Create Assembly Codes";
            this.BtnCreateAssembly.UseVisualStyleBackColor = true;
            this.BtnCreateAssembly.Visible = false;
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(29, 366);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(135, 35);
            this.BtnExit.TabIndex = 5;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            // 
            // TxtResult
            // 
            this.TxtResult.Location = new System.Drawing.Point(211, 31);
            this.TxtResult.Multiline = true;
            this.TxtResult.Name = "TxtResult";
            this.TxtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtResult.Size = new System.Drawing.Size(290, 370);
            this.TxtResult.TabIndex = 6;
            // 
            // FrmCompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 435);
            this.Controls.Add(this.TxtResult);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnCreateAssembly);
            this.Controls.Add(this.BtnSemantic);
            this.Controls.Add(this.BtnGrammar);
            this.Controls.Add(this.BtnMorphology);
            this.Controls.Add(this.BtnReadSource);
            this.Name = "FrmCompiler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compiler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnReadSource;
        private System.Windows.Forms.Button BtnMorphology;
        private System.Windows.Forms.Button BtnGrammar;
        private System.Windows.Forms.Button BtnSemantic;
        private System.Windows.Forms.Button BtnCreateAssembly;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.TextBox TxtResult;
    }
}

