let menuOpen = false;
function openGameMenu(){
    let menu = document.getElementById("gameMenu");
    if (menuOpen){
        menu.style.width="0";
        menu.style.paddingLeft="0";
        menu.style.paddingRight="0";
        menuOpen=false;
    }
    else{
        menu.style.width="35%";
        menu.style.paddingLeft="10px";
        menu.style.paddingRight="10px";
        menuOpen=true;
    }
}