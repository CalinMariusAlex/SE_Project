let playlist = [];
let currentIndex = -1;
let playCountTimeout;

const audio = new Audio();

const titleEl = document.getElementById("mini-title");
const artistEl = document.getElementById("mini-artist");
const thumbEl = document.getElementById("mini-thumbnail");
const playBtn = document.getElementById("play-button");
const prevBtn = document.getElementById("prev-button");
const nextBtn = document.getElementById("next-button");

const progressBar = document.getElementById("progress-bar");
const currentTimeEl = document.getElementById("current-time");
const totalTimeEl = document.getElementById("total-time");




function playSong(id, url, title, artist, thumbnail) {

    const existingIndex = playlist.findIndex(s => s.id === id);

    if (existingIndex !== -1) {
        currentIndex = existingIndex;
    } else {
        playlist.push({ id, url, title, artist, thumbnail });

        currentIndex = playlist.length - 1;
    }

    loadAndPlay(playlist[currentIndex]);
}

function togglePlayPause() {
    if (audio.paused) {
        audio.play();
    } else {
        audio.pause();
    }
    updatePlayButton();
}

function updatePlayButton() {
    if (audio.paused) {
        playBtn.innerHTML = '<i class="fa fa-play"></i>';
    } else {
        playBtn.innerHTML = '<i class="fa fa-pause"></i>';
    }
}

function nextSong() {
    if (currentIndex < playlist.length - 1) {
        currentIndex++;
        loadAndPlay(playlist[currentIndex]);
    }
}

function prevSong() {
    if (currentIndex > 0) {
        currentIndex--;
        loadAndPlay(playlist[currentIndex]);
    }
}

// ⏱️ Afișare durată actuală și totală
audio.addEventListener("loadedmetadata", () => {
    progressBar.max = audio.duration;
    totalTimeEl.textContent = formatTime(audio.duration);
});

audio.addEventListener("timeupdate", () => {
    progressBar.value = audio.currentTime;
    currentTimeEl.textContent = formatTime(audio.currentTime);
});

// 🖱️ Când userul modifică sliderul
progressBar.addEventListener("input", () => {
    audio.currentTime = progressBar.value;
});

// ⌛ Format timp MM:SS
function formatTime(seconds) {
    const mins = Math.floor(seconds / 60);
    const secs = Math.floor(seconds % 60);
    return `${mins}:${secs < 10 ? '0' : ''}${secs}`;
}

// 🎧 Butoane
playBtn.addEventListener("click", togglePlayPause);
nextBtn.addEventListener("click", nextSong);
prevBtn.addEventListener("click", prevSong);

const volumeSlider = document.getElementById("volume-slider");
volumeSlider.addEventListener("input", () => {
    audio.volume = volumeSlider.value;
});


function loadAndPlay(song) {
    audio.src = song.url;
    titleEl.textContent = song.title;
    artistEl.textContent = song.artist;
    thumbEl.src = song.thumbnail;
    audio.play();
    updatePlayButton();

    // ❤️ Actualizează butonul de favorite din footer
    const footerFavBtn = document.getElementById("footer-fav-btn");
    const icon = footerFavBtn.querySelector("i");

    footerFavBtn.setAttribute("data-song-id", song.id);

    // Setează provizoriu inimă goală
    icon.className = "fa-regular fa-heart text-white";

    // 🔄 Verifică dacă este deja la favorite
    fetch("/Favorite/IsFavorite", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(song.id)
    })
        .then(response => response.json())
        .then(isFavorite => {
            if (isFavorite) {
                icon.className = "fa-solid fa-heart text-danger";
            } else {
                icon.className = "fa-regular fa-heart text-white";
            }
        });

    clearTimeout(playCountTimeout);
    playCountTimeout = setTimeout(() => {
        sendPlayCount(song.id);
    }, 5000);
}



function sendPlayCount(songId) {
    fetch('/Song/IncrementPlayCount', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ songId: songId })
    });
}


function toggleFavorite(songId, event, isInMyFavorites = false) {
    event.stopPropagation();

    // 🔐 Verifică dacă butonul este în interiorul unui song-card
    const originButton = event.currentTarget;


    
    console.log("CLICK FAVORITE:", songId); // 💥 DEBUG
    // ✅ Dacă songId invalid, ignorăm
    if (!songId || isNaN(songId)) {
        console.warn("ID invalid la toggleFavorite");
        return;
    }

    fetch("/Favorite/Toggle", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(songId)
    }).then(() => {
        const buttons = document.querySelectorAll(`.only-song-favorite[data-song-id="${songId}"] i`);

        buttons.forEach(button => {
            if (button.classList.contains("fa-heart") && button.classList.contains("text-danger")) {
                button.classList.remove("fa-heart", "text-danger");
                button.classList.add("fa-regular", "fa-heart", "text-white");
            } else {
                button.classList.remove("fa-regular", "text-white");
                button.classList.add("fa-solid", "fa-heart", "text-danger");
            }
        });

        if (isInMyFavorites) {
            const card = document.getElementById(`fav-song-${songId}`);
            if (card) {
                card.remove();
                const remaining = document.querySelectorAll(".favorite-card").length;
                if (remaining === 0) {
                    document.getElementById("no-favorites-message").style.display = "block";
                }
            }
        }
    });
}

// 🔁 Activează click pe inima din playerul de jos
document.addEventListener("DOMContentLoaded", () => {
    const footerFavBtn = document.getElementById("footer-fav-btn");

    if (footerFavBtn) {
        footerFavBtn.addEventListener("click", function (event) {
            const songId = parseInt(this.getAttribute("data-song-id"));
            toggleFavorite(songId, event);
        });
    }
});

function scrollTrendingRight() {
    const container = document.getElementById("trending-container");
    container.scrollBy({ left: 300, behavior: 'smooth' });
}
