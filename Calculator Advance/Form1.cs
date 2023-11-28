using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
/*
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
*/
namespace Calculator_Advance
{
    public partial class Form1 : Form
    {

        public Resolve resolve;

        public Form1()
        {
            InitializeComponent();

            resolve = new Resolve();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string txt = button.Text;
            OnInputChangeValue(txt);
        }

        char[] exten_char = { '+', '-', '*', '/', '(', ')', '%', '√', '^'};
        //string plusMinus = "+/-";
        char[] num_char = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        private bool check_Exten(char txt)
        {
            bool is_ok = false;
            for (int i = 0; i < exten_char.Length; i++)
            {
                if (txt == exten_char[i])
                {
                    is_ok = true;
                }
            }
            return is_ok;
        }

        private bool check_Num(char txt)
        {
            bool is_ok = false;
            for (int i = 0; i < num_char.Length; i++)
            {
                if (txt == num_char[i])
                {
                    is_ok = true;
                }
            }
            return is_ok;
        }

        bool last_num = false;
        bool last_char = false;
        string help = "";
        string repair_txt = "";

        private string Text_Verification(string txt)
        {
            string new_txt = "";
            repair_txt = "";
            for (int i = 0; i < txt.Length; i++)
            {

                if (check_Num(txt[i]) == true)         // if char is num
                {
                    if (last_num == false && i > 0 && last_char == true)
                    {
                        new_txt += " ";
                        repair_txt += " ";
                    }
                    new_txt += txt[i];
                    repair_txt += txt[i];                                  
                    last_char = true;
                    last_num = true;
                }
                else                                   // if char isn´t num
                {
                    /*
                    if (txt[i].ToString() == "x²")
                    {
                        new_txt += "^";
                        repair_txt += "x²";
                    }
                    else
                    */if (txt[i] == ' ' && last_char == true)
                    {
                        last_char = false;   
                        new_txt += txt[i];
                        repair_txt += txt[i];
                    }
                    else if (txt[i] == ' ' && last_char == false)
                    {
                        last_char = false;   
                        new_txt += "";
                        repair_txt += "";
                    }
                    else if (check_Exten(txt[i]) == true && last_char == true)              
                    {
                        if (last_num == true)
                        {
                            new_txt += " ";
                            repair_txt += " ";
                        }
                        new_txt += txt[i] + " ";
                        repair_txt += txt[i] + " ";
                        last_char = false;
                    }
                    else if (check_Exten(txt[i]) == true && last_char == false)
                    {
                        last_char = true;
                        new_txt += txt[i];
                        repair_txt += txt[i];

                    }
                    last_num = false;
                }
                
            }
            // Console.WriteLine(new_txt);
            return new_txt;
        }

        private void OnInputChangeValue(string txt)
        {
            /*
             * this.textBoxInput = new System.Windows.Forms.TextBox();
             * this.labelRersult = new System.Windows.Forms.Label();
             */
            if (txt == "=")
            {
                string txt_1 = Text_Verification(textBoxInput.Text);     // return correct text
                float num_result = resolve.Solve(txt_1);                 // return result to float
                labelRersult.Text = num_result.ToString();               // set text to result label
                textBoxInput.Text = repair_txt;                          // textBoxInput repair to correct
            }
            else if (txt == "C")
            {
                textBoxInput.Text = "";                                // clear textBoxInput
            }
            else if (txt == "◄—") //" < --")
            {
                string txtText = textBoxInput.Text;                      
                if (txtText.Length > 0)
                {
                    StringBuilder str = new StringBuilder(txtText);
                    str.Remove(txtText.Length - 1, 1);                    // remove last char in textBoxInput
                    txtText = str.ToString();
                }
                textBoxInput.Text = txtText;
            }
            else
            {
                string txtText = textBoxInput.Text;
                if (txtText.Length > 0)
                {
                    txtText = txtText + " " + txt;                        // add free space to textBoxInput
                }
                else
                {
                    txtText = txt;
                }
                textBoxInput.Text = txtText;
            }
        }

    }
}