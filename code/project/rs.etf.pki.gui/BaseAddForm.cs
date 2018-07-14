using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PKI_project
{
    public partial class BaseAddForm : Form
    {
        private int type = 0;
        private string selection = string.Empty;
        private string[] selectionList = null;

        private string caption = string.Empty;

        public string Caption
        {
            get { return caption; }
            set { caption = value; }
        }

        private AdminApplication caller;

        public BaseAddForm()
        {
            InitializeComponent();

            clearListBoxIndexes();
        }

        private void clearListBoxIndexes()
        {
            allListBox.SelectedIndex = -1;
            selectedListBox.SelectedIndex = -1;
        }

        public string[] getSelected()
        {
            List<string> list = new List<string>();

            for (int i = 0; i < selectedListBox.Items.Count; i++)
            {
                list.Add(selectedListBox.Items[i].ToString());
            }

            return list.ToArray();
        }

        public void setCaller(AdminApplication caller)
        {
            this.caller = caller;
        }

        public void setAllItemsinAllListBox(string[] all)
        {
            allListBox.Items.Clear();
            allListBox.Items.AddRange(all);
        }

        public void setAllItemsinSelectedListBox(string[] selected)
        {
            selectedListBox.Items.Clear();
            selectedListBox.Items.AddRange(selected);
        }

        public void clearSelected()
        {
            selectedListBox.Items.Clear();
            clearListBoxIndexes();
        }

        /*
         * parameters:
         * enabled - if cancel button is to be enabled
         * selection - classroom for which we add personnel or other way around
         * type - 0 if we add from visible to non visible, 1 if we create classroom,
         *        2 if we add/remove personnel
         *        -1 if non of the above
         */
        public void setCancelButtonEnabled(bool enabled, string selection, int type)
        {
            this.cancelButton.Enabled = enabled;
            this.selection = selection;
            this.type = type;

            this.Text = this.Caption + " " + selection;
        }

        public void setCancelButtonEnabled(bool enabled, string[] selectedList)
        {
            this.cancelButton.Enabled = enabled;

            this.selectionList = selectedList;
            this.Text = this.Caption + " " + selectedList[0];
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (this is AddPersonnel)
            {
                // if this is AddPersonnel form
                if (!selection.Equals(string.Empty))
                {
                    if (type == 0) caller.addToVisibleClassroomFinish(this.selection);
                    else if (type == 2) caller.personnelChanged(this.selection);

                    this.Hide();
                }
                else if (selection.Equals(string.Empty))
                {
                    this.Hide();
                }
                else if (selectionList != null && selectionList.Length > 0)
                {
                    this.clearSelected();
                    this.Hide();

                    caller.addToVisibleClassroomFinish(this.selectionList[0]);

                    List<string> temp = selectionList.ToList();
                    temp.Remove(selectionList[0]);

                    selectionList = temp.ToArray();

                    if (selectionList.Length != 0)
                    {
                        this.Show();
                        this.setCancelButtonEnabled(false, this.selectionList);
                    }
                    else
                    {
                        this.clearSelected();
                        this.Hide();
                    }
                }
            }
            else
            {
                // if this is addClassrooms form
                this.Hide();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.clearSelected();
            this.Hide();
        }

        private void removeAllButton_Click(object sender, EventArgs e)
        {
            selectedListBox.Items.Clear();
        }

        private void addAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < allListBox.Items.Count; i++)
            {
                if (!selectedListBox.Items.Contains(allListBox.Items[i].ToString()))
                {
                    selectedListBox.Items.Add(allListBox.Items[i].ToString());
                }
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (selectedListBox.SelectedIndex != -1)
            {
                selectedListBox.Items.Remove(selectedListBox.Items[selectedListBox.SelectedIndex]);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (allListBox.SelectedIndex != -1)
            {
                if (!selectedListBox.Items.Contains((string)allListBox.SelectedItem))
                {
                    selectedListBox.Items.Add((string)allListBox.SelectedItem);
                }
                else
                {
                    selectedListBox.SelectedItem = (string)allListBox.SelectedItem;
                }
            }
        }

    }
}
