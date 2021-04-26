using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

namespace vibe_check
{
    enum CheckMode
    {
        Surface, Structure, Deep
    }

    class VibeChecker
    {
        int LONG_TIRE_CODE = Config.Default.LONG_TIRE_CODE;
        float FIRST_LINE = Config.Default.FIRST_LINE, TOP_BOTTOM = Config.Default.TOP_BOTTOM, LEFT = Config.Default.LEFT, RIGHT = Config.Default.RIGHT;
        //const float eps = 0.1f;

        HashSet<int> refs = new HashSet<int>();

        enum ImageStatus
        {
            None, Img, Cap
        }
        ImageStatus imCheck = ImageStatus.None;
        string imNumber = "";

        enum TableStatus
        {
            None, Cap, Tab1Row, Tab
        }
        TableStatus tabCheck = TableStatus.None;
        string tabNumber = "";

        enum ListStatus
        {
            None, List,
        }
        ListStatus listStatus = ListStatus.None;

        enum AttachCheck
        {
            None, Name
        }
        AttachCheck atCheck = AttachCheck.None;
        string att = "";

        string path;
        string savePath;
        string fileName = "";
        string mainFolderName = "Проверенные документы";
        string folderName = "";
        string folderPath;

        bool fioFound = false;

        public string SavePath { get => savePath; set => savePath = value; }
        public string FolderPath { get => folderPath; set => folderPath = value; }

        Dictionary<string, int> Structure;
        int CurRazdel = 0;
        int usedSourcesCount = 0;

        int curPage = 0;
        bool newPage = false;

        bool _continue;

        HashSet<string> RequiredParts, MainParts, SubParts;
        Dictionary<string, string> Attachments;

        CheckMode checkMode;

        public VibeChecker(CheckMode checkMode, string path, string fileName, Dictionary<string, int> structure,
            HashSet<string> requiredParts, HashSet<string> mainParts, HashSet<string> subParts, Dictionary<string, string> attachments, DateTime dateTime)
        {
            this.checkMode = checkMode;
            this.path = path;
            this.fileName = fileName;
            Structure = structure;
            RequiredParts = requiredParts;
            MainParts = mainParts;
            SubParts = subParts;
            Attachments = attachments;
            folderName = dateTime.Day + "_" + dateTime.Month + "_" + dateTime.Year + " " + dateTime.Hour + "_" + dateTime.Minute;
        }

        void Close(Microsoft.Office.Interop.Word.Application app)
        {
            if (app != null)
                app.Quit();
        }

        void Close(Document doc, Microsoft.Office.Interop.Word.Application app)
        {
            if (doc != null)
                doc.Close();
            if (app != null)
                app.Quit();
        }

        string CreatePath()
        {
            string appPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string folder = Path.GetDirectoryName(appPath);
            folder += ("\\" + mainFolderName);

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            folderPath = folder + "\\" + folderName;

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            return folderPath + "\\" + fileName;
        }

        void Save(Document doc)
        {
            savePath = CreatePath();
            doc.SaveAs2(savePath);
        }

        //bool ColorCheck(Style style)
        //{
        //    return style.Font.Color != WdColor.wdColorAutomatic && style.Font.Color != WdColor.wdColorBlack;
        //}

        bool CommonCheck(Style style)
        {
            return style.Font.Name != Config.Default.Font
                //|| (style.Font.Color != WdColor.wdColorAutomatic && style.Font.Color != WdColor.wdColorBlack)
                || style.Font.Size != Config.Default.FontSize
                //|| style.ParagraphFormat.LineSpacingRule != WdLineSpacing.wdLineSpace1pt5
                || style.Font.Italic == 1;
            //   || range.Font.Underline != WdUnderline.wdUnderlineNone;
        }

        bool TextCheck(Style style)
        {
            return CommonCheck(style)
                || style.ParagraphFormat.FirstLineIndent - FIRST_LINE > 1
                || style.Font.Bold == 1
                || style.ParagraphFormat.LineSpacingRule != WdLineSpacing.wdLineSpace1pt5
                || style.Font.AllCaps == 1
                || style.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphJustify;
        }

        bool RequiredPartHeaderCheck(Style style)
        {
            return CommonCheck(style) ||
                style.Font.AllCaps == 0
                || style.Font.Bold == 1
                || style.ParagraphFormat.CharacterUnitFirstLineIndent != 0
                || style.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphCenter;
        }

        bool MainPartHeaderCheck(Style style, string listMarker)
        {
            return CommonCheck(style) ||
                style.Font.AllCaps == 1
                || style.Font.Bold == 0
                || style.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphLeft
                || !Regularochki.PartList(listMarker)
                || style.ParagraphFormat.FirstLineIndent - FIRST_LINE > 1;
        }

        bool MainSubPartHeaderCheck(Style style, string listMarker)
        {
            return CommonCheck(style) ||
                style.Font.AllCaps == 1
                || style.Font.Bold == 0
                || style.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphLeft
                || !Regularochki.SubPartList(listMarker)
                || style.ParagraphFormat.FirstLineIndent - FIRST_LINE > 1;
        }

        bool ImageCaptionCheck(Style style)
        {
            return CommonCheck(style) ||
                style.Font.AllCaps == 1
                || style.Font.Bold == 1
                || style.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphCenter
                || style.ParagraphFormat.CharacterUnitFirstLineIndent != 0
                || style.ParagraphFormat.LineSpacingRule != WdLineSpacing.wdLineSpaceSingle;
        }

        bool TableCaptionCheck(Style style)
        {
            return CommonCheck(style) ||
                style.Font.AllCaps == 1
                || style.Font.Bold == 1
                || style.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphLeft
                || style.ParagraphFormat.CharacterUnitFirstLineIndent != 0
                || style.ParagraphFormat.LineSpacingRule != WdLineSpacing.wdLineSpaceSingle;
        }

        bool AttachmentCheck(Style style)
        {
            return CommonCheck(style) ||
                style.Font.AllCaps == 1
                || style.Font.Bold == 0
                || style.ParagraphFormat.CharacterUnitFirstLineIndent != 0
                || style.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphCenter;
        }

        bool UsedSourceCheck(Style style, string listMarker)
        {
            return CommonCheck(style) ||
                style.Font.AllCaps == 1
                || style.Font.Bold == 1
                || style.ParagraphFormat.Alignment != WdParagraphAlignment.wdAlignParagraphJustify
                || !Regularochki.LitrList(listMarker)
                || style.ParagraphFormat.FirstLineIndent - FIRST_LINE > 1;
        }

        bool FieldsCheck(PageSetup pageSetup, Microsoft.Office.Interop.Word.Application app)
        {
            if (pageSetup.Orientation == WdOrientation.wdOrientPortrait)
                return pageSetup.TopMargin - TOP_BOTTOM > 1
                    || pageSetup.BottomMargin - TOP_BOTTOM > 1
                    || pageSetup.LeftMargin - LEFT > 1
                    || pageSetup.RightMargin - RIGHT > 1;
            else
                return pageSetup.TopMargin - LEFT > 1
                    || pageSetup.BottomMargin - RIGHT > 1
                    || pageSetup.LeftMargin - TOP_BOTTOM > 1
                    || pageSetup.RightMargin - TOP_BOTTOM > 1;
        }

        //bool ListCheck(Style style)
        //{
        //    return 
        //}

        string ListCheck(Paragraph paragraph)
        {
            string comm = "";

            Style style = paragraph.get_Style();
            string listMarker = paragraph.Range.ListFormat.ListString;
            string text = Regularochki.DeleteExtraSpaces(paragraph.Range.Text);

            switch (listStatus)
            {
                case ListStatus.None:
                    if (text[text.Length - 1] == ':')
                        listStatus = ListStatus.List;

                    if (listMarker != "")
                        comm = "Не найден абзац, инициализирующий начало списка";
                    break;

                case ListStatus.List:

                    if (listMarker == "")
                        comm = "Ожидался маркер списка";
                    else
                    if (listMarker[0] != LONG_TIRE_CODE || !Regularochki.ListMarker(listMarker))
                        comm = "Неверный маркер списка";

                    if (text[text.Length - 1] == '.')
                        listStatus = ListStatus.None;
                    break;
            }

            return comm;
        }

        int PageNumber(Range range)
        {
            return (int)range.get_Information(WdInformation.wdActiveEndPageNumber);
        }

        string ImageCheck(Paragraph paragraph)
        {
            string comm = "";

            InlineShapes inShapes = paragraph.Range.InlineShapes;

            switch (imCheck)
            {
                case ImageStatus.None:
                    if (Regularochki.FindImageReference(paragraph.Range.Text) && !Regularochki.IsImageCaption(paragraph.Range.Text, 0))
                        imCheck = ImageStatus.Img;

                    if (inShapes.Count != 0)
                    {
                        comm = "Не найдена ссылка на рисунок";
                        imCheck = ImageStatus.Cap;
                        _continue = true;
                    }

                    break;

                case ImageStatus.Img:
                    if (paragraph.Range.Tables.Count == 0 && inShapes.Count == 0)
                    {
                        comm = "Не найден рисунок";
                        imCheck = ImageStatus.None;
                        break;
                    }

                    //if (paragraph.Alignment != WdParagraphAlignment.wdAlignParagraphCenter)
                    //    comm = "Неверно оформлен рисунок";

                    _continue = true;
                    imCheck = ImageStatus.Cap;
                    break;

                case ImageStatus.Cap:
                    //if (paragraph.Range.Tables.Count != 0)
                    //{
                    //    if (newPage)
                    //        comm = "Неверно оформлен переход на новую страницу";
                    //    break;
                    //}

                    if (Regularochki.IsImageCaption(paragraph.Range.Text, 0))
                    {
                        if (ImageCaptionCheck(paragraph.get_Style()))
                            comm = "Неверно оформлено наименование рисунка";

                        _continue = true;
                    }
                    else
                        comm = "Не найдено наименование рисунка";
                    imCheck = ImageStatus.None;
                    break;
            }

            return comm;
        }

        string TableCheck(Paragraph paragraph)
        {
            string comm = "";

            switch (tabCheck)
            {
                case TableStatus.None:
                    if (Regularochki.FindTableReference(paragraph.Range.Text) && !Regularochki.IsTableCaption(paragraph.Range.Text, 0))
                        tabCheck = TableStatus.Cap;

                    //if (paragraph.Range.Tables.Count != 0)
                    //{
                    //    comm = "Не найдена ссылка на таблицу";
                    //    tabCheck = TableStatus.Tab;
                    //}
                    break;

                case TableStatus.Cap:
                    if (Regularochki.IsTableCaption(paragraph.Range.Text, 0))
                    {
                        if (TableCaptionCheck(paragraph.get_Style()))
                            comm = "Неверно оформлено наименование таблицы";
                        tabCheck = TableStatus.Tab1Row;
                        _continue = true;
                    }
                    else
                    {
                        if (paragraph.Range.Tables.Count != 0)
                        {
                            comm = "Не найдено наименование таблицы";
                            tabCheck = TableStatus.Tab;
                        }
                        else
                        {
                            comm = "Не найдено наименование таблицы и сама таблица";
                            tabCheck = TableStatus.None;
                        }
                    }
                    break;

                case TableStatus.Tab1Row:
                    if (paragraph.Range.Tables.Count == 0)
                    {
                        comm = "Не найдена таблица";
                        tabCheck = TableStatus.None;
                    }
                    else
                        tabCheck = TableStatus.Tab;
                    break;

                case TableStatus.Tab:
                    if (paragraph.Range.Tables.Count == 0)
                        tabCheck = TableStatus.None;                   
                    break;
            }

            return comm;
        }

        //string AttachmentCheck(Paragraph paragraph)
        //{
        //    if (atCheck == AttachCheck.Name)
        //    {
        //        atCheck = AttachCheck.None;
        //        att = "";

        //        if (paragraph.Range.Text.ToLower().Contains(Attachments[att]))
        //        //if (paragraph.Range.Text.ToLower() == Attachments[att])
        //        {
        //            if (AttachmentCheck(paragraph.get_Style()))
        //                return "Неверно оформлено название приложения";
        //            _continue = true;
        //        }
        //        else
        //            return "Не найдено название приложения";
        //    }

        //    return "";
        //}

        string NewPageCheck(Paragraph paragraph, Microsoft.Office.Interop.Word.Application app)
        {
            if (PageNumber(paragraph.Range) == curPage + 1)
            {
                curPage++;
                newPage = true;

                if (FieldsCheck(paragraph.Range.PageSetup, app))
                    return "Неверные размеры полей страницы";
            }
            else
                newPage = false;

            return "";
        }

        bool CheckDeep()
        {
            int a = 0;

            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Document doc;

            try
            {
                doc = app.Documents.Open(path);
            }
            catch (Exception e)
            {
                Close(app);
                MessageBox.Show($"Проверка файла \"{fileName}\" была отменена, т.к. файл не был закрыт", "Отмена");
                return false;
            }
                        
            string fio = Regularochki.FIO(fileName);

            string comment = "";

            foreach (Paragraph paragraph in doc.Paragraphs)
            {
                _continue = false;

                comment = NewPageCheck(paragraph, app);

                if (comment != "")
                    paragraph.Range.Comments.Add(paragraph.Range, comment);

                if (Regularochki.PageBreak(paragraph.Range.Text))
                    continue;
               
                if (PageNumber(paragraph.Range) < 2)
                    if (paragraph.Range.Text.Contains(fio))
                        fioFound = true;

                comment = ImageCheck(paragraph);

                if (comment != "")
                    paragraph.Range.Comments.Add(paragraph.Range, comment);

                comment = TableCheck(paragraph);

                if (comment != "")
                    paragraph.Range.Comments.Add(paragraph.Range, comment);

                if (_continue)
                    continue;

                if (paragraph.Range.Tables.Count == 0)
                {
                    if (paragraph.Range.Text.ToLower().Contains("приложение"))
                        ;

                    if (Regularochki.EmptyString(paragraph.Range.Text))
                    {
                        paragraph.Range.Comments.Add(paragraph.Range, "Пустая строка");
                        continue;
                    }

                    if (!Regularochki.GetSourceRefNums(paragraph.Range.Text, ref refs))
                        paragraph.Range.Comments.Add(paragraph.Range, "Неверная последовательность ссылок на использованные источники");

                    Style style = paragraph.get_Style();
                    
                    string listMarker = paragraph.Range.ListFormat.ListString;

                    //comment = AttachmentCheck(paragraph);

                    //if (comment != "")
                    //    paragraph.Range.Comments.Add(paragraph.Range, comment);

                                        
                    string header = Regularochki.DeleteExtraSpaces(paragraph.Range.Text.ToLower());

                    if (Structure.ContainsKey(header))
                    {
                        if (Structure[header] - CurRazdel != 1)
                            paragraph.Range.Comments.Add(paragraph.Range, "Неверный порядок разделов");
                        CurRazdel = Structure[header];

                        if (RequiredParts.Contains(header))
                        {
                            if (RequiredPartHeaderCheck(style))
                                paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен заголовок обязательного структурного элемента");

                            if (Attachments.ContainsKey(header))
                            {
                                att = header;
                                atCheck = AttachCheck.Name;
                            }
                        }
                        else
                        if (MainParts.Contains(header))
                        {
                            if (MainPartHeaderCheck(style, listMarker))
                                paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен заголовок раздела");
                        }
                        else
                        if (SubParts.Contains(header))
                        {
                            if (MainSubPartHeaderCheck(style, listMarker))
                                paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен заголовок подраздела");
                        }
                    }
                    else
                    if (Regularochki.IsImageCaption(paragraph.Range.Text, 0))
                    {
                        if (ImageCaptionCheck(style))
                            paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлена подпись рисунка");
                    }
                    else
                    if (Regularochki.IsTableCaption(paragraph.Range.Text, 0))
                    {
                        if (TableCaptionCheck(style))
                            paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлена подпись таблицы");
                    }
                    else
                    if (CurRazdel == Structure["список использованных источников"])
                    {
                        if (listMarker != "")
                        {
                            usedSourcesCount++;
                            if (UsedSourceCheck(style, listMarker))
                            {
                                paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен использованный источник");
                            }
                        }
                    }
                    else
                    if (Regularochki.IsImageCaption(paragraph.Range.Text, 0))
                    {
                        if (TableCaptionCheck(style))
                            paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлено наименование рисунка");
                    }
                    else
                    if (Regularochki.IsTableCaption(paragraph.Range.Text, 0))
                    {
                        if (TableCaptionCheck(style))
                            paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлено наименование таблицы");
                    }
                    else
                    if (CurRazdel == Structure["содержание"])
                    {
                        if (CommonCheck(style))
                        {
                            paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен текст");
                        }
                    }
                    else
                    {
                        //if (ColorCheck(style))
                        //{
                        //    paragraph.Range.Comments.Add(paragraph.Range, "Возможно, неверно выбран цвет текста");
                        //}

                        if (TextCheck(style))
                        {
                            paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен текст");
                        }
                    }
                }
            }

            if (!fioFound)
                doc.Paragraphs[1].Range.Comments.Add(doc.Paragraphs[1].Range, "Не найдено имя автора отчета на титульном листе");

            if (CurRazdel == 0)
                doc.Paragraphs[1].Range.Comments.Add(doc.Paragraphs[1].Range, "Не найден ни один из обязательных разделов");
            
            if (usedSourcesCount > refs.Count)
                doc.Paragraphs[1].Range.Comments.Add(doc.Paragraphs[1].Range, "Отсутствуют все или некоторые ссылки на использованные источники");

            if (usedSourcesCount < refs.Count)
                doc.Paragraphs[1].Range.Comments.Add(doc.Paragraphs[1].Range, "Найдены ссылки на источники, не указанные в списке использованных источников");


            bool vibeCheckPassed = doc.Comments.Count == 0;

            //if (!vibeCheckPassed)
                Save(doc);

            Close(doc, app);

            return true;
        }

        bool CheckSurface()
        {
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Document doc;

            try
            {
                doc = app.Documents.Open(path);
            }
            catch (Exception e)
            {
                Close(app);
                MessageBox.Show($"Проверка файла \"{fileName}\" была отменена, т.к. файл не был закрыт", "Отмена");
                return false;
            }

            string fio = Regularochki.FIO(fileName);

            string comment = "";

            int c = doc.Paragraphs.Count;

            foreach (Paragraph paragraph in doc.Paragraphs)
            {
                comment = NewPageCheck(paragraph, app);

                //InlineShapes s = doc.InlineShapes;
                //InlineShapes a = paragraph.Range.InlineShapes;
                //int d = s.Count;
                //int f = a.Count;

                if (comment != "")
                    paragraph.Range.Comments.Add(paragraph.Range, comment);

                if (Regularochki.PageBreak(paragraph.Range.Text))
                    continue;

                if (PageNumber(paragraph.Range) < 2)
                    if (paragraph.Range.Text.Contains(fio))
                        fioFound = true;

                if (paragraph.Range.Tables.Count == 0)
                {
                    if (Regularochki.EmptyString(paragraph.Range.Text))
                    {
                        paragraph.Range.Comments.Add(paragraph.Range, "Пустая строка");
                        continue;
                    }

                    Style style = paragraph.get_Style();
                                   
                    //if (ColorCheck(style))
                    //{
                    //    paragraph.Range.Comments.Add(paragraph.Range, "Возможно, неверно выбран цвет текста");
                    //}

                    if (CommonCheck(style))
                    {
                        paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен текст");
                    }
                }
            }

            if (!fioFound)
                doc.Paragraphs[1].Range.Comments.Add(doc.Paragraphs[1].Range, "Не найдено имя автора отчета на титульном листе");

            bool vibeCheckPassed = doc.Comments.Count == 0;

            //if (!vibeCheckPassed)
            Save(doc);

            Close(doc, app);

            return true;
        }

        bool CheckStructure()
        {
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Document doc;

            try
            {
                doc = app.Documents.Open(path);
            }
            catch (Exception e)
            {
                Close(app);
                MessageBox.Show($"Проверка файла \"{fileName}\" была отменена, т.к. файл не был закрыт", "Отмена");
                return false;
            }

            string fio = Regularochki.FIO(fileName);

            string comment = "";

            foreach (Paragraph paragraph in doc.Paragraphs)
            {
                comment = NewPageCheck(paragraph, app);

                if (comment != "")
                    paragraph.Range.Comments.Add(paragraph.Range, comment);

                if (Regularochki.PageBreak(paragraph.Range.Text))
                    continue;

                if (PageNumber(paragraph.Range) < 2)
                    if (paragraph.Range.Text.Contains(fio))
                        fioFound = true;

                if (paragraph.Range.Tables.Count == 0)
                {
                    if (Regularochki.EmptyString(paragraph.Range.Text))
                    {
                        paragraph.Range.Comments.Add(paragraph.Range, "Пустая строка");
                        continue;
                    }

                    Style style = paragraph.get_Style();
                    string listMarker = paragraph.Range.ListFormat.ListString;

                    //comment = AttachmentCheck(paragraph);

                    //if (comment != "")
                    //    paragraph.Range.Comments.Add(paragraph.Range, comment);

                    string header = Regularochki.DeleteExtraSpaces(paragraph.Range.Text.ToLower());

                    if (Structure.ContainsKey(header))
                    {
                        if (Structure[header] - CurRazdel != 1)
                            paragraph.Range.Comments.Add(paragraph.Range, "Неверный порядок разделов");
                        CurRazdel = Structure[header];

                        if (RequiredParts.Contains(header))
                        {
                            if (RequiredPartHeaderCheck(style))
                                paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен заголовок обязательного структурного элемента");

                            if (Attachments.ContainsKey(header))
                            {
                                att = header;
                                atCheck = AttachCheck.Name;
                            }
                        }
                        else
                        if (MainParts.Contains(header))
                            if (MainPartHeaderCheck(style, listMarker))
                                paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен заголовок раздела");
                    }
                    else
                    {
                        //if (ColorCheck(style))
                        //{
                        //    paragraph.Range.Comments.Add(paragraph.Range, "Возможно, неверно выбран цвет текста");
                        //}

                        if (CommonCheck(style))
                        {
                            paragraph.Range.Comments.Add(paragraph.Range, "Неверно оформлен текст");
                        }
                    }
                }
            }

            if (!fioFound)
                doc.Paragraphs[1].Range.Comments.Add(doc.Paragraphs[1].Range, "Не найдено имя автора отчета на титульном листе");

            if (CurRazdel == 0)
                doc.Paragraphs[1].Range.Comments.Add(doc.Paragraphs[1].Range, "Не найден ни один из обязательных разделов");

            bool vibeCheckPassed = doc.Comments.Count == 0;

            //if (!vibeCheckPassed)
            Save(doc);

            Close(doc, app);

            return true;
        }

        public bool Check()
        {
            switch (checkMode)
            {
                case CheckMode.Deep:
                    return CheckDeep();

                case CheckMode.Structure:
                    return CheckStructure();

                default:
                    return CheckSurface();
            }
        }
    }
}
