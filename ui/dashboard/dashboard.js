document.addEventListener('DOMContentLoaded', function(){
    checkAuthentication();
    fetchTangerines();
    
});

function checkAuthentication() {
    const token = getCookie('app-special-key'); 
    const authActions = document.getElementById('auth-actions');
    const logoutBtn = document.getElementById('logout-btn');

    if (token) {
        authActions.innerHTML = `<p>Welcome back!</p>`;
        logoutBtn.style.display = 'inline';
        logoutBtn.addEventListener('click', logout);
    } else {
        document.getElementById('login-btn').addEventListener('click', () => {
            window.location.href = '/auth/login.html'; 
        });

        document.getElementById('register-btn').addEventListener('click', () => {
            window.location.href = '/auth/register.html'; 
        });
    }
}

function fetchTangerines(){
    fetch('http://localhost:5085/api/Tangerines')
        .then(response => response.json())
        .then(tangerines => renderTangerines(tangerines))
        .catch(error => console.log(error));
}

function renderTangerines(tangerines) {
    const tangerineList = document.getElementById('tangerine-list');
    tangerineList.innerHTML = '';

    let index = 0;
    tangerines.forEach(tangerine => {
        const card = document.createElement('div');
        card.classList.add('tangerine-card');
        index++;
        const imageUrl = `http://localhost:5085/api/Image/getImage/${tangerine.imageUrl}`;
        card.innerHTML = `
            <img src="${imageUrl}" alt="Tangerine">
            <h2>Лот ${index}</h2>
            <p>Цена: ${tangerine.currentPrice}</p>
            <p>Аукцион заканчивается: ${new Date(tangerine.expiryDate).toLocaleString()}</p>
            <div class="actions">
                ${canPlaceBid() ? `<button onclick="placeBid('${tangerine.id}')">Place Bid</button>` : `<p>You must be logged in to place a bid.</p>`}
            </div>
        `;
        tangerineList.appendChild(card);
    });
}

function canPlaceBid() {
    const token = getCookie("app-special-key");
    return !!token;
}

function placeBid(tangerineId) {
    const token = getCookie('app-special-key');
    const amount = prompt("Enter your bid amount:");

    if (amount) {
        fetch('http://localhost:5085/api/Bids', {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                TangerineId: tangerineId,
                Amount: amount
            })
        })
        .then(response => {
            if (response.ok) {
                alert('Bid placed successfully!');
            } 
            else if (response.status === 400) {
                response.text().then(message => {
                    alert(message);
                });
            }
            else {
                alert('Failed to place bid.');
            }
        })
        .catch(error => console.error('Error:', error));
    }
}

function getCookie(name) {
    let cookieValue = null;
    if (document.cookie && document.cookie !== '') {
      const cookies = document.cookie.split(';');
      for (let i = 0; i < cookies.length; i++) {
        const cookie = cookies[i].trim();
        if (cookie.substring(0, name.length + 1) === (name + '=')) {
          cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
          break;
        }
      }
    }
    return cookieValue;
}

function logout() {
    fetch('http://localhost:5085/api/Auth/logout', {
        method: 'GET',
        credentials: 'include'
    })
    .then(response => {
        if (response.ok) {
            alert('You have been logged out.');
            window.location.reload(); // Обновляем страницу
        } else {
            alert('Logout failed.');
        }
    })
    .catch(error => console.error('Error during logout:', error));
}