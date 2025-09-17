/*const apiUrl = "http://localhost:5140/api/books/";*/
const apiUrl = "https://localhost:7260/api/books/"


async function loadBooks() {
    try {
        const response = await fetch(apiUrl +"GetBooks");
        const books = await response.json();

        const booksTable = document.getElementById("booksTable").getElementsByTagName('tbody')[0];
        booksTable.innerHTML = ""; // מנקה את התוכן הקיים בטבלה

        books.forEach(book => {
            const row = booksTable.insertRow();

            // יצירת תאים לכל עמודה
/*            row.insertCell(0).textContent = book.isbn;*/
            row.insertCell(0).textContent = book.title;
            row.insertCell(1).textContent = book.authors.join(", ");
            row.insertCell(2).textContent = book.year;
            row.insertCell(3).textContent = book.price;
            row.insertCell(4).textContent = book.category;
            row.insertCell(5).textContent = book.cover;

            // כפתור לעריכה
            const actionsCell = row.insertCell(6);
            const editIcon = document.createElement("i");
            editIcon.className = "fas fa-edit action-icon edit";
            editIcon.title = "Edit";
            editIcon.setAttribute("aria-label", "Edit Book");
            editIcon.onclick = () => editBook(book);
            actionsCell.appendChild(editIcon);

            // כפתור למחיקה
            const deleteIcon = document.createElement("i");
            deleteIcon.className = "fas fa-trash-alt action-icon delete";
            deleteIcon.title = "Delete";
            deleteIcon.onclick = () => {
                console.log("Deleting book with ISBN:", book.isbn); // בדוק אם זה מופיע
                deleteBook(book.isbn);
            };
            actionsCell.appendChild(deleteIcon);
        });
    } catch (error) {
        console.error("Error loading books:", error);
    }
}
window.onload = loadBooks;

// פונקציה למחיקת ספר
async function deleteBook(isbn) {
    const confirmDelete = confirm("Are you sure you want to delete this book?");
    if (confirmDelete) {
        try {
            await fetch(apiUrl + `DeleteBook/${isbn}`, {
                method: "DELETE",
            });
            loadBooks();
            resetForm();// טוען מחדש את הספרים לאחר מחיקה
        } catch (error) {
            console.error("Error deleting book:", error);
        }
    }
}

// פונקציה לעריכת ספר
function editBook(book) {
    document.getElementById("isbn").value = book.isbn;
    document.getElementById("title").value = book.title;
    document.getElementById("authors").value = book.authors.join(", ");
    document.getElementById("year").value = book.year;
    document.getElementById("price").value = book.price;
    document.getElementById("category").value = book.category,
        document.getElementById("cover").value = book.cover

    // שינוי כפתור ההוספה לכפתור עדכון
    const addButton = document.querySelector("#addBookForm button");
    addButton.textContent = "Update Book";
    addButton.onclick = () => updateBook(book.isbn);
}

// פונקציה לעדכון ספר
async function updateBook(isbn) {
    const updatedBook = {
        isbn: document.getElementById("isbn").value,
        title: document.getElementById("title").value,
        authors: document.getElementById("authors").value.split(","),
        year: parseInt(document.getElementById("year").value),
        price: parseFloat(document.getElementById("price").value),
        category: document.getElementById("category").value,
        cover: document.getElementById("cover").value
    };

    try {
        const response = await fetch(apiUrl + `UpdateBook/${isbn}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(updatedBook),
        });
        loadBooks();
        resetForm();
    } catch (error) {
        console.error("Error updating book:", error);
    }
}

// פונקציה להוספת ספר חדש
async function addBook() {
    const newBook = {
        isbn: document.getElementById("isbn").value,
        title: document.getElementById("title").value,
        authors: document.getElementById("authors").value.split(","),
        year: parseInt(document.getElementById("year").value),
        price: parseFloat(document.getElementById("price").value),
        category: document.getElementById("category").value,
        cover: document.getElementById("cover").value
    };

    try {
        await fetch(apiUrl +"AddBook", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(newBook),
        });
        loadBooks();
        resetForm();
    } catch (error) {
        console.error("Error adding book:", error);
    }
}


function resetForm() {
    document.getElementById("isbn").value = "";
    document.getElementById("title").value = "";
    document.getElementById("authors").value = "";
    document.getElementById("year").value = "";
    document.getElementById("price").value = "";
    document.getElementById("category").value = "";
    document.getElementById("cover").value = "";
}