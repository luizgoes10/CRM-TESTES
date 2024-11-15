window.toggleSidebar = {
    show: function () {
        document.querySelector('.sidebar').classList.add('expanded');
    },
    hide: function () {
        document.querySelector('.sidebar').classList.remove('expanded');
    }
};