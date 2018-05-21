using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poker_interface
{
    public partial class Form1 : Form
    {
        Button hold_button = null;
        List<Button> holded_poker = new List<Button>();
        List<String> button_string = new List<string>();
        List<String> mycards = new List<string>();
        List<String> mytop = new List<string>();
        List<String> mymid = new List<string>();
        List<String> mybottom = new List<string>();
        List<String> top = new List<string>();
        List<String> mid = new List<string>();
        List<String> other = new List<string>();
        List<String> othercards = new List<string>();
        List<String> bottom = new List<string>();
        List<Button> holded_button = new List<Button>();
        DataTable playdata_3 = ExReader.GetDataFromExcelByCom(false, 3);
        DataTable playdata_2 = ExReader.GetDataFromExcelByCom(false, 2);
        public Form1()
        {
            InitializeComponent();
            
            for (int i = 0; i < 43; i++)
            {
                button_string.Add("button" +(i+1).ToString());
            }

            List<Button> lColors = Controls.OfType<Button>().ToList();
            for (int i = 0; i < lColors.Count; i++)
            {
                if (button_string.Contains(lColors[i].Name))
                {
                    
                    lColors[i].MouseDown += new MouseEventHandler(button_Click);
                }
                else
                {
                    if (lColors[i].Name == "button96" || lColors[i].Name == "button97")
                    {
                        
                    }
                    else
                    {
                        lColors[i].Click += new EventHandler(poker_Click);
                    }
                    
                }
                
            }
        }
        private void button_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                foreach (Button b in holded_poker)
                {
                    if ((sender as Button).Text == b.Text)
                    {
                        b.Visible = true;
                    }
                }
                (sender as Button).Text = null;
                (sender as Button).BackgroundImage = null;
            }
            
            if (e.Button == MouseButtons.Left)
            {
                if (hold_button != null)
                {
                    hold_button.FlatStyle = FlatStyle.Standard;
                }
                hold_button = (Button)sender;
                hold_button.FlatStyle = FlatStyle.Flat;
                if(holded_button.Contains((sender as Button)))
                {
                    
                }
                else
                {
                    holded_button.Add((sender as Button));
                }
                
                
            }

        }

        private void poker_Click(object sender, EventArgs e)
        {
            if (hold_button != null)
            {
                foreach (Button b in holded_poker)
                {
                    if (hold_button.Text == b.Text)
                    {
                        b.Visible = true;
                    }
                }
                hold_button.BackgroundImage = (sender as Button).BackgroundImage;
                hold_button.Text = (sender as Button).Text;
                (sender as Button).Visible = false;
                hold_button.FlatStyle = FlatStyle.Standard;
                hold_button = null;

                holded_poker.Add((sender as Button));
            }
            
        }        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button96_Click(object sender, EventArgs e)
        {
            for(int i= 0; i < holded_poker.Count;i++)
            {
                holded_poker[i].Visible = true;
                
            }
            for (int i = 0; i < holded_button.Count; i++)
            {
                holded_button[i].Text = null;
                holded_button[i].BackgroundImage = null;


            }
            label1.Text = "Top: ";
            label2.Text = "Mid: ";
            label3.Text = "Bottom: ";
            mytop = new List<string>(); mybottom = new List<string>(); mymid = new List<string>(); holded_poker = new List<Button>(); holded_button = new List<Button>();
        }

        private void button97_Click(object sender, EventArgs e)
        {
            mytop = new List<string>(); mybottom = new List<string>(); mymid = new List<string>(); othercards = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                top.Add("button" + (i + 1).ToString());
            }
            for (int i = 3; i < 8; i++)
            {
                mid.Add("button" + (i + 1).ToString());
            }
            for (int i = 8; i < 13; i++)
            {
                bottom.Add("button" + (i + 1).ToString());
            }
            for (int i = 13; i < 95; i++)
            {
                other.Add("button" + (i + 1).ToString());
            }
            label1.Text = "Top: ";
            label2.Text = "Mid: ";
            label3.Text = "Bottom: ";
            if (holded_button != null)
            {
                foreach (Button b in holded_button)
                {
                    if (top.Contains(b.Name))
                    {
                        if (b.Text != "")
                        {
                            mytop.Add(b.Text);
                        }
                        
                    }
                    if (mid.Contains(b.Name))
                    {
                        if (b.Text != "")
                        {
                            mymid.Add(b.Text);
                        }
                        
                    }
                    if (bottom.Contains(b.Name))
                    {
                        if (b.Text != "")
                        {
                            mybottom.Add(b.Text);
                            
                        }
                        
                    }
                    if (other.Contains(b.Name))
                    {
                        if (b.Text != "")
                        {
                            othercards.Add(b.Text);
                            
                        }

                    }
                    
                }
            }
            if (mytop.Count != 0 && mytop.Count !=3)
            {
                label1.Text += top_probability.calculator(mytop, new List<String>(othercards.Concat(mybottom).Concat(mymid).ToList()));
                
            }
            if (mymid.Count != 0 && mymid.Count != 5)
            {
                label2.Text += Probability.calculator(mymid, new List<String>(othercards.Concat(mybottom).Concat(mytop).ToList()));
            }
            if (mybottom.Count != 0 && mybottom.Count != 5)
            {
                label3.Text += Probability.calculator(mybottom, new List<String>(othercards.Concat(mymid).Concat(mytop).ToList()));
            }
            
            
            


        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
