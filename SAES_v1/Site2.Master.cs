using MySql.Data.MySqlClient;
using SAES_Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static iTextSharp.text.pdf.AcroFields;

namespace SAES_v1
{
    public partial class Site2 : System.Web.UI.MasterPage
    {
        Menu mnu = new Menu();
        protected System.Web.UI.WebControls.Repeater SubMenu;
        MenuService serviceMenu = new MenuService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated || Session["rol"] == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            else

            {
                Menu.DataSource = serviceMenu.obtenListMenuPrincipal();
                Menu.DataBind();

            }





        }
        protected void logout_btn_Click(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            else
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Abandon();
                Session.Clear();
                Response.Redirect("Default.aspx");
            }
        }
        protected void mnu_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            //smd.ShowStartingNode = false;
            //smd.Provider = testXmlProvider;
            //SiteMapPath1.Provider = testXmlProvider;
            Repeater cbi = (Repeater)(sender);
            string clave;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptSubMenu = e.Item.FindControl("SubMenu") as Repeater;
                string customerId = (e.Item.FindControl("hddnClavePadre") as HiddenField).Value;

                //{
                rptSubMenu.DataSource = serviceMenu.obtenListSubMenu(1, customerId);
                //GetData("SELECT ParentMenuId, Title, Url FROM Menus WHERE ParentMenuId =" + ((System.Data.DataRowView)(e.Item.DataItem)).Row[0]);
                rptSubMenu.DataBind();
            }

          
        }
        protected void submnu1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            //smd.ShowStartingNode = false;
            //smd.Provider = testXmlProvider;
            //SiteMapPath1.Provider = testXmlProvider;
            Repeater cbi = (Repeater)(sender);
            string clave;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptSubSubMenu = e.Item.FindControl("SubSubMenu") as Repeater;
                string customerId = (e.Item.FindControl("hddnClaveSub") as HiddenField).Value;

                //{
                rptSubSubMenu.DataSource = serviceMenu.obtenListSubMenu(2, customerId);
                //GetData("SELECT ParentMenuId, Title, Url FROM Menus WHERE ParentMenuId =" + ((System.Data.DataRowView)(e.Item.DataItem)).Row[0]);
                rptSubSubMenu.DataBind();
            }

            //RepeaterItem item = e.Item;
            //if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            //{
            //    //SubMenu = (Repeater)item.FindControl("SubMenu");
            //    //SiteMapNode drv = (SiteMapNode)item.DataItem;
            //    SubMenu.DataSource = serviceMenu.obtenListSubMenu(item.ID); //drv.ChildNodes;
            //    SubMenu.DataBind();
            //}
        }
        protected void subsubmnu1_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            //smd.ShowStartingNode = false;
            //smd.Provider = testXmlProvider;
            //SiteMapPath1.Provider = testXmlProvider;
            Repeater cbi = (Repeater)(sender);
            string clave;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptSubSubSubMenu = e.Item.FindControl("rptSubSubSubMenu") as Repeater;
                string customerId = (e.Item.FindControl("hddnClaveSubSub") as HiddenField).Value;

                //{
                rptSubSubSubMenu.DataSource = serviceMenu.obtenListSubMenu(2, customerId);
                //GetData("SELECT ParentMenuId, Title, Url FROM Menus WHERE ParentMenuId =" + ((System.Data.DataRowView)(e.Item.DataItem)).Row[0]);
                rptSubSubSubMenu.DataBind();
            }

            //RepeaterItem item = e.Item;
            //if ((item.ItemType == ListItemType.Item) || (item.ItemType == ListItemType.AlternatingItem))
            //{
            //    //SubMenu = (Repeater)item.FindControl("SubMenu");
            //    //SiteMapNode drv = (SiteMapNode)item.DataItem;
            //    SubMenu.DataSource = serviceMenu.obtenListSubMenu(item.ID); //drv.ChildNodes;
            //    SubMenu.DataBind();
            //}
        }

    }
}