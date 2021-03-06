class mapPoint {
    //subject to massive change, this is a grid cell
    constructor(x, y, type, url = "", portId = null) {
        this.x = x;
        this.y = y;
        this.type = type;
        this.url = url;
        if (portId) { this.portId = portId; }
    }
}

class gameMap {
    constructor(w, h, locations) {
        this.layout = [];
        //layout is a standard 2x2 matrix, it's the rendering and the logic that turn it into a hex grid. See https://www.redblobgames.com/grids/hexagons/
        for (let rowId = 0; rowId < h; rowId++) {
            let row = []
            for (let cellId = 0; cellId < w; cellId++) {
                let m = {
                    type: "sea",
                    id: cellId + rowId,
                    ulr: ""
                };
                row.push(m);
            }
            this.layout.push(row);
        }
        //helper fields. don't do anything and may be redundant
        this.width = this.layout[0].length;
        this.height = this.layout.length;
        //this is the actual map - the special locations, the land, the ports
        for (let locale of locations) {
            if ((locale.x < this.width) && (locale.y < this.height)) {
                this.layout[locale.y][locale.x].type = locale.type;
                this.layout[locale.y][locale.x].url = locale.url;
            }
        }
    }

    drawGrid() {
        //size is for rendering purposes only, and should probably be a global variable eventually
        for (let row = 0; row < this.layout.length; row++) {
            for (let cell = 0; cell < this.layout[row].length; cell++) {
                this.drawCell(cell, row);
            }
        }
    }

    drawCell(xPos, yPos) {
        //again size may have to go away and become a global variable. In fact scratch the "may" part. DEFINITELY a global variable, probably affected by the window size

        //x and y are offsets from the corner of a square grid to the center of the hex cell, xPos and yPos are coordinates in the 2x2 matrix that records the map
        //Using ODD-Q layout here
        let x, y;
        x = size + xPos * (3 / 2) * size;
        y = (Math.sqrt(3) * size) * (yPos + 0.5 - (0.5 * (xPos % 2)));
        //trigonometry for the win, calculate locations of equal length lines at 120 degree angles to one another and move canvas pen to them one by one
        drawHex(x,y,size,this.layout[yPos][xPos].type,this.layout[yPos][xPos].type,xPos,yPos);
    }

    drawGridAtCenter(xPos,yPos,horRange,vertRange){
        let x = canvas.width/2;
        let y = canvas.height/2;
        for (let ver = 0-vertRange; ver<=vertRange;ver++){
            for (let hor = 0-horRange; hor<=horRange;hor++){
                let type="sea";
                let url="";
                if (this.layout[yPos+ver]){
                    if(this.layout[yPos+ver][xPos+hor]){
                        type = this.layout[yPos+ver][xPos+hor].type;
                        url = this.layout[yPos+ver][xPos+hor].url;
                    }
                }
                drawHex(
                    x+hor*(3/2)*size,
                    y+(Math.sqrt(3) * size) * (ver - (0.5 * (hor % 2))),
                    size,
                    type,
                    url,
                    xPos+hor,
                    yPos+ver);
            }
        }
    }
}
//there will have to be a better way to make these, but this is the meat - actual locations in the sea
let newLoc = [
    new mapPoint(1, 1, "land"),
    new mapPoint(3, 1, "land"),
    new mapPoint(4, 1, "land"),
    new mapPoint(5, 1, "land"),
];

let myMap = new gameMap(9, 12, newLoc);


// CANVAS STUFF STARTS HERE
//All pretty standard stuff, 2D canvas the size of the window, pre-animation frame. NOTE!!! Never not be box-sizing them border-boxes. Otherwise the canvas won't fit.
let canvas = document.querySelector("canvas")
let c = canvas.getContext("2d");
canvas.width = window.innerWidth;
canvas.height = window.innerHeight;

//this is just a helper. once the math for checking inside a hex is done, will replace this. Food for thught: maybe check the clicks for being inside of a circle, rather than a hex? Math is simple
canvas.addEventListener("click", (e) => {
    console.log("x: " + e.x + ", y: " + e.y);
});

let showingCoords = false;

let currentX=0;
let currentY=0;
document.addEventListener("keydown",(e)=>{
    console.log(e);
    //admin turning 2D coords rendering on and off when G(lower or upper case) is pressed
    if (e.code == "KeyG") {
        if (showingCoords) { showingCoords = false; }
        else { showingCoords = true; }
    }
    else if (e.code=="ArrowRight"){
        currentX++;
    }
    else if (e.code=="ArrowLeft"){
        currentX--;
    }
    else if (e.code=="ArrowUp"){
        currentY--;
    }
    else if (e.code=="ArrowDown"){
        currentY++;
    }
});

function drawHex(xCoord, yCoord, sideSize, type, url,arrX="",arrY="") {
    c.beginPath();
    c.moveTo(xCoord + sideSize * Math.cos(0), yCoord + sideSize * Math.sin(0));

    for (let side = 0; side < 7; side++) {
        c.lineTo(xCoord + sideSize * Math.cos(side * 2 * Math.PI / 6), yCoord + sideSize * Math.sin(side * 2 * Math.PI / 6));
    }

    if (type == "port") {
        c.fillStyle = "rgba(49, 88, 88, 0.404)";
        c.fill();
    }
    else if (type == "land") {
        c.fillStyle = "rgb(21, 112, 41)";
        c.fill();

    }
    else {
        c.strokeStyle = "#fa34a3";
        c.stroke();
    }
    if (showingCoords){
        c.font = "10px Arial";
        c.fillStyle = "black";
        c.fillText(arrX + "," + arrY, xCoord, yCoord);
    }

}

let size = canvas.height / myMap.height;


function animate() {
    //basic animation setup
    requestAnimationFrame(animate);
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    c.clearRect(0, 0, innerWidth, innerHeight);
    //actual animation stuff
    c.rect(canvas.width/2,canvas.height/2,1,1);
    c.stroke();

    // myMap.drawGrid();
    myMap.drawGridAtCenter(currentX,currentY,7,6);

}
animate();
// drawHex(canvas.width / 2, canvas.height / 2, size, "land", "");