using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DuaSaudaraFinance.GlobalVariable;
using static DuaSaudaraFinance.LibSQLServer;
using System.Data.SqlClient;
using DuaSaudaraFinance.Administrator;
using DuaSaudaraFinance.Kain;
using DuaSaudaraFinance.Barang;
using System.IO;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DuaSaudaraFinance.Transaksi;

namespace DuaSaudaraFinance
{
    public partial class frmMainWindow : Form
    {
        public frmMainWindow()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
              


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmDaftarTransaksi>().Any())
            {
                Application.OpenForms.OfType<frmDaftarTransaksi>().First().Close();
            }
            frmDaftarTransaksi frmDaftarTransaksiInstance = new frmDaftarTransaksi();
            frmDaftarTransaksiInstance.MdiParent = this;
            frmDaftarTransaksiInstance.Show();




        }

        private void frmMainWindow_Load(object sender, EventArgs e)
        {
            if (System.Net.Dns.GetHostName() == "PCToko")
            {
                //ConnString = "Server=tcp:primasqldatabase.database.windows.net,1433;Initial Catalog=DSFinance;Persist Security Info=False;User ID=strangerman;Password=Ilc960x6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;";
                ConnString = "Server=PCTOKO\\SQLEXPRESS; User Id=sa; password=ilc960x6;Database=DuaSaudaraFinance; MultipleActiveResultSets=True";
                IsOwner = 0;
                UserName = "Karyawan";

                barangToolStripMenuItem.Visible = true;
                StoreMyIpPublic("Toko");
            }

            if (System.Net.Dns.GetHostName() == "PrimaLegion")
            {
                ConnString = "Server=192.168.1.120; User Id=sa; password=ilc960x6; Database=DuaSaudaraFinance; MultipleActiveResultSets=True";
                //ConnString = "Server=" + GetTokoIpPublic() + "; User Id=sa; password=ilc960x6; Database=DuaSaudaraFinance; MultipleActiveResultSets=True";
                //ConnString = "Server=localhost; User Id=sa; password=ilc960x6; Database=DuaSaudaraFinance; MultipleActiveResultSets=True";
                IsOwner = 1;
                UserName = "Administrator";

                barangToolStripMenuItem.Visible = true;
                suratPerintahKerjaToolStripMenuItem.Visible = true;
            }

            if (System.Net.Dns.GetHostName() == "Prima-Thinkpad")
            {
                //ConnString = "Server=tcp:primasqldatabase.database.windows.net,1433;Initial Catalog=DSFinance;Persist Security Info=False;User ID=strangerman;Password=Ilc960x6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;";
                //ConnString = "Server=PRIMA-THINKPAD\\SQLEXPRESS; User Id=sa; password=ilc960x6; Database=DSFinance; MultipleActiveResultSets=True";
                IsOwner = 1;
                UserName = "Administrator";

                barangToolStripMenuItem.Visible = true;
            }

            if (System.Net.Dns.GetHostName() == "DESKTOP-KANTOR")
            {
                IsOwner = 1;
                UserName = "Administrator";

                barangToolStripMenuItem.Visible = true;
            }

            CreateSqlConnection(ref GlobalVariable.Conn, GlobalVariable.ConnString);

            InitializeParameter();
            
            //this.Enabled = false;


            
        }

        private void InitializeParameter()
        {
            SqlDataReader myReader = null;

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeBahan");
            while (myReader.Read())
            {
                Console.WriteLine(myReader["NamaBahan"].ToString());
                ListAttributeJasDokterBahan.Add(new Model.AttributeJasDokterBahan(Convert.ToInt32(myReader["Id"]), myReader["NamaBahan"].ToString(), myReader["KodeBahan"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeJenisKelamin");
            while (myReader.Read())
            {
                ListSex.Add(new Model.Sex(Convert.ToInt32(myReader["Id"]), myReader["NamaJenisKelamin"].ToString(), myReader["KodeJenisKelamin"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeLengan");
            while (myReader.Read())
            {
                ListAttributeLengan.Add(new Model.AttributeLengan(Convert.ToInt32(myReader["Id"]), myReader["NamaLengan"].ToString(), myReader["KodeLengan"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeSize");
            while (myReader.Read())
            {
                ListAttributeSize.Add(new Model.AttributeSize(Convert.ToInt32(myReader["Id"]), myReader["NamaSize"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeSetBarang");
            while (myReader.Read())
            {
                ListAttributeResidenSetBarang.Add(new Model.AttributeResidenSetBarang(Convert.ToInt32(myReader["Id"]), myReader["NamaSetBarang"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeBajuOKSet");
            while (myReader.Read())
            {
                ListAttributeBajuOKSet.Add(new Model.AttributeBajuOKSet(Convert.ToInt32(myReader["Id"]), myReader["NamaSetBajuOK"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeBajuOKJenis");
            while (myReader.Read())
            {
                ListAttributeBajuOKJenis.Add(new Model.AttributeBajuOKJenis(Convert.ToInt32(myReader["Id"]), myReader["NamaJenisBajuJaga"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblAttributeBajuOKWarna");
            while (myReader.Read())
            {
                ListAttributeBajuOKWarna.Add(new Model.AttributeBajuOKWarna(Convert.ToInt32(myReader["Id"]), myReader["NamaWarnaBajuJaga"].ToString()));
            }
            CloseReadSqlData(ref myReader);

            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblKainMerk");
            while (myReader.Read())
            {
                ListItemKain.Add(new Model.ItemKain(Convert.ToInt32(myReader["Id"]), myReader["KodeKain"].ToString(), Convert.ToDecimal(myReader["HargaDefault"]), myReader["Satuan"].ToString(), myReader["Keterangan"].ToString()));
            }
            CloseReadSqlData(ref myReader);


            ReadSqlData(ref Conn, ref myReader, "SELECT * FROM tblKainMerkWarna");
            while (myReader.Read())
            {
                ListAttributeKainWarna.Add(new Model.AttributeKainWarna(Convert.ToInt32(myReader["Id"]), Convert.ToInt32(myReader["KainMerkId"]), myReader["KodeWarna"].ToString(), myReader["NamaWarna"].ToString(), myReader["Keterangan"].ToString(), Convert.ToDecimal(myReader["StokAwal"])));
            }
            CloseReadSqlData(ref myReader);


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmTambahTransaksiPenjualan frmTambahTransaksiPenjualanInstance = new frmTambahTransaksiPenjualan();
            frmTambahTransaksiPenjualanInstance.MdiParent = this;
            frmTambahTransaksiPenjualanInstance.Show();

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmTambahTransaksi>().Count() > 0)
            {
                foreach (var item in Application.OpenForms.OfType<frmTambahTransaksi>())
                {
                    item.Close();
                }
            }
            toolStripButton3.PerformClick();
            Application.OpenForms.OfType<frmDaftarTransaksi>().First().ExecuteButton("Add Transaksi Keluar");
        }        

        private void transaksiKeluarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2.PerformClick();

        }

        private void daftarTransaksiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3.PerformClick();
        }

        private void daftarMerkKainToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms.OfType<frmDaftarMerkKain>().Any())
            {
                Application.OpenForms.OfType<frmDaftarMerkKain>().First().Close();
            }

            frmDaftarMerkKain frmDaftarMerkKainInstance = new frmDaftarMerkKain();
            frmDaftarMerkKainInstance.MdiParent = this;
            frmDaftarMerkKainInstance.Show();
        }

        private void daftarJenisKainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmDaftarJenisKain>().Any())
            {
                Application.OpenForms.OfType<frmDaftarJenisKain>().First().Close();
            }

            frmDaftarJenisKain frmDaftarJenisKainInstance = new frmDaftarJenisKain();
            frmDaftarJenisKainInstance.MdiParent = this;
            frmDaftarJenisKainInstance.Show();
        }
                

        private void daftarArusKainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmDaftarArusKain>().Any())
            {
                Application.OpenForms.OfType<frmDaftarArusKain>().First().Close();
            }

            frmDaftarArusKain frmDaftarArusKainInstance = new frmDaftarArusKain();
            frmDaftarArusKainInstance.MdiParent = this;
            frmDaftarArusKainInstance.Show();
        }

        private void kainMasukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmKainMasuk>().Any())
            {
                Application.OpenForms.OfType<frmKainMasuk>().First().Close();
            }

            frmKainMasuk frmKainMasukInstance = new frmKainMasuk();
            frmKainMasukInstance.IsIn = 1;
            frmKainMasukInstance.MdiParent = this;
            frmKainMasukInstance.Show();
        }

        private void barangMasukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBarangMasuk>().Any())
            {
                Application.OpenForms.OfType<frmBarangMasuk>().First().Close();
            }

            frmBarangMasuk frmBarangMasukInstance = new frmBarangMasuk();
            frmBarangMasukInstance.MdiParent = this;
            frmBarangMasukInstance.Show();
        }

        private void StoreMyIpPublic(string currentPc)
        {

            string fileName = AppDomain.CurrentDomain.BaseDirectory + currentPc + @"CurrentPublicIP.txt";
            var address = "";
            var request = WebRequest.Create("http://checkip.dyndns.org/");
            using (var response = request.GetResponse())
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            var first = address.IndexOf("Address: ") + 9;
            var last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);


            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] CurrentIpPublic = new UTF8Encoding(true).GetBytes(address);
                    fs.Write(CurrentIpPublic, 0, CurrentIpPublic.Length);
                    
                }

                // Open the stream and read it back.    
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }


        }

        private string GetTokoIpPublic()
        {
            string tokoIpPublic = "";

            try
            {
                using (StreamReader sr = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"TokoCurrentPublicIP.txt"))
                {
                    tokoIpPublic = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return tokoIpPublic;
        }

        private void daftarBarangMasukToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmDaftarBarangMasuk>().Any())
            {
                Application.OpenForms.OfType<frmDaftarBarangMasuk>().First().Close();
            }

            frmDaftarBarangMasuk frmDaftarBarangMasukInstance = new frmDaftarBarangMasuk();
            frmDaftarBarangMasukInstance.MdiParent = this;
            frmDaftarBarangMasukInstance.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmTambahTransaksi frmTambahTransaksiInstance = new frmTambahTransaksi();
            frmTambahTransaksiInstance.MdiParent = this;
            frmTambahTransaksiInstance.IsIn = 1;
            frmTambahTransaksiInstance.Show();
        }

        private void daftarSuratPerintahKerjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmDaftarSuratPerintahKerja>().Any())
            {
                Application.OpenForms.OfType<frmDaftarSuratPerintahKerja>().First().Close();
            }

            frmDaftarSuratPerintahKerja frmDaftarSuratPerintahKerjaInstance = new frmDaftarSuratPerintahKerja();
            frmDaftarSuratPerintahKerjaInstance.MdiParent = this;
            frmDaftarSuratPerintahKerjaInstance.Show();
        }
    }
}
