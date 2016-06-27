﻿/*
    This file is part of libEDSsharp.

    libEDSsharp is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Foobar is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with libEDSsharp.  If not, see <http://www.gnu.org/licenses/>.
 
    Copyright(c) 2016 Robin Cornelius <robin.cornelius@gmail.com>
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libEDSsharp;

namespace EDSTest
{
    public partial class NewIndex : Form
    {
        public UInt16 index;
        public ObjectType ot;
        public string name;
        public DataType dt = DataType.UNKNOWN;
        public byte nosubindexes;
        EDSsharp eds;

        public NewIndex(EDSsharp eds)
        {
            this.eds = eds;

            InitializeComponent();

            foreach (DataType foo in Enum.GetValues(typeof(DataType)))
            {
                comboBox_datatype.Items.Add(foo.ToString());
            }

            DialogResult = DialogResult.Cancel;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            index = (UInt16) numericUpDown_index.Value;
            ot = ObjectType.UNKNOWN;

            if (radioButton_array.Checked)
                ot = ObjectType.ARRAY;
            if (radioButton_var.Checked)
                ot = ObjectType.VAR;
            if (radioButton_rec.Checked)
                ot = ObjectType.REC;

            name = textBox_name.Text;

            nosubindexes = (byte)numericUpDown_subindexes.Value;

            dt = (DataType) Enum.Parse(typeof(DataType), comboBox_datatype.SelectedItem.ToString());


            if (eds.ods.ContainsKey(index))
            {
                MessageBox.Show(String.Format("Index 0x{0:x4} already exists in OD", index));
                return;
            }

            if(dt == DataType.UNKNOWN)
            {
                MessageBox.Show(String.Format("Please select a datatype"));
                return;
            }

            DialogResult = DialogResult.OK;
            Close();

        }

        private void radioButton_var_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_subindexes.Enabled = false;
        }

        private void radioButton_array_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_subindexes.Enabled = true;
        }

        private void radioButton_rec_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_subindexes.Enabled = true;
        }
    }
}