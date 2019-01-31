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
                    url: "sea-hex.png"
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

    drawGrid(ctx, xPos, yPos, horRange, vertRange, xOffset = 0, yOffset = 0) {
        //Odd-Q!!! Very important.“odd-q” vertical layout shoves odd columns down
        xOffset = xOffset * ((3 / 2) * size);
        if (xPos % 2 != 0) {
            yOffset = yOffset * (Math.sqrt(3) * size)*(-1+0.5);
        }
        else {
            yOffset = yOffset * (Math.sqrt(3) * size)*(-1-0.5);
        }
        let x = (canvas.width / 2) - xOffset;
        let y = (canvas.height / 2) + yOffset;
        for (let ver = 0 - vertRange; ver <= vertRange; ver++) {
            for (let hor = 0 - horRange; hor <= horRange; hor++) {
                let type = "sea";
                let url = "sea-hex.png";
                if (this.layout[yPos + ver]) {
                    if (this.layout[yPos + ver][xPos + hor]) {
                        type = this.layout[yPos + ver][xPos + hor].type;
                        url = this.layout[yPos + ver][xPos + hor].url;
                    }
                }
                let flip1 = (ver + (0.5 * (Math.abs(hor) % 2)));
                let flip2 = (ver - (0.5 * (Math.abs(hor) % 2)));
                if (xPos % 2 != 0) {
                    flip1 = flip2;
                }
                drawHex(
                    ctx,
                    x + hor * (3 / 2) * size,
                    y + (Math.sqrt(3) * size) * flip1,
                    size,
                    type,
                    url,
                    xPos + hor,
                    yPos + ver);
            }
        }
    }
}
//there will have to be a better way to make these, but this is the meat - actual locations in the sea
let newLoc = [
    new mapPoint(1, 1, "land", "land-hex.png"),
    new mapPoint(3, 1, "land", "land-hex.png"),
    new mapPoint(4, 1, "land", "land-hex.png"),
    new mapPoint(5, 1, "land", "land-hex.png"),
];

let myMap = new gameMap(9, 12, newLoc);

//this is just a helper. once the math for checking inside a hex is done, will replace this. Food for thught: maybe check the clicks for being inside of a circle, rather than a hex? Math is simple

function drawHex(ctx, xCoord, yCoord, sideSize, type, url = "sea-hex.png", arrX = "", arrY = "") {
    console.log(ctx);
    
    let img = new Image();
    img.src = "/img/" + url;

    img.onload = () => {
    }
    ctx.drawImage(
        img,
        xCoord - sideSize,
        yCoord - (Math.sqrt(3) * sideSize) / 2,
        2 * sideSize,
        Math.sqrt(3) * sideSize
        );


        ctx.beginPath();
        ctx.moveTo(xCoord + sideSize * Math.cos(0), yCoord + sideSize * Math.sin(0));

    for (let side = 0; side < 7; side++) {
        ctx.lineTo(xCoord + sideSize * Math.cos(side * 2 * Math.PI / 6), yCoord + sideSize * Math.sin(side * 2 * Math.PI / 6));
    }

    // if (type == "port") {
    //     c.fillStyle = "rgba(49, 88, 88, 0.404)";
    //     c.fill();
    // }
    // else {
    //     c.strokeStyle = "#fa34a3";
    //     c.stroke();

    // }
    if (showingCoords) {
        ctx.font = "30px Arial";
        ctx.fillStyle = "black";
        ctx.fillText(arrX + "," + arrY, xCoord, yCoord);
    }

}

