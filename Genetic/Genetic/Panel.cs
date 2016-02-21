using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Genetic.Classes;
using System.Collections;

namespace Genetic
{
    public partial class Panel : Form
    {
        List<Animal> animais = new List<Animal>();
        List<Animal> Saude = new List<Animal>();
        int populacaoini = 0;
        int geracao;
        bool auto = false;
        Random r = new Random();

        public Panel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            Color c = colorDialog1.Color;
            panelCor.BackColor = c;
        }

        private void selecaoNatural()
        {
            Saude = new List<Animal>();

            for (int i = 0; i < animais.Count; i++)
            {
                int r = Convert.ToInt32(animais[i].DNA[0], 2);
                int g = Convert.ToInt32(animais[i].DNA[1], 2);
                int b = Convert.ToInt32(animais[i].DNA[2], 2);

                r = panelCor.BackColor.R - r;
                g = panelCor.BackColor.G - g;
                b = panelCor.BackColor.B - b;

                if (r < 0)
                {
                    r *= -1;
                }

                if (g < 0)
                {
                    g *= -1;
                }

                if (b < 0)
                {
                    b *= -1;
                }

                animais[i].Saude = r + g + b;

                Saude.Add(animais[i]);
            }

            Saude.Sort(delegate (Animal a1, Animal a2)
            {
                return a1.Saude.CompareTo(a2.Saude);
            });


            for (int i = Saude.Count / 5; i < Saude.Count; i++)
            {
                animais.Remove(Saude[i]);
            }
        }

        private void getbest()
        {
            int r = Convert.ToInt32(Saude[0].DNA[0], 2);
            int g = Convert.ToInt32(Saude[0].DNA[1], 2);
            int b = Convert.ToInt32(Saude[0].DNA[2], 2);

            panelCorAnimal.BackColor = Color.FromArgb(r, g, b);


            r = Convert.ToInt32(Saude[1].DNA[0], 2);
            g = Convert.ToInt32(Saude[1].DNA[1], 2);
            b = Convert.ToInt32(Saude[1].DNA[2], 2);

            panel1.BackColor = Color.FromArgb(r, g, b);

            r = Convert.ToInt32(Saude[2].DNA[0], 2);
            g = Convert.ToInt32(Saude[2].DNA[1], 2);
            b = Convert.ToInt32(Saude[2].DNA[2], 2);

            panel2.BackColor = Color.FromArgb(r, g, b);


            r = Convert.ToInt32(Saude[3].DNA[0], 2);
            g = Convert.ToInt32(Saude[3].DNA[1], 2);
            b = Convert.ToInt32(Saude[3].DNA[2], 2);

            panel3.BackColor = Color.FromArgb(r, g, b);

            r = Convert.ToInt32(Saude[4].DNA[0], 2);
            g = Convert.ToInt32(Saude[4].DNA[1], 2);
            b = Convert.ToInt32(Saude[4].DNA[2], 2);

            panel4.BackColor = Color.FromArgb(r, g, b);

            r = Convert.ToInt32(Saude[Saude.Count - 1].DNA[0], 2);
            g = Convert.ToInt32(Saude[Saude.Count - 1].DNA[1], 2);
            b = Convert.ToInt32(Saude[Saude.Count - 1].DNA[2], 2);

            panel5.BackColor = Color.FromArgb(r, g, b);

            r = Convert.ToInt32(Saude[Saude.Count - 2].DNA[0], 2);
            g = Convert.ToInt32(Saude[Saude.Count - 2].DNA[1], 2);
            b = Convert.ToInt32(Saude[Saude.Count - 2].DNA[2], 2);

            panel6.BackColor = Color.FromArgb(r, g, b);

            r = Convert.ToInt32(Saude[Saude.Count - 3].DNA[0], 2);
            g = Convert.ToInt32(Saude[Saude.Count - 3].DNA[1], 2);
            b = Convert.ToInt32(Saude[Saude.Count - 3].DNA[2], 2);

            panel7.BackColor = Color.FromArgb(r, g, b);

            r = Convert.ToInt32(Saude[Saude.Count - 4].DNA[0], 2);
            g = Convert.ToInt32(Saude[Saude.Count - 4].DNA[1], 2);
            b = Convert.ToInt32(Saude[Saude.Count - 4].DNA[2], 2);

            panel8.BackColor = Color.FromArgb(r, g, b);

            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }

        private void cruzamento()
        {
            while (animais.Count != populacaoini)
            {
                int index1 = r.Next(0, animais.Count - 1);
                int index2 = r.Next(0, animais.Count - 1);

                Animal animal = animais[index1].cruzar(animais[index2]);
                animais.Add(animal);
            }
        }

        private void timerSelecao_Tick(object sender, EventArgs e)
        {
            geracao++;

            this.BackColor = Color.Gray;
            selecaoNatural();
            cruzamento();
            getbest();
            this.BackColor = Color.White;

            labelGeracao.Text = "Geração: " + geracao;

            if (!auto)
            {
                timerSelecao.Enabled = false;
            }

        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            populacaoini = Int32.Parse(textBox2.Text);

            if (animais.Count == 0)
            {
                while (animais.Count != populacaoini)
                {
                    Animal a = new Animal();

                    int R = r.Next(0, 255);
                    int G = r.Next(0, 255);
                    int B = r.Next(0, 255);

                    a.Randomizar(R, G, B);

                    animais.Add(a);
                }
            }
            else
            {
                timerSelecao.Enabled = !timerSelecao.Enabled;
            }

            timerSelecao.Enabled = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Height, pictureBox1.Width);
            Graphics gra = Graphics.FromImage(image);

            int a = 0;


            for (int y = 0; y <= pictureBox1.Height; y += 10)
            {

                for (int x = 0; x <= pictureBox1.Width; x += 10)
                {
                    try
                    {
                        Brush brus = new SolidBrush(Color.FromArgb(Convert.ToInt32(Saude[a].DNA[0], 2), Convert.ToInt32(Saude[a].DNA[1], 2), Convert.ToInt32(Saude[a].DNA[2], 2)));
                        gra.FillRectangle(brus, x, y, 10, 10);
                    }
                    catch
                    {
                        break;
                    }

                    a++;

                    if (a >= Saude.Count)
                        break;
                }
            }

            pictureBox1.Image = image;
        }

        private void Panel_Load(object sender, EventArgs e)
        {

        }
    }
}
