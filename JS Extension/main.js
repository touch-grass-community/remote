document.addEventListener('keydown', function (event) {
    const video = document.querySelector('video.jw-video');

    if (!video) return; // Esci se il video non è trovato

    switch (event.key.toLowerCase()) {
        case '+': // Avanza di 10 secondi
            video.currentTime = Math.min(video.currentTime + 10, video.duration);
            break;

        case '-': // Torna indietro di 10 secondi
            video.currentTime = Math.max(video.currentTime - 10, 0);
            break;

        case 'q': // Avanza di 88 secondi
            video.currentTime = Math.min(video.currentTime + 88, video.duration);
            break;

        case 'w': // Passa all'episodio successivo
            const nextEpisodeButtons = document.querySelectorAll('div.btn.btn-light.mb-1.mt-1');
            if (nextEpisodeButtons[1]) nextEpisodeButtons[1].click();
            break;

        case 'e': // Riproduci/Metti in pausa e vai in fullscreen
            if (video.paused) {
                video.play();
                if (video.requestFullscreen) video.requestFullscreen();
                else if (video.webkitRequestFullscreen) video.webkitRequestFullscreen();
                else if (video.mozRequestFullScreen) video.mozRequestFullScreen();
                else if (video.msRequestFullscreen) video.msRequestFullscreen();
            } else {
                video.pause();
            }
            break;

        case 'a': // Abbassa il volume
            video.volume = Math.max(video.volume - 0.1, 0);
            break;

        case 's': // Alza il volume
            video.volume = Math.min(video.volume + 0.1, 1);
            break;
    }
});
