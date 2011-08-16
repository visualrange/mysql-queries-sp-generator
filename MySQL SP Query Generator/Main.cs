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

            string connString = string.Format("server={0}; port={1}; uid={2}; pwd={3}; database={4};",server,port,userId,password,database);
            conn = new MySqlConnection(connString);
            return conn;
        }

        private void BindTables()
        {
            string query = string.Format("show tables from {0}",database);
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
                if(btnSelectAll.Text.Equals("Select All"))
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
            if (rbInline.Checked == true)
            {
                this.GenerateInlineQuery();
            }
            else if (rbStoredProc.Checked == true)
            {
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
                    string query = string.Format("explain {0}",tableName);
                    MySqlCommand cmd = new MySqlCommand(query,conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dtInfo);

                    this.GenerateSelectStoredProcedure(tableName, dtInfo);
                    this.GenerateInsertStoredProcedure(tableName, dtInfo);
                    this.GenerateUpdateStoredProcedure(tableName, dtInfo);
                    this.GenerateDeleteStoredProcedure(tableName, dtInfo);

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

        private void GenerateSelectStoredProcedure(string tableName, DataTable tableInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELIMITER $$");
            sb.AppendLine(string.Format("CREATE PROCEDURE `{0}`.`{1}_Get` ()",database,tableName));
            sb.AppendLine("BEGIN");

            for (int i = 0; i < tableInfo.Rows.Count; i++)
            { 
                
            }

            sb.AppendLine("END");
        }

        private void GenerateInsertStoredProcedure(string tableName, DataTable tableInfo)
        { }

        private void GenerateUpdateStoredProcedure(string tableName, DataTable tableInfo)
        { }

        private void GenerateDeleteStoredProcedure(string tableName, DataTable tableInfo)
        { }
    }
}
