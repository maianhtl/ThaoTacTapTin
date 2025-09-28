using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Read_JSON
{
    public partial class fMainForm : Form
    {
        public fMainForm()
        {
            InitializeComponent();
        }

        private void btnDocFileJSON_Click(object sender, EventArgs e)
        {
            string str = "";
            string path = "students.json";
            List<StudentInfo> list = LoadJSON(path);

            for (int i = 0; i < list.Count; i++)
            {
                StudentInfo info = list[i];
                str += string.Format("Sinh viên {0} có MSSV: {1}, họ tên: {2}," +
                    "điểm TB: {3}\r\n", (i + 1), info.MSSV, info.HoTen, info.Diem);
            }
            MessageBox.Show(str);
        }

        private List<StudentInfo> LoadJSON(string path)
        {
            List<StudentInfo> list = new List<StudentInfo>();
            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd(); //đọc hết

            var array = (JObject)JsonConvert.DeserializeObject(json); ;

            var students = array["sinhvien"].Children();

            foreach (var item in students)
            {
                string mssv = item["MSSV"].Value<string>();
                string hoten = item["hoten"].Value<string>();
                int tuoi = item["tuoi"].Value<int>();
                double diem = item["diem"].Value<double>();
                bool tongiao = item["tongiao"].Value<bool>();

                StudentInfo info = new StudentInfo(mssv, hoten, tuoi, diem, tongiao);
                list.Add(info);
            }
            return list;
        }

        
    }
}
