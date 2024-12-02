document.addEventListener('DOMContentLoaded', function () {
    // Add search and sort controls to the page
    const controlsHtml = `
        <div class="controls-section">
            <div class="search-filters">
                <div class="search-box">
                    <input type="text" 
                           class="search-input" 
                           placeholder="Tìm kiếm theo tên người mượn..."
                           id="searchInput">
                </div>
                <div class="sort-controls">
                    <select class="sort-select" id="sortField">
                        <option value="">Sắp xếp theo...</option>
                        <option value="id-asc">ID ↑</option>
                        <option value="id-desc">ID ↓</option>
                        <option value="name-asc">Tên người mượn ↑</option>
                        <option value="name-desc">Tên người mượn ↓</option>
                        <option value="date-asc">Ngày mượn ↑</option>
                        <option value="date-desc">Ngày mượn ↓</option>
                    </select>
                </div>
            </div>
        </div>
    `;

    // Insert controls before the table
    const table = document.querySelector('.table');
    table.insertAdjacentHTML('beforebegin', controlsHtml);

    // Get all table rows (excluding header)
    const tbody = table.querySelector('tbody');
    const rows = Array.from(tbody.querySelectorAll('tr'));

    // Search functionality
    const searchInput = document.getElementById('searchInput');
    searchInput.addEventListener('input', function () {
        const searchTerm = this.value.toLowerCase();

        rows.forEach(row => {
            const borrowerName = row.children[1].textContent.toLowerCase();
            row.style.display = borrowerName.includes(searchTerm) ? '' : 'none';
        });
    });

    // Sorting functionality
    const sortSelect = document.getElementById('sortField');
    sortSelect.addEventListener('change', function () {
        const sortValue = this.value;

        const sortedRows = rows.slice().sort((a, b) => {
            let compareValueA, compareValueB;

            switch (sortValue) {
                case 'id-asc':
                case 'id-desc':
                    compareValueA = parseInt(a.children[0].textContent);
                    compareValueB = parseInt(b.children[0].textContent);
                    break;

                case 'name-asc':
                case 'name-desc':
                    compareValueA = a.children[1].textContent;
                    compareValueB = b.children[1].textContent;
                    break;

                case 'date-asc':
                case 'date-desc':
                    compareValueA = new Date(a.children[2].textContent.split('/').reverse().join('-'));
                    compareValueB = new Date(b.children[2].textContent.split('/').reverse().join('-'));
                    break;

                default:
                    return 0;
            }

            if (sortValue.endsWith('-desc')) {
                [compareValueA, compareValueB] = [compareValueB, compareValueA];
            }

            return compareValueA > compareValueB ? 1 : -1;
        });

        // Clear and re-append sorted rows
        tbody.innerHTML = '';
        sortedRows.forEach(row => tbody.appendChild(row));
    });

    // Add smooth animations for row updates
    rows.forEach(row => {
        row.style.transition = 'background-color 0.3s';
    });

    // Add loading indicator for form submission
    const returnForms = document.querySelectorAll('form');
    returnForms.forEach(form => {
        form.addEventListener('submit', function (e) {
            const button = this.querySelector('button');
            button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> Đang xử lý...';
            button.disabled = true;
        });
    });

    // Add tooltips for better UX
    const returnButtons = document.querySelectorAll('.btn-success');
    returnButtons.forEach(button => {
        button.title = 'Nhấn để xác nhận trả item';
    });
});