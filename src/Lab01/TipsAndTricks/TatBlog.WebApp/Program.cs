﻿using Microsoft.EntityFrameworkCore;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WebApp.Extensions;
using TatBlog.WebApp.Mapsters;
using TatBlog.WebApp.Validations;

var builder = WebApplication.CreateBuilder(args);
{
    builder
        .ConfigureMvc()
        .ConfigureNLog()
        .ConfigureServices()
        .ConfigureMapster()
        .ConfigureFluentValidation();
}



var app = builder.Build();
{
    app.UseRequestPipeline();
    app.UseBlogRoutes();
    app.UseDataSeeder();
}

app.Run();

//var builder = WebApplication.CreateBuilder(args);
//{
//    //Thêm các dịch vụ được yêu cầu bởi MVC Framework
//    builder.Services.AddControllersWithViews();

//    // Đăng ký các dịch vụ với DI Container
//    builder.Services.AddDbContext<BlogDBContext>(options =>
//        options.UseSqlServer(
//            builder.Configuration.GetConnectionString("DefaultConnection")));

//    builder.Services.AddScoped<IBlogRepository, BlogRepository>();
//    builder.Services.AddScoped<IDataSeeder, DataSeeder>();
//}

//var app = builder.Build();
//{
//    // Cấu hình HTTP Request pipeline
//    // Thêm middleware để hiển thị thông báo lỗi
//    if (app.Environment.IsDevelopment())
//    {
//        app.UseDeveloperExceptionPage();
//    }
//    else
//    {
//        app.UseExceptionHandler("/Blog/Error");

//        //Thêm middleware cho việc áp dụng HSTS (thêm header
//        // Strict-Transport-Security vào HTTP Response).
//        app.UseHsts();
//    }

//    // Thêm middleware để chuyển hướng HTTP sang HTTPS
//    app.UseHttpsRedirection();

//    // Thêm middleware phục vụ các yêu cầu liên quan
//    // tới các tập tin nội dung tĩnh như hình ảnh, css,...
//    app.UseStaticFiles();

//    // Thêm middleware lựa chọn endpoint phù hợp nhất
//    // để xử lý một HTTP request
//    app.UseRouting();

//    // Định nghĩa route templates, route constraint cho các
//    // endpoints kết hợp với các action trong các controller
//    app.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Blog}/{action=Index}/{id?}");
//}

//// Thêm dữ liệu mẫu vào CSDL
//using (var scope = app.Services.CreateScope())
//{
//    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
//    seeder.Initialize();
//}

//app.Run();

//app.MapGet("/", () => "Hello World!");

//app.Run();
