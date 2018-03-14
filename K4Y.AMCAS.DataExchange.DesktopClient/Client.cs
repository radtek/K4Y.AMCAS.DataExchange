using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess = K4Y.AMCAS.DataExchange.DataStore;

namespace K4Y.AMCAS.DataExchange.DesktopClient
{
    public partial class Client : Form
    {
        private RestApi.IApiClient apiClient;
        private DataStore.AmcasRepository repository;
        private List<DataModel.Application> apiApplications = new List<DataModel.Application>();
        private List<DataModel.Application> repositoryApplications = new List<DataModel.Application>();
        public Client()
        {
            InitializeComponent();
            apiClient = new RestApi.MockApiClient();
            repository = new DataStore.AmcasRepository();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2.DataSource = bindingSource2;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            updateApiApplications();

            updateRepositoryApplications();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateApiApplications();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateRepositoryApplications();
        }

        private void updateApiApplications()
        {
            apiApplications = apiClient.GetApplicationList(DataModel.MedicalInstitutions.University1);
            bindingSource1.DataSource = new BindingList<DataModel.Application>(apiApplications);
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        private void updateRepositoryApplications()
        {
            repositoryApplications = repository.GetApplicationList();
            bindingSource2.DataSource = new BindingList<DataModel.Application>(repositoryApplications);
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            repository.SyncApplications(apiApplications);
        }
    }
}
