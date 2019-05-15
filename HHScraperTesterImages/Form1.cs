using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HHScraperTesterImages
{
    public partial class Form1 : Form
    {
        public List<string> imagePaths = new List<string>();
        public int pointer = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private Random rnd = new Random();
        private void Button1_Click(object sender, EventArgs e)
        {
            List<string> thumbnails = new List<string>();
            System.IO.Directory.CreateDirectory("hhImages");

            string baseDirectory = "..\\..\\..\\HHScraperTester\\bin\\Release\\hh";
            var allSeries = System.IO.Directory.GetDirectories(baseDirectory);

            foreach(var series in allSeries)
            {
                List<string> linksText = System.IO.File.ReadAllLines(series + "\\links.txt").ToList();
                thumbnails.AddRange(linksText.Where(x => x.Contains("thumbnail: ")).ToList());
            }

            int thumbIndex = 0;
            foreach(var thumb in thumbnails)
            {
                string thumbnail = thumb.Replace("thumbnail: ", string.Empty);
                try
                {
                    if (thumbnail != string.Empty && !thumbnail.Contains("hh-black-bkkg.jpg"))
                    {
                        Thread.Sleep(rnd.Next(250, 600));
                        pictureBox1.Load(thumbnail);
                        pictureBox1.Update();
                        label1.Text = thumbIndex.ToString() + "/" + thumbnails.Count();
                        label1.Update();
                        pictureBox1.Image.Save("hhImages\\" + thumbIndex + ".png");
                        thumbIndex++;
                    }
                }
                catch (Exception) { }
                
                    
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            pointer++;
            if(pointer >= imagePaths.Count())
            {
                pointer = 0;
            }
            label1.Text = pointer.ToString();
            pictureBox1.Image = new Bitmap(imagePaths[pointer]);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            pointer--;
            if (pointer <= 0)
            {
                pointer = imagePaths.Count()-1;
            }
            label1.Text = pointer.ToString();
            pictureBox1.Image = new Bitmap(imagePaths[pointer]);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (System.IO.Directory.Exists("hhImages"))
                imagePaths = System.IO.Directory.GetFiles("hhImages").ToList();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Process.Start("hhImages");
        }
    }
}
