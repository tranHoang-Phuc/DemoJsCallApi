# DemoJsCallApi

## Clone project
`git clone https://github.com/tranHoang-Phuc/DemoJsCallApi.git`

## Run
* Mở project bằng Visual Studio
* Sửa file appsettings.json
  * Thay thông tin connection string của bạn
* Chạy migration (nếu chưa có DB MySaleDb của thầy đã gửi)
  `dotnet ef migrations add 'Init'`
  `dotnet ef database update`
* Chạy project 
