using System;
using System.Drawing;
using System.Windows.Forms;

namespace Calculator
{
    class MainForm : Form
    {
        Button[] number = new Button[10];
        Button[] action = new Button[14];
        TextBox input = new TextBox();
        TextBox history = new TextBox();
        RadioButton degrees = new RadioButton();
        RadioButton radians = new RadioButton();
        RadioButton grads = new RadioButton();
        Font fontButton = new Font("Microsoft Sans Serif", 16);
        Font fontHistory = new Font("Microsoft Sans Serif", 20);
        Font fontInput = new Font("Microsoft Sans Serif", 40);
        double num;
        bool rezView = false;
        char act;
        bool doCleanHistory = false;

        public MainForm()
        {
            InitComponent();
        }

        private void InitComponent()
        {
            Width = 335;
            Height = 465;
            Text = "Calculator";

            history.Width = 320;
            history.Top = 0;
            history.Left = 0;
            history.Font = fontHistory;
            history.TextAlign = HorizontalAlignment.Right;
            history.Enabled = false;
            Controls.Add(history);

            input.Width = 320;
            input.Top = 35;
            input.Left = 0;
            input.Text = "0";
            input.Select(1, 1);
            input.Font = fontInput;
            input.TextAlign = HorizontalAlignment.Right;
            input.KeyPress += Input_KeyPress;
            Controls.Add(input);

            degrees.Width = 80;
            degrees.Top = 400;
            degrees.Left = 0;
            degrees.Text = "degrees";
            Controls.Add(degrees);

            radians.Width = 80;
            radians.Top = 400;
            radians.Left = 111;
            radians.Text = "radians";
            radians.Checked = true;
            Controls.Add(radians);

            grads.Width = 80;
            grads.Top = 400;
            grads.Left = 222;
            grads.Text = "grads";
            Controls.Add(grads);

            InitButton(ref number, 10);
            InitButton(ref action, 14);
            CreateButton();
        }

        private void InitButton(ref Button[] button, int n)
        {
            for (int i = 0; i < n; i++)
            {
                button[i] = new Button();
                button[i].Font = fontButton;
            }
        }

        private void CreateButton()
        {
            int g = 9;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 2; j > -1; j--)
                {
                    number[g].Width = 80;
                    number[g].Height = 50;
                    number[g].Top = 50 * i + 200;
                    number[g].Left = 80 * j;
                    number[g].Text = $"{g}";
                    number[g].Click += new EventHandler(number_Click);
                    g--;
                }
            }

            number[0].Width = 80;
            number[0].Height = 50;
            number[0].Top = 250 + 100;
            number[0].Left = 80;
            number[0].Text = $"{0}";
            number[0].Click += new EventHandler(number_Click);
            Controls.AddRange(number);

            g = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    action[g].Width = 80;
                    action[g].Height = 50;
                    action[g].Top = 50 * i + 100;
                    action[g].Left = 80 * j;
                    action[g].Click += new EventHandler(action_Click);
                    g++;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                action[g].Width = 80;
                action[g].Height = 50;
                action[g].Top = 50 * i + 200;
                action[g].Left = 240;
                action[g].Click += new EventHandler(action_Click);
                g++;
            }

            action[g].Width = 80;
            action[g].Height = 50;
            action[g].Top = 150 + 200;
            action[g].Left = 0;
            action[g].Click += new EventHandler(action_Click);
            g++;
            action[g].Width = 80;
            action[g].Height = 50;
            action[g].Top = 150 + 200;
            action[g].Left = 160;
            action[g].Click += new EventHandler(action_Click);
            action[0].Text = "sin(x)";
            action[0].Click += new EventHandler(sin_Click);
            action[1].Text = "CE";
            action[1].Click += new EventHandler(ce_Click);
            action[2].Text = "C";
            action[2].Click += new EventHandler(c_Click);
            action[3].Text = "Clean";
            action[3].Click += new EventHandler(clean_Click);
            action[4].Text = "cos(x)";
            action[4].Click += new EventHandler(cos_Click);
            action[5].Text = "x*x";
            action[6].Text = "Root(x)";
            action[6].Click += new EventHandler(root_Click);
            action[7].Text = "/";
            action[7].Click += new EventHandler(division_Click);
            action[8].Text = "x";
            action[8].Click += new EventHandler(multiplication_Click);
            action[9].Text = "-";
            action[9].Click += new EventHandler(minus_Click);
            action[10].Text = "+";
            action[10].Click += new EventHandler(plus_Click);
            action[11].Text = "=";
            action[11].Click += new EventHandler(equals_Click);
            action[12].Text = "+/-";
            action[12].Click += new EventHandler(inversion_Click);
            action[13].Text = ",";
            action[13].Click += new EventHandler(dot_Click);
            Controls.AddRange(action);
        }

        private void number_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (doCleanHistory)
            {
                history.Text = string.Empty;
            }
            doCleanHistory = false;
            if (rezView)
            {
                input.Text = string.Empty;
            }
            rezView = false;
            if (input.Text == "0")
            {
                input.Text = button.Text;
            }
            else
            {
                input.Text += button.Text;
            }
        }

        private void Input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (input.Text == "0") input.Text = string.Empty;
            if (e.KeyChar == ',') dot_Click(sender, e);
            if (Char.IsDigit(e.KeyChar))
            {
                if (doCleanHistory)
                {
                    history.Text = string.Empty;
                }
                doCleanHistory = false;
                if (rezView)
                {
                    input.Text = string.Empty;
                }
                rezView = false;
                return;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void action_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
        }

        private void clean_Click(object sender, EventArgs e)
        {
            if (input.Text.Length > 1)
            {
                char[] value = input.Text.ToCharArray();
                input.Text = string.Empty;
                for (int i = 0; i < value.Length - 1; i++)
                {
                    input.Text += value[i];
                }
            }
            else
            {
                input.Text = "0";
            }
        }

        private void dot_Click(object sender, EventArgs e)
        {
            char[] value = input.Text.ToCharArray();
            bool dot = false;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == ',')
                {
                    dot = true;
                }
            }
            if (dot == false)
            {
                input.Text += ",";
            }
            if (doCleanHistory)
            {
                history.Text = string.Empty;
                input.Text = "0,";
                doCleanHistory = false;
            }
        }

        private void inversion_Click(object sender, EventArgs e)
        {
            input.Text = Convert.ToString(-1 * Convert.ToInt32(input.Text));
        }

        private void root_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(history.Text))
            {
                char[] histr = history.Text.ToCharArray();
                char[] signs = { '-', '+', '*', '/' };
                if (Array.IndexOf(signs, histr[history.Text.Length - 1]) == -1)
                {
                    history.Text = $"sqrt({history.Text})";
                }
                else
                {
                    history.Text += $"sqrt({input.Text})";
                }
            }
            else
            {
                history.Text += $"sqrt({input.Text})";
            }

            input.Text = Convert.ToString(Math.Sqrt(double.Parse(input.Text)));
        }

        private void sin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(history.Text))
            {
                char[] histr = history.Text.ToCharArray();
                char[] signs = { '-', '+', '*', '/' };
                if (Array.IndexOf(signs, histr[history.Text.Length - 1]) == -1)
                {
                    history.Text = $"sin({history.Text})";
                }
                else
                {
                    history.Text += $"sin({input.Text})";
                }
            }
            else
            {
                history.Text += $"sin({input.Text})";
            }

            if (degrees.Checked)
            {
                input.Text = Convert.ToString(Math.Sin(double.Parse(input.Text)) * 180 / Math.PI);
            }
            else if (grads.Checked)
            {
                input.Text = Convert.ToString(Math.Sin(double.Parse(input.Text)) * 200 / Math.PI);
            }
            else
            {
                input.Text = Convert.ToString(Math.Sin(double.Parse(input.Text)));
            }
        }

        private void cos_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(history.Text))
            {
                char[] histr = history.Text.ToCharArray();
                char[] signs = { '-', '+', '*', '/' };
                if (Array.IndexOf(signs, histr[history.Text.Length - 1]) == -1)
                {
                    history.Text = $"cos({history.Text})";
                }
                else
                {
                    history.Text += $"cos({input.Text})";
                }
            }
            else
            {
                history.Text += $"cos({input.Text})";
            }

            if (degrees.Checked)
            {
                input.Text = Convert.ToString(Math.Cos(double.Parse(input.Text)) * 180 / Math.PI);
            }
            else if (grads.Checked)
            {
                input.Text = Convert.ToString(Math.Cos(double.Parse(input.Text)) * 200 / Math.PI);
            }
            else
            {
                input.Text = Convert.ToString(Math.Cos(double.Parse(input.Text)));
            }
        }

        private void c_Click(object sender, EventArgs e)
        {
            history.Text = string.Empty;
            input.Text = "0";
        }

        private void ce_Click(object sender, EventArgs e)
        {
            input.Text = "0";
            if (doCleanHistory)
            {
                history.Text = string.Empty;
                doCleanHistory = false;
            }
        }

        private void plus_Click(object sender, EventArgs e)
        {
            rezView = true;
            if (doCleanHistory)
            {
                num = double.Parse(input.Text);
                history.Text = input.Text + '+';
                doCleanHistory = false;
            }
            else
            {
                if (history.Text.Length == 0)
                {
                    num = double.Parse(input.Text);
                    history.Text = input.Text + "+";
                }
                else
                {
                    doing();
                    input.Text = history.Text;
                    num = double.Parse(history.Text);
                    history.Text += "+";
                }
            }
            act = '+';
        }

        private void minus_Click(object sender, EventArgs e)
        {
            rezView = true;
            if (doCleanHistory)
            {
                num = double.Parse(input.Text);
                history.Text = input.Text + '-';
                doCleanHistory = false;
            }
            else
            {
                if (history.Text.Length == 0)
                {
                    num = double.Parse(input.Text);
                    history.Text = input.Text + "-";
                }
                else
                {
                    doing();
                    input.Text = history.Text;
                    num = double.Parse(history.Text);
                    history.Text += "-";
                }
            }
            act = '-';
        }

        private void multiplication_Click(object sender, EventArgs e)
        {
            rezView = true;
            if (doCleanHistory)
            {
                num = double.Parse(input.Text);
                history.Text = input.Text + '*';
                doCleanHistory = false;
            }
            else
            {
                if (history.Text.Length == 0)
                {
                    num = double.Parse(input.Text);
                    history.Text = input.Text + "*";
                }
                else
                {
                    doing();
                    input.Text = history.Text;
                    num = double.Parse(history.Text);
                    history.Text += "*";
                }
            }
            act = '*';
        }

        private void division_Click(object sender, EventArgs e)
        {
            rezView = true;
            if (doCleanHistory)
            {
                num = double.Parse(input.Text);
                history.Text = input.Text + '/';
                doCleanHistory = false;
            }
            else
            {
                if (history.Text.Length == 0)
                {
                    num = double.Parse(input.Text);
                    history.Text = input.Text + "/";
                }
                else
                {
                    doing();
                    input.Text = history.Text;
                    num = double.Parse(history.Text);
                    history.Text += "/";
                }
            }
            act = '/';
        }

        private void equals_Click(object sender, EventArgs e)
        {
            rezView = true;
            doing();
            string answer = history.Text;
            history.Text = Convert.ToString(num) + act + input.Text + '=';
            input.Text = answer;
            doCleanHistory = true;
        }

        private void doing()
        {
            switch (act)
            {
                case '+':
                    history.Text = Convert.ToString(num + double.Parse(input.Text));
                    break;
                case '-':
                    history.Text = Convert.ToString(num - double.Parse(input.Text));
                    break;
                case '*':
                    history.Text = Convert.ToString(num * double.Parse(input.Text));
                    break;
                case '/':
                    history.Text = Convert.ToString(num / double.Parse(input.Text));
                    break;
            }
        }
    }
}