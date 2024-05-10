

using Microsoft.AspNetCore.Mvc;
using StoreAPI.Data;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ApplicationDbContext context) : ControllerBase
{
    // สร้าง Object ของ ApplicationDbContext
    private readonly ApplicationDbContext _context = context;
    
     // ฟังก์ชันสำหรับการดึงข้อมูลสินค้าทั้งหมด
    // GET: /api/Product
    [HttpGet]
    public ActionResult<category> GetCategories()
    {
        // LINQ สำหรับการดึงข้อมูลจากตาราง Products ทั้งหมด
        var category = _context.categories.ToList();

        // แบบอ่านที่มีเงื่อนไข
        // var products = _context.products.Where(p => p.unit_price > 45000).ToList();


         // แบบเชื่อมกับตารางอื่น products เชื่อมกับ categories
        // var categories = _context.categories
        //     .Join(
        //         _context.categories,
        //         c => c.category_id,
        //         p => p.category_id,
        //         (c,p) => new
        //         {
        //             c.category_id,
        //             c.category_name,
        //             c.category_status,               
        //             p.product_name
        //         }
        //     ).ToList();

        
        // ส่งข้อมูลกลับไปให้ผู้ใช้งาน
        return Ok(category);
    }

    // ฟังก์ชันสำหรับการดึงข้อมูลสินค้าตาม id
    // GET: /api/Product/{id}
    [HttpGet("{id}")]
    public ActionResult<category> GetCategories(int id)
    {
        // LINQ สำหรับการดึงข้อมูลจากตาราง Products ตาม id
        var category = _context.categories.FirstOrDefault(c => c.category_id == id);

        // ถ้าไม่พบข้อมูลจะแสดงข้อความ Not Found
        if (category == null)
        {
            return NotFound();
        }

        // ส่งข้อมูลกลับไปให้ผู้ใช้งาน
        return Ok(category);
    }

    // ฟังก์ชันสำหรับการเพิ่ม categories
    // POST: /api/Category
    [HttpPost]
    public ActionResult<category> CreateCategory(category category)
    {
        // เพิ่มข้อมูลลงในตาราง categories
        _context.categories.Add(category);

        _context.SaveChanges();


        // ส่งข้อมูลกลับไปให้ผู้ใช้
        return Ok(category);
    }

    


    // ฟังก์ชันสำหรับการแก้ไขข้อมูลสินค้า
    // PUT: /api/Product/{id}
    [HttpPut("{id}")]
    public ActionResult<category> UpdateCategory(int id, category category)
    {
        // ดึงข้อมูลสินค้าตาม id
        var existingCategory = _context.categories.FirstOrDefault(c => c.category_id == id);

        // ถ้าไม่พบข้อมูลจะแสดงข้อความ Not Found
        if (existingCategory == null)
        {
            return NotFound();
        }

        // แก้ไขข้อมูลสินค้า
        existingCategory.category_name = category.category_name;
        existingCategory.category_status   = category.category_status;

        // บันทึกข้อมูล
        _context.SaveChanges();

        // ส่งข้อมูลกลับไปให้ผู้ใช้
        return Ok(existingCategory);
    }

    // ฟังก์ชันสำหรับการลบข้อมูลสินค้า
    // DELETE: /api/Product/{id}
    [HttpDelete("{id}")]
    public ActionResult<category> DeleteCategory(int id)
    {
        // ดึงข้อมูลสินค้าตาม id
        var category = _context.categories.FirstOrDefault(c => c.category_id == id);

        // ถ้าไม่พบข้อมูลจะแสดงข้อความ Not Found
        if (category == null)
        {
            return NotFound();
        }

        // ลบข้อมูล
        _context.categories.Remove(category);
        _context.SaveChanges();

        // ส่งข้อมูลกลับไปให้ผู้ใช้
        return Ok(category);
    }


    



}