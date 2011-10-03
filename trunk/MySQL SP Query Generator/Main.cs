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

        #region Helper Methods
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

        private string getFilterExpression(CheckBox cbx, CheckBox cbxFKUN)
        {
            string filterExpression = string.Empty;

            if (cbx.Checked)
            {
                filterExpression = " Key = 'PRI' ";
            }

            if (cbxFKUN.Checked)
            {
                filterExpression = " Key <> '' and key <> 'PRI' ";
            }

            if (cbx.Checked && cbxFKUN.Checked)
            {
                filterExpression = " Key <> '' ";
            }

            return filterExpression;
        }
        #endregion

        #region For Inline Query
        private void GenerateInlineQuery()
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

                    if (cbxSelect.Checked || cbxSelectFKUN.Checked)
                    {
                        this.GenerateSelectQuery(tableName, dtInfo);
                    }

                    if (cbxInsert.Checked == true)
                    {
                        this.GenerateInsertQuery(tableName, dtInfo);
                    }

                    if (cbxUpdate.Checked || cbxUpdateFKUN.Checked)
                    {
                        this.GenerateUpdateQuery(tableName, dtInfo);
                    }

                    if (cbxDelete.Checked || cbxDeleteFKUN.Checked)
                    {
                        this.GenerateDeleteQuery(tableName, dtInfo);
                    }

                    if (cbxCount.Checked)
                    {
                        this.GenerateCountQuery(tableName);
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

        private void GenerateSelectQuery(string tableName, DataTable tableInfo)
        {
            DataRow[] rows = tableInfo.Select(this.getFilterExpression(cbxSelect, cbxSelectFKUN));

            for (int a = 0; a < rows.Length; a++)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Environment.NewLine);
                string fieldName = rows[a]["Field"].ToString().ToLower();
                sb.AppendLine(" SELECT ");
                string columns = string.Empty;

                for (int i = 0; i < tableInfo.Rows.Count; i++)
                {
                    if (i == (tableInfo.Rows.Count - 1))
                    {
                        columns += "\t" + tableName + "." + tableInfo.Rows[i]["Field"].ToString() ;
                        continue;
                    }

                    columns += "\t" +  tableName+ "." + tableInfo.Rows[i]["Field"].ToString() + ", " + Environment.NewLine;
                }

                sb.AppendLine(columns);
                sb.AppendLine(string.Format(" From {0}  ", tableName));
                
                
                if (cbxDollarSign.Checked)
                {
                    sb.AppendLine(string.Format(" WHERE {0}.{1} = ${2}; ", tableName, fieldName, fieldName));
                }
                else
                {
                    sb.AppendLine(string.Format(" WHERE {0}.{1} = {2}; ", tableName, fieldName, fieldName));
                }
                
                sb.AppendLine(Environment.NewLine);

                txtResult.AppendText(sb.ToString());
            }

        }

        private void GenerateInsertQuery(string tableName, DataTable tableInfo)
        {
            string dollarSign = string.Empty;

            if (cbxDollarSign.Checked)
            {
                dollarSign = "$";
            }

            string columns = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);
            sb.AppendLine(string.Format(" INSERT INTO {0} ", tableName));
            sb.AppendLine("(");
            columns = string.Empty;
            for (int i = 0; i < tableInfo.Rows.Count; i++)
            {
                if (tableInfo.Rows[i]["Extra"].ToString().Equals("auto_increment"))
                {
                    continue;
                }

                if (i == (tableInfo.Rows.Count - 1))
                {
                    columns += "\t" + tableInfo.Rows[i]["Field"].ToString();
                    continue;
                }

                columns += "\t" + tableInfo.Rows[i]["Field"].ToString() + ", " + Environment.NewLine;
            }

            sb.AppendLine(columns);
            sb.AppendLine(")");

            columns = string.Empty;
            sb.AppendLine(" values");
            sb.AppendLine("(");
            for (int y = 0; y < tableInfo.Rows.Count; y++)
            {
                if (tableInfo.Rows[y]["Extra"].ToString().Equals("auto_increment"))
                {
                    continue;
                }

                if (y == (tableInfo.Rows.Count - 1))
                {
                    columns += "\t" + dollarSign + tableInfo.Rows[y]["Field"].ToString();
                    continue;
                }

                columns += "\t" + dollarSign + tableInfo.Rows[y]["Field"].ToString() + ", " + Environment.NewLine;
            }

            sb.AppendLine(columns);
            sb.AppendLine(")");
            sb.AppendLine(Environment.NewLine);
            txtResult.AppendText(sb.ToString());
        }

        private void GenerateUpdateQuery(string tableName, DataTable tableInfo)
        {
            string dollarSign = string.Empty;

            if (cbxDollarSign.Checked)
            {
                dollarSign = "$";
            }

            DataRow[] rows = tableInfo.Select(this.getFilterExpression(cbxUpdate, cbxUpdateFKUN));

            for (int a = 0; a < rows.Length; a++)
            {

                string columns = string.Empty;
                string fieldName = rows[a]["Field"].ToString().ToLower();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine(string.Format(" UPDATE {0} SET ", tableName));

                columns = string.Empty;
                for (int i = 0; i < tableInfo.Rows.Count; i++)
                {
                    if (tableInfo.Rows[i]["Extra"].ToString().Equals("auto_increment"))
                    {
                        continue;
                    }

                    if (i == (tableInfo.Rows.Count - 1))
                    {
                        columns += "\t" + tableInfo.Rows[i]["Field"].ToString() + " = " + dollarSign + tableInfo.Rows[i]["Field"].ToString();
                        continue;
                    }

                    columns += "\t" + tableInfo.Rows[i]["Field"].ToString() + " = " + dollarSign + tableInfo.Rows[i]["Field"].ToString() + ", " + Environment.NewLine;
                }

                sb.AppendLine(columns);
                sb.AppendLine(string.Format(" WHERE {0} = {1}{2} ;", fieldName, dollarSign, fieldName));
                sb.AppendLine(Environment.NewLine);
                txtResult.AppendText(sb.ToString());
            }
        }

        private void GenerateDeleteQuery(string tableName, DataTable tableInfo)
        {
            string dollarSign = string.Empty;

            if (cbxDollarSign.Checked)
            {
                dollarSign = "$";
            }

            DataRow[] rows = tableInfo.Select(this.getFilterExpression(cbxDelete, cbxDeleteFKUN));

            for (int a = 0; a < rows.Length; a++)
            {
                string fieldName = rows[a]["Field"].ToString().ToLower();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine(string.Format(" DELETE FROM {0} WHERE {1} = {2}{3} ;", tableName, fieldName, dollarSign, fieldName));
                sb.AppendLine(Environment.NewLine);
                txtResult.AppendText(sb.ToString());
            }
        }

        private void GenerateCountQuery(string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Environment.NewLine);
            sb.Append(string.Format(" Select COUNT(1) FROM {0} as total_{1}; ",tableName,tableName));
            sb.Append(Environment.NewLine);
            txtResult.AppendText(sb.ToString());
        }

        #endregion

        #region For Stored Procedure

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
                        mode = " CREATE ";
                    }

                    if (rbAlter.Checked == true)
                    {
                        mode = string.Format(" CREATE DEFINER=`{0}`@`{1}` ", userId, server);
                    }

                    if (cbxSelect.Checked || cbxSelectFKUN.Checked)
                    {
                        this.GenerateSelectStoredProcedure(tableName, dtInfo, mode);
                    }

                    if (cbxInsert.Checked)
                    {
                        this.GenerateInsertStoredProcedure(tableName, dtInfo, mode);
                    }

                    if (cbxUpdate.Checked || cbxUpdateFKUN.Checked)
                    {
                        this.GenerateUpdateStoredProcedure(tableName, dtInfo, mode);
                    }

                    if (cbxDelete.Checked || cbxDeleteFKUN.Checked)
                    {
                        this.GenerateDeleteStoredProcedure(tableName, dtInfo, mode);
                    }

                    if (cbxCount.Checked)
                    {
                        this.GenerateSPForCount(tableName, mode);
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
            DataRow[] rows = tableInfo.Select(this.getFilterExpression(cbxSelect, cbxSelectFKUN));

            for (int a = 0; a < rows.Length; a++)
            {
                string columns = string.Empty;
                StringBuilder sb = new StringBuilder();
                string fieldName = rows[a]["Field"].ToString().ToLower();
                sb.AppendLine(" DELIMITER ; ");
                sb.AppendLine(string.Format("DROP procedure IF EXISTS `{0}_get_by_{1}`;", tableName, fieldName));
                sb.AppendLine(" DELIMITER $$ ");
                sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_get_by_{3}` ", mode, database, tableName, fieldName));
                sb.AppendLine("(");
                sb.AppendLine(string.Format("\t in p_{0} {1}", rows[a].ItemArray[0].ToString(), rows[a].ItemArray[1].ToString()));
                sb.AppendLine(")");

                sb.AppendLine(" BEGIN ");
                sb.AppendLine(" SELECT ");

                columns = string.Empty;
                for (int i = 0; i < tableInfo.Rows.Count; i++)
                {
                    if (i == (tableInfo.Rows.Count - 1))
                    {
                        columns += "\t" + tableName + "." + tableInfo.Rows[i]["Field"].ToString();
                        continue;
                    }

                    columns += "\t" + tableName + "." + tableInfo.Rows[i]["Field"].ToString() + ", " + Environment.NewLine;
                }

                sb.AppendLine(columns);
                sb.AppendLine(string.Format(" From {0}  ", tableName));
                sb.AppendLine(string.Format(" WHERE {0}.{1} = p_{1}; ", tableName, fieldName, fieldName));

                sb.AppendLine(" END $$ ");
                sb.AppendLine(Environment.NewLine);

                txtResult.AppendText(sb.ToString());
            }


        }

        private void GenerateInsertStoredProcedure(string tableName, DataTable tableInfo, string mode)
        {

            string columns = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" DELIMITER ; ");
            sb.AppendLine(string.Format("DROP procedure IF EXISTS `{0}_add`;", tableName));
            sb.AppendLine(" DELIMITER $$ ");
            sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_add` ", mode, database, tableName));
            sb.AppendLine("(");

            for (int i = 0; i < tableInfo.Rows.Count; i++)
            {
                if (tableInfo.Rows[i]["Extra"].ToString().Equals("auto_increment"))
                {
                    continue;
                }

                if (i == (tableInfo.Rows.Count - 1))
                {
                    columns += "\t in p_" + tableInfo.Rows[i]["Field"].ToString() + " " + tableInfo.Rows[i]["Type"].ToString();
                    continue;
                }

                columns += "\t in p_" + tableInfo.Rows[i]["Field"].ToString() + " " + tableInfo.Rows[i]["Type"].ToString() + ", " + Environment.NewLine;
            }

            sb.AppendLine(columns);
            sb.AppendLine(")");
            sb.AppendLine(" BEGIN ");

            sb.AppendLine(string.Format(" INSERT INTO {0} ", tableName));
            sb.AppendLine("(");

            columns = string.Empty;
            for (int i = 0; i < tableInfo.Rows.Count; i++)
            {
                if (tableInfo.Rows[i]["Extra"].ToString().Equals("auto_increment"))
                {
                    continue;
                }

                if (i == (tableInfo.Rows.Count - 1))
                {
                    columns += "\t `" + tableInfo.Rows[i]["Field"].ToString() + "`";
                    continue;
                }

                columns += "\t `" + tableInfo.Rows[i]["Field"].ToString() + "`, " + Environment.NewLine;
            }

            sb.AppendLine(columns);
            sb.AppendLine(")");

            columns = string.Empty;
            sb.AppendLine(" values");
            sb.AppendLine("(");
            for (int y = 0; y < tableInfo.Rows.Count; y++)
            {
                if (tableInfo.Rows[y]["Extra"].ToString().Equals("auto_increment"))
                {
                    continue;
                }

                if (y == (tableInfo.Rows.Count - 1))
                {
                    columns += "\t p_" + tableInfo.Rows[y]["Field"].ToString() ;
                    continue;
                }

                columns += "\t p_" + tableInfo.Rows[y]["Field"].ToString() + ", " + Environment.NewLine;
            }

            sb.AppendLine(columns);
            sb.AppendLine(");");

            if (cbxLasterInsertId.Checked)
            {
                sb.AppendLine(" select last_insert_id() as lastInsertId; ");
            }

            sb.AppendLine(" END $$ ");
            sb.AppendLine(Environment.NewLine);

            txtResult.AppendText(sb.ToString());

        }

        private void GenerateUpdateStoredProcedure(string tableName, DataTable tableInfo, string mode)
        {
            DataRow[] rows = tableInfo.Select(this.getFilterExpression(cbxUpdate, cbxUpdateFKUN));

            for (int a = 0; a < rows.Length; a++)
            {

                string columns = string.Empty;
                string fieldName = rows[a]["Field"].ToString().ToLower();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" DELIMITER ; ");
                sb.AppendLine(string.Format("DROP procedure IF EXISTS `{0}_update_by_{1}`;", tableName, fieldName));
                sb.AppendLine(" DELIMITER $$ ");
                sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_update_by_{3}` ", mode, database, tableName, fieldName));
                sb.AppendLine("(");

                for (int i = 0; i < tableInfo.Rows.Count; i++)
                {
                    if (i == (tableInfo.Rows.Count - 1))
                    {
                        columns += "\t in p_" + tableInfo.Rows[i]["Field"].ToString() + " " + tableInfo.Rows[i]["Type"].ToString();
                        continue;
                    }

                    columns += "\t in p_" + tableInfo.Rows[i]["Field"].ToString() + " " + tableInfo.Rows[i]["Type"].ToString() + ", " + Environment.NewLine;
                }

                sb.AppendLine(columns);
                sb.AppendLine(")");
                sb.AppendLine(" BEGIN ");

                sb.AppendLine(string.Format(" UPDATE {0} SET ", tableName));

                columns = string.Empty;
                for (int i = 0; i < tableInfo.Rows.Count; i++)
                {
                    if (tableInfo.Rows[i]["Extra"].ToString().Equals("auto_increment"))
                    {
                        continue;
                    }

                    if (i == (tableInfo.Rows.Count - 1))
                    {
                        columns += "\t `" + tableInfo.Rows[i]["Field"].ToString() + "` = p_" + tableInfo.Rows[i]["Field"].ToString();
                        continue;
                    }

                    columns += "\t `" + tableInfo.Rows[i]["Field"].ToString() + "` = p_" + tableInfo.Rows[i]["Field"].ToString() + ", " + Environment.NewLine;
                }

                sb.AppendLine(columns);
                sb.AppendLine(string.Format(" WHERE `{0}` = p_{1} ;", fieldName, fieldName));

                if (cbxRowsAffected.Checked)
                {
                    sb.AppendLine(" select row_count() as rowsAffected; ");
                }

                sb.AppendLine(" END $$ ");
                sb.AppendLine(Environment.NewLine);

                txtResult.AppendText(sb.ToString());
            }
        }

        private void GenerateDeleteStoredProcedure(string tableName, DataTable tableInfo, string mode)
        {
            DataRow[] rows = tableInfo.Select(this.getFilterExpression(cbxDelete, cbxDeleteFKUN));

            for (int a = 0; a < rows.Length; a++)
            {
                string fieldName = rows[a]["Field"].ToString().ToLower();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" DELIMITER ; ");
                sb.AppendLine(string.Format(" DROP procedure IF EXISTS `{0}_delete_by_{1}`; ", tableName, fieldName));
                sb.AppendLine(" DELIMITER $$ ");
                sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_delete_by_{3}` ", mode, database, tableName, fieldName));
                sb.AppendLine("(");
                sb.AppendLine(string.Format("\t in p_{0} {1}", rows[a].ItemArray[0].ToString(), rows[a].ItemArray[1].ToString()));
                sb.AppendLine(")");
                sb.AppendLine(" BEGIN ");
                sb.AppendLine(string.Format(" DELETE FROM {0} WHERE `{1}` = p_{2} ;", tableName, fieldName, rows[a].ItemArray[0].ToString()));
                
                if (cbxRowsAffected.Checked)
                {
                    sb.AppendLine(" select row_count() as rowsAffected; ");
                }

                sb.AppendLine(" END $$ ");
                sb.AppendLine(Environment.NewLine);
                txtResult.AppendText(sb.ToString());
            }
        }

        private void GenerateSPDropStatements()
        {
            conn = this.GetConnection();
            DataTable dtInfo = new DataTable();
            string query = string.Format("select ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE=\"PROCEDURE\" AND ROUTINE_SCHEMA=\"{0}\"; ", database);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            try
            {
                adapter.SelectCommand = cmd;
                adapter.Fill(dtInfo);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dtInfo.Rows.Count; i++)
                {
                    sb.AppendLine(string.Format("drop procedure `{0}`.`{1}`;", database, dtInfo.Rows[i][0].ToString()));
                }

                txtResult.AppendText(sb.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }

        }

        private void GenerateSPForCount(string tableName, string mode)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" DELIMITER ; ");
            sb.AppendLine(string.Format(" DROP procedure IF EXISTS `{0}_count`; ", tableName));
            sb.AppendLine(" DELIMITER $$ ");
            sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_count`() ", mode, database, tableName));
            sb.AppendLine(" BEGIN ");
            sb.AppendLine(string.Format(" SELECT COUNT(1) as total_{0} FROM {1}  ;", tableName, tableName));
            sb.AppendLine(" END $$ ");
            sb.AppendLine(Environment.NewLine);
            txtResult.AppendText(sb.ToString());
        }

        private void rbStoredProc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStoredProc.Checked == true)
            {
                panelCreateAlter.Enabled = true;
                rbCreate.Checked = true;
            }
            else
            {
                panelCreateAlter.Enabled = false;
            }
        }

        #endregion

        #region Events
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
            txtResult.AppendText(string.Format(" USE {0};", database));
            txtResult.AppendText(Environment.NewLine);

            if (rbInline.Checked == true)
            {
                this.GenerateInlineQuery();
            }
            else if (rbStoredProc.Checked == true)
            {
                this.GenerateStoredProcedure();
            }
            else if (rbDrop.Checked == true)
            {
                this.GenerateSPDropStatements();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
            about.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "SQL File (*.sql)|*.sql|Text File (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 0;

            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string fileName = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(fileName, txtResult.Text);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.ConnectionPrerequisite() == false)
            {
                return;
            }
            conn = GetConnection();

            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    gbSelectTables.Enabled = true;
                    gbOptions.Enabled = true;
                    gbResult.Enabled = true;
                    this.BindTables();
                }

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

        private bool ConnectionPrerequisite()
        {
            if (txtServer.Text.Equals(""))
            {
                MessageBox.Show("Server is required","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtServer.Focus();
                return false;
            }

            if (txtPort.Text.Equals(""))
            {
                MessageBox.Show("Port is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPort.Focus();
                return false;
            }

            if (txtDB.Text.Equals(""))
            {
                MessageBox.Show("Database name is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDB.Focus();
                return false;
            }


            return true;
        }

        #endregion

        private void rbInline_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInline.Checked)
            {
                cbxDollarSign.Enabled = true;
            }
            else if (rbInline.Checked == false)
            {
                cbxDollarSign.Checked = false;
                cbxDollarSign.Enabled = false;
            }
        }

    }
}
