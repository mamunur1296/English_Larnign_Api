using App.Domain.OthersDto;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;

public class ExcelProcessor
{
    public List<InputSentenceItem> ReadExcelData(IFormFile file)
    {
        var sentences = new List<InputSentenceItem>();

        // Open the Excel file
        using (var stream = file.OpenReadStream())
        using (SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false))
        {
            WorkbookPart workbookPart = document.WorkbookPart;
            Sheet sheet = workbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id.Value);
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            // Loop through the rows, skip the first header row
            foreach (Row row in sheetData.Elements<Row>().Skip(1)) // Assuming first row is header
            {
                // Get cell values, trim them, and skip rows with empty or null values
                var subCategory = GetCellValue(workbookPart, row.Elements<Cell>().ElementAtOrDefault(0))?.Trim();
                var banglaAffirmative = GetCellValue(workbookPart, row.Elements<Cell>().ElementAtOrDefault(1))?.Trim();
                var englishAffirmative = GetCellValue(workbookPart, row.Elements<Cell>().ElementAtOrDefault(2))?.Trim();
                var banglaNegative = GetCellValue(workbookPart, row.Elements<Cell>().ElementAtOrDefault(3))?.Trim();
                var englishNegative = GetCellValue(workbookPart, row.Elements<Cell>().ElementAtOrDefault(4))?.Trim();
                var banglaInterrogative = GetCellValue(workbookPart, row.Elements<Cell>().ElementAtOrDefault(5))?.Trim();
                var englishInterrogative = GetCellValue(workbookPart, row.Elements<Cell>().ElementAtOrDefault(6))?.Trim();

                // Skip rows where SubCategory or all sentence types are missing
                if (string.IsNullOrEmpty(subCategory) &&
                    string.IsNullOrEmpty(banglaAffirmative) &&
                    string.IsNullOrEmpty(englishAffirmative) &&
                    string.IsNullOrEmpty(banglaNegative) &&
                    string.IsNullOrEmpty(englishNegative) &&
                    string.IsNullOrEmpty(banglaInterrogative) &&
                    string.IsNullOrEmpty(englishInterrogative))
                {
                    continue;
                }

                // Add sentences (Affirmative, Negative, Interrogative)
                AddSentence(sentences, subCategory, banglaAffirmative, englishAffirmative, "Affirmative");
                AddSentence(sentences, subCategory, banglaNegative, englishNegative, "Negative");
                AddSentence(sentences, subCategory, banglaInterrogative, englishInterrogative, "Interrogative");
            }
        }

        return sentences;
    }

    // Helper method to add sentence only if both Bangla and English sentences are present
    private void AddSentence(List<InputSentenceItem> sentences, string subCategory, string banglaSentence, string englishSentence, string formName)
    {
        if (!string.IsNullOrEmpty(banglaSentence) || !string.IsNullOrEmpty(englishSentence))
        {
            sentences.Add(new InputSentenceItem
            {
                SubCatagoryName = subCategory,
                BanglaSentences = banglaSentence,
                EnglishSentences = englishSentence,
                FormName = formName
            });
        }
    }

    private string GetCellValue(WorkbookPart workbookPart, Cell cell)
    {
        if (cell == null || cell.CellValue == null)
        {
            return null;
        }

        string value = cell.CellValue.InnerText;
        if (cell.DataType?.Value == CellValues.SharedString)
        {
            return workbookPart.SharedStringTablePart.SharedStringTable
                .Elements<SharedStringItem>().ElementAt(int.Parse(value)).InnerText;
        }
        return value;
    }
}
