// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {

    const btn = document.getElementById("darkModeToggle");
    const icon = document.getElementById("themeIcon");
    const text = document.getElementById("themeText");

    if (!btn) return;

    function setTheme(theme) {
        document.body.setAttribute("data-bs-theme", theme);
        localStorage.setItem("theme", theme);

        if (theme === "dark") {
            icon.textContent = "☀️";
            text.textContent = "Light Mode";
            btn.classList.remove("btn-outline-dark");
            btn.classList.add("btn-outline-light");
        } else {
            icon.textContent = "🌙";
            text.textContent = "Dark Mode";
            btn.classList.remove("btn-outline-light");
            btn.classList.add("btn-outline-dark");
        }
    }

    const saved = localStorage.getItem("theme") || "light";
    setTheme(saved);

    btn.addEventListener("click", function () {
        const current = document.body.getAttribute("data-bs-theme");
        setTheme(current === "dark" ? "light" : "dark");
    });

});