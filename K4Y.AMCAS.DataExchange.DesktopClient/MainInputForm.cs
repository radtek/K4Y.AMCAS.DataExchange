using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K4Y.AMCAS.DataExchange.DesktopClient
{
    public partial class MainInputForm : Form
    {
        private RestApi.IApiClient apiClient;
        private List<DataModel.Application> apiApplications = new List<DataModel.Application>();
        private DataStore.AmcasRepository repository;
        private List<DataModel.Application> repositoryApplications = new List<DataModel.Application>();
        public MainInputForm()
        {
            InitializeComponent();
            apiClient = new RestApi.MockApiClient();
            repository = new DataStore.AmcasRepository();
        }

        private void buttonParse_Click(object sender, EventArgs e)
        {
            updateApiApplications();
        }
        private void updateApiApplications()
        {
            apiApplications = apiClient.ParseApplications(textBox1.Text);
            bindingSource1.DataSource = new BindingList<DataModel.Application>(apiApplications);
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }
        private void updateRepositoryApplications()
        {
            repositoryApplications = repository.GetApplicationList();
            bindingSource2.DataSource = new BindingList<DataModel.Application>(repositoryApplications);
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

        private void MainInputForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2.DataSource = bindingSource2;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            updateRepositoryApplications();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            repository.SyncApplications(apiApplications);
            updateRepositoryApplications();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            updateRepositoryApplications();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBox1.Text = apiClient.GetApiResponseContent(DataModel.MedicalInstitutions.University1);
        }
    }
}
