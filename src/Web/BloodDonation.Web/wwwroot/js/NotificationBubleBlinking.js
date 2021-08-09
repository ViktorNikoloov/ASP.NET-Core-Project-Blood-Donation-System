let bigBubble = document.getElementById('bigBubble');
let smallBubble = document.getElementById('smallBubble');


setTimeout(function () {
    fadeOut(bigBubble);
    fadeIn(smallBubble);
}, 4000)

function fadeOut(bubble) {
    const currOpacity = [0.7, 0.6, 0.5, 0.4, 0.3, 0.2, 0.1, 0];
    let x = 0;
    (function next() {
        bubble.style.opacity = currOpacity[x];
        if (++x < currOpacity.length) {
            setTimeout(next, 50);
        }
    })();
}
function fadeIn(bubble) {
    const currOpacity = [0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8];
    let x = 0;
    (function next() {
        bubble.style.opacity = currOpacity[x];
        if (++x < currOpacity.length) {
            setTimeout(next, 50);
        }
    })();
}