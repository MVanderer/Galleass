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
        menu.style.width="20%";
        menu.style.paddingLeft="10px";
        menu.style.paddingRight="10px";
        menuOpen=true;
    }
}

function openModal(modalName){
    console.log(window.location.pathname);
    let modalToOpen;
    switch (modalName) {
        case "wharf":
            modalToOpen=document.getElementById("wharfModal");
            break;
        case "tavern":
            modalToOpen=document.getElementById("tavernModal");
            break;
        case "market":
            modalToOpen=document.getElementById("marketModal");
            break;
        default:
            break;
        }
    modalToOpen.style.display="block";

}
function closeAllModals(){
    let modals = document.getElementsByClassName("modal");
    for (let modal of modals){
        console.log(modal);
        
        modal.style.display="none";
    }
}