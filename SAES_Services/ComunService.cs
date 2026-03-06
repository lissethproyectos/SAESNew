using SAES_DBA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace SAES_Services
{
    public class ComunService

    {
        public void LlenaCombo(string SP, ref DropDownList DDL)// ref List<ModelComun> list)
        {
            DBA objData = new DBA();
            DataSet Cursor = new DataSet();
            List<ModelComun> Lista = new List<ModelComun>();
            
            DDL.Items.Clear();

            Cursor = objData.ExcecSP(SP);// ("ObtenerListaRoles");


            foreach (DataRow dr in Cursor.Tables[0].Rows)
            {
                ModelComun objModelRol = new ModelComun();
                objModelRol.IdStr = dr.ItemArray[0].ToString();
                objModelRol.Descripcion = dr.ItemArray[1].ToString();
                Lista.Add(objModelRol);
            }

            if (Lista.Count > 0)
            {
                DDL.DataSource = Lista;
                DDL.DataValueField = "IdStr";
                DDL.DataTextField = "Descripcion";
                DDL.DataBind();
            }
            else
            {
                DDL.Items.Add("La opción no contiene datos");
            }

        }
    }
}
