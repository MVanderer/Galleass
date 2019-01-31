// CANVAS Initialization
// context to be passed into functions
let canvas = document.querySelector("canvas")
let c = canvas.getContext("2d");
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

// canvas.addEventListener("click", (e) => {
//     console.log("x: " + e.x + ", y: " + e.y);
// });

let showingCoords = false;

let size = 50;//will need to calculate based on size and range

let currentX = 0;// will change, 
let currentY = 0;// dependent on the player

let xRenderRange = 4;//same as size
let yRenderRange = 4;//change later

let currentOffsetX = 0;//this is for animating
let currentOffsetY = 0;//movement.
let directionImg = "southwest.png";
let shifting = false;

//this is the actual meat of the rendering pipeline
let renderPlan = new renderView(currentX, currentY, xRenderRange, yRenderRange);
console.log(renderPlan);

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
        return world;

    }

    xhr.open("GET", url);
    xhr.send(null);
}

function outOfBounds(x, y) {
    console.log(x+" "+y);
    console.log(renderPlan.playerX);
    console.log(renderPlan.playerY);
    
    console.log(renderPlan.layout[renderPlan.playerY][renderPlan.playerX]);
    console.log(renderPlan.layout[renderPlan.playerY][renderPlan.playerX]);
    
    
    if (renderPlan.layout[renderPlan.playerY-1 + y]) {

        if (renderPlan.layout[renderPlan.playerY-1 + y][renderPlan.playerX-1 + x]) {
            return false;
        }
    }
    else {
        return true;
    }
}

function movePlayer(direction) {
    directionImg = direction + ".png";
    console.log(renderPlan);
    
    switch (direction) {
        case "north":
            if (!outOfBounds(0, - 1)) {
                currentY--;
            }
            break;
        case "northeast":
            if (currentX % 2 == 0) {
                if (!outOfBounds(1, - 1)) {
                    currentX++;
                    currentY--;
                }
            }
            else {
                if (!outOfBounds(1, 0)) {
                    currentX++;
                }
            }
            break;
        case "northwest":
            if (currentX % 2 == 0) {
                if (!outOfBounds(- 1, - 1)) {
                    currentX--;
                    currentY--;
                }
            }
            else {
                if (!outOfBounds(- 1, 0)) {
                    currentX--;
                }
            }
            break;
        case "south":
            if (!outOfBounds(0, 1)) {
                currentY++;
            }
            break;
        case "southwest":
            if (currentX % 2 == 0) {
                if (!outOfBounds(- 1, - 1)) {
                    currentX--;
                }
            }
            else {
                if (!outOfBounds(- 1, 1)) {
                    currentY++;
                    currentX--;
                }
            }
            break;
        case "southeast":
            if (currentX % 2 == 0) {
                if (!outOfBounds(1, 0)) {
                    currentX++;
                }
            }
            else {
                if (!outOfBounds(1, 1)) {
                    currentX++;
                    currentY++;
                }
            } break;
        default:
            break;
    }
    renderPlan.getGrid(currentX, currentY, xRenderRange, yRenderRange);
}

// console.log(myMap);

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
    if (renderPlan.layout) {
        renderPlan.render(
            c,
            canvas.width,
            canvas.height,
            size,
            currentOffsetX,
            currentOffsetY);

    }
    let shipAvatar = new Image();
    shipAvatar.src = "/img/gal-avatar/" + directionImg;
    c.drawImage(
        shipAvatar,
        canvas.width / 2 - size,
        canvas.height / 2 - (Math.sqrt(3) * size) / 2,
        2 * size,
        Math.sqrt(3) * size
    )

    // myMap.drawGrid(c, currentX, currentY, xRenderRange, yRenderRange, currentOffsetX, currentOffsetY);

    if (showingCoords) {
        c.rect(canvas.width / 2, canvas.height / 2, 3, 3);
        c.strokeStyle = "red";
        c.stroke();
    }

}
animate();
