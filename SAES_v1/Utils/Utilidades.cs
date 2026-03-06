using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SAES_v1.Utils
{
    public class Utilidades
    {
        public DropDownList BeginDropdownList(DropDownList dropDownList, DataTable dt, string CampoValue = "Clave", string campoText = "Descripcion")
        {            
            dropDownList.DataSource = dt;
            dropDownList.DataValueField = CampoValue;
            dropDownList.DataTextField = campoText;
            String clave ="";
            String descripción = "";
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                clave = Convert.ToString(dr["Clave"]);
                descripción = Convert.ToString(dr["Descripcion"]);
                if (descripción =="-- Seleccione --")
                {
                    //dr.Delete();
                    dr["Descripcion"] = "-----";
                }
            }
            dt.AcceptChanges();
            dropDownList.DataBind();
            return dropDownList;
        }

        public DropDownList BeginDropdownList(DropDownList dropDownList, string CampoValue = "Clave", string campoText = "Descripcion")
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(CampoValue);
            dt.Columns.Add(campoText);

            dt.Rows.Add(null, "-----");

            dropDownList.DataSource = dt;
            dropDownList.DataValueField = CampoValue;
            dropDownList.DataTextField = campoText;
            dropDownList.DataBind();
            return dropDownList;
        }

        public GridView BeginGrid(GridView Grid, DataTable dt = null, bool AutoGenerateColumns = false)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Grid.DataSource = dt;

                    if (AutoGenerateColumns)
                    {
                        Grid.AutoGenerateColumns = true;
                        Grid.DataBind();
                    }
                    else
                    {
                        Grid.DataBind();
                        Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                        Grid.UseAccessibleHeader = true;
                    }

                    Grid.Visible = true;
                }
                else
                {
                    Grid.DataSource = null;
                    Grid.DataBind();
                }
            }
            else
            {
                Grid.DataSource = null;
                Grid.DataBind();
            }
            
            return Grid;
        }

        public GridView BeginGrid2(GridView Grid, DataTable dt = null, bool AutoGenerateColumns = false)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Grid.DataSource = dt;

                    if (AutoGenerateColumns)
                    {
                        Grid.AutoGenerateColumns = true;
                        Grid.DataBind();
                    }
                    else
                    {
                        Grid.DataBind();
                        //Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //Grid.UseAccessibleHeader = true;
                    }

                    Grid.Visible = true;
                }
                else
                {
                    Grid.DataSource = null;
                    Grid.DataBind();
                }
            }
            else
            {
                Grid.DataSource = null;
                Grid.DataBind();
            }

            return Grid;
        }

        public GridView BeginGridForInsert(GridView Grid, DataTable dt, int NumEmptyRows, bool AutoGenerateColumns = false)
        {
            if (Grid.Columns.Count > 0)
            {
                for (int i = 1; i <= NumEmptyRows; i++)
                {
                    object[] objects = new object[Grid.Columns.Count];
                    for (int j = 0; j < objects.Length; j++)
                        objects[j] = "";

                    dt.Rows.Add(objects);
                }
            }
            

            if (dt.Rows.Count > 0)
            {
                Grid.DataSource = dt;

                if (AutoGenerateColumns)
                {
                    Grid.AutoGenerateColumns = true;
                    Grid.DataBind();
                }
                else
                {
                    Grid.DataBind();
                    Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                    Grid.UseAccessibleHeader = true;
                }

                Grid.Visible = true;
            }
            return Grid;
        }

        public GridView ClearGridView(GridView Grid)
        {
            Grid.DataSource = null;
            Grid.DataBind();
            return Grid;
        }
    }
}