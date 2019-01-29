document.addEventListener("DOMContentLoaded", (e)=>{
    let canvas=document.querySelector("canvas");
    let c = canvas.getContext("2D");

    let xhr = new XMLHttpRequest();
    xhr.onload = () => {
        let worldMap = JSON.parse(xhr.response);
        console.log(worldMap);
    }
    xhr.open("GET","/wholemap");
    xhr.send(null);
});