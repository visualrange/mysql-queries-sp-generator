using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace MySQL_SP_Query_Generator
{
    public partial class Main : Form
    {
        string server, port, userId, password, database;
        MySqlConnection conn;

        public Main()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            conn = GetConnection();

            try
            {
                conn.Open();
                this.BindTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            finally
            {
                conn.Close();
                conn = null;
            }
        }

        private MySqlConnection GetConnection()
        {
            server = txtServer.Text.Trim();
            port = txtPort.Text.Trim();
            userId = txtUserId.Text.Trim();
            password = txtPassword.Text.Trim();
            database = txtDB.Text.Trim();

            string connString = string.Format("server={0}; port={1}; uid={2}; pwd={3}; database={4};", server, port, userId, password, database);
            conn = new MySqlConnection(connString);
            return conn;
        }

        private void BindTables()
        {
            clbTables.Items.Clear();

            string query = string.Format("show tables from {0}", database);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dt = new DataTable();

            try
            {
                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    clbTables.Items.Add(dt.Rows[i][0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }



        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (clbTables.Items.Count > 0)
            {
                if (btnSelectAll.Text.Equals("Select All"))
                {
                    for (int i = 0; i < clbTables.Items.Count; i++)
                    {
                        clbTables.SetItemChecked(i, true);
                    }

                    btnSelectAll.Text = "Un-Select All";
                    return;
                }

                if (btnSelectAll.Text.Equals("Un-Select All"))
                {
                    for (int i = 0; i < clbTables.Items.Count; i++)
                    {
                        clbTables.SetItemChecked(i, false);
                    }

                    btnSelectAll.Text = "Select All";
                    return;
                }
            }

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";

            if (rbInline.Checked == true)
            {
                this.GenerateInlineQuery();
            }
            else if (rbStoredProc.Checked == true)
            {
                txtResult.AppendText(" DELIMITER $$ ");
                this.GenerateStoredProcedure();
            }
        }

        private void GenerateInlineQuery()
        { }

        private void GenerateStoredProcedure()
        {
            conn = this.GetConnection();

            for (int i = 0; i < clbTables.CheckedItems.Count; i++)
            {
                string tableName = clbTables.CheckedItems[i].ToString();
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    DataTable dtInfo = new DataTable();
                    string query = string.Format("explain {0}", tableName);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dtInfo);

                    string mode = string.Empty;
                    if (rbCreate.Checked == true)
                    {
                        mode = "CREATE";
                    }

                    if (rbAlter.Checked == true)
                    {
                        mode = string.Format("CREATE DEFINER=`{0}`@`{1}`",userId,server);
                    }

                    if (cbxSelect.Checked == true)
                    {
                        this.GenerateSelectStoredProcedure(tableName, dtInfo, mode);
                    }

                    if (cbxInsert.Checked == true)
                    {
                        this.GenerateInsertStoredProcedure(tableName, dtInfo, mode);
                    }

                    if (cbxUpdate.Checked == true)
                    {
                        this.GenerateUpdateStoredProcedure(tableName, dtInfo, mode);
                    }

                    if (cbxDelete.Checked == true)
                    {
                        this.GenerateDeleteStoredProcedure(tableName, dtInfo, mode);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
                finally
                {
                    conn.Close();
                }
            }

            conn = null;
        }

        private void GenerateSelectStoredProcedure(string tableName, DataTable tableInfo, string mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_Get`() ", mode, database, tableName));
            sb.AppendLine(" BEGIN ");
            sb.AppendLine(" SELECT ");
            string columns = string.Empty;

            

            for (int i = 0; i < tableInfo.Rows.Count; i++)
            {
                if (i == (tableInfo.Rows.Count - 1))
                {
                    columns += tableInfo.Rows[i]["Field"].ToString();
                    continue;
                }

                columns += tableInfo.Rows[i]["Field"].ToString() + ", ";
            }

            sb.AppendLine(columns);

            sb.AppendLine(string.Format(" From {0} ; ", tableName));
            sb.AppendLine(" END $$ ");
            sb.AppendLine(Environment.NewLine);

            txtResult.AppendText(sb.ToString());
        }

        private void GenerateInsertStoredProcedure(string tableName, DataTable tableInfo, string mode)
        {
            string columns = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_Add`( ", mode, database, tableName));

            for (int i = 0; i < tableInfo.Rows.Count; i++)
            {
                if (i == (tableInfo.Rows.Count - 1))
                {
                    columns += " in " + tableInfo.Rows[i]["Field"].ToString() + " " + tableInfo.Rows[i]["Type"].ToString();
                    continue;
                }

                columns += " in " + tableInfo.Rows[i]["Field"].ToString() + " " + tableInfo.Rows[i]["Type"].ToString() + ", ";
            }

            sb.AppendLine(columns);
            sb.AppendLine(")");
            sb.AppendLine(" BEGIN ");

            sb.AppendLine(string.Format(" INSERT INTO {0}( ", tableName));

            columns = string.Empty;
            for (int i = 0; i < tableInfo.Rows.Count; i++)
            {
                if (i == (tableInfo.Rows.Count - 1))
                {
                    columns += "`" + tableInfo.Rows[i]["Field"].ToString() + "`)";
                    continue;
                }

                columns += "`" + tableInfo.Rows[i]["Field"].ToString() + "`, ";
            }

            sb.AppendLine(columns);

            columns = string.Empty;
            sb.AppendLine(" values(");
            for (int y = 0; y < tableInfo.Rows.Count; y++)
            {
                if (y == (tableInfo.Rows.Count - 1))
                {
                    columns += tableInfo.Rows[y]["Field"].ToString() + ");";
                    continue;
                }

                columns += tableInfo.Rows[y]["Field"].ToString() + ", ";
            }

            sb.AppendLine(columns);
            sb.AppendLine(" END $$ ");
            sb.AppendLine(Environment.NewLine);

            txtResult.AppendText(sb.ToString());

        }

        private void GenerateUpdateStoredProcedure(string tableName, DataTable tableInfo, string mode)
        {
            string columns = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_Update`( ", mode, database, tableName));

            for (int i = 0; i < tableInfo.Rows.Count; i++)
            {
                if (i == (tableInfo.Rows.Count - 1))
                {
                    columns += " in " + tableInfo.Rows[i]["Field"].ToString() + " " + tableInfo.Rows[i]["Type"].ToString();
                    continue;
                }

                columns += " in " + tableInfo.Rows[i]["Field"].ToString() + " " + tableInfo.Rows[i]["Type"].ToString() + ", ";
            }

            sb.AppendLine(columns);
            sb.AppendLine(")");
            sb.AppendLine(" BEGIN ");

            sb.AppendLine(string.Format(" UPDATE {0} SET ", tableName));

            columns = string.Empty;
            for (int i = 0; i < tableInfo.Rows.Count; i++)
            {
                if (i == (tableInfo.Rows.Count - 1))
                {
                    columns += "`" + tableInfo.Rows[i]["Field"].ToString() + "` = " + tableInfo.Rows[i]["Field"].ToString() ;
                    continue;
                }

                columns += " `" + tableInfo.Rows[i]["Field"].ToString() + "` = " + tableInfo.Rows[i]["Field"].ToString() + ", ";
            }

            sb.AppendLine(columns);
            sb.AppendLine(" WHERE COLUMN = SOMEVALUE ;");
            sb.AppendLine(" END $$ ");
            sb.AppendLine(Environment.NewLine);

            txtResult.AppendText(sb.ToString());
        }

        private void GenerateDeleteStoredProcedure(string tableName, DataTable tableInfo, string mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_Delete`() ", mode, database, tableName));
            sb.AppendLine(" BEGIN ");
            sb.AppendLine(string.Format(" DELETE FROM {0} WHERE COLUMN = SOMEVALUE ;", tableName));
            sb.AppendLine(" END $$ ");
            txtResult.AppendText(sb.ToString());
        }
    }
}
