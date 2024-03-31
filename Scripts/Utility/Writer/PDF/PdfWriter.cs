using System.IO;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine.WSA;
using Application = UnityEngine.Application;

public class PdfWriter
{
    private string pdfFilePath = $"{Application.persistentDataPath}/test.pdf"; // $"{Application.persistentDataPath}/test.pdf";
    
    [Button]
    public void Generate()
    {
        using (FileStream fs = new FileStream(pdfFilePath, FileMode.Create, FileAccess.Write))
        {
            StreamWriter sw = new StreamWriter(fs, Encoding.ASCII);

            // PDF Header
            sw.WriteLine("%PDF-1.7");

            // 1. Catalog and Pages (required for a minimal PDF)
            sw.WriteLine("1 0 obj");
            sw.WriteLine("<< /Type /Catalog /Pages 2 0 R >>");
            sw.WriteLine("endobj");

            // 2. Page Tree (required for one or more pages)
            sw.WriteLine("2 0 obj");
            sw.WriteLine("<< /Type /Pages /Kids [3 0 R] /Count 1 >>");
            sw.WriteLine("endobj");

            // 3. Page (you can add more pages)
            sw.WriteLine("3 0 obj");
            sw.WriteLine("<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] >>");
            sw.WriteLine("endobj");

            // xref table (required - it's like an index of objects in the file)
            sw.WriteLine("xref");
            sw.WriteLine("0 4");
            sw.WriteLine("0000000000 65535 f ");
            sw.WriteLine("0000000010 00000 n ");
            sw.WriteLine("0000000058 00000 n ");
            sw.WriteLine("0000000113 00000 n ");

            // trailer (required, points to the root object - Catalog)
            sw.WriteLine("trailer");
            sw.WriteLine("<< /Size 4 /Root 1 0 R >>");

            // End of File
            sw.WriteLine("startxref");
            sw.WriteLine("184");
            sw.WriteLine("%%EOF");

            sw.Flush();
        }
    }
}
