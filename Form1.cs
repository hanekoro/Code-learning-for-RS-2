using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        List<Class1> classes = new List<Class1>();
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            folderBrowserDialog1.Description = "请选择文件夹";
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath; // 获取选中的文件夹路径
            }
            string[] files = Directory.GetFiles(folderPath, "", SearchOption.AllDirectories);
            foreach (string filePath in files)
            {

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding("GB2312"));
                for (int i = 0; i <8; i++)
                {
                    sr.ReadLine();
                }
                string line = sr.ReadLine();
                int index1 = line.IndexOf("总体精度：");
                int index2 = line.IndexOf(",K");
                double points = Convert.ToDouble(line.Substring(index1 + 5, index2 - (index1 + 5)));
                Class1 cls1 = new Class1(filePath, points);
                classes.Add(cls1);
                /*string line = sr.ReadToEnd();
                int index1 = line.IndexOf("总体精度：");
                int index2 = line.IndexOf(",K");
                double points = Convert.ToDouble(line.Substring(index1 + 5, index2 - (index1 + 5)));
                Class1 cls1 = new Class1(filePath, points);
                classes.Add(cls1);*/
                sr.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var classes1 = classes.OrderByDescending(t => t.Points).ToList();
            richTextBox1.Text = classes1[0].Filename+"\n"+ classes1[1].Filename;
        }
    }
}
