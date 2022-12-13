using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KURSACH_UD
{
    public partial class MainForm : Form
    {
        private SqlConnection sqlConnection = null;
       
        private SqlCommandBuilder sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;
        private string NameTable = "Orderr";
        public int ColInd = -1;
        private bool newRowAdding = false;

        public MainForm()
        {
            InitializeComponent();

        }
        private void Load_Data()
        {
            try
            {
                ColInd = 7;
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'DELETE' AS [DELETE] FROM Orderr",sqlConnection);
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet,"Orderr");

                dataGridView1.DataSource = dataSet.Tables["Orderr"];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkcell = new DataGridViewLinkCell();

                    dataGridView1[ColInd, i] = linkcell;
                         
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void Reload_Data(string NameTable, int ColInd)
        {
            try
            {
                dataSet.Tables[NameTable].Clear();

                sqlDataAdapter.Fill(dataSet, NameTable);

                dataGridView1.DataSource = dataSet.Tables[NameTable];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkcell = new DataGridViewLinkCell();

                    dataGridView1[ColInd, i] = linkcell;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=rikimaru;Initial Catalog=phService;Integrated Security=True");
            sqlConnection.Open();
            Load_Data();
        }

        private void btnWed_Click(object sender, EventArgs e)
        {
            NameTable = "WeddingSalon";
            ColInd = 5;
            sqlDataAdapter = new SqlDataAdapter("SELECT *, 'DELETE' AS [DELETE] FROM WeddingSalon", sqlConnection);
            sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

            sqlDataAdapter.Fill(dataSet, "WeddingSalon");

            dataGridView1.DataSource = dataSet.Tables["WeddingSalon"];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewLinkCell linkcell = new DataGridViewLinkCell();

                dataGridView1[ColInd, i] = linkcell;

            }

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NameTable = "photographer_services";
            
            ColInd = 5;
            sqlDataAdapter = new SqlDataAdapter("SELECT *, 'DELETE' AS [DELETE] FROM photographer_services", sqlConnection);
            sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

            sqlDataAdapter.Fill(dataSet, "photographer_services");

            dataGridView1.DataSource = dataSet.Tables["photographer_services"];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewLinkCell linkcell = new DataGridViewLinkCell();

                dataGridView1[ColInd, i] = linkcell;

            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NameTable = "BanquetHall";
            ColInd = 6;
            
            sqlDataAdapter = new SqlDataAdapter("SELECT *, 'DELETE' AS [DELETE] FROM BanquetHall", sqlConnection);
            sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
            sqlDataAdapter.Fill(dataSet, "BanquetHall");

            dataGridView1.DataSource = dataSet.Tables["BanquetHall"];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewLinkCell linkcell = new DataGridViewLinkCell();

                dataGridView1[ColInd, i] = linkcell;

            }
       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NameTable = "Orderr";
            ColInd = 7;
            sqlDataAdapter = new SqlDataAdapter("SELECT *, 'DELETE' AS [DELETE] FROM Orderr", sqlConnection);
            sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);

            sqlDataAdapter.Fill(dataSet, "Orderr");

            dataGridView1.DataSource = dataSet.Tables["Orderr"];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewLinkCell linkcell = new DataGridViewLinkCell();

                dataGridView1[ColInd, i] = linkcell;

            }
          
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Reload_Data(NameTable,ColInd);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColInd)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[ColInd].Value.ToString();
                    if (task == "DELETE")
                    {
                        if (MessageBox.Show("Удалить эту строку?","Удаление",MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                            == DialogResult.Yes)
                        {
                            int RowIndex = e.RowIndex;
                            dataGridView1.Rows.RemoveAt(RowIndex);
                            dataSet.Tables[NameTable].Rows[RowIndex].Delete();
                            sqlDataAdapter.Update(dataSet, NameTable);
                        }
                    }
                    else if(task == "INSERT")
                    {
                        int rowind = dataGridView1.Rows.Count - 2;
                        DataRow row = dataSet.Tables[NameTable].NewRow();
                        if( NameTable == "Orderr")
                        {
                            row["CodeORDER"] = dataGridView1.Rows[rowind].Cells["CodeORDER"].Value;
                            row["FIOKlienta"] = dataGridView1.Rows[rowind].Cells["FIOKlienta"].Value;
                            row["DateWedding"] = dataGridView1.Rows[rowind].Cells["DateWedding"].Value;
                            row["Budget"] = dataGridView1.Rows[rowind].Cells["Budget"].Value;
                            row["CodeZala"] = dataGridView1.Rows[rowind].Cells["CodeZala"].Value;
                            row["CodeSalona"] = dataGridView1.Rows[rowind].Cells["CodeSalona"].Value;
                            row["CodePh"] = dataGridView1.Rows[rowind].Cells["CodePh"].Value;
                        }
                        else if(NameTable == "BanquetHall")
                        {
                            row["CodeZala"] = dataGridView1.Rows[rowind].Cells["CodeZala"].Value;
                            row["NameHall"] = dataGridView1.Rows[rowind].Cells["NameHall"].Value;
                            row["FIODirector"] = dataGridView1.Rows[rowind].Cells["FIODirector"].Value;
                            row["NumberHall"] = dataGridView1.Rows[rowind].Cells["NumberHall"].Value;
                            row["Address"] = dataGridView1.Rows[rowind].Cells["Address"].Value;
                            row["DateArenda"] = dataGridView1.Rows[rowind].Cells["DateArenda"].Value;
                           
                        }
                        else if (NameTable == "photographer_services")
                        {
                            row["CodePh"] = dataGridView1.Rows[rowind].Cells["CodePh"].Value;
                            row["FIOPh"] = dataGridView1.Rows[rowind].Cells["FIOPh"].Value;
                            row["NumberTel"] = dataGridView1.Rows[rowind].Cells["NumberTel"].Value;
                            row["Date_proved_merop"] = dataGridView1.Rows[rowind].Cells["Date_proved_merop"].Value;
                            row["Budget"] = dataGridView1.Rows[rowind].Cells["Budget"].Value;
                        }
                        else if (NameTable == "WeddingSalon")
                        {
                            row["CodeSalon"] = dataGridView1.Rows[rowind].Cells["CodeSalon"].Value;
                            row["NameSalon"] = dataGridView1.Rows[rowind].Cells["NameSalon"].Value;
                            row["FIODirector"] = dataGridView1.Rows[rowind].Cells["FIODirector"].Value;
                            row["TelSalon"] = dataGridView1.Rows[rowind].Cells["TelSalon"].Value;
                            row["Address"] = dataGridView1.Rows[rowind].Cells["Address"].Value;
                        }
                        dataSet.Tables[NameTable].Rows.Add(row);
                        dataSet.Tables[NameTable].Rows.RemoveAt(dataSet.Tables[NameTable].Rows.Count- 1);
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);

                        dataGridView1.Rows[e.RowIndex].Cells[ColInd].Value = "DELETE";
                        sqlDataAdapter.Update(dataSet, NameTable);
                        newRowAdding = false;
                    }
                    else if (task == "UPDATE")
                    {
                        int r = e.RowIndex;
                        if (NameTable == "Orderr")
                        {
                            dataSet.Tables[NameTable].Rows[r]["CodeORDER"] = dataGridView1.Rows[r].Cells["CodeORDER"].Value;
                            dataSet.Tables[NameTable].Rows[r]["FIOKlienta"] = dataGridView1.Rows[r].Cells["FIOKlienta"].Value;
                            dataSet.Tables[NameTable].Rows[r]["DateWedding"] = dataGridView1.Rows[r].Cells["DateWedding"].Value;
                            dataSet.Tables[NameTable].Rows[r]["Budget"] = dataGridView1.Rows[r].Cells["Budget"].Value;
                            dataSet.Tables[NameTable].Rows[r]["CodeZala"] = dataGridView1.Rows[r].Cells["CodeZala"].Value;
                            dataSet.Tables[NameTable].Rows[r]["CodeSalona"] = dataGridView1.Rows[r].Cells["CodeSalona"].Value;
                            dataSet.Tables[NameTable].Rows[r]["CodePh"] = dataGridView1.Rows[r].Cells["CodePh"].Value;
                        }
                        else if (NameTable == "BanquetHall")
                        {
                            dataSet.Tables[NameTable].Rows[r]["CodeZala"] = dataGridView1.Rows[r].Cells["CodeZala"].Value;
                            dataSet.Tables[NameTable].Rows[r]["NameHall"] = dataGridView1.Rows[r].Cells["NameHall"].Value;
                            dataSet.Tables[NameTable].Rows[r]["FIODirector"] = dataGridView1.Rows[r].Cells["FIODirector"].Value;
                            dataSet.Tables[NameTable].Rows[r]["NumberHall"] = dataGridView1.Rows[r].Cells["NumberHall"].Value;
                            dataSet.Tables[NameTable].Rows[r]["Address"] = dataGridView1.Rows[r].Cells["Address"].Value;
                            dataSet.Tables[NameTable].Rows[r]["DateArenda"] = dataGridView1.Rows[r].Cells["DateArenda"].Value;

                        }
                        else if (NameTable == "photographer_services")
                        {
                           dataSet.Tables[NameTable].Rows[r]["CodePh"] = dataGridView1.Rows[r].Cells["CodePh"].Value;
                           dataSet.Tables[NameTable].Rows[r]["FIOPh"] = dataGridView1.Rows[r].Cells["FIOPh"].Value;
                           dataSet.Tables[NameTable].Rows[r]["NumberTel"] = dataGridView1.Rows[r].Cells["NumberTel"].Value;
                           dataSet.Tables[NameTable].Rows[r]["Date_proved_merop"] = dataGridView1.Rows[r].Cells["Date_proved_merop"].Value;
                           dataSet.Tables[NameTable].Rows[r]["Budget"] = dataGridView1.Rows[r].Cells["Budget"].Value;
                        }
                        else if (NameTable == "WeddingSalon")
                        {
                            dataSet.Tables[NameTable].Rows[r]["CodeSalon"] = dataGridView1.Rows[r].Cells["CodeSalon"].Value;
                            dataSet.Tables[NameTable].Rows[r]["NameSalon"] = dataGridView1.Rows[r].Cells["NameSalon"].Value;
                            dataSet.Tables[NameTable].Rows[r]["FIODirector"] = dataGridView1.Rows[r].Cells["FIODirector"].Value;
                            dataSet.Tables[NameTable].Rows[r]["TelSalon"] = dataGridView1.Rows[r].Cells["TelSalon"].Value;
                            dataSet.Tables[NameTable].Rows[r]["Address"] = dataGridView1.Rows[r].Cells["Address"].Value;
                        }
                        sqlDataAdapter.Update(dataSet, NameTable);
                        dataGridView1.Rows[e.RowIndex].Cells[ColInd].Value = "DELETE";
                    }
                }
                Reload_Data(NameTable,ColInd);
                    
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message,"Ошибка!",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdding == false)
                {
                    newRowAdding = true;
                   
                    int lastRow = dataGridView1.Rows.Count - 2;
                    DataGridViewRow row = dataGridView1.Rows[lastRow]; 
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[ColInd, lastRow] = linkCell;
                    row.Cells["DELETE"].Value = "INSERT";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (newRowAdding == false)
                {
                    int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow editingRow = dataGridView1.Rows[rowIndex];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    dataGridView1[ColInd, rowIndex] = linkCell;
                    editingRow.Cells["DELETE"].Value = "UPDATE";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {/*
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                TextBox textBox = e.Control as TextBox;
                if (textBox!=null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }
            */
        }

   

        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if(!char.IsControl(e.KeyChar)&& !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            */
        }
    }
}


