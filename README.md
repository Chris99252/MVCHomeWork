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

### [Second Week - MVCHomeWork02](https://docs.google.com/a/cashwu.com/forms/d/1i8vBbULA6V8wmImUAtuTfDSFvGe_tQUS63-ft3oBvj8/viewform)

1. 對於從網址列上出現的 id 值，撰寫一個自訂的 Action Filter (動作過濾器) 檢查傳入的 id 格式是否符合要求，格式不對就導向回首頁。
2. 寫一個 BaseController 並覆寫 HandleUnknownAction 方法，找不到 Action 就顯示一頁自訂的 404 錯誤頁面。
3. 所有看到 db.Entry(client).State = EntityState.Modified; 的寫法，都要改成資料繫結延遲驗證的方式做檢查 ( TryUpdateModel )，然後搭配自訂的 Interface 去針對特定表單欄位做 Model Binding，避免 over-posting 的問題發生。
4. 實作匯出資料功能，可將「客戶資料」匯出，用 FileResult 輸出檔案，輸出格式不拘 (XLS, CSV, ...)，下載檔名規則："YYYYMMDD_客戶資料匯出.xlsx"
5. 實作 Master/Details 頁面，修改「客戶資料」的 Details 與 Edit 頁面，讓該頁面同時顯示「客戶銀行資訊」與「客戶聯絡人」的清單資料。
6. 「客戶資料」的 Details 頁面，請列出客戶銀行資訊與客戶連絡人的清單 (List)，並且可以增加「刪除」功能，可在這頁刪除客戶銀行資訊與客戶連絡人的資料，刪除成功後會繼續顯示客戶資料的 Details 頁面 (提示: 透過 ViewBag 或 ViewData 傳遞多個 Model 到 View 裡顯示 )
7. 「客戶資料」的 Edit 頁面，請實作客戶銀行資訊與客戶連絡人的批次編輯功能，按下儲存按鈕，可以一次將所有資料儲存
8. 無論做哪個表單，都要做欄位輸入驗證。
9. 實作客戶聯絡人的「模型驗證」，同一個客戶下的聯絡人，其 Email 不能重複。
