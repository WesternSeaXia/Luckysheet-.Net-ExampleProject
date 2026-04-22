using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExcelDemo.Data;
using ExcelDemo.Models;

namespace ExcelDemo.Controllers
{
    public class ExcelController : Controller
    {
        private readonly AppDbContext _context;

        public ExcelController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var document = await _context.ExcelDocuments.FindAsync(id);
            if (document == null)
            {
                document = new ExcelDocument
                {
                    Id = Guid.NewGuid(),
                    FileName = "NewDocument.xlsx",
                    SheetData = "[]"
                };
                _context.ExcelDocuments.Add(document);
                await _context.SaveChangesAsync();
            }

            ViewBag.SheetData = document.SheetData;
            ViewBag.DocumentId = document.Id;
            ViewBag.FileName = document.FileName;

            return View();
        }

        // ================== 修改：接收 JSON 对象的后端接口 ==================
        [HttpPost]
        [DisableRequestSizeLimit] // 允许超大负载
        public async Task<IActionResult> SaveData([FromBody] SaveDataRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.SheetDataJSON))
            {
                return BadRequest(new { success = false, message = "数据为空或超长丢失" });
            }

            var document = await _context.ExcelDocuments.FindAsync(request.Id);
            if (document != null)
            {
                document.SheetData = request.SheetDataJSON;
                document.UpdateTime = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok(new { success = true });
            }

            return NotFound(new { success = false, message = "文档不存在" });
        }
    }

    // 新增：用于模型绑定的 DTO
    public class SaveDataRequest
    {
        public Guid Id { get; set; }
        public string SheetDataJSON { get; set; } = string.Empty;
    }
}