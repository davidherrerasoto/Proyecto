using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.button1.Location = new Point(100, 50);
            this.button2.Location = new Point(this.button1.Location.X + this.Size.Width / 3 + 10, this.button1.Location.Y);
            this.button3.Location = new Point(this.button2.Location.X + this.Size.Width / 3 + 10, this.button2.Location.Y);
            this.button4.Location = new Point(this.button3.Location.X + this.Size.Width / 3 + 10, this.button3.Location.Y);
            this.button5.Location = new Point(100, (this.Height / 2 + 100));
            this.button6.Location = new Point(this.button5.Location.X + this.Size.Width / 3 + 10, this.button5.Location.Y);
            this.button7.Location = new Point(this.button6.Location.X + this.Size.Width / 3 + 10, this.button6.Location.Y);
            this.button8.Location = new Point(this.button7.Location.X + this.Size.Width / 3 + 10, this.button7.Location.Y);

            this.button1.SetBounds(this.button1.Location.X, this.button1.Location.Y, this.Size.Width / 3, this.Size.Height / 2);
            this.button2.SetBounds(this.button2.Location.X, this.button2.Location.Y, this.Size.Width / 3, this.Size.Height / 2);
            this.button3.SetBounds(this.button3.Location.X, this.button3.Location.Y, this.Size.Width / 3, this.Size.Height / 2);
            this.button4.SetBounds(this.button4.Location.X, this.button4.Location.Y, this.Size.Width / 3, this.Size.Height / 2);
            this.button5.SetBounds(this.button5.Location.X, this.button5.Location.Y, this.Size.Width / 3, this.Size.Height / 2);
            this.button6.SetBounds(this.button6.Location.X, this.button6.Location.Y, this.Size.Width / 3, this.Size.Height / 2);
            this.button7.SetBounds(this.button7.Location.X, this.button7.Location.Y, this.Size.Width / 3, this.Size.Height / 2);
            this.button8.SetBounds(this.button8.Location.X, this.button8.Location.Y, this.Size.Width / 3, this.Size.Height / 2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        Form2 forma2 = null;
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (forma2 == null)
            {
                forma2 = new Form2();
                forma2.Show();
                forma2.FormClosed += instanceHasBeenClosed;
            }else
            {
                forma2.Focus();
            }
        }

        private void instanceHasBeenClosed(object sender, FormClosedEventArgs e)
        {
            forma2 = null;
            movs = null;
            citas = null;
            
        }

        Movimientos movs = null;
        private void button2_Click(object sender, EventArgs e)
        {

            if (movs == null)
            {
                movs = new Movimientos();
                movs.Show();
                movs.FormClosed += instanceHasBeenClosed;
            }
            else
            {
                movs.Focus();
            }
        }

        Citas citas = null;
        private void button3_Click(object sender, EventArgs e)
        {
            if (citas == null)
            {
                citas = new Citas();
                citas.Show();
                citas.FormClosed += instanceHasBeenClosed;
            }
            else
            {
                citas.Focus();
            }
        }
    }
}
