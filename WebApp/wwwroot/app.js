const $ = (sel) => document.querySelector(sel);

const statusEl = $("#status");
const tbody = $("#tbody");
const keywordInput = $("#keyword");
const btnSearch = $("#btnSearch");
const btnClear = $("#btnClear");

function escapeHtml(s) {
  return String(s ?? "")
    .replaceAll("&", "&amp;")
    .replaceAll("<", "&lt;")
    .replaceAll(">", "&gt;")
    .replaceAll('"', "&quot;")
    .replaceAll("'", "&#039;");
}

function setStatus(msg) {
  statusEl.textContent = msg;
}

function renderRows(results) {
  tbody.innerHTML = "";

  for (const s of results) {
    const tr = document.createElement("tr");
    tr.innerHTML = `
      <td>${escapeHtml(s.observatoryname)}</td>
      <td>${escapeHtml(s.observatoryidentifier)}</td>
      <td>${escapeHtml(s.rivername)}</td>
      <td>${escapeHtml(s.locationaddress)}</td>
      <td>${escapeHtml(s.observationstatus)}</td>
    `;
    tbody.appendChild(tr);
  }
}

async function search() {
  const q = keywordInput.value.trim();
  setStatus("查詢中…");

  try {
    const res = await fetch(`/api/stations?q=${encodeURIComponent(q)}`);
    if (!res.ok) throw new Error(`API 回應錯誤：${res.status}`);

    const data = await res.json();
    renderRows(data.results);

    setStatus(`總資料：${data.total} 筆｜符合「${data.keyword}」：${data.count} 筆`);
  } catch (err) {
    console.error(err);
    setStatus(`發生錯誤：${err.message}`);
  }
}

// 事件
btnSearch.addEventListener("click", search);
btnClear.addEventListener("click", () => {
  keywordInput.value = "";
  tbody.innerHTML = "";
  setStatus("已清除。");
});

// Enter 直接搜尋
keywordInput.addEventListener("keydown", (e) => {
  if (e.key === "Enter") search();
});
