using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using BeerService;

//AutoGen ag = new AutoGen("RepoAM",Request.PhysicalApplicationPath + "App_Data/");
public class AutoGen
{

    private string RepoName = "RepoAM";
    private SqlCommand CMD;
    private string path;

    public AutoGen(string repoName, string outputPath)
    {
     
        path = outputPath;
        RepoName = repoName;

        string SQL = "SELECT Table_Name as Name FROM information_schema.tables";

        CMD = new SqlCommand(SQL);

        foreach (DataRow row in GetData(CMD, Conn.GetCon()).Rows)
        {
            MaikFolders();
            CreateClases(row["Name"].ToString());
            GenProClasses(row["Name"].ToString());

        }

        //txtDone.Text += "Done!";
    }

    private void CreateClases(string tableName)
    {
        string strFacName = tableName;
        string strClass = "";

        strClass += "using System;" + "\n\n";

        strClass += "namespace " + RepoName + "\n{" + "\n";

        strClass += "\n\n";
        strClass += "\t public class " + strFacName + "Fac:AutoFac<" + tableName + ">\n";
        strClass += "\t {" + "\n";


        strClass += "\n\n";
        strClass += "\t }";

        strClass += "\n\n";
        strClass += "}";

        System.IO.StreamWriter writer = System.IO.File.CreateText(path + "/AutoGen/Factories/" + strFacName + "Fac.cs");
        writer.WriteLine(strClass);
        writer.Close();
        //txtDone.Text += strFacName + "Fac.cs\n";

    }


    void GenProClasses(string tableName)
    {
        string strName = tableName;
        string strClass = "";
        string pro = "";
        string SQL = "SELECT Column_Name as Name, data_type as Type FROM information_schema.columns WHERE Table_Name='" + tableName + "'";
        //string SQL = "SELECT Table_Name as Name FROM information_schema.tables";

        CMD = new SqlCommand(SQL);

        foreach (DataRow row in GetData(CMD, Conn.GetCon()).Rows)
        {
            string name = row["Name"].ToString();
            string typ = PropDataTypeConverter(row["Type"].ToString());

            pro += "\t\t public " + typ + " " + name + " { get; set; }\n\n";

        }
        strClass += "using System;\n\n";
        strClass += "namespace " + RepoName + "\n{" + "\n\n";
        strClass += "\t public class " + strName + "\n";
        strClass += "\t{\n";


        strClass += pro;

        strClass += "\t }";

        strClass += "\n\n";
        strClass += "}";

        System.IO.StreamWriter writer = System.IO.File.CreateText(path + "/AutoGen/Models/" + strName + ".cs");
        writer.WriteLine(strClass);
        writer.Close();
        // txtDone.Text += strName + ".cs\n";
    }


    public DataTable GetData(SqlCommand CMD, SqlConnection CON)
    {

        DataSet objDS = new DataSet();
        SqlDataAdapter objDA = new SqlDataAdapter();

        CMD.Connection = CON;
        objDA.SelectCommand = CMD;
        objDA.Fill(objDS);

        return objDS.Tables[0];
    }

    string SQLDataTypeConverter(string strDataType)
    {
        string res = "";

        switch (strDataType)
        {
            case "int":
                res = "Int";
                break;
            case "varchar":
                res = "VarChar";
                break;
            case "dateTime":
                res = "DateTime";
                break;
            case "text":
                res = "Text";
                break;
            case "float":
                res = "Float";
                break;
            case "decimal":
                res = "Decimal";
                break;
            default:
                res = "VarChar";
                break;
        }
        return res;
    }

    string PropDataTypeConverter(string strDataType)
    {
        string res = "";

        switch (strDataType)
        {
            case "int":
                res = "int";
                break;
            case "varchar":
                res = "string";
                break;
            case "datetime":
                res = "DateTime";
                break;
            case "text":
                res = "string";
                break;
            case "decimal":
                res = "decimal";
                break;
            case "float":
                res = "float";
                break;
            case "bit":
                res = "bool";
                break;
            default:
                res = "string";
                break;
        }
        return res;
    }

    void MaikFolders()
    {
        if (!Directory.Exists(path + "/AutoGen/"))
        {
            DirectoryInfo di = Directory.CreateDirectory(path + "/AutoGen/");
            //txtDone.Text += "/App_Code/\n";
        }

        if (!Directory.Exists(path + "/AutoGen/Models/"))
        {
            DirectoryInfo di = Directory.CreateDirectory(path + "/AutoGen/Models/");
            //txtDone.Text += "/App_Code/Types/\n";
        }

        if (!Directory.Exists(path + "/AutoGen/Factories/"))
        {
            DirectoryInfo di = Directory.CreateDirectory(path + "/AutoGen/Factories/");
            //txtDone.Text += "/App_Code/Factories/\n";
        }

        //string sti = Path.GetDirectoryName(_AppPath);
        //System.IO.File.Copy(sti + "/Classes/Data/DataAccess.cs", path + "/App_Code/DataAccess.cs", true);
        //File.Copy(sti + "/Classes/Data/web.config", path + "/web.config", true);
        //txtDone.Text += "/App_Code/DataAccess/ \n";



    }
}
