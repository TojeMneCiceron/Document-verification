using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace vibe_check
{
    class Regularochki
    {
        private static readonly Regex sppoRegex = new Regex(@"^ЛР1.?,?( |_)Ч1.?,?( |_)[А-Я]([а-я]|-|'|[А-Я])+( |_)([А-Я].)?([А-Я].)?,?[ |_]?[А-Я]{3}-[0-9]{1,2}-[0-9]{1,2}.docx?$");
        private static readonly Regex labRegex = new Regex(@"^ЛР[1-9].?,?( |_)[А-Я]([а-я]|-|'|[А-Я])+( |_)([А-Я].)?([А-Я].)?,?[ |_]?[А-Я]{3}-[0-9]{1,2}-[0-9]{1,2}.docx?$");
        private static readonly Regex pzRegex = new Regex(@"^(Д|П)З[1-9].?,?( |_)[А-Я]([а-я]|-|'|[А-Я])+( |_)([А-Я].)?([А-Я].)?,?[ |_]?[А-Я]{3}-[0-9]{1,2}-[0-9]{1,2}.docx?$");
        private static readonly Regex imageReferenceRegex = new Regex(@"(Р|р)исун[а-я]+ [1-9][0-9]?.?([1-9][0-9]?)?-?([1-9][0-9]?.?([1-9][0-9]?)?)?");
        private static readonly Regex tableReferenceRegex = new Regex(@"(Т|т)аблиц[а-я]+ [1-9][0-9]?.?([1-9][0-9]?)?-?([1-9][0-9]?.?([1-9][0-9]?)?)?");
        private static readonly Regex refNumsRegex = new Regex(@"[1-9][0-9]?.?([1-9][0-9]?)?-?([1-9][0-9]?.?([1-9][0-9]?)?)?");
        private static readonly Regex fioRegex = new Regex(@"( |_)[А-Я]([а-я]|-|'|[А-Я])+"); // [А-Я].[А-Я].
        private static readonly Regex empStrRegex = new Regex(@"^[\n\r]+$");
        private static readonly Regex pageBreakRegex = new Regex(@"^[\f\n\r]+$");
        private static readonly Regex partRegex = new Regex(@"^\d+$");
        private static readonly Regex litrRegex = new Regex(@"^\d+.$");
        private static readonly Regex subPartRegex = new Regex(@"^\d+.\d+$");
        private static readonly Regex numListRegex = new Regex(@"^\d+\)$");
        private static readonly Regex sourceRefRegex = new Regex(@"\[\d[\d-, ]*\]");
        private static readonly Regex listMarkerRegex = new Regex(@"^[абвгдежиклмнпрстуфхцчшэю]|\d+\)$");

        public static string DeleteExtraSpaces(string str)
        {
            return Regex.Replace(Regex.Replace(DeleteBorderSpaces(str), "\\s+", " "), @"[\r\n\f\t19]+", "");
        }

        public static string DeleteBorderSpaces(string str)
        {
            return Regex.Replace(Regex.Replace(str, "^\\s+", ""), "\\s+$", "");
        }

        public static bool EmptyString(string str)
        {
            return empStrRegex.IsMatch(str);
        }

        public static bool PageBreak(string str)
        {
            return pageBreakRegex.IsMatch(str);
        }

        public static bool CheckFileName_sppo(string filename)
        {
            return sppoRegex.IsMatch(filename);
        }

        public static bool CheckFileName_lab(string filename)
        {
            return labRegex.IsMatch(filename);
        }

        public static bool CheckFileName_pz(string filename)
        {
            return pzRegex.IsMatch(filename);
        }

        public static bool CheckFileName(string filename)
        {
            return sppoRegex.IsMatch(filename) || labRegex.IsMatch(filename) || pzRegex.IsMatch(filename);
        }

        public static bool IsImageCaption(string caption, int num)
        {
            Regex imRegex1 = new Regex("^Рисунок ([1-9][0-9]?|[А-Я]).?([1-9][0-9]?)? – [А-Я][A-Za-zА-Яа-я0-9\\.,!=\\+#№;:&\\*%_ -\\\"\\(\\)]+[^.][\f\r\n]?$");
            //Regex imRegex2 = new Regex("^Продолжение рисунка ([1-9][0-9]?|[А-Я]).?([1-9][0-9]?)?[\f\r\n]?$");
            return imRegex1.Matches(caption).Count > 0 /*|| imRegex2.IsMatch(caption)*/;
        }

        public static bool IsTableCaption(string caption, int num)
        {
            Regex tabRegex = new Regex("^Таблица ([1-9][0-9]?|[А-Я])?.?([1-9][0-9]?)? – [А-Я][A-Za-zА-Яа-я0-9\\.«»,!=\\+#№;:&\\*%_ -\\\"\\(\\)]+[^.][\f\r\n]?$");
            return tabRegex.Matches(caption).Count > 0;
        }

        public static bool IsTableContinuationCaption(string caption, int num)
        {
            Regex tabRegex = new Regex("^Продолжение таблицы ([1-9][0-9]?|[А-Я]).?([1-9][0-9]?)?[\f\r\n]?$");
            return tabRegex.IsMatch(caption);
        }

        public static bool FindImageReference(string text)
        {
            return imageReferenceRegex.IsMatch(text);
        }

        public static bool FindTableReference(string text)
        {
            return tableReferenceRegex.IsMatch(text);
        }

        public static (string, string) RefNums(string text)
        {
            var match = refNumsRegex.Match(text);
            string[] borders = match.Value.Split('-');

            if (borders.Length == 1)
                return (borders[0], "");
            else
                return (borders[0], borders[1]);
        }

        public static string FIO(string filename)
        {
            return fioRegex.Match(filename).Value.Remove(0, 1);
        }

        public static bool PartList(string listString)
        {
            return partRegex.IsMatch(listString);
        }

        public static bool LitrList(string listString)
        {
            return litrRegex.IsMatch(listString);
        }

        public static bool SubPartList(string listString)
        {
            return subPartRegex.IsMatch(listString);
        }

        public static bool numList(string listString)
        {
            return numListRegex.IsMatch(listString);
        }

        public static (string, string) SoftCarry(string text)
        {
            string[] att = Regex.Split(text, "\\v");

            if (att.Length == 1)
                return (att[0], "");
            else
                return (att[0], att[1]);
        }

        public static bool GetSourceRefNums(string text, ref HashSet<int> refs)
        {
            if (!text.Contains("["))
                return true;

            bool flag = true;

            var matches = sourceRefRegex.Matches(text);

            foreach (Match match in matches)
            {
                string s = match.Value;

                string from = "", to = "";
                bool _to = false;

                for (int i = 1; i < s.Length; i++)
                {
                    if (s[i] > '0' && s[i] < '9')
                    {
                        if (_to)
                            to += s[i];
                        else
                            from += s[i];
                    }

                    if (s[i] == '-')
                        _to = true;

                    if (s[i] == ',' || s[i] == ']')
                    {
                        int from_ = int.Parse(from);

                        if (to == "")
                        {
                            if (from_ == 1 || refs.Contains(from_ - 1))
                                refs.Add(from_);
                            else
                                flag = false;
                        }
                        else
                        {
                            int to_ = int.Parse(to);

                            for (int j = from_; j <= to_; j++)
                                if (j == 1 || refs.Contains(j - 1))
                                    refs.Add(j);
                                else
                                    flag = false;

                            if (from_ >= to_)
                                flag = false;
                        }

                        from = to = "";
                        _to = false;
                    }
                }
            }
            return flag;
        }

        public static bool ListMarker(string listString)
        {
            return listMarkerRegex.IsMatch(listString);
        }
    }
}
