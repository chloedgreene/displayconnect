using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sc.Util.Rendering;

namespace DisplayControler
{
    public partial class Form1 : Form
    {

        private static readonly HttpClient client = new HttpClient();
        string RaspberryPI_IP = "";
        List<Button> buttons = new List<Button>();
        Color SelectedColour = Color.White;

        public Button GetButton(int x,int y)
        {

            return buttons[x*8+y];

        }

        float map(float s, float a1, float a2, float b1, float b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }

        private static String HexConverter(System.Drawing.Color c)
        {
            return c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString();
        }

        public Form1()
        {
            InitializeComponent();

            int ButtonWidth = 40;
            int ButtonHeight = 40;
            int Distance = 20;
            int start_x = 10;
            int start_y = 10;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    Button tmpButton = new Button();
                    tmpButton.Top = start_x + (x * ButtonHeight + Distance);
                    tmpButton.Left = start_y + (y * ButtonWidth + Distance);
                    tmpButton.Width = ButtonWidth;
                    tmpButton.Height = ButtonHeight;
                    tmpButton.BackColor = Color.White;
                    tmpButton.Text = y + "," + x;
                    // Possible add Buttonclick event etc..

                    tmpButton.Click += new EventHandler(gridbutton_Click);

                    buttons.Add(tmpButton);

                    this.Controls.Add(tmpButton);

                }

            }

            //button2.Hide();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("plain/text"));
            //Console.WriteLine(client.GetStringAsync($"http://{RaspberryPI_IP}:1880/api/init").Result);

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 64;

            timer1.Tick += dispatcherTimer_Tick;
            timer1.Interval = 1;
            timer1.Start();

            timer2.Tick += offsetTimer_Tick;
            timer2.Interval = 20;
            timer2.Start();

        }

        private void offsetTimer_Tick(object sender, EventArgs e) {



        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {


            if (checkBox2.Checked) {

                //checkBox1.Checked = true;
                int i = 0;

                foreach (Button b in buttons) {

                    i++;

                    

                    SelectedColour = SimpleColorTransforms.HsLtoRgb(map(i,0,64,0,360),98,0.5);

                    b.BackColor = SelectedColour;

                    int x = Int32.Parse(b.Text.Split(',')[0]);
                    int y = Int32.Parse(b.Text.Split(',')[1]);

                    



                }

               // Console.WriteLine("Working");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;

                SelectedColour = colorDialog1.Color;
            }
        }

        private void gridbutton_Click(object sender, EventArgs e) {


            Button OrgButton = (Button)sender;

            OrgButton.BackColor = SelectedColour;

            if (checkBox1.Checked) {

                int x = Int32.Parse(OrgButton.Text.Split(',')[0]);
                int y = Int32.Parse(OrgButton.Text.Split(',')[1]);





                try
                {
                    Console.WriteLine(client.GetStringAsync($"http://{RaspberryPI_IP}:1880/api/pixel?x={x}&y={y}&c={OrgButton.BackColor.R},{OrgButton.BackColor.G},{OrgButton.BackColor.B}").Result);
                }
                catch
                {
                    
                    MessageBox.Show("INVALID IP OR NETWORK ERROR");
                    return;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {



            
            foreach(Button b in buttons)
            {

                    int x = Int32.Parse(b.Text.Split(',')[0]);
                    int y = Int32.Parse(b.Text.Split(',')[1]);

                try
                {
                    Console.WriteLine(client.GetStringAsync($"http://{RaspberryPI_IP}:1880/api/pixel?x={x}&y={y}&c={b.BackColor.R},{b.BackColor.G},{b.BackColor.B}").Result);
                }
                catch {

                    MessageBox.Show("INVALID IP OR NETWORK ERROR");
                    break;
                }
                    //Thread.Sleep(25);


                progressBar1.Value++;

            }
            Thread.Sleep(250);
            progressBar1.Value = 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Button b in buttons) {

                b.BackColor = Color.White;

                b.PerformClick();

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            RaspberryPI_IP = textBox1.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(client.GetStringAsync($"http://{RaspberryPI_IP}:1880/api/text?t={textBox2.Text}").Result);
            }
            catch
            {

                MessageBox.Show("INVALID IP OR NETWORK ERROR");
               
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
