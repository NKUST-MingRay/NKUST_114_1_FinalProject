# NKUST_114_1

## 專案說明

本專案為使用 **.NET 9.0** 開發之資料查詢系統，示範如何：

1. 讀取政府開放資料 JSON
2. 進行資料解析與關鍵字搜尋
3. 建立 **主控台應用程式** 與 **Web 網頁系統**
4. 實作 **前後端分離架構（API + Web UI）**

系統同時提供：
- **Console 版本**：以終端機操作資料搜尋  
- **Web 版本**：以瀏覽器進行圖形化查詢

---

## 資料來源

本專案使用課堂提供之「河川水位測站基本資料」JSON 檔案，  
原始資料來源為政府開放資料平台之水利相關資料集（如水利署水文開放資料）。

> 實際使用檔案：`affdata.json`  
> 檔案內容為多筆測站屬性資料之 JSON 陣列

---

## 開發環境需求

- **.NET 9.0 SDK**
- **Visual Studio 2022 / VS Code**

---

## 專案功能

### 🔹 主控台版本（ConsoleApp）
- 讀取 `App_Data/affdata.json`
- 反序列化 JSON 成 C# 物件
- 使用者輸入關鍵字搜尋測站名稱
- 顯示：
  - 測站名稱
  - 測站代碼
  - 河川名稱
  - 位置地址
  - 測站狀態

### 🔹 Web 版本（WebApp）【新增功能】
- ASP.NET Core Minimal API 建立後端服務
- API 端點：GET /api/stations?q=關鍵字
- 前端使用 HTML + JavaScript + Fetch API 呼叫後端
- 搜尋結果以表格顯示於網頁介面

---

## 專案結構
```
NKUST_114_1/
├── ConsoleApp/
│ ├── Program.cs
│ ├── AffStation.cs
│ └── App_Data/
│ └── affdata.json
│
├── WebApp/
│ ├── Program.cs
│ ├── AffStation.cs
│ ├── Services/
│ │ └── StationRepository.cs
│ ├── wwwroot/
│ │ ├── index.html
│ │ ├── app.js
│ │ └── styles.css
│ └── App_Data/
│ └── affdata.json
│
└── README.md
```

---

## 資料準備

### ConsoleApp

1. 建立資料夾：ConsoleApp/App_Data/
2. 放入 `affdata.json`
3. 設定檔案屬性：
- Build Action：None 或 Content
- Copy to Output Directory：Copy if newer

### WebApp

1. 建立資料夾：WebApp/App_Data/
2. 放入 `affdata.json`
3. 確認已設定為輸出時複製

---

## 執行方式

### 🔹 執行主控台版本

```bash
cd NKUST_114_1
dotnet run --project ConsoleApp
```
### 🔹 執行 Web 版本
```bash
cd NKUST_114_1/WebApp
dotnet run
```
啟動後使用瀏覽器開啟顯示的網址即可操作系統。

## 技術重點
    - JSON 反序列化
    - LINQ 資料搜尋
    - ASP.NET Core Minimal API
    - JavaScript Fetch API
    - 前後端分離架構

## 備註
    - App_Data 資料夾需自行建立並放入 JSON 檔案
    - 若 JSON 欄位變動，需同步更新 AffStation 類別

---
