using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace lab_11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FileInfo file = new FileInfo("file.txt");

        }

        public struct NOTE
        {
            public string NAME;
            public string TEL;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo("file.txt");

            if (file.Exists == false)
            {
                file.Create();
            }
            else MessageBox.Show("Такий файл вже створений!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo("file.txt");
            if (file.Exists == true)
            {
                file.Delete();
            }
            else MessageBox.Show("Такого файлу не існує!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamReader streamReader = new StreamReader("file.txt");
            string str = "";

            while (!streamReader.EndOfStream)
            {
                str += streamReader.ReadLine();
            }
            textBox1.Text = str;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StreamReader streamReader = new StreamReader("file.txt");
            string str = "";

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                if (line.Contains("NAME:") && line.Contains("TEL:"))
                {
                    str += line + Environment.NewLine;
                }
            }
            textBox1.Text = str;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;
            string[] numberStrings = inputText.Split(',');

            List<int> numbers = new List<int>();

            foreach (string numStr in numberStrings)
            {
                if (int.TryParse(numStr.Trim(), out int number))
                {
                    numbers.Add(number);
                }
                else
                {
                    MessageBox.Show($"Неправильний формат введення: {numStr}");
                    return;
                }
            }

            if (numbers.Count > 0)
            {
                FileInfo file = new FileInfo("file.txt");

                using (StreamWriter write_text = file.AppendText())
                {
                    write_text.WriteLine(string.Join(", ", numbers));
                }

                MessageBox.Show("Масив був записаний в файл!");
            }
            else
            {
                MessageBox.Show("Не вдалося зчитати жодне число :(");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;

            string[] parts = inputText.Split(',');

            if (parts.Length == 2)
            {
                NOTE newNote = new NOTE
                {
                    NAME = parts[0].Trim(),
                    TEL = parts[1].Trim()
                };

                StreamWriter write_text;

                FileInfo file = new FileInfo("file.txt");

                write_text = file.AppendText();

                write_text.WriteLine($"NAME: {newNote.NAME}, TEL: {newNote.TEL}");

                write_text.Close();
            }
            else
            {
                MessageBox.Show("Неправильний формат введення. Використовуйте кому для розділення імені та номера телефону.");
            }
        }
    }
}