using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Xsl;

namespace OOP_2
{

    public partial class Form1 : Form
    {
        string path = @"C:\Users\lenovo\Desktop\projects\OOP_2\XMLFile1.xml";
        List<Student> final = new List<Student>();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllStudents();
        }


        private void Transform_Click(object sender, EventArgs e)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(@"C:\Users\lenovo\Desktop\projects\OOP_2\XSLFile1.xsl");
            string input = @"C:\Users\lenovo\Desktop\projects\OOP_2\XMLFile1.xml";
            string output = @"C:\Users\lenovo\Desktop\projects\OOP_2\HTMLFile1.html";
            xslt.Transform(input, output);
            MessageBox.Show("Transformed!");
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Speciality.Checked = false;
            Group.Checked = false;
            Room.Checked = false;
            Surname.Checked = false;
            NameCheck.Checked = false;
            PhoneNumber.Checked = false;
            SpecialityBox.Text = null;
            GroupBox.Text = null;
            RoomBox.Text = null;
            SurnameBox.Text = null;
            NameBox.Text = null;
            PhoneBox.Text = null;
            LINQ.Checked = false;
            DOM.Checked = false;
            SAX.Checked = false;

            Success.Checked = false;
            SuccessBox.Text = null;
        }

        private void Search_Click(object sender, EventArgs e)
        {

            richTextBox1.Clear();
            Student _student = OurStudent();
            if (LINQ.Checked)
            {
                IStrategy CurrentStrategy = new Linq(path);
                final = CurrentStrategy.Algorithm(_student, path);
                Output(final);
            }
            if (DOM.Checked)
            {
                IStrategy CurrentStrategy = new Dom(path);
                final = CurrentStrategy.Algorithm(_student, path);
                Output(final);
            }
            if (SAX.Checked)
            {
                IStrategy CurrentStrategy = new Sax(path);
                final = CurrentStrategy.Algorithm(_student, path);
                Output(final);
            }
        }

        private Student OurStudent()
        {
            string[] info = new string[7];
            if (Speciality.Checked) info[0] = Convert.ToString(SpecialityBox.Text);
            if (Group.Checked) info[1] = Convert.ToString(GroupBox.Text);
            if (Room.Checked) info[2] = Convert.ToString(RoomBox.Text);
            if (Surname.Checked) info[3] = Convert.ToString(SurnameBox.Text);
            if (NameCheck.Checked) info[4] = Convert.ToString(NameBox.Text);
            if (PhoneNumber.Checked) info[5] = Convert.ToString(PhoneBox.Text);

            if (Success.Checked) info[6] = Convert.ToString(SuccessBox.Text);

            Student IdealStudent = new Student(info);
            return IdealStudent;

        }
        private void Output(List<Student> final)
        {
            int i = 1;
            Console.WriteLine("Alg+");
            foreach (Student n in final)
            {

                richTextBox1.AppendText(i++ + "." + "\n");
                richTextBox1.AppendText("Speciality: " + n.Speciality + "\n");
                richTextBox1.AppendText("Group: " + n.Group + "\n");
                richTextBox1.AppendText("Room: " + n.Room + "\n");
                richTextBox1.AppendText("Surname: " + n.Surname + "\n");
                richTextBox1.AppendText("Name: " + n.Name + "\n");
                richTextBox1.AppendText("Phone Number: " + n.PhoneNumber + "\n");
                richTextBox1.AppendText("Success: " + n.Success + "\n");
                richTextBox1.AppendText("--------------------------------\n");

            }
        }


        public void GetAllStudents()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\lenovo\Desktop\projects\OOP_2\XMLFile1.xml");
            XmlNodeList elem = doc.SelectNodes("//speciality");
            foreach (XmlNode e in elem)
            {
                XmlNodeList list1 = e.ChildNodes;
                foreach (XmlNode el in list1)
                {
                    XmlNodeList list2 = el.ChildNodes;
                    foreach (XmlNode ell in list2)
                    {
                        string speciality = ell.ParentNode.ParentNode.Attributes.GetNamedItem("SPECIALITY").Value;
                        if (!SpecialityBox.Items.Contains(speciality))
                            SpecialityBox.Items.Add(speciality);
                        string group = ell.ParentNode.Attributes.GetNamedItem("GROUP").Value;
                        if (!GroupBox.Items.Contains(group))
                            GroupBox.Items.Add(group);
                        string room = ell.Attributes.GetNamedItem("ROOM").Value;
                        if (!RoomBox.Items.Contains(room))
                            RoomBox.Items.Add(room);
                        string surname = ell.Attributes.GetNamedItem("SURNAME").Value;
                        if (!SurnameBox.Items.Contains(surname))
                            SurnameBox.Items.Add(surname);
                        string name = ell.Attributes.GetNamedItem("NAME").Value;
                        if (!NameBox.Items.Contains(name))
                            NameBox.Items.Add(name);
                        string phonenumber = ell.Attributes.GetNamedItem("PHONENUMBER").Value;
                        if (!PhoneBox.Items.Contains(phonenumber))
                            PhoneBox.Items.Add(phonenumber);

                        string success = ell.Attributes.GetNamedItem("SUCCESS").Value;
                        if (!SuccessBox.Items.Contains(success))
                            SuccessBox.Items.Add(success);
                        //GroupBox.Items.Add(ell.ParentNode.Attributes.GetNamedItem("GROUP").Value);
                        //RoomBox.Items.Add(ell.Attributes.GetNamedItem("ROOM").Value);
                        //SurnameBox.Items.Add(ell.Attributes.GetNamedItem("SURNAME").Value);
                        //NameBox.Items.Add(ell.Attributes.GetNamedItem("NAME").Value);
                        //PhoneBox.Items.Add(ell.Attributes.GetNamedItem("PHONENUMBER").Value);


                    }
                }
            }
        }
    }
}
