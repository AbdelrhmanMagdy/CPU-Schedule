﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPU_Scheduling
{
    public partial class MainForm : Form
    {
        private int mode;

        public MainForm()
        {
            InitializeComponent();
            quantamBox.Hide();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            int processNumber;

            dataGridView1.Rows.Clear();
            processNumber = Convert.ToInt32(textBox1.Text);

            for (int i = 0; i < processNumber; i++)
            {
                String[] data = {"Process " + (i + 1).ToString(), "", ""};

                dataGridView1.Rows.Add(data);
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            String name;
            int arrivalTime, cpuBurst;
            if (checking())
            {
                int length = dataGridView1.Rows.Count;
                Process[] processArray = new Process[length];
                for (int i = 0; i < length; i++)
                {
                    name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "") { MessageBox.Show("Please Fill the empty cells"); return; }
                    if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "") { MessageBox.Show("Please Fill the empty cells"); return; }
                    arrivalTime = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    cpuBurst = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    int priority;
                    if (mode == 4 || mode == 5)
                    {
                        priority = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        processArray[i] = new Process(name, arrivalTime, cpuBurst, priority);
                    }
                    else processArray[i] = new Process(name, arrivalTime, cpuBurst);
                }

                SchedulingAlgorithm scheduler = new FirstComeFirstServed(processArray);
                switch (mode)
                {
                    case 0:
                        scheduler = new FirstComeFirstServed(processArray);
                        MessageBox.Show("FCFS Algorithm is selected!");
                        break;
                    case 1:
                        scheduler = new ShortestTimeFirst(processArray);
                        MessageBox.Show("STF Algorithm is selected!");
                        break;
                    case 2:
                        scheduler = new ShortestRemainingTimeFirst(processArray);
                        MessageBox.Show("SRTF Algorithm is selected!");
                        break;
                    case 3:
                        int quantam = Convert.ToInt32(quantamBox.Text);
                        scheduler = new RoundRobin(quantam,processArray);
                        MessageBox.Show("RR Algorithm is selected!");
                        break;
                    case 4:
                        scheduler = new PriorityNonPreemptive(processArray);
                        MessageBox.Show("Priority Non-Preemptive Algorithm is selected!");
                        break;
                    case 5:
                        scheduler = new PriorityPreemptive(processArray);
                        MessageBox.Show("Priority Preemptive Algorithm is selected!");
                        break;
                }

                scheduler.Run();
                DataTable processData = scheduler.GetProcessData();
                DataTable eventData = scheduler.GetEventData();

                this.Hide();

                ProcessResultForm prf = new ProcessResultForm(processData, eventData);
                prf.ShowDialog();

                this.Show();
            }
        }

        private void radioButtonFCFS_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFCFS.Checked)
                mode = 0;
        }

        private void radioButtonSTF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSTF.Checked)
                mode = 1;
        }

        private void radioButtonSRTF_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSRTF.Checked)
                mode = 2;
        }

        private void radioButtonRR_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRR.Checked)
                quantamBox.Show();
                mode = 3;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPNP.Checked)
                mode = 4;
        }

        private void radioButtonPP_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPP.Checked)
                mode = 5;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public bool checking()
        {
            int length = dataGridView1.Rows.Count;
            int check = 1;
            for (int i = 0; i < length; i++)
            {
                if ((mode == 4 || mode == 5) && dataGridView1.Rows[i].Cells[3].Value == null /* && string.IsNullOrEmpty(dataGridView1.Rows[i].Cells[3].Value.ToString())*/)
                {
                    check = 0;
                    MessageBox.Show("Please Enter Priority");
                    break;
                }
                else if (dataGridView1.Rows[i].Cells[1].Value == null || dataGridView1.Rows[i].Cells[2].Value == null)
                {
                    check = 0;
                    MessageBox.Show("Please check on your inputs");
                    break;
                }
            }

            if (check == 1) return true;
            else return false;
        }
    }
}
