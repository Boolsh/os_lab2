using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace os_lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string dir1 = txtDir1.Text;
            string dir2 = txtDir2.Text;
            int m = int.Parse(txtM.Text);

            CatalogProcessor p1 = new CatalogProcessor(dir1, m);
            CatalogProcessor p2 = new CatalogProcessor(dir2, m);

            Thread t1 = new Thread(() =>
            {
                p1.Process();
            });

            Thread t2 = new Thread(() =>
            {
                p2.Process();
            });

            t1.Start();
            t2.Start();

            // Агрегирующий поток
            Thread aggregator = new Thread(() =>
            {
                t1.Join();
                t2.Join();

                // Получаем результаты и обновляем форму
                Invoke(new Action(() =>
                {
                    lblResult1.Text = $"Каталог 1: {p1.ResultCount}";
                    lblResult2.Text = $"Каталог 2: {p2.ResultCount}";

                    if (p1.ResultCount > p2.ResultCount)
                        lblCompare.Text = "Каталог 1 имеет больше подкаталогов с числом файлов > m.";
                    else if (p1.ResultCount < p2.ResultCount)
                        lblCompare.Text = "Каталог 2 имеет больше таких подкаталогов.";
                    else
                        lblCompare.Text = "Оба каталога одинаковы.";
                }));
            });

            aggregator.Start();
        }
    }
}
