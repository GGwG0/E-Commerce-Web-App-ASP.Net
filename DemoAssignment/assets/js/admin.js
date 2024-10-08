const sideMenu = document.querySelector("aside");
const menuBtn = document.querySelector("#menu-btn");
const closeBtn = document.querySelector("#close-btn");
const themeToggler = document.querySelector(".theme-toggler");


function checkWindowSize() {
    if (window.innerWidth >= 768) {
        sideMenu.style.display = 'block';
        menuBtn.style.display = 'none';

    } else {
        if (sideMenu.style.display !== 'none') {
            sideMenu.style.display = 'none';
            menuBtn.style.display = 'block';
        }

        menuBtn.addEventListener('click', () => {
            sideMenu.style.display = 'block';
        })

        closeBtn.addEventListener('click', () => {
            sideMenu.style.display = 'none';
        })
    }
}
window.addEventListener('resize', checkWindowSize);

// Call checkWindowSize when the page first loads
checkWindowSize();



themeToggler.addEventListener('click', () => {
    document.body.classList.toggle('dark-theme-variables');
    themeToggler.querySelector('span:nth-child(1)').classList.toggle('active');
    themeToggler.querySelector('span:nth-child(2)').classList.toggle('active');
})
