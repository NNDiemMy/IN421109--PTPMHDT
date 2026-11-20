# Thư mục chứa minh chứng chạy Swagger (Câu 5c)

Theo yêu cầu của đề thi (Câu 5c):
> "Chạy thử bằng Swagger, chụp màn hình một lần gọi thành công và một lần gọi lỗi, lưu vào thư mục MinhChung/ trong kho."

### Các hình ảnh cần lưu tại đây:
1. `swagger-thanh-cong.png` (hoặc `.jpg`): Ảnh chụp màn hình Swagger thực hiện một request thành công (ví dụ: POST `/api/clubs/{clubId}/events` trả về mã `201 Created` hoặc GET trả về `200 OK`).
2. `swagger-that-bai.png` (hoặc `.jpg`): Ảnh chụp màn hình Swagger thực hiện một request lỗi (ví dụ: POST `/api/clubs/{clubId}/events` với tiêu đề trống trả về mã `400 Bad Request` hoặc GET `/api/events/{id}` với Id không tồn tại trả về `404 Not Found`).

*(Lưu ý: Bạn có thể nhấn F5 hoặc chạy `dotnet run --project EduHub.API`, mở trình duyệt vào Swagger, test 2 trường hợp trên rồi chụp màn hình lưu vào thư mục này trước khi nộp bài).*
