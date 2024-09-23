document.getElementById('register-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const confirmPassword = document.getElementById('confirm-password').value;
    const errorMessage = document.getElementById('error-message');

    // Очистка ошибки
    errorMessage.textContent = '';

    if (password !== confirmPassword) {
        errorMessage.textContent = 'Пароли не совпадают!';
        return;
    }

    // Отправка данных на сервер
    try {
        const response = await fetch('http://localhost:5085/api/Auth/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
        });

        if (!response.ok) {
            const errorData = await response.json();
            errorMessage.textContent = errorData.message || 'Ошибка при регистрации';
            return;
        }

        // Перенаправление на страницу входа после успешной регистрации
        window.location.href = './login.html';
    } catch (error) {
        errorMessage.textContent = 'Ошибка подключения к серверу';
        console.log(error);
    }
});
