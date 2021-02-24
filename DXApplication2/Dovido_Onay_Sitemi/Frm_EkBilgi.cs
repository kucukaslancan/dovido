using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Dovido_Onay_Sitemi.Connection;

namespace Dovido_Onay_Sitemi
{
    public partial class Frm_EkBilgi : DevExpress.XtraEditors.XtraForm
    {
        public static DovidoEntitie db = DbAccess.Db();
        int proformaId = 0;
        public Frm_EkBilgi()
        {
            InitializeComponent();
        }

        private void Frm_EkBilgi_Load(object sender, EventArgs e)
        {
            proformaList();
        }

        private void proformaList()
        {
            var proforma = db.C_ANTE_PROFORMA_LIST.AsNoTracking().ToList();
            gridControl1.DataSource = proforma;
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            btnClear.Visible = true;
            grpInfo.Enabled = true;
            if (gridView1.GetFocusedRowCellValue("ID") != null)
            {
                proformaId = 1;
                btnSave.Text = "Güncelle";
                btnDelete.Enabled = true;
                var n = gridView1.GetFocusedRowCellValue("PROFORMA_NO").ToString();
                var checkPr = db.C_ANT_PROFORMA_BILGI.SingleOrDefault(t => t.PROFORMA_NO == n);
                txtAciklama.Text = (checkPr.ACIKLAMA == null) ? "" : checkPr.ACIKLAMA;
                txtHafta.Value = (checkPr.HAFTA == null) ? 0 : Convert.ToDecimal(checkPr.HAFTA);
                txtProformaNo.Text = (checkPr.PROFORMA_NO == null) ? "" : checkPr.PROFORMA_NO;
                cmbAmbalaj.Text = (checkPr.AMBALAJ_TIPI == null) ? "" : checkPr.AMBALAJ_TIPI;
                txtPalet1.Text = (checkPr.PALET_BILGI == null) ? "" : checkPr.PALET_BILGI;
                txtPalet2.Text = (checkPr.PALET_BILGI2 == null) ? "" : checkPr.PALET_BILGI2;
            }
            else
            {
                btnDelete.Enabled = false;
                btnSave.Text = "Kaydet";
                txtProformaNo.Text = gridView1.GetFocusedRowCellValue("SIP_NO").ToString();
                txtAciklama.Text = "";
                txtHafta.Value = 0;
                txtPalet1.Text = "";
                txtPalet2.Text = "";
                cmbAmbalaj.Text = "";
                proformaId = 0;
            }
            
           
           // siparisEkran.lblBaskiSekli.Text = (gridView1.GetFocusedRowCellValue("BASKI_SEKLI") == null) ? "-" : gridView1.GetFocusedRowCellValue("BASKI_SEKLI").ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (proformaId == 0)
            {
                if (txtProformaNo.Text != "")
                {
                    C_ANT_PROFORMA_BILGI pr = new C_ANT_PROFORMA_BILGI();
                    pr.ACIKLAMA = (txtAciklama.Text == "") ? null : txtAciklama.Text;
                    pr.AMBALAJ_TIPI = (cmbAmbalaj.Text == "") ? null : cmbAmbalaj.Text;
                    pr.HAFTA = (txtHafta.Value == 0) ? 0 : Convert.ToInt32(txtHafta.Value);
                    pr.PALET_BILGI = (txtPalet1.Text == "") ? null : txtPalet1.Text;
                    pr.PALET_BILGI2 = (txtPalet2.Text == "") ? null : txtPalet2.Text;
                    pr.PROFORMA_NO = txtProformaNo.Text;
                    pr.DURUM = 0;
                    db.C_ANT_PROFORMA_BILGI.Add(pr);
                    if (db.SaveChanges() > 0)
                    {
                        grpInfo.Enabled = false;
                        btnClear.Visible = false;
                        proformaClear();
                        proformaList();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Proforma No Boş!");
                }

            }
            else
            {
                int id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID").ToString());
                var pr = db.C_ANT_PROFORMA_BILGI.SingleOrDefault(t => t.ID == id);
                if (pr == null)
                {
                    XtraMessageBox.Show("Böyle bir proforma sistemde kayıtlı değil! Lütfen Kontrol ediniz");
                }
                else
                {
                    pr.ACIKLAMA = (txtAciklama.Text == "") ? null : txtAciklama.Text;
                    pr.AMBALAJ_TIPI = (cmbAmbalaj.Text == "") ? null : cmbAmbalaj.Text;
                    pr.HAFTA = (txtHafta.Value == 0) ? 0 : Convert.ToInt32(txtHafta.Value);
                    pr.PALET_BILGI = (txtPalet1.Text == "") ? null : txtPalet1.Text;
                    pr.PALET_BILGI2 = (txtPalet2.Text == "") ? null : txtPalet2.Text;
                    pr.PROFORMA_NO = (txtProformaNo.Text == "") ? null : txtProformaNo.Text;
                    if (db.SaveChanges() > 0)
                    {
                        grpInfo.Enabled = false;
                        btnClear.Visible = false;
                        proformaClear();
                        proformaList();
                    }

                }
            }
        }

        private void proformaClear()
        {
            btnDelete.Enabled = false;
            txtProformaNo.Text = "";
            txtAciklama.Text = "";
            txtHafta.Value = 0;
            txtPalet1.Text = "";
            txtPalet2.Text = "";
            cmbAmbalaj.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (proformaId > 0)
            {
                btnSave.Text = "Kaydet";
            }
            proformaClear();
            proformaId = 0;
            grpInfo.Enabled = false;
            btnClear.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = XtraMessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Dikkat! Veriyi Silmek Üzeresiniz!", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID").ToString());
                var deleted = db.C_ANT_PROFORMA_BILGI.SingleOrDefault(t => t.ID == id);
                db.C_ANT_PROFORMA_BILGI.Remove(deleted);
                if (db.SaveChanges() > 0 )
                {
                    grpInfo.Enabled = false;
                    btnClear.Visible = false;
                    proformaClear();
                    proformaList();
                }
            }
        }
    }
}