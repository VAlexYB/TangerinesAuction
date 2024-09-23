document.getElementById('login-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('error-message');

    // Очистка ошибки
    errorMessage.textContent = '';

    try {
        const response = await fetch('http://localhost:5085/api/Auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify({ email, password })
        });

        if (!response.ok) {
            const errorData = await response.json();
            errorMessage.textContent = errorData.message || 'Ошибка при входе';
            return;
        }

        // Перенаправление на главную страницу
        window.location.href = '/dashboard/dashboard.html';
    } catch (error) {
        errorMessage.textContent = 'Ошибка подключения к серверу';
        console.log(error);
    }
});
