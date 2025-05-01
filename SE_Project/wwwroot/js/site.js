// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// wwwroot/js/player.js
const playButtons = document.querySelectorAll('.play-button');
const pauseButton = document.querySelector('#pause-button');
const nextButton = document.querySelector('#next-button');
const prevButton = document.querySelector('#prev-button');

// Attach event listeners to play buttons
playButtons.forEach(button => {
    button.addEventListener('click', async () => {
        const trackUri = button.getAttribute('data-uri');
        try {
            await fetch('/Spotify/Play', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ uri: trackUri })
            });
        } catch (error) {
            console.error('Error playing track:', error);
        }
    });
});

// Pause button functionality
if (pauseButton) {
    pauseButton.addEventListener('click', async () => {
        try {
            await fetch('/Spotify/Pause', {
                method: 'POST'
            });
        } catch (error) {
            console.error('Error pausing playback:', error);
        }
    });
}

// Update UI based on current playback state
async function updatePlaybackState() {
    try {
        const response = await fetch('/Spotify/PlaybackState');
        const data = await response.json();

        if (data.IsPlaying) {
            // Update UI to show currently playing track
            document.querySelector('#current-track').textContent = data.Item.Name;
            document.querySelector('#current-artist').textContent = data.Item.Artists[0].Name;
            // Update progress bar if you have one
        }
    } catch (error) {
        console.error('Error getting playback state:', error);
    }
}

// Poll for playback state updates
setInterval(updatePlaybackState, 5000);
updatePlaybackState(); // Initial call