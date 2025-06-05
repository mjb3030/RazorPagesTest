// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Dark Mode Theme Management
(function() {
    // Get the current theme from localStorage or default to light
    const currentTheme = localStorage.getItem('theme') || 'light';
    
    // Apply the theme on page load
    document.documentElement.setAttribute('data-theme', currentTheme);
    
    // Function to toggle theme
    function toggleTheme() {
        const currentTheme = document.documentElement.getAttribute('data-theme');
        const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
        
        document.documentElement.setAttribute('data-theme', newTheme);
        localStorage.setItem('theme', newTheme);
        
        // Update toggle button text
        updateToggleButton(newTheme);
    }
    
    // Function to update toggle button text
    function updateToggleButton(theme) {
        const toggleButton = document.getElementById('theme-toggle');
        if (toggleButton) {
            const icon = toggleButton.querySelector('i');
            if (theme === 'dark') {
                icon.className = 'bi bi-sun-fill';
                toggleButton.setAttribute('title', 'Switch to light mode');
            } else {
                icon.className = 'bi bi-moon-fill';
                toggleButton.setAttribute('title', 'Switch to dark mode');
            }
        }
    }
    
    // Initialize when DOM is ready
    document.addEventListener('DOMContentLoaded', function() {
        const toggleButton = document.getElementById('theme-toggle');
        if (toggleButton) {
            toggleButton.addEventListener('click', toggleTheme);
            updateToggleButton(currentTheme);
        }
    });
})();
