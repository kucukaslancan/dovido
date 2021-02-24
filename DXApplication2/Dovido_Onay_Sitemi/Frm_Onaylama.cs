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
    public partial class Frm_Onaylama : DevExpress.XtraEditors.XtraForm
    {
        public static DovidoEntitie db = DbAccess.Db();
        public Frm_Onaylama()
        {
            InitializeComponent();
        }
        private void proformaList()
        {
            var proforma = db.C_ANTE_PROFORMA_LIST.AsNoTracking().Where(t => t.DURUM == 0).ToList();
            grdProforma.DataSource = proforma;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                if (gridView1.IsRowSelected(i))
                {
                    /*
                     * BURDA SEÇİLEN SATIRLARIN ID DEĞERİNİ ALIYORUM ONA GÖRE KAYIT YAPABİLİRSİN ID ÜZERİNDEN ÇEKİP
                     * GÜNCELLEME İÇİNDE BURDAKİ ID DEĞERİNİ KULLANABİLİRSİN NETOPENX TARAFINDAN OLUMLU YANIT GELİRSE.
                     **/
                   // gridView1.GetRowCellValue(i, gridView1.Columns[0]);
                }
                   
            }
        }

        private void Frm_Onaylama_Load(object sender, EventArgs e)
        {
            proformaList();
        }
    }
}