﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diogenes
{
    public partial class GenericPicker : Form
    {
        private string[][] Table;
        private string[] Headers;
        public string[] SelectedResult;

        public GenericPicker(string[][] table, string[] headers)
        {
            InitializeComponent();
            Table = table;
            Headers = headers;
        }

        private void PresentRows()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Index", typeof(int));
            foreach (string header in Headers)
            {
                dt.Columns.Add(header, typeof(String));
            }
            dgvMain.DataSource = dt;

            for (int i = 0; i < Table.Length; i++)
            {
                List<string> row = new List<string>(Table[i]);
                row.Insert(0, i.ToString());
                dt.Rows.Add(row.ToArray());
            }

            for (int i = 1; i < Headers.Length; i++)
            {
                dgvMain.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            dgvMain.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvMain.Columns[Headers.Length].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMain.Columns[0].Visible = false;
        }

        private void GenericPicker_Load(object sender, EventArgs e)
        {
            PresentRows();
        }

        private void dgvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.SelectedRows.Count == 1)
            {
                int selectedRowIndex = int.Parse(dgvMain.SelectedRows[0].Cells[0].Value.ToString());
                SelectedResult = Table[selectedRowIndex];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}