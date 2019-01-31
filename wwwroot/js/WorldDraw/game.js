//User controls section
document.addEventListener("keydown", (e) => {
    console.log(e);
    //admin turning 2D coords rendering on and off when G(lower or upper case) is pressed
    if (e.code == "KeyG") {
        if (showingCoords) { showingCoords = false; }
        else { showingCoords = true; }
    }
});

function movePlayerInDb(originX, originY, rangeX, rangeY) {
    let xhr = new XMLHttpRequest();
    let url = "/RenderMap/" + originX + "/" + originY + "/" + rangeX + "/" + rangeY;
    xhr.onload = () => {
        let world = JSON.parse(xhr.response);
        console.log(world);

    }

    xhr.open("GET", url);
    xhr.send(null);
}

function movePlayer(direction) {
    switch (direction) {
        case "north":
            currentY--;

            break;
        case "northeast":
            currentY--;

            break;
        case "northwest":
            currentY--;

            break;
        case "south":
            currentY--;

            break;
        case "southwest":
            currentY--;

            break;
        case "southeast":
            currentY--;
            break;
        default:
            break;
    }
    movePlayerInDb(currentX, currentY, xRenderRange, yRenderRange);
}


// CANVAS STUFF STARTS HERE
//All pretty standard stuff, 2D canvas the size of the window, pre-animation frame. NOTE!!! Never not be box-sizing them border-boxes. Otherwise the canvas won't fit.
let canvas = document.querySelector("canvas")
let c = canvas.getContext("2d");
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

canvas.addEventListener("click", (e) => {
    console.log("x: " + e.x + ", y: " + e.y);
});

let showingCoords = true;
let size = 50;

let currentX = 0;
let currentY = 0;
let xRenderRange = 4;
let yRenderRange = 4;
let currentOffsetX = 0;
let currentOffsetY = 0;
let direction = "none";
let shifting = false;

console.log(myMap);

function animate() {
    //basic animation setup
    requestAnimationFrame(animate);

    var sizeW = window.innerWidth;
    var sizeH = window.innerHeight;
    canvas.style.width = sizeW + "px";
    canvas.style.height = sizeH + "px";
    var scale = window.devicePixelRatio; // this is to avoid rendering at low res and scaling up.
    canvas.width = sizeW * scale;
    canvas.height = sizeH * scale;

    c.clearRect(0, 0, innerWidth, innerHeight);
    //actual animation stuff
    if (canvas.width > canvas.height) {
        size = canvas.width / 13;
    } else {
        size = canvas.height / 13;
    }

    if (shifting) {

    }

    myMap.drawGrid(c, currentX, currentY, xRenderRange, yRenderRange, currentOffsetX, currentOffsetY);
    if (showingCoords) {
        c.rect(canvas.width / 2, canvas.height / 2, 3, 3);
        c.strokeStyle = "red";
        c.stroke();
    }

}
animate();
