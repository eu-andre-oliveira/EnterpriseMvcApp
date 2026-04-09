// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(() => {
  const body = document.body;
  const sidebarToggle = document.getElementById("sidebarToggle");
  const mobileMenuToggle = document.getElementById("mobileMenuToggle");

  if (sidebarToggle) {
    sidebarToggle.addEventListener("click", () => {
      body.classList.toggle("sidebar-collapsed");
    });
  }

  if (mobileMenuToggle) {
    mobileMenuToggle.addEventListener("click", () => {
      body.classList.toggle("sidebar-open");
    });
  }
})();
