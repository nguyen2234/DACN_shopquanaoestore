using estore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        private readonly DataContex _context;

        public ThongKeController(DataContex context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime? tuNgay, DateTime? denNgay)
        {
            var query = _context.orderDetail
                .Include(x => x.Order)
                .AsQueryable();

            if (tuNgay.HasValue)
                query = query.Where(x => x.Order.CreatedAt >= tuNgay.Value);

            if (denNgay.HasValue)
                query = query.Where(x => x.Order.CreatedAt <= denNgay.Value);

            var data = query
                .GroupBy(x => x.Order.CreatedAt.Month)
                .Select(g => new ThongkeDoanhThuView
                {
                    Thang = g.Key,
                    DoanhThu = g.Sum(x => x.Quantity * x.Price),
                    SoLuongSanPham = g.Sum(x => x.Quantity)
                })
                .OrderBy(x => x.Thang)
                .ToList();
            var topSanPham = query
                    .GroupBy(x => x.Product.Name)
                    .Select(g => new ThongkeSanPham
                    {
                        TenSanPham = g.Key,
                        TongSoLuong = g.Sum(x => x.Quantity),
                        TongTien = g.Sum(x => x.Quantity * x.Price)
                    })
                    .OrderByDescending(x => x.TongSoLuong)
                    .Take(10)
                    .ToList();
            ViewBag.TopSanPham = topSanPham;
            ViewBag.TuNgay = tuNgay;
            ViewBag.DenNgay = denNgay;

            return View(data);
        }
        public IActionResult ExportExcel(DateTime? tuNgay, DateTime? denNgay)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            var query = _context.orderDetail
                .Include(x => x.Order)
                .Include(x => x.Product)
                .AsQueryable();

            if (tuNgay.HasValue)
                query = query.Where(x => x.Order.CreatedAt >= tuNgay.Value);
            if (denNgay.HasValue)
                query = query.Where(x => x.Order.CreatedAt <= denNgay.Value);

            var data = query
                .GroupBy(x => x.Product.Name)
                .Select(g => new
                {
                    TenSanPham = g.Key,
                    TongSoLuong = g.Sum(x => x.Quantity),
                    TongTien = g.Sum(x => x.Quantity * x.Price)
                })
                .OrderByDescending(x => x.TongSoLuong)
                .Take(10)
                .ToList();

            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("TopSanPham");

            // Header
            sheet.Cells[1, 1].Value = "Tên sản phẩm";
            sheet.Cells[1, 2].Value = "Số lượng bán";
            sheet.Cells[1, 3].Value = "Doanh thu";

            // Data
            for (int i = 0; i < data.Count; i++)
            {
                sheet.Cells[i + 2, 1].Value = data[i].TenSanPham;
                sheet.Cells[i + 2, 2].Value = data[i].TongSoLuong;
                sheet.Cells[i + 2, 3].Value = data[i].TongTien;
            }

            var stream = new MemoryStream(package.GetAsByteArray());
            var fileName = $"TopSanPham_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public IActionResult ExportDoanhThuThang(DateTime? tuNgay, DateTime? denNgay)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var query = _context.orderDetail
                .Include(x => x.Order)
                .AsQueryable();

            if (tuNgay.HasValue)
                query = query.Where(x => x.Order.CreatedAt >= tuNgay.Value);
            if (denNgay.HasValue)
                query = query.Where(x => x.Order.CreatedAt <= denNgay.Value);

            var data = query
                .GroupBy(x => x.Order.CreatedAt.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    TongSoLuong = g.Sum(x => x.Quantity),
                    TongTien = g.Sum(x => x.Quantity * x.Price)
                })
                .OrderBy(x => x.Thang)
                .ToList();

            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("DoanhThuTheoThang");

            // Header
            sheet.Cells[1, 1].Value = "Tháng";
            sheet.Cells[1, 2].Value = "Số lượng sản phẩm";
            sheet.Cells[1, 3].Value = "Doanh thu";

            // Dữ liệu
            for (int i = 0; i < data.Count; i++)
            {
                sheet.Cells[i + 2, 1].Value = $"Tháng {data[i].Thang}";
                sheet.Cells[i + 2, 2].Value = data[i].TongSoLuong;
                sheet.Cells[i + 2, 3].Value = data[i].TongTien;
            }

            var stream = new MemoryStream(package.GetAsByteArray());
            var fileName = $"DoanhThuThang_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


    }

}
