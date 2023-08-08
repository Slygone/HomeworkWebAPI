var getAllUsers = document.getElementById('getAllUsers')
var getUserById = document.getElementById('getUserById')

getAllUsers.addEventListener('click', async () => {
    const response = await fetch('http://localhost:5034/api/users')
    const data = await response.json();
    displayData(data);
});

getUserById.addEventListener('click', async () => {
    const index = parseInt(prompt('Enter user index:'));
    if (!isNaN(index)) {
        const response = await fetch(`http://localhost:5034/api/users/${index}`);
        const data = await response.text();
        displayData(data);
    }
});

function displayData(data) {
    const output = document.getElementById('output');
    output.innerHTML = `<p>${data}</p>`;
}