MVCHomeWork
===========

### [First Week - MVCHomeWork01](https://docs.google.com/a/cashwu.com/forms/d/1cTuqVXjHUAoODaFeQvI72p9Ge6HkEJ3iYEk3TDCu_1o/viewform)

1. 請使用 ASP . NET MVC 5 + Entity Framework 6
2. 請使用我提供的資料庫進行開發 (如下附件)
3. 請實作出「客戶資料管理」、「客戶聯絡人管理」與「客戶銀行帳戶管理」等 CRUD 功能
4. 提供一份簡易報表，報表欄位有「客戶名稱、聯絡人數量、銀行帳戶數量」共三個欄位，用一個表格呈現報表即可。
5. 其他要求：
  - 必須使用 EFRepository 存取資料
  - 該專案要有一個自訂的 SQL Server 檢視表 (建議將報表輸出的資料寫成檢視表)，並且匯入 EDMX
  - 主選單要有連結可以直接連到主要的 CRUD 功能
  - 對於 Create 與 Edit 表單要套用欄位驗證 (必填或 Email 格式驗證)
  - 刪除資料功能不能真的刪除資料庫中的資料，必須修改資料庫，加上一個「是否已刪除」欄位，資料庫中的該欄位為 bit 類型，0 代表未刪除，1 代表已刪除，且列表頁不得顯示已刪除的資料。
