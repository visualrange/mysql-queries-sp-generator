﻿using System;
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
                        mode = string.Format("CREATE DEFINER=`{0}`@`{1}`", userId, server);
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
            DataRow[] rows = tableInfo.Select(" Key <>'' ");

            for (int a = 0; a < rows.Length; a++)
            {
                StringBuilder sb = new StringBuilder();
                string fieldName = rows[a]["Field"].ToString().ToLower();
                sb.AppendLine(" DELIMITER ; ");
                sb.AppendLine(string.Format("DROP procedure IF EXISTS `{0}_get_by_{1}`;", tableName, fieldName));
                sb.AppendLine(" DELIMITER $$ ");
                sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_get_by_{3}`() ", mode, database, tableName, fieldName));
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


        }

        private void GenerateInsertStoredProcedure(string tableName, DataTable tableInfo, string mode)
        {

            string columns = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(" DELIMITER ; ");
            sb.AppendLine(string.Format("DROP procedure IF EXISTS `{0}_add`;", tableName));
            sb.AppendLine(" DELIMITER $$ ");
            sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_add`( ", mode, database, tableName));

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
            DataRow[] rows = tableInfo.Select(" Key <>'' ");

            for (int a = 0; a < rows.Length; a++)
            {

                string columns = string.Empty;
                string fieldName = rows[a]["Field"].ToString().ToLower();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" DELIMITER ; ");
                sb.AppendLine(string.Format("DROP procedure IF EXISTS `{0}_update_by_{1}`;", tableName, fieldName));
                sb.AppendLine(" DELIMITER $$ ");
                sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_update_by_{3}`( ", mode, database, tableName, fieldName));

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
                        columns += "`" + tableInfo.Rows[i]["Field"].ToString() + "` = " + fieldName;
                        continue;
                    }

                    columns += " `" + tableInfo.Rows[i]["Field"].ToString() + "` = " + fieldName + ", ";
                }

                sb.AppendLine(columns);
                sb.AppendLine(string.Format(" WHERE `{0}` = {1} ;", fieldName, fieldName));
                sb.AppendLine(" END $$ ");
                sb.AppendLine(Environment.NewLine);

                txtResult.AppendText(sb.ToString());
            }
        }

        private void GenerateDeleteStoredProcedure(string tableName, DataTable tableInfo, string mode)
        {
            DataRow[] rows = tableInfo.Select(" Key <>'' ");

            for (int a = 0; a < rows.Length; a++)
            {
                string fieldName = rows[a]["Field"].ToString().ToLower();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" DELIMITER ; ");
                sb.AppendLine(string.Format(" DROP procedure IF EXISTS `{0}_delete_by_{1}`; ", tableName, fieldName));
                sb.AppendLine(" DELIMITER $$ ");
                sb.AppendLine(string.Format(" {0} PROCEDURE `{1}`.`{2}_delete_by_{3}`() ", mode, database, tableName, fieldName));
                sb.AppendLine(" BEGIN ");
                sb.AppendLine(string.Format(" DELETE FROM {0} WHERE `{0}` = {1} ;", tableName, fieldName, fieldName));
                sb.AppendLine(" END $$ ");
                txtResult.AppendText(sb.ToString());
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
                    sb.AppendLine(string.Format("drop procedure `{0}`.`{1}`;",database,dtInfo.Rows[i][0].ToString()));    
                }

                txtResult.AppendText(sb.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            
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
            System.IO.File.WriteAllText(fileName,txtResult.Text);
        }

        private void rbStoredProc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStoredProc.Checked == true)
            {
                panelCreateAlter.Enabled = true;
            }
            else
            {
                panelCreateAlter.Enabled = false;
            }
        }


    }
}
