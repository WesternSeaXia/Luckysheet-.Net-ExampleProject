namespace ExcelDemo.Models
{
    public class ExcelDocument
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;

        // 用于存储 Luckysheet 的全量 JSON 配置，包含 Base64 图片
        public string SheetData { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; }
    }
}