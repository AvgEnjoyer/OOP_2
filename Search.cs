using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace OOP_2
{
    interface IStrategy
    {
        List<Student> Algorithm(Student p, string path);
    }



    class Linq : IStrategy
    {
        List<Student> info = new List<Student>();
        XDocument doc = new XDocument();
        public Linq(string path)
        {
            doc = XDocument.Load(path);
        }
        public List<Student> Algorithm(Student student, string path)
        {
            List<XElement> match = (from val in doc.Descendants("student")
                                    where
                                    ((student.Speciality == null || student.Speciality == val.Parent.Parent.Attribute("SPECIALITY").Value) &&
                                     (student.Group == null || student.Group == val.Parent.Attribute("GROUP").Value) &&
                                     (student.Room == null || student.Room == val.Attribute("ROOM").Value) &&
                                     (student.Surname == null || student.Surname == val.Attribute("SURNAME").Value) &&
                                     (student.Name == null || student.Name == val.Attribute("NAME").Value) &&
                                     (student.PhoneNumber == null || student.PhoneNumber == val.Attribute("PHONENUMBER").Value)

                                     &&
                                     (student.Success == null || student.Success == val.Attribute("SUCCESS").Value))
                                    select val).ToList();
            foreach (XElement obj in match)
            {
                Student student1 = new Student();
                student1.Speciality = obj.Parent.Parent.Attribute("SPECIALITY").Value;
                student1.Group = obj.Parent.Attribute("GROUP").Value;
                student1.Room = obj.Attribute("ROOM").Value;
                student1.Surname = obj.Attribute("SURNAME").Value;
                student1.Name = obj.Attribute("NAME").Value;
                student1.PhoneNumber = obj.Attribute("PHONENUMBER").Value;

                student1.Success = obj.Attribute("SUCCESS").Value;
                info.Add(student1);
            }
            return info;
        }
    }













    class Dom : IStrategy
    {
        XmlDocument doc = new XmlDocument();
        public Dom(string path)
        {
            doc.Load(path);
        }
        public List<Student> Algorithm(Student student, string path)
        {
            List<List<Student>> info = new List<List<Student>>();
            try
            {
                if (student.Speciality != null) info.Add(SearchByParam("speciality", "SPECIALITY", student.Speciality, doc, 0));
                if (student.Group != null) info.Add(SearchByParam("group", "GROUP", student.Group, doc, 1));
                if (student.Room != null) info.Add(SearchByParam("student", "ROOM", student.Room, doc, 2));
                if (student.Surname != null) info.Add(SearchByParam("student", "SURNAME", student.Surname, doc, 2));
                if (student.Name != null) info.Add(SearchByParam("student", "NAME", student.Name, doc, 2));

                if (student.Success != null) info.Add(SearchByParam("student", "SUCCESS", student.Success, doc, 2));

                if (student.PhoneNumber != null) info.Add(SearchByParam("student", "PHONENUMBER", student.PhoneNumber, doc, 2));
            }
            catch { }
            return Cross(info);

        }


        public static Student Info(XmlNode node)
        {
            Student nw = new Student();
            nw.Speciality = node.ParentNode.ParentNode.Attributes.GetNamedItem("SPECIALITY").Value;
            nw.Group = node.ParentNode.Attributes.GetNamedItem("GROUP").Value;
            nw.Room = node.Attributes.GetNamedItem("ROOM").Value;
            nw.Surname = node.Attributes.GetNamedItem("SURNAME").Value;
            nw.Name = node.Attributes.GetNamedItem("NAME").Value;
            nw.PhoneNumber = node.Attributes.GetNamedItem("PHONENUMBER").Value;

            nw.Success = node.Attributes.GetNamedItem("SUCCESS").Value;
            return nw;
        }

        public static List<Student> Allstudents(XmlDocument doc)
        {
            List<Student> data2 = new List<Student>();
            XmlNodeList elem = doc.SelectNodes("//student");
            try
            {
                foreach (XmlNode el in elem)
                    data2.Add(Info(el));
            }
            catch { }
            return data2;
        }


        public static List<Student> SearchByParam(string nodename, string val, string param, XmlDocument doc, int n)
        {
            List<Student> students = new List<Student>();
            if (param != String.Empty && param != null)
            {
                switch (n)
                {
                    case 0:
                        {
                            XmlNodeList elem = doc.SelectNodes("//" + nodename + "[@" + val + "=\"" + param + "\"]");
                            try
                            {
                                foreach (XmlNode e in elem)
                                {
                                    XmlNodeList list1 = e.ChildNodes;
                                    foreach (XmlNode el in list1)
                                    {
                                        XmlNodeList list2 = el.ChildNodes;
                                        foreach (XmlNode ell in list2)
                                        {
                                            students.Add(Info(ell));
                                        }
                                    }
                                }
                            }


                            catch { }
                            return students;

                        }
                    case 1:
                        {
                            XmlNodeList elem = doc.SelectNodes("//" + nodename + "[@" + val + "=\"" + param + "\"]");
                            try
                            {
                                
                                    foreach (XmlNode el in elem)
                                    {
                                        XmlNodeList list2 = el.ChildNodes;
                                        foreach (XmlNode ell in list2)
                                        {
                                            students.Add(Info(ell));
                                        }
                                    }
                                
                            }


                            catch { }
                            return students;

                        }
                    case 2:
                        {
                            XmlNodeList elem = doc.SelectNodes("//" + nodename + "[@" + val + "=\"" + param + "\"]");
                            try
                            {
                                foreach (XmlNode e in elem)
                                {
                                    students.Add(Info(e));
                                }
                            }
                            catch { }
                            return students;
                        }
                    default: break;

                }
            }
            return students;
        }

        private static List<Student> Cross(List<List<Student>> list)
        {

            List<Student> result = new List<Student>();
            try
            {
                if (list != null)
                {
                    Student[] st = list[0].ToArray();
                    if (st != null)
                    {
                        foreach (Student elem in st)
                        {
                            bool IsIn = true;
                            foreach (List<Student> t in list)
                            {
                                if (t.Count <= 0) return new List<Student>();
                                foreach (Student s in t)
                                {
                                    IsIn = false;
                                    if (elem.Comparing(s))
                                    {
                                        IsIn = true;
                                        break;
                                    }
                                }
                                if (!IsIn) break;
                            }
                            if (IsIn)
                            {
                                result.Add(elem);
                            }
                        }
                    }
                }
            }
            catch { }
            return result;
        }



    }
    class Sax : IStrategy
    {
        List<Student> info = new List<Student>();
        XmlDocument BestReader = new XmlDocument();
        public Sax(string path)
        {
            BestReader.Load(path);
        }
        public List<Student> Algorithm(Student student, string path)
        {
            XmlReader BestReader = XmlReader.Create(path);
            info.Clear();
            List<Student> result = new List<Student>();
            Student st = null;
            string _speciality = null;

            string _group = null;
            while (BestReader.Read())
            {
                if (BestReader.Name == "speciality")
                {
                    while (BestReader.MoveToNextAttribute())
                    {
                        if (BestReader.Name == "SPECIALITY")
                        {
                            _speciality = BestReader.Value;
                        }
                    }
                }
                if (BestReader.Name == "group")
                {
                    while (BestReader.MoveToNextAttribute())
                    {
                        if (BestReader.Name == "GROUP")
                        {
                            _group = BestReader.Value;
                        }
                    }

                }
                if (BestReader.Name == "student")
                {
                    if (st == null)
                    {
                        st = new Student();
                        st.Speciality = _speciality;
                        st.Group = _group;
                    }
                    else
                    {
                        st = new Student();
                        st.Speciality = _speciality;
                        st.Group = _group;
                    }

                    if (BestReader.HasAttributes)
                    {
                        while (BestReader.MoveToNextAttribute())
                        {
                            if (BestReader.Name == "ROOM")
                            {
                                st.Room = BestReader.Value;
                            }
                            if (BestReader.Name == "SURNAME")
                            {
                                st.Surname = BestReader.Value;
                            }
                            if (BestReader.Name == "NAME")
                            {
                                st.Name = BestReader.Value;
                            }
                            if (BestReader.Name == "PHONENUMBER")
                            {
                                st.PhoneNumber = BestReader.Value;
                            }

                            if (BestReader.Name == "SUCCESS")
                            {
                                st.Success = BestReader.Value;
                            }
                        }
                    }
                    if (st.Surname != null)
                    {
                        result.Add(st);
                    }
                }
            }
            info = Filtr(result, student);
            return info;
        }







        public List<Student> Filtr(List<Student> allStud, Student param)
        {
            List<Student> result = new List<Student>();
            if (allStud != null)
            {
                foreach (Student e in allStud)
                {
                    try
                    {
                        if (
                             (e.Speciality == param.Speciality || param.Speciality == null) &&
                             (e.Group == param.Group || param.Group == null) &&
                             (e.Room == param.Room || param.Room == null) &&
                             (e.Surname==param.Surname || param.Surname==null) &&
                             (e.Name==param.Name || param.Name == null) &&
                             (e.PhoneNumber==param.PhoneNumber || param.PhoneNumber == null)
                             &&
                             (e.Success == param.Success || param.Success == null)
                            )
                            
                       {
                            result.Add(e);
                       }
                    }
                    catch { }
                } 
            }
            return result;
        }
    }
}