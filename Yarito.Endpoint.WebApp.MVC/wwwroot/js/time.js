const el = document.getElementById('liveClock');

function tick() {
    el .textContent = new Date().toLocaleTimeString('fa-IR');
}

tick();
setInterval(tick, 1000);
