document.addEventListener("DOMContentLoaded", (e)=>{
    
    let xhr = new XMLHttpRequest();
    xhr.onload = () => {
        // console.log(xhr.response);
        let canvas=document.querySelector("canvas");
        let c = canvas.getContext("2d");
        console.log(canvas.parentNode);
        
        canvas.width=canvas.parentNode.clientWidth;
        canvas.height=canvas.parentNode.clientHeight;
        console.log(canvas.width+" "+canvas.height);
        

        console.log(canvas);
        console.log(c);
        
        
        
        let worldMap = JSON.parse(xhr.response);
        console.log(worldMap);

        let height = canvas.height/worldMap.length;
        let width = canvas.width/worldMap[1].length;

        for (let row=0;row< worldMap.length;row++){
            for (let cell=0;cell<worldMap[0].length;cell++){
                let xOrig=cell*width;
                let yOrig=row*height;
                c.rect(xOrig,yOrig,xOrig+width,yOrig+height);
                c.strokeStyle = "white";
                c.stroke();
                c.font = "20px Arial";
                c.fillStyle = "white";
                c.fillText(cell + "," + row, 
                xOrig, 
                yOrig+height);
                xOrig+=xOrig;
                yOrig+=yOrig;
            }
        }
    }
    xhr.open("GET","/wholemap");
    xhr.send(null);
});